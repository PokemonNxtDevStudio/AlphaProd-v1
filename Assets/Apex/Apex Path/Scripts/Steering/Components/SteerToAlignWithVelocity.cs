/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    [AddComponentMenu("Apex/Navigation/Steering/Steer to Align with Velocity")]
    [ApexComponent("Steering")]
    public class SteerToAlignWithVelocity : OrientationComponent
    {
        public float minimumSpeedToTurn = 0.1f;
        public bool alignWithElevation;

        public override void GetOrientation(SteeringInput input, OrientationOutput output)
        {
            if (input.currentPlanarVelocity.sqrMagnitude < this.minimumSpeedToTurn * this.minimumSpeedToTurn)
            {
                output.desiredAngularAcceleration = -input.currentAngularSpeed / Time.fixedDeltaTime;
                return;
            }

            var align = this.alignWithElevation && input.unit.isGrounded;

            var targetOrientation = align ? input.currentFullVelocity.normalized : input.currentPlanarVelocity.normalized;
            output.desiredOrientation = targetOrientation;
            output.desiredAngularAcceleration = GetAngularAcceleration(targetOrientation, input);
        }

        public void CloneFrom(SteerToAlignWithVelocity steerToAlign)
        {
            this.priority = steerToAlign.priority;
            this.slowingDistance = steerToAlign.slowingDistance;
            this.slowingAlgorithm = steerToAlign.slowingAlgorithm;

            this.minimumSpeedToTurn = steerToAlign.minimumSpeedToTurn;
            this.alignWithElevation = steerToAlign.alignWithElevation;
        }
    }
}
