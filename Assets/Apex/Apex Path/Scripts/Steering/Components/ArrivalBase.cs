/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Steering.Components
{
    using Apex.Utilities;
    using UnityEngine;

    public abstract class ArrivalBase : SteeringComponent
    {
        /// <summary>
        /// The distance within which the unit will start to slow down for arrival.
        /// </summary>
        [MinCheck(0f)]
        public float slowingDistance = 1.5f;

        /// <summary>
        /// Determines whether the slowing distance is automatically calculated based on the unit's speed and deceleration capabilities.
        /// </summary>
        public bool autoCalculateSlowingDistance = true;

        /// <summary>
        /// The distance from the final destination where the unit will stop
        /// </summary>
        [MinCheck(0.1f)]
        public float arrivalDistance = 0.2f;

        /// <summary>
        /// The algorithm used to slow the unit for arrival.
        /// Linear works fine with short slowing distances, but logarithmic shows its worth at longer ones.
        /// </summary>
        public SlowingAlgorithm slowingAlgorithm = SlowingAlgorithm.Logarithmic;

        /// <summary>
        /// Gets a value indicating whether this instance has arrived.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has arrived; otherwise, <c>false</c>.
        /// </value>
        protected bool hasArrived
        {
            get;
            private set;
        }

        protected override void Awake()
        {
            base.Awake();

            if (this.autoCalculateSlowingDistance)
            {
                this.slowingDistance = 0f;
            }
        }

        protected Vector3 Arrive(Vector3 destination, SteeringInput input)
        {
            return Arrive(destination, input, input.desiredSpeed);
        }

        protected Vector3 Arrive(Vector3 destination, SteeringInput input, float desiredSpeed)
        {
            var dir = input.unit.position.DirToXZ(destination);

            var distance = dir.magnitude;
            var arriveDistance = distance - this.arrivalDistance;

            //Arrival is accurate within 10 centimeters
            this.hasArrived = arriveDistance <= 0.01f;
            if (this.hasArrived)
            {
                if (this.autoCalculateSlowingDistance)
                {
                    this.slowingDistance = 0f;
                }

                return -input.currentPlanarVelocity / Time.fixedDeltaTime;
            }

            //Calculate slowing distance if applicable
            if (this.autoCalculateSlowingDistance)
            {
                CalculateRequiredSlowingDistance(input);
            }

            //Find the target speed for arrival
            var targetSpeed = desiredSpeed;

            if (arriveDistance < this.slowingDistance)
            {
                if (this.slowingAlgorithm == SlowingAlgorithm.Logarithmic)
                {
                    targetSpeed *= Mathf.Log10(((9.0f / this.slowingDistance) * arriveDistance) + 1.0f);
                }
                else
                {
                    targetSpeed *= (arriveDistance / this.slowingDistance);
                }
            }

            var desiredVelocity = (dir / distance) * Mathf.Max(targetSpeed, input.unit.minimumSpeed);

            //Before determining the delta we need to evaluate if we are on arrival
            float targetAcceleration;
            if (desiredVelocity.sqrMagnitude < input.currentPlanarVelocity.sqrMagnitude)
            {
                targetAcceleration = input.maxDeceleration;
            }
            else
            {
                targetAcceleration = input.maxAcceleration;
            }

            desiredVelocity = (desiredVelocity - input.currentPlanarVelocity) / Time.fixedDeltaTime;

            return Vector3.ClampMagnitude(desiredVelocity, targetAcceleration);
        }

        protected void CalculateRequiredSlowingDistance(SteeringInput input)
        {
            float currentSpeed = input.currentPlanarVelocity.magnitude;
            if (this.slowingAlgorithm == SlowingAlgorithm.Logarithmic)
            {
                this.slowingDistance = Mathf.Max(this.slowingDistance, Mathf.Ceil(((currentSpeed * currentSpeed) / (2 * input.maxDeceleration)) * 1.1f) + 1f);
            }
            else
            {
                this.slowingDistance = Mathf.Max(this.slowingDistance, Mathf.Ceil((currentSpeed * currentSpeed) / (2 * input.maxDeceleration)) + 1f);
            }
        }
    }
}