/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    using System;
    using System.Collections;
    using Apex.Common;
    using Apex.DataStructures;
    using Apex.PathFinding.MoveCost;
    using Apex.Units;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// Base class for pathing engines
    /// </summary>
    public abstract class PathingEngineBase : IPathingEngine
    {
        private readonly ICellCostStrategy _cellCostStrategy;
        private readonly IMoveCost _costProvider;
        private readonly ISmoothPaths _smoother;
        private IPathNode _goal;
        private IPathRequest _currentRequest;
        private SafeIterator _coroutineIter;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathingEngineBase"/> class.
        /// </summary>
        /// <param name="moveCostProvider">The move cost provider.</param>
        /// <param name="smoother">The path smoother to use</param>
        protected PathingEngineBase(IMoveCost moveCostProvider, ICellCostStrategy cellCostStrategy, ISmoothPaths smoother)
        {
            Ensure.ArgumentNotNull(moveCostProvider, "moveCostProvider");
            Ensure.ArgumentNotNull(cellCostStrategy, "cellCostStrategy");
            Ensure.ArgumentNotNull(smoother, "smoother");

            _costProvider = moveCostProvider;
            _smoother = smoother;
            _cellCostStrategy = cellCostStrategy;
            _coroutineIter = new SafeIterator(this);
        }

        /// <summary>
        /// Gets the cost provider.
        /// </summary>
        /// <value>
        /// The cost provider.
        /// </value>
        protected IMoveCost costProvider
        {
            get { return _costProvider; }
        }

        /// <summary>
        /// Gets the cell cost strategy.
        /// </summary>
        /// <value>
        /// The cell cost strategy.
        /// </value>
        protected ICellCostStrategy cellCostStrategy
        {
            get { return _cellCostStrategy; }
        }

        /// <summary>
        /// Gets the path smoother.
        /// </summary>
        /// <value>
        /// The path smoother.
        /// </value>
        protected ISmoothPaths pathSmoother
        {
            get { return _smoother; }
        }

        /// <summary>
        /// Gets the current request.
        /// </summary>
        /// <value>
        /// The current request.
        /// </value>
        protected IPathRequest currentRequest
        {
            get { return _currentRequest; }
        }

        /// <summary>
        /// Gets the goal, i.e. final destination.
        /// </summary>
        /// <value>
        /// The goal.
        /// </value>
        protected IPathNode goal
        {
            get { return _goal; }
        }

        /// <summary>
        /// Processes the request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessRequest(IPathRequest request)
        {
            try
            {
                if (!StartRequest(request))
                {
                    return;
                }

                var status = PathingStatus.Running;
                while (status == PathingStatus.Running)
                {
                    status = ProcessNext();
                }

                CompleteRequest(status);
            }
            catch (Exception e)
            {
                FailRequest(request, e);
            }
        }

        /// <summary>
        /// Processes the request as a coroutine.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The coroutine enumerator</returns>
        public IEnumerator ProcessRequestCoroutine(IPathRequest request)
        {
            _coroutineIter.Init(request);
            return _coroutineIter;
        }

        /// <summary>
        /// Updates the goal to a new value.
        /// </summary>
        /// <param name="newGoal">The new goal.</param>
        protected void UpdateGoal(IPathNode newGoal)
        {
            _goal = newGoal;
            _currentRequest.to = newGoal.position;
        }

        private IEnumerator ProcessRequestCoroutineInternal(IPathRequest request)
        {
            if (!StartRequest(request))
            {
                yield break;
            }

            var status = PathingStatus.Running;
            while (status == PathingStatus.Running)
            {
                status = ProcessNext();
                yield return null;
            }

            CompleteRequest(status);
        }

        private bool StartRequest(IPathRequest request)
        {
            Ensure.ArgumentNotNull(request, "request");

            if (!request.isValid)
            {
                throw new ArgumentException("The request is invalid.", "request");
            }

            if (_currentRequest != null)
            {
                throw new InvalidOperationException("A new request cannot be started while another request is being processed.");
            }

            _currentRequest = request;

            var start = request.fromGrid.GetCell(request.from, false) as IPathNode;
            _goal = request.toGrid.GetCell(request.to, false) as IPathNode;

            if (start == null)
            {
                CompleteRequest(PathingStatus.StartOutsideGrid);
                return false;
            }

            if (_goal == null)
            {
                if (_currentRequest.pathFinderOptions.navigateToNearestIfBlocked)
                {
                    _goal = request.toGrid.GetCell(request.to, true) as IPathNode;
                    request.to = _goal.position;
                }
                else
                {
                    CompleteRequest(PathingStatus.EndOutsideGrid);
                    return false;
                }
            }

            if (!_goal.isWalkable(_currentRequest.requesterProperties.attributes) && !_currentRequest.pathFinderOptions.navigateToNearestIfBlocked)
            {
                CompleteRequest(PathingStatus.DestinationBlocked);
                return false;
            }

            OnStart(start);

            return true;
        }

        private void CompleteRequest(PathingStatus status)
        {
            PathResult result;

            if (_currentRequest.type == RequestType.IntelOnly)
            {
                result = new PathResult(status, null, _goal.g, _currentRequest);
            }
            else if (status == PathingStatus.Complete)
            {
                Path path;

                var maxPathLength = Mathf.CeilToInt(_goal.g / (_goal.parent.cellSize * _costProvider.baseMoveCost));

                //Fix the actual destination so it does not overlap obstructions
                FixupGoal(_currentRequest);

                if (_currentRequest.pathFinderOptions.usePathSmoothing)
                {
                    path = _smoother.Smooth(_goal, maxPathLength, _currentRequest, _cellCostStrategy);
                }
                else
                {
                    path = new Path(maxPathLength);

                    //Push the actual end position as the goal
                    path.Push(new Position(_currentRequest.to));

                    IPathNode current = _goal.predecessor;
                    while (current != null)
                    {
                        path.Push(current);
                        current = current.predecessor;
                    }

                    //Instead of testing for it in the while loop, just pop off the start node and replace it with the actual start position
                    if (path.count > 1)
                    {
                        path.Pop();
                    }

                    path.Push(new Position(_currentRequest.from));
                }

                result = new PathResult(status, path, _goal.g, _currentRequest);
            }
            else
            {
                result = new PathResult(status, null, 0, _currentRequest);
            }

            _currentRequest.Complete(result);
            _currentRequest = null;
        }

        private void FailRequest(IPathRequest request, Exception e)
        {
            var result = new PathResult(PathingStatus.Failed, null, 0, request)
            {
                errorInfo = string.Concat(e.Message, Environment.NewLine, e.StackTrace)
            };

            request.Complete(result);
            _currentRequest = null;
        }

        private void FixupGoal(IPathRequest request)
        {
            var unitProps = request.requesterProperties;
            var requesterRadius = request.requesterProperties.radius;
            var actualGoal = request.to;

            var halfCell = _goal.parent.cellSize / 2.0f;

            var dx = actualGoal.x - _goal.position.x;
            var dz = actualGoal.z - _goal.position.z;

            var overlapLeft = (requesterRadius - dx) - halfCell;
            var overlapRight = (dx + requesterRadius) - halfCell;
            var overlapTop = (dz + requesterRadius) - halfCell;
            var overlapBottom = (requesterRadius - dz) - halfCell;

            var adjX = 0.0f;
            var adjZ = 0.0f;

            if (overlapLeft > 0.0f && !ValidateGoalNeighbour(unitProps, -1, 0))
            {
                adjX = -overlapLeft;
            }
            else if (overlapRight > 0.0f && !ValidateGoalNeighbour(unitProps, 1, 0))
            {
                adjX = overlapRight;
            }

            if (overlapTop > 0.0f && !ValidateGoalNeighbour(unitProps, 0, 1))
            {
                adjZ = overlapTop;
            }
            else if (overlapBottom > 0.0f && !ValidateGoalNeighbour(unitProps, 0, -1))
            {
                adjZ = -overlapBottom;
            }

            if ((adjX == 0.0f) && (adjZ == 0.0f))
            {
                if ((overlapLeft > 0.0f) && (overlapTop > 0.0f) && !ValidateGoalNeighbour(unitProps, -1, 1))
                {
                    adjX = -overlapLeft;
                    adjZ = overlapTop;
                }
                else if ((overlapLeft > 0.0f) && (overlapBottom > 0.0f) && !ValidateGoalNeighbour(unitProps, -1, -1))
                {
                    adjX = -overlapLeft;
                    adjZ = -overlapBottom;
                }
                else if ((overlapRight > 0.0f) && (overlapTop > 0.0f) && !ValidateGoalNeighbour(unitProps, 1, 1))
                {
                    adjX = overlapRight;
                    adjZ = overlapTop;
                }
                else if ((overlapRight > 0.0f) && (overlapBottom > 0.0f) && !ValidateGoalNeighbour(unitProps, 1, -1))
                {
                    adjX = overlapRight;
                    adjZ = -overlapBottom;
                }
            }

            if ((adjX != 0.0f) || (adjZ != 0.0f))
            {
                request.to = new Vector3(actualGoal.x - adjX, actualGoal.y, actualGoal.z - adjZ);
            }
        }

        private bool ValidateGoalNeighbour(IUnitProperties unitProps, int dx, int dz)
        {
            var c = _goal.GetNeighbour(dx, dz);
            return ((c == null) || c.isWalkableFrom(_goal, unitProps));
        }

        /// <summary>
        /// Processes the next node.
        /// </summary>
        /// <returns>The current pathing status</returns>
        protected abstract PathingStatus ProcessNext();

        /// <summary>
        /// Called when a request is about to be processed.
        /// </summary>
        /// <param name="start">The start node.</param>
        protected virtual void OnStart(IPathNode start)
        {
        }

        private class SafeIterator : IEnumerator
        {
            private PathingEngineBase _engine;
            private IEnumerator _innerIter;
            private IPathRequest _request;

            public SafeIterator(PathingEngineBase engine)
            {
                _engine = engine;
            }

            //This is not used for anything
            public object Current
            {
                get { return _innerIter.Current; }
            }

            public bool MoveNext()
            {
                try
                {
                    return _innerIter.MoveNext();
                }
                catch (Exception e)
                {
                    _engine.FailRequest(_request, e);
                }

                return false;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public void Init(IPathRequest request)
            {
                _request = request;
                _innerIter = _engine.ProcessRequestCoroutineInternal(request);
            }
        }
    }
}
