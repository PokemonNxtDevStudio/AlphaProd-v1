/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Steering.Components
{
    using System;
    using Apex.DataStructures;
    using Apex.Messages;
    using Apex.PathFinding;
    using Apex.Services;
    using Apex.Units;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// A steering component that enables the unit to issue <see cref="IPathRequest"/>s and move along the resulting path.
    /// </summary>
    [AddComponentMenu("Apex/Navigation/Steering/Steer for Path")]
    [ApexComponent("Steering")]
    public class SteerForPathComponent : ArrivalBase, IMovable, INeedPath
    {
        /// <summary>
        /// The priority with which this unit's path requests should be processed.
        /// </summary>
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public int pathingPriority = 0;

        /// <summary>
        /// Whether to use path smoothing.
        /// Path smoothing creates more natural routes at a small cost to performance.
        /// </summary>
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public bool usePathSmoothing = true;

        /// <summary>
        /// Controls whether to allow the path to cut corners. Corner cutting has slightly better performance, but produces less natural routes.
        /// </summary>
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public bool allowCornerCutting = false;

        /// <summary>
        /// Controls whether navigation off-grid is prohibited.
        /// </summary>
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public bool preventOffGridNavigation = false;

        /// <summary>
        /// Controls whether the unit is allowed to move to diagonal neighbours.
        /// </summary>
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public bool preventDiagonalMoves = false;

        /// <summary>
        /// Controls whether the unit will navigate to the nearest possible position if the actual destination is blocked or otherwise inaccessible.
        /// </summary>
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public bool navigateToNearestIfBlocked = false;

        /// <summary>
        /// Gets the maximum escape cell distance if origin blocked.
        /// This means that when starting a path and the origin (from position) is blocked, this determines how far away the pather will look for a free cell to escape to, before resuming the planned path.
        /// </summary>
        [MinCheck(0)]
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public int maxEscapeCellDistanceIfOriginBlocked = 3;

        /// <summary>
        /// The distance from the current destination node on the path at which the unit will switch to the next node.
        /// </summary>
        [MinCheck(0.1f)]
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public float nextNodeDistance = 1f;

        /// <summary>
        /// The distance from the current way point at which the next way point will be requested
        /// </summary>
        [MinCheck(0f)]
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public float requestNextWaypointDistance = 2.0f;

        /// <summary>
        /// Controls whether a <see cref="Apex.Messages.UnitNavigationEventMessage"/> is raised each time a node is reached.
        /// </summary>
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public bool announceAllNodes = false;

        /// <summary>
        /// The replan mode
        /// </summary>
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public ReplanMode replanMode = ReplanMode.Dynamic;

        /// <summary>
        /// The replan interval
        /// When <see cref="replanMode"/> is <see cref="ReplanMode.AtInterval"/> the replan interval is the fixed interval in seconds between replanning.
        /// When <see cref="replanMode"/> is <see cref="ReplanMode.Dynamic"/> the replan interval is the minimum required time between each replan.
        /// </summary>
        [MinCheck(0.1f)]
        [Obsolete("Set on the PathFinderOptions component instead.")]
        public float replanInterval = 0.5f;

        private readonly object _syncLock = new object();
        private IUnitFacade _unit;
        private IPathNavigationOptions _pathSettings;
        private IPathRequest _pendingPathRequest;
        private PathResult _pendingResult;
        private Path _currentPath;
        private ReplanCallback _manualReplan;
        private Vector3 _previousDestination;
        private IPositioned _currentDestination;
        private float _currentDestinationDistanceSquared;
        private float _remainingSquaredDistance;
        private IGrid _currentGrid;
        private Transform _transform;
        private Vector3 _endOfResolvedPath;
        private Vector3 _endOfPath;

        private bool _stop;
        private bool _blockMoveOrders;
        private bool _stopped;
        private bool _onFinalApproach;
        private bool _isPortaling;
        private float _lastPathRequestTime;

        private UnitNavigationEventMessage _navMessage;
        private IProcessPathResults[] _resultProcessors;

        private SimpleQueue<Vector3> _wayPoints;
        private SimpleQueue<Vector3> _pathboundWayPoints;

        /// <summary>
        /// Gets the current path.
        /// </summary>
        /// <value>
        /// The current path.
        /// </value>
        public Path currentPath
        {
            get { return _currentPath; }
        }

        /// <summary>
        /// Gets the current way points.
        /// </summary>
        /// <value>
        /// The current way points.
        /// </value>
        public IIterable<Vector3> currentWaypoints
        {
            get { return _wayPoints; }
        }

        /// <summary>
        /// Gets the final destination, which is either the last point in the <see cref="currentPath" /> or the last of the <see cref="currentWaypoints" /> if there are any.
        /// </summary>
        /// <value>
        /// The final destination.
        /// </value>
        public Vector3? finalDestination
        {
            get
            {
                if (_wayPoints.count > 0)
                {
                    return _wayPoints.Last();
                }

                if (_pathboundWayPoints.count > 0)
                {
                    return _pathboundWayPoints.Last();
                }

                if (_currentPath != null && _currentPath.count > 0)
                {
                    return _currentPath.Last().position;
                }

                if (_currentDestination != null)
                {
                    return _currentDestination.position;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the position of the next node along the path currently being moved towards.
        /// </summary>
        /// <value>
        /// The next node position.
        /// </value>
        public Vector3? nextNodePosition
        {
            get
            {
                if (_currentDestination != null)
                {
                    return _currentDestination.position;
                }

                return null;
            }
        }

        /// <summary>
        /// Called on Awake
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            this.WarnIfMultipleInstances();

            _transform = this.transform;

            _wayPoints = new SimpleQueue<Vector3>();
            _pathboundWayPoints = new SimpleQueue<Vector3>();
            _navMessage = new UnitNavigationEventMessage(this.gameObject);

            _resultProcessors = this.GetComponents<SteerForPathResultProcessorComponent>();
            Array.Sort(_resultProcessors, (a, b) => a.processingOrder.CompareTo(b.processingOrder));

            _unit = this.GetUnitFacade();
            _pathSettings = _unit.pathNavigationOptions;

            if (this.arrivalDistance > _pathSettings.nextNodeDistance)
            {
                Debug.LogError("The Arrival Distance must be equal to or less that the Next Node Distance.");
            }

            _stopped = true;
            _unit.hasArrivedAtDestination = true;
        }

        /// <summary>
        /// Asks the object to move to the specified position
        /// </summary>
        /// <param name="position">The position to move to.</param>
        /// <param name="append">if set to <c>true</c> the destination is added as a way point.</param>
        public void MoveTo(Vector3 position, bool append)
        {
            if (_blockMoveOrders)
            {
                return;
            }

            _onFinalApproach = false;

            //If this is a way point and we are already moving, just queue it up
            if (append && !_stopped)
            {
                _wayPoints.Enqueue(position);
                return;
            }

            var from = _isPortaling ? _currentDestination.position : _transform.position;

            //Either we don't have a request or this is the first point in a way point route
            StopInternal();

            RequestPath(from, position, InternalPathRequest.Type.Normal);
        }

        /// <summary>
        /// Asks the object to move along the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void MoveAlong(Path path)
        {
            MoveAlong(path, null);
        }

        /// <summary>
        /// Asks the object to move along the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="onReplan">The callback to call when replanning is needed.</param>
        public void MoveAlong(Path path, ReplanCallback onReplan)
        {
            Ensure.ArgumentNotNull(path, "path");

            StopInternal();

            SetManualPath(path, onReplan);
        }

        /// <summary>
        /// Enables the movement orders following a call to <see cref="DisableMovementOrders" />.
        /// </summary>
        public void EnableMovementOrders()
        {
            _blockMoveOrders = false;
        }

        /// <summary>
        /// Disables movement orders, i.e. calls to <see cref="MoveTo" /> will be ignored until <see cref="EnableMovementOrders" /> is called.
        /// </summary>
        public void DisableMovementOrders()
        {
            _blockMoveOrders = true;
        }

        /// <summary>
        /// Stop following the path.
        /// </summary>
        public override void Stop()
        {
            _stop = true;
        }

        /// <summary>
        /// Replans the path.
        /// </summary>
        public void ReplanPath()
        {
            if (_stopped || _pendingPathRequest != null)
            {
                return;
            }

            var pathToReplan = _currentPath;
            _currentPath = null;
            _pathboundWayPoints.Clear();

            if (_manualReplan != null)
            {
                var updatedPath = _manualReplan(this.gameObject, _currentDestination, pathToReplan);
                SetManualPath(updatedPath, _manualReplan);
            }
            else
            {
                RequestPath(_transform.position, _endOfPath, InternalPathRequest.Type.Normal);
            }
        }

        //Note this may be called from another thread if the PathService is running asynchronously
        void INeedPath.ConsumePathResult(PathResult result)
        {
            lock (_syncLock)
            {
                //If we have stooped or get back the result of a request other than the one we currently expect, just toss it as it will be outdated.
                if (result.originalRequest != _pendingPathRequest)
                {
                    return;
                }

                _pendingResult = result;
                _pendingPathRequest = null;
            }
        }

        public override void GetDesiredSteering(SteeringInput input, SteeringOutput output)
        {
            if (_isPortaling)
            {
                output.pause = true;
                return;
            }

            if (_stopped)
            {
                return;
            }

            if (_stop)
            {
                StopInternal();
                return;
            }

            if (_currentPath == null && !ResolveNextPoint())
            {
                return;
            }

            //Get the remaining distance
            _currentDestinationDistanceSquared = _transform.position.DirToXZ(_currentDestination.position).sqrMagnitude;

            HandleWaypointsAndArrival();

            if (_currentDestinationDistanceSquared < _pathSettings.nextNodeDistance * _pathSettings.nextNodeDistance)
            {
                if (!ResolveNextPoint())
                {
                    return;
                }
            }
            else
            {
                HandlePathReplan();
            }

            if (_onFinalApproach)
            {
                output.desiredAcceleration = Arrive(_currentDestination.position, input);
                if (this.hasArrived)
                {
                    _unit.hasArrivedAtDestination = true;
                    AnnounceEvent(UnitNavigationEventMessage.Event.DestinationReached, _transform.position, null);
                    StopInternal();
                }
            }
            else
            {
                //Calculate slowing distance since we don't start arrival until this it known and within range.
                if (this.autoCalculateSlowingDistance)
                {
                    CalculateRequiredSlowingDistance(input);
                }

                output.desiredAcceleration = Seek(_currentDestination.position, input);
            }
        }

        /// <summary>
        /// Requests the path.
        /// </summary>
        /// <param name="request">The request.</param>
        public void RequestPath(IPathRequest request)
        {
            var unit = this.GetUnitFacade();

            request = InternalPathRequest.Internalize(request);

            lock (_syncLock)
            {
                request.requester = this;
                request.requesterProperties = unit;

                _pendingPathRequest = request;

                _stop = false;
                _stopped = false;
            }

            _lastPathRequestTime = Time.time;
            GameServices.pathService.QueueRequest(_pendingPathRequest, unit.pathFinderOptions.pathingPriority);
        }

        private void RequestPath(Vector3 from, Vector3 to, InternalPathRequest.Type type)
        {
            var unit = this.GetUnitFacade();

            lock (_syncLock)
            {
                _pendingPathRequest = new InternalPathRequest
                {
                    from = from,
                    to = to,
                    pathType = type,
                    requester = this,
                    requesterProperties = unit,
                    pathFinderOptions = unit.pathFinderOptions
                };

                _stop = false;
                _stopped = false;
            }

            _lastPathRequestTime = Time.time;
            GameServices.pathService.QueueRequest(_pendingPathRequest, unit.pathFinderOptions.pathingPriority);
        }

        private void SetManualPath(Path path, ReplanCallback onReplan)
        {
            if (path == null || path.count == 0)
            {
                StopInternal();
                return;
            }

            _stop = false;
            _stopped = false;
            _onFinalApproach = false;
            _manualReplan = onReplan;
            _currentPath = path;
            _endOfResolvedPath = _currentPath.Last().position;
            _currentDestination = path.Pop();
            _currentGrid = GridManager.instance.GetGrid(_currentDestination.position);

            _endOfPath = _endOfResolvedPath;
            _lastPathRequestTime = Time.time;
        }

        private void AnnounceEvent(UnitNavigationEventMessage.Event e, Vector3 destination, Vector3[] pendingWaypoints)
        {
            _navMessage.isHandled = false;
            _navMessage.eventCode = e;
            _navMessage.destination = destination;
            _navMessage.pendingWaypoints = pendingWaypoints ?? Consts.EmptyVectorArray;
            GameServices.messageBus.Post(_navMessage);
        }

        private bool ResolveNextPoint()
        {
            if (_currentPath == null || _currentPath.count == 0)
            {
                if (_pendingResult != null)
                {
                    //A pending route exists (i.e. next way point) so move on to that one right away
                    var req = _pendingResult.originalRequest as InternalPathRequest;
                    if (req.pathType != InternalPathRequest.Type.PathboundWaypoint && _currentDestination != null && _currentPath != null)
                    {
                        AnnounceEvent(UnitNavigationEventMessage.Event.WaypointReached, _currentDestination.position, null);
                    }

                    return ConsumeResult();
                }

                return (_currentDestination != null);
            }

            if (_currentDestination != null)
            {
                _remainingSquaredDistance -= (_previousDestination - _currentDestination.position).sqrMagnitude;
                _previousDestination = _currentDestination.position;
            }

            _currentDestination = _currentPath.Pop();

            var portal = _currentDestination as IPortalNode;
            if (portal != null)
            {
                _isPortaling = true;

                //Since a portal will never be the last node on a path, we can safely pop the next in line as the actual destination of the portal
                //doing it like this also caters for the scenario where the destination is the last node.
                _currentDestination = _currentPath.Pop();

                _currentGrid = portal.Execute(
                    _transform,
                    _currentDestination,
                    () =>
                    {
                        _isPortaling = false;
                    });
            }
            else if (_pathSettings.announceAllNodes)
            {
                AnnounceEvent(UnitNavigationEventMessage.Event.NodeReached, _previousDestination, null);
            }

            return !_isPortaling;
        }

        private bool ConsumeResult()
        {
            //Since result processing may actually repath and consequently a new result may arrive we need to operate on locals and null the pending result
            PathResult result;
            lock (_syncLock)
            {
                result = _pendingResult;
                _pendingResult = null;
            }

            var req = result.originalRequest as InternalPathRequest;

            //Consume way points if appropriate. This must be done prior to the processing of the result, since if the request was a way point request, the first item in line is the one the result concerns.
            if (req.pathType == InternalPathRequest.Type.Waypoint)
            {
                _wayPoints.Dequeue();
            }
            else if (req.pathType == InternalPathRequest.Type.PathboundWaypoint)
            {
                _pathboundWayPoints.Dequeue();
            }

            //Reset current destination and path no matter what
            _previousDestination = _transform.position;
            _currentDestination = null;
            _currentPath = null;

            //Process the result
            if (!ProcessAndValidateResult(result))
            {
                return false;
            }

            //Consume the result
            _onFinalApproach = false;
            _currentPath = result.path;
            _currentGrid = req.fromGrid;
            _remainingSquaredDistance = _currentPath.CalculateSquaredLength();
            _endOfResolvedPath = _currentPath.Last().position;
            _endOfPath = _endOfResolvedPath;

            //Update pending way points
            UpdatePathboundWaypoints(result.pendingWaypoints);

            //Pop the first node as our next destination.
            _unit.hasArrivedAtDestination = false;
            _currentDestination = _currentPath.Pop();
            return true;
        }

        private bool ProcessAndValidateResult(PathResult result)
        {
            for (int i = 0; i < _resultProcessors.Length; i++)
            {
                if (_resultProcessors[i].HandleResult(result, this))
                {
                    return (result.status == PathingStatus.Complete);
                }
            }

            UnitNavigationEventMessage.Event msgEvent = UnitNavigationEventMessage.Event.None;

            switch (result.status)
            {
                case PathingStatus.Complete:
                {
                    /* All is good, no more to do */
                    return true;
                }

                case PathingStatus.NoRouteExists:
                case PathingStatus.StartOutsideGrid:
                case PathingStatus.EndOutsideGrid:
                {
                    msgEvent = UnitNavigationEventMessage.Event.StoppedNoRouteExists;
                    break;
                }

                case PathingStatus.DestinationBlocked:
                {
                    msgEvent = UnitNavigationEventMessage.Event.StoppedDestinationBlocked;
                    break;
                }

                case PathingStatus.Decayed:
                {
                    //We cannot reissue the request here, since we may be on a different thread, but then again why would we issue the request again if it had a decay threshold its no longer valid.
                    msgEvent = UnitNavigationEventMessage.Event.StoppedRequestDecayed;
                    break;
                }

                case PathingStatus.Failed:
                {
                    Debug.LogError("Path request failed: " + result.errorInfo);
                    break;
                }
            }

            var destination = result.originalRequest.to;
            var pendingWaypoints = _wayPoints.count > 0 ? _wayPoints.ToArray() : Consts.EmptyVectorArray;

            StopInternal();

            if (msgEvent != UnitNavigationEventMessage.Event.None)
            {
                AnnounceEvent(msgEvent, destination, pendingWaypoints);
            }

            return false;
        }

        private void HandleWaypointsAndArrival()
        {
            //if we are close to the end node, start testing if we need to slow down or queue the next waypoint, unless we are moving on to another waypoint.
            if (_pendingResult == null && _pendingPathRequest == null)
            {
                var dirEnd = _transform.position.DirToXZ(_endOfResolvedPath);
                var distanceToEndSquared = dirEnd.sqrMagnitude;

                if (_pathboundWayPoints.count > 0)
                {
                    var requestNextWaypointDistanceSquared = _pathSettings.requestNextWaypointDistance * _pathSettings.requestNextWaypointDistance;
                    if ((distanceToEndSquared < requestNextWaypointDistanceSquared) && ((_remainingSquaredDistance < requestNextWaypointDistanceSquared) || (_currentPath != null && _currentPath.count == 0)))
                    {
                        RequestPath(_endOfResolvedPath, _pathboundWayPoints.Peek(), InternalPathRequest.Type.PathboundWaypoint);
                    }
                }
                else if (_wayPoints.count > 0)
                {
                    var requestNextWaypointDistanceSquared = _pathSettings.requestNextWaypointDistance * _pathSettings.requestNextWaypointDistance;
                    if ((distanceToEndSquared < requestNextWaypointDistanceSquared) && ((_remainingSquaredDistance < requestNextWaypointDistanceSquared) || (_currentPath != null && _currentPath.count == 0)))
                    {
                        RequestPath(_endOfResolvedPath, _wayPoints.Peek(), InternalPathRequest.Type.Waypoint);
                    }
                }
                else if (!_onFinalApproach && (distanceToEndSquared < this.slowingDistance * this.slowingDistance) && ((_remainingSquaredDistance < this.slowingDistance * this.slowingDistance) || (_currentPath != null && _currentPath.count == 0)))
                {
                    _onFinalApproach = true;
                }
            }
        }

        private void HandlePathReplan()
        {
            //If we are moving entirely off grid, there is no point in replanning, as there is nothing to replan on.
            if (_currentGrid == null || _pathSettings.replanMode == ReplanMode.NoReplan)
            {
                return;
            }

            var now = Time.time;
            if (now - _lastPathRequestTime < _pathSettings.replanInterval)
            {
                return;
            }

            bool replan = true;
            if (_pathSettings.replanMode == ReplanMode.Dynamic)
            {
                replan = _currentGrid.HasSectionsChangedSince(_transform.position, _lastPathRequestTime);
            }

            if (replan)
            {
                ReplanPath();
            }
        }

        private void UpdatePathboundWaypoints(Vector3[] newPoints)
        {
            if (newPoints == null || newPoints.Length == 0)
            {
                return;
            }

            _endOfPath = newPoints[newPoints.Length - 1];

            _pathboundWayPoints.Clear();
            for (int i = 0; i < newPoints.Length; i++)
            {
                _pathboundWayPoints.Enqueue(newPoints[i]);
            }
        }

        private void StopInternal()
        {
            lock (_syncLock)
            {
                _onFinalApproach = false;
                _stopped = true;
                _wayPoints.Clear();
                _pathboundWayPoints.Clear();
                _currentPath = null;
                _pendingPathRequest = null;
                _currentDestination = null;
                _pendingResult = null;
                _manualReplan = null;
            }
        }

        public void CloneFrom(SteerForPathComponent steerForPath)
        {
            this.priority = steerForPath.priority;
            this.weight = steerForPath.weight;

            this.slowingAlgorithm = steerForPath.slowingAlgorithm;
            this.autoCalculateSlowingDistance = steerForPath.autoCalculateSlowingDistance;
            this.slowingDistance = steerForPath.slowingDistance;
            this.arrivalDistance = steerForPath.arrivalDistance;
        }

        private class InternalPathRequest : BasicPathRequest
        {
            internal enum Type
            {
                Normal,
                Waypoint,
                PathboundWaypoint
            }

            internal Type pathType
            {
                get;
                set;
            }

            internal static InternalPathRequest Internalize(IPathRequest request)
            {
                var internalized = request as InternalPathRequest;
                if (internalized == null)
                {
                    internalized = new InternalPathRequest();
                    Utils.CopyProps(request, internalized);
                }

                internalized.pathType = Type.Normal;
                return internalized;
            }
        }
    }
}