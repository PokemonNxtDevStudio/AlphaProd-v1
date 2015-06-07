/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Units
{
    using Apex.Common;
    using Apex.DataStructures;
    using Apex.PathFinding;
    using Apex.Steering;
    using Apex.WorldGeometry;
    using UnityEngine;

    public class UnitFacade : IUnitFacade
    {
        private int _formationIndex = -1;

        private IUnitProperties _props;
        private IMovable _movable;
        private IMovingObject _moving;
        private IDefineSpeed _speeder;
        private IPathFinderOptions _pathFinderOptions;
        private IPathNavigationOptions _pathNavOptions;

        public AttributeMask attributes
        {
            get { return _props.attributes; }
        }

        public float baseToPositionOffset
        {
            get { return _props.baseToPositionOffset; }
        }

        public Collider collider
        {
            get;
            private set;
        }

        public Path currentPath
        {
            get { return _movable.currentPath; }
        }

        public IIterable<Vector3> currentWaypoints
        {
            get { return _movable.currentWaypoints; }
        }

        public float fieldOfView
        {
            get { return _props.fieldOfView; }
        }

        public Vector3? finalDestination
        {
            get { return _movable.finalDestination; }
        }

        public Vector3 forward
        {
            get { return this.transform.forward; }
        }

        public GameObject gameObject
        {
            get;
            private set;
        }

        public float height
        {
            get { return _props.height; }
        }

        public HeightNavigationCapabilities heightNavigationCapability
        {
            get { return _props.heightNavigationCapability; }
        }

        public bool isGrounded
        {
            get { return _moving.isGrounded; }
        }

        public bool isMovable
        {
            get;
            private set;
        }

        public bool isSelectable
        {
            get { return _props.isSelectable; }
        }

        public bool isSelected
        {
            get { return _props.isSelected; }

            set { _props.isSelected = value; }
        }

        public float maxAcceleration
        {
            get { return _speeder.maxAcceleration; }
        }

        public float maxAngularAcceleration
        {
            get { return _speeder.maxAngularAcceleration; }
        }

        public float maxDeceleration
        {
            get { return _speeder.maxDeceleration; }
        }

        public float maximumAngularSpeed
        {
            get { return _speeder.maximumAngularSpeed; }
        }

        public float maximumSpeed
        {
            get { return _speeder.maximumSpeed; }
        }

        public float minimumSpeed
        {
            get { return _speeder.minimumSpeed; }
        }

        public Vector3? nextNodePosition
        {
            get { return _movable.nextNodePosition; }
        }

        public IPathFinderOptions pathFinderOptions
        {
            get { return _pathFinderOptions; }
        }

        public IPathNavigationOptions pathNavigationOptions
        {
            get { return _pathNavOptions; }
        }

        public Vector3 position
        {
            get { return this.transform.position; }
        }

        public float radius
        {
            get { return _props.radius; }
        }

        public Vector3 basePosition
        {
            get { return _props.basePosition; }
        }

        public Transform transform
        {
            get;
            private set;
        }

        public TransientGroup<IUnitFacade> transientGroup
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the velocity of the unit. This represents the movement force applied to the unit. Also see <see cref="actualVelocity" />.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public Vector3 velocity
        {
            get { return _moving.velocity; }
        }

        /// <summary>
        /// Gets the actual velocity of the unit. This may differ from <see cref="velocity"/> in certain scenarios, e.g. during collisions, if being moved by other means etc.
        /// </summary>
        /// <value>
        /// The actual velocity.
        /// </value>
        public Vector3 actualVelocity
        {
            get { return _moving.actualVelocity; }
        }

        public int formationIndex
        {
            get
            {
                return _formationIndex;
            }

            set
            {
                _formationIndex = value;
            }
        }

        public IPositioned formationPos
        {
            get;
            set;
        }

        public bool hasArrivedAtDestination
        {
            get;
            set;
        }

        public int determination
        {
            get { return _props.determination; }
            set { _props.determination = value; }
        }

        public void DisableMovementOrders()
        {
            _movable.DisableMovementOrders();
        }

        public void EnableMovementOrders()
        {
            _movable.EnableMovementOrders();
        }

        public void SetPreferredSpeed(float speed)
        {
            _speeder.SetPreferredSpeed(speed);
        }

        public float GetPreferredSpeed(Vector3 currentMovementDirection)
        {
            return _speeder.GetPreferredSpeed(currentMovementDirection);
        }

        public void MarkSelectPending(bool pending)
        {
            _props.MarkSelectPending(pending);
        }

        public void MoveAlong(Path path)
        {
            _movable.MoveAlong(path);
        }

        public void MoveAlong(Path path, ReplanCallback onReplan)
        {
            _movable.MoveAlong(path, onReplan);
        }

        public void MoveTo(Vector3 position, bool append)
        {
            _movable.MoveTo(position, append);
        }

        public void RecalculateBasePosition()
        {
            _props.RecalculateBasePosition();
        }

        public void Resume()
        {
            _moving.Resume();
        }

        public void SignalStop()
        {
            _speeder.SignalStop();
        }

        public void Stop()
        {
            _moving.Stop();
        }

        public void Wait(float? seconds)
        {
            _moving.Wait(seconds);
        }

        void IDefineSpeed.CloneFrom(IDefineSpeed speedComponent)
        {
            _speeder.CloneFrom(speedComponent);
        }

        public virtual void Initialize(GameObject unitObject)
        {
            _props = unitObject.As<IUnitProperties>(false, true);
            _movable = unitObject.As<IMovable>(false, false);
            _moving = unitObject.As<IMovingObject>(false, true);
            _speeder = unitObject.As<IDefineSpeed>(false, true);
            _pathFinderOptions = unitObject.As<IPathFinderOptions>(false, false) ?? new PathFinderOptions();
            _pathNavOptions = unitObject.As<IPathNavigationOptions>(false, false) ?? new PathNavigationOptions();

            this.isMovable = _movable != null;
            if (!this.isMovable)
            {
                _movable = new MovableDummy(unitObject.name);
            }

            this.gameObject = unitObject;
            this.transform = unitObject.transform;
            this.collider = unitObject.GetComponent<Collider>();
        }

        private class MovableDummy : IMovable
        {
            private string _gameObjectName;

            public MovableDummy(string gameObjectName)
            {
                _gameObjectName = gameObjectName;
            }

            public Path currentPath
            {
                get
                {
                    ThrowException();
                    return null;
                }
            }

            public IIterable<Vector3> currentWaypoints
            {
                get
                {
                    ThrowException();
                    return null;
                }
            }

            public Vector3? finalDestination
            {
                get
                {
                    ThrowException();
                    return null;
                }
            }

            public Vector3? nextNodePosition
            {
                get
                {
                    ThrowException();
                    return null;
                }
            }

            public void MoveTo(Vector3 position, bool append)
            {
                ThrowException();
            }

            public void MoveAlong(Path path)
            {
                ThrowException();
            }

            public void MoveAlong(Path path, ReplanCallback onReplan)
            {
                ThrowException();
            }

            public void Wait(float? seconds)
            {
                ThrowException();
            }

            public void Resume()
            {
                ThrowException();
            }

            public void EnableMovementOrders()
            {
                ThrowException();
            }

            public void DisableMovementOrders()
            {
                ThrowException();
            }

            private void ThrowException()
            {
                throw new MissingComponentException(string.Format("Game object {0} does not have a component of type IMovable, which is required to call IMovable methods and properties.", _gameObjectName));
            }
        }
    }
}