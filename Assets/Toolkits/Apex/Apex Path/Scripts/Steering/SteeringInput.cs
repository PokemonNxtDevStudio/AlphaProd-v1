/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering
{
    using Apex.Units;
using Apex.WorldGeometry;
using UnityEngine;

    public sealed class SteeringInput
    {
        public float gravity;

        public float maxAcceleration;

        public float maxDeceleration;

        public float maxAngularAcceleration;

        public Vector3 currentPlanarVelocity;

        public Vector3 currentSpatialVelocity;

        public Vector3 currentFullVelocity;

        public float currentAngularSpeed;

        public float desiredSpeed;

        public float desiredAngularSpeed;

        public IGrid grid;

        public IUnitFacade unit;
    }
}
