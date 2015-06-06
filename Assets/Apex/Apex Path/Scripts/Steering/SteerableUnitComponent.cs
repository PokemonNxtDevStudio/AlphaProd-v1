/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Steering
{
    using System.Collections.Generic;
    using Apex.LoadBalancing;
    using Apex.Messages;
    using Apex.Services;
    using Apex.Units;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// Main steering controller that combines the input from attached <see cref="SteeringComponent" />s to move the unit.
    /// </summary>
    [AddComponentMenu("Apex/Navigation/Steering/Steerable Unit")]
    [ApexComponent("Steering")]
    public class SteerableUnitComponent : ExtendedMonoBehaviour, ILoadBalanced, IMovingObject
    {
        private const float AngularSpeedLowerLimit = 0.0001f;

        /// <summary>
        /// Controls to what degree a unit will attempt to stick to the ground when moving down slopes.
        /// By default the unit will stick to the slope if its forward motion does not bring it to freefall.
        /// Increasing the value will make units more aggressive in sticking to the ground, i.e. they will convert their forward velocity more downwards.
        /// </summary>
        [MinCheck(0f, tooltip = "Controls to what degree a unit will attempt to stick to the ground when moving down slopes.")]
        public float groundStickynessFactor = 1f;

        /// <summary>
        /// The time over which to stop when waiting or when no steering components have any output (as permitted by deceleration capabilities).
        /// </summary>
        [Tooltip("The time over which to stop when waiting or when no steering components have any output (as permitted by deceleration capabilities).")]
        public float stopTimeFrame = 0.2f;

        /// <summary>
        /// The amount of seconds after which the unit will stop if it is stuck.
        /// </summary>
        [Tooltip("The amount of seconds after which the unit will stop if it is stuck.")]
        public float stopIfStuckForSeconds = 3.0f;

        /// <summary>
        /// The gravity acceleration
        /// </summary>
        [Tooltip("The gravity acceleration")]
        public float gravity = -9.81f;

        /// <summary>
        /// The terminal velocity, i.e. maximum fall speed (m/s)
        /// </summary>
        [Tooltip("The terminal velocity, i.e. maximum fall speed (m/s)")]
        public float terminalVelocity = 50f;

        private Vector3 _currentVelocity;
        private Vector3 _currentPlanarVelocity;
        private Vector3 _currentSpatialVelocity;
        private float _gravitationVelocity;
        private float _currentAngularSpeed;
        private float _targetHeight;

        private bool _waiting;
        private bool _stopped;
        private float _stuckCheckLastMove;

        private Vector3 _lastFramePosition;
        private Vector3 _actualVelocity;

        private Transform _transform;
        private IUnitFacade _unit;
        private IMoveUnits _mover;
        private ISampleHeights _heights;
        private List<ISteeringBehaviour> _steeringComponents;
        private List<IOrientationBehaviour> _orientationComponents;

        private SteeringOutput _steering;
        private OrientationOutput _orientation;
        private SteeringInput _steeringInput;

        /// <summary>
        /// Gets the speed of the unit in m/s.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public float speed
        {
            get { return _currentVelocity.magnitude; }
        }

        /// <summary>
        /// Gets the velocity of the unit. This represents the movement force applied to the object. Also see <see cref="actualVelocity"/>.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public Vector3 velocity
        {
            get { return _currentVelocity; }
        }

        /// <summary>
        /// Gets the actual velocity of the unit. This may differ from <see cref="velocity"/> in certain scenarios, e.g. during collisions, if being moved by other means etc.
        /// </summary>
        /// <value>
        /// The actual velocity.
        /// </value>
        public Vector3 actualVelocity
        {
            get { return _actualVelocity; }
        }

        /// <summary>
        /// Gets a value indicating whether this object is grounded, i.e. not falling or otherwise raised above its natural base position.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is grounded; otherwise, <c>false</c>.
        /// </value>
        public bool isGrounded
        {
            get;
            private set;
        }

        bool ILoadBalanced.repeat
        {
            get { return true; }
        }

        private void Awake()
        {
            this.WarnIfMultipleInstances();

            _transform = this.transform;
            var rb = this.GetComponent<Rigidbody>();

            //Resolve the mover
            _mover = this.As<IMoveUnits>();
            if (_mover == null)
            {
                var fact = this.As<IMoveUnitsFactory>();
                var charController = this.GetComponent<CharacterController>();

                if (fact != null)
                {
                    _mover = fact.Create();
                }
                else if (charController != null)
                {
                    _mover = new CharacterControllerMover(charController);
                }
                else if (rb != null)
                {
                    _mover = new RigidBodyMover(rb);
                }
                else
                {
                    _mover = new DefaultMover(_transform);
                }
            }

            //Make sure the rigidbody obeys the rules
            if (rb != null)
            {
                rb.constraints |= RigidbodyConstraints.FreezeRotation;
                rb.useGravity = false;
            }

            //Height resolver
            _heights = GameServices.heightStrategy.heightSampler;

            //Assign unit ref, container for components and steering visitors
            _steeringComponents = new List<ISteeringBehaviour>();
            _orientationComponents = new List<IOrientationBehaviour>();
            _steering = new SteeringOutput();
            _orientation = new OrientationOutput();
            _steeringInput = new SteeringInput();
        }

        /// <summary>
        /// Called on Start
        /// </summary>
        protected override void Start()
        {
            base.Start();

            //Get unit ref
            _unit = this.GetUnitFacade();
            _steeringInput.unit = _unit;

            //Init other vars
            _targetHeight = _unit.baseToPositionOffset;
            _lastFramePosition = _unit.position;
            _stopped = true;

            UpdateInput();
        }

        /// <summary>
        /// Called on Start and OnEnable, but only one of the two, i.e. at startup it is only called once.
        /// </summary>
        protected override void OnStartAndEnable()
        {
            //Hook up with the load balancer
            NavLoadBalancer.steering.Add(this);
        }

        private void OnDisable()
        {
            Stop();

            NavLoadBalancer.steering.Remove(this);
        }

        private void FixedUpdate()
        {
            Steer(Time.deltaTime);
        }

        /// <summary>
        /// Registers a steering component.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        public void RegisterSteeringBehavior(ISteeringBehaviour behavior)
        {
            int count = _steeringComponents.Count;
            for (int i = 0; i < count; i++)
            {
                var c = _steeringComponents[i];
                if (c.priority == behavior.priority)
                {
                    var grp = c as SteeringGroup;
                    if (grp == null)
                    {
                        grp = new SteeringGroup(c.priority);
                        _steeringComponents[i] = grp;
                        grp.Add(c);
                    }

                    grp.Add(behavior);
                    return;
                }
            }

            _steeringComponents.Add(behavior);
            _steeringComponents.Sort((a, b) => b.priority.CompareTo(a.priority));
        }

        /// <summary>
        /// Unregisters a steering behavior.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        public void UnregisterSteeringBehavior(ISteeringBehaviour behavior)
        {
            int start = _steeringComponents.Count - 1;
            for (int i = start; i >= 0; i--)
            {
                var c = _steeringComponents[i];
                if (c.priority == behavior.priority)
                {
                    var grp = c as SteeringGroup;
                    if (grp != null)
                    {
                        grp.Remove(behavior);
                    }
                    else
                    {
                        _steeringComponents.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Registers a steering component.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        public void RegisterOrientationBehavior(IOrientationBehaviour behavior)
        {
            _orientationComponents.Add(behavior);
            _orientationComponents.Sort((a, b) => b.priority.CompareTo(a.priority));
        }

        /// <summary>
        /// Unregisters a steering behavior.
        /// </summary>
        /// <param name="behavior">The behavior.</param>
        public void UnregisterOrientationBehavior(IOrientationBehaviour behavior)
        {
            _orientationComponents.Remove(behavior);
        }

        /// <summary>
        /// Stops the unit from moving.
        /// </summary>
        public void Stop()
        {
            for (int i = 0; i < _steeringComponents.Count; i++)
            {
                _steeringComponents[i].Stop();
            }

            _waiting = false;
        }

        /// <summary>
        /// Waits the specified seconds before continuing the move.
        /// </summary>
        /// <param name="seconds">The seconds to wait or null to wait until explicitly <see cref="Resume" />d.</param>
        public void Wait(float? seconds)
        {
            _waiting = true;

            if (seconds.HasValue)
            {
                NavLoadBalancer.defaultBalancer.Add(new OneTimeAction((ignored) => this.Resume()), seconds.Value, true);
            }
        }

        /// <summary>
        /// Resumes movements after a <see cref="Wait" />.
        /// </summary>
        public void Resume()
        {
            _waiting = false;
        }

        private void StopMovement()
        {
            if (_stopped)
            {
                return;
            }

            _unit.SignalStop();

            _stopped = true;

            _currentVelocity = _currentPlanarVelocity = _currentSpatialVelocity = Vector3.zero;
            _currentAngularSpeed = 0f;

            _mover.Stop();
        }

        private bool IsStuck(float deltaTime)
        {
            if (this.stopIfStuckForSeconds <= 0.0f)
            {
                return false;
            }

            if (_actualVelocity.sqrMagnitude > 0.0001f)
            {
                _stuckCheckLastMove = Time.time;
                return false;
            }

            return ((Time.time - _stuckCheckLastMove) > this.stopIfStuckForSeconds);
        }

        float? ILoadBalanced.ExecuteUpdate(float deltaTime, float nextInterval)
        {
            if (_stopped || _waiting)
            {
                return null;
            }

            if (IsStuck(deltaTime))
            {
                Stop();
                StopMovement();
                GameServices.messageBus.Post(new UnitNavigationEventMessage(this.gameObject, UnitNavigationEventMessage.Event.Stuck));
                return null;
            }

            UpdateInput();

            return null;
        }

        private void Steer(float deltaTime)
        {
            _actualVelocity = (_transform.position - _lastFramePosition) / deltaTime;
            _lastFramePosition = _transform.position;

            //prepare input and reset output
            var grid = GridManager.instance.GetGrid(_transform.position);
            _steeringInput.currentFullVelocity = _currentVelocity;
            _steeringInput.currentSpatialVelocity = _currentSpatialVelocity;
            _steeringInput.currentPlanarVelocity = _currentPlanarVelocity;
            _steeringInput.grid = grid;

            _steering.Clear();

            if (!_waiting)
            {
                //Get the steering output from all registered components
                var count = _steeringComponents.Count;
                for (int i = 0; i < count; i++)
                {
                    _steeringComponents[i].GetSteering(_steeringInput, _steering);
                    if (_steering.hasOutput)
                    {
                        break;
                    }
                }

                if (_steering.pause)
                {
                    return;
                }
            }

            //If we are waiting or no steering components had an output, slow down to a stop
            if (!_steering.hasOutput && !_stopped)
            {
                var timeToTarget = Mathf.Max(this.stopTimeFrame, deltaTime);
                _steering.desiredAcceleration = Vector3.ClampMagnitude(-_currentPlanarVelocity / timeToTarget, _steeringInput.maxDeceleration);
            }

            //Adjust the current velocity to the output
            _currentSpatialVelocity = _currentSpatialVelocity + (_steering.desiredAcceleration * deltaTime);

            //Check if the velocity is below the minimum to move
            var minSpeed = _unit.minimumSpeed;
            if (_currentSpatialVelocity.sqrMagnitude < minSpeed * minSpeed)
            {
                StopMovement();
            }
            else if (_stopped)
            {
                _stuckCheckLastMove = Time.time;
                _stopped = false;
            }

            //Set the current velocity
            var maxSpeed = Mathf.Max(_steering.maxAllowedSpeed, _steeringInput.desiredSpeed);
            _currentSpatialVelocity = Vector3.ClampMagnitude(_currentSpatialVelocity, maxSpeed);
            _currentVelocity = _currentSpatialVelocity;
            _currentPlanarVelocity = _currentSpatialVelocity.OnlyXZ();

            //Adjust to elevation and gravity
            var moveNorm = Vector3.ClampMagnitude(_currentSpatialVelocity, 1f);

            var matrix = grid != null ? grid.cellMatrix : null;

            _targetHeight = _heights.GetProposedHeight(_unit, matrix, moveNorm);

            var heightDiff = (_targetHeight - _transform.position.y);
            var freefallThreshold = -maxSpeed * deltaTime * this.groundStickynessFactor;

            this.isGrounded = (heightDiff >= freefallThreshold);

            //Apply the needed vertical force to handle gravity or ascend
            if (!this.isGrounded && !_steering.overrideGravity)
            {
                if (_gravitationVelocity > -this.terminalVelocity)
                {
                    _gravitationVelocity += this.gravity * deltaTime;
                }

                if (_gravitationVelocity * deltaTime < heightDiff)
                {
                    _gravitationVelocity = heightDiff / deltaTime;
                }

                _currentVelocity.y += _gravitationVelocity;
            }
            else if (heightDiff != 0f)
            {
                _gravitationVelocity = 0f;

                if (heightDiff > 0f || !_steering.overrideGravity)
                {
                    _currentVelocity.y += heightDiff / deltaTime;

                    _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, maxSpeed);
                }
            }

            if (_steering.verticalForce != 0f)
            {
                _currentVelocity.y += _steering.verticalForce * deltaTime;
            }

            //Get the orientation component
            _orientation.Clear();
            var orientationCount = _orientationComponents.Count;
            for (int i = 0; i < orientationCount; i++)
            {
                _orientationComponents[i].GetOrientation(_steeringInput, _orientation);
                if (_orientation.hasOutput)
                {
                    break;
                }
            }

            if (Mathf.Abs(_orientation.desiredAngularAcceleration) > 0.01f)
            {
                _currentAngularSpeed = _currentAngularSpeed + (_orientation.desiredAngularAcceleration * deltaTime);
                _currentAngularSpeed = Mathf.Clamp(_currentAngularSpeed, 0f, _steeringInput.desiredAngularSpeed);
                _steeringInput.currentAngularSpeed = _currentAngularSpeed;
            }

            //Do the movement and rotation
            _mover.Move(_currentVelocity, deltaTime);

            if (_currentAngularSpeed > AngularSpeedLowerLimit && _orientation.desiredOrientation.sqrMagnitude > 0f)
            {
                _mover.Rotate(_orientation.desiredOrientation, _currentAngularSpeed, deltaTime);
            }
        }

        private void UpdateInput()
        {
            _steeringInput.gravity = this.gravity;
            _steeringInput.maxAcceleration = _unit.maxAcceleration;
            _steeringInput.maxDeceleration = _unit.maxDeceleration;
            _steeringInput.maxAngularAcceleration = _unit.maxAngularAcceleration;
            _steeringInput.desiredSpeed = _unit.GetPreferredSpeed(_currentPlanarVelocity.normalized);
            _steeringInput.desiredAngularSpeed = _unit.maximumAngularSpeed;
        }

        public void CloneFrom(SteerableUnitComponent steerableUnit)
        {
            this.groundStickynessFactor = steerableUnit.groundStickynessFactor;
            this.stopIfStuckForSeconds = steerableUnit.stopIfStuckForSeconds;
            this.stopTimeFrame = steerableUnit.stopTimeFrame;
            this.gravity = steerableUnit.gravity;
            this.terminalVelocity = steerableUnit.terminalVelocity;
        }

        private class DefaultMover : IMoveUnits
        {
            private Transform _transform;

            public DefaultMover(Transform transform)
            {
                _transform = transform;
            }

            public void Move(Vector3 velocity, float deltaTime)
            {
                _transform.position = _transform.position + (velocity * deltaTime);
            }

            public void Rotate(Vector3 targetOrientation, float angularSpeed, float deltaTime)
            {
                var targetRotation = Quaternion.LookRotation(targetOrientation);
                _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, angularSpeed * Mathf.Rad2Deg * deltaTime);
            }

            public void Stop()
            {
                /* NOOP */
            }
        }

        private class RigidBodyMover : IMoveUnits
        {
            private Rigidbody _rigidBody;

            public RigidBodyMover(Rigidbody rigidBody)
            {
                _rigidBody = rigidBody;
            }

            public void Move(Vector3 velocity, float deltaTime)
            {
                if (!_rigidBody.isKinematic)
                {
                    _rigidBody.velocity = _rigidBody.angularVelocity = Vector3.zero;
                }

                _rigidBody.MovePosition(_rigidBody.position + (velocity * deltaTime));
            }

            public void Rotate(Vector3 targetOrientation, float angularSpeed, float deltaTime)
            {
                var targetRotation = Quaternion.LookRotation(targetOrientation);
                var orientation = Quaternion.RotateTowards(_rigidBody.rotation, targetRotation, angularSpeed * Mathf.Rad2Deg * deltaTime);
                _rigidBody.MoveRotation(orientation);
            }

            public void Stop()
            {
                if (!_rigidBody.isKinematic)
                {
                    _rigidBody.velocity = Vector3.zero;
                    _rigidBody.Sleep();
                }
            }
        }

        private class CharacterControllerMover : IMoveUnits
        {
            private Transform _transform;
            private CharacterController _controller;

            public CharacterControllerMover(CharacterController controller)
            {
                _transform = controller.transform;
                _controller = controller;
            }

            public void Move(Vector3 velocity, float deltaTime)
            {
                _controller.Move(velocity * deltaTime);
            }

            public void Rotate(Vector3 targetOrientation, float angularSpeed, float deltaTime)
            {
                var targetRotation = Quaternion.LookRotation(targetOrientation);
                _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, angularSpeed * Mathf.Rad2Deg * deltaTime);
            }

            public void Stop()
            {
                /* NOOP */
            }
        }
    }
}
