/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering.Components
{
    using UnityEngine;

    /// <summary>
    /// Simple behavior that will slow the unit to a stop as fast as possible (at maxDeceleration).
    /// </summary>
    [AddComponentMenu("Apex/Navigation/Steering/Steer for Stop")]
    //TODO: remove before Apex Path release
    public class SteerForStop : SteeringComponent
    {
        /// <summary>
        /// The time over which to stop as permitted by deceleration capabilities.
        /// </summary>
        [Tooltip("The time over which to stop as permitted by deceleration capabilities.")]
        public float stopTimeFrame = 0.2f;

        /// <summary>
        /// Gets the desired steering output.
        /// </summary>
        /// <param name="input">The steering input containing relevant information to use when calculating the steering output.</param>
        /// <param name="output">The steering output to be populated.</param>
        public override void GetDesiredSteering(SteeringInput input, SteeringOutput output)
        {
            output.desiredAcceleration = Arrive(this.stopTimeFrame, input);
        }

        public void CloneTo(SteerForStop steerStop)
        {
            this.priority = steerStop.priority;
            this.weight = steerStop.weight;

            this.stopTimeFrame = steerStop.stopTimeFrame;
        }
    }
}
