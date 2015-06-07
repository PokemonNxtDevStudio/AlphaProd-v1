/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Steering
{
    using Apex.Utilities;
    using UnityEngine;

    /// <summary>
    /// Base class for steering components, that is components that steer the unit in some direction at some speed according to some logic.
    /// </summary>
    public abstract class SteeringComponent : ExtendedMonoBehaviour, ISteeringBehaviour
    {
        /// <summary>
        /// The weight this component's input will have in relation to other steering components.
        /// </summary>
        [Tooltip("The weight this component's input will have in relation to other steering components.")]
        public float weight = 1.0f;

        /// <summary>
        /// The priority of this steering behaviour relative to others. Only behaviours with the highest priority will influence the steering of the unit, provided they have any steering output.
        /// </summary>
        [MinCheck(0, tooltip = "The priority of this steering behaviour relative to others. Only behaviours with the highest priority will influence the steering of the unit, provided they have any steering output")]
        public int priority;

        int ISteeringBehaviour.priority
        {
            get { return this.priority; }
        }

        /// <summary>
        /// Stop the unit.
        /// </summary>
        public virtual void Stop()
        {
        }

        /// <summary>
        /// Gets the steering output.
        /// </summary>
        /// <param name="input">The steering input containing relevant information to use when calculating the steering output.</param>
        /// <param name="output">The steering output to be populated.</param>
        public void GetSteering(SteeringInput input, SteeringOutput output)
        {
            GetDesiredSteering(input, output);

            if (output.hasOutput)
            {
                output.desiredAcceleration *= this.weight;
            }
        }

        /// <summary>
        /// Gets the desired steering output.
        /// </summary>
        /// <param name="input">The steering input containing relevant information to use when calculating the steering output.</param>
        /// <param name="output">The steering output to be populated.</param>
        public abstract void GetDesiredSteering(SteeringInput input, SteeringOutput output);

        protected virtual void Awake()
        {
            this.WarnIfMultipleInstances();
        }

        /// <summary>
        /// Called on Start and OnEnable, but only one of the two, i.e. at startup it is only called once.
        /// </summary>
        protected override void OnStartAndEnable()
        {
            var parent = GetComponent<SteerableUnitComponent>();
            parent.RegisterSteeringBehavior(this);
        }

        /// <summary>
        /// Called when disabled.
        /// </summary>
        protected virtual void OnDisable()
        {
            var parent = GetComponent<SteerableUnitComponent>();
            parent.UnregisterSteeringBehavior(this);
        }

        protected Vector3 Seek(Vector3 destination, SteeringInput input)
        {
            return Seek(input.unit.position, destination, input);
        }

        protected Vector3 Seek(Vector3 destination, SteeringInput input, float acceleration)
        {
            return Seek(input.unit.position, destination, input, acceleration);
        }

        protected Vector3 Seek(Vector3 position, Vector3 destination, SteeringInput input)
        {
            return Seek(position, destination, input, input.maxAcceleration);
        }

        protected Vector3 Seek(Vector3 position, Vector3 destination, SteeringInput input, float acceleration)
        {
            var dir = position.DirToXZ(destination);
            var desiredVelocity = dir.normalized * acceleration;

            return desiredVelocity - input.currentPlanarVelocity;
        }

        protected Vector3 Flee(Vector3 from, SteeringInput input)
        {
            return Seek(from, input.unit.transform.position, input);
        }

        protected Vector3 Flee(Vector3 position, Vector3 from, SteeringInput input)
        {
            return Seek(from, position, input);
        }

        protected Vector3 Arrive(SteeringInput input)
        {
            return Vector3.ClampMagnitude(-input.currentPlanarVelocity / Time.fixedDeltaTime, input.maxDeceleration);
        }

        protected Vector3 Arrive(float timeToTarget, SteeringInput input)
        {
            timeToTarget = Mathf.Max(timeToTarget, Time.fixedDeltaTime);
            return Vector3.ClampMagnitude(-input.currentPlanarVelocity / timeToTarget, input.maxDeceleration);
        }
    }
}