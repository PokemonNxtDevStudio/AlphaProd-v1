/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering
{
    using UnityEngine;

    public sealed class OrientationOutput
    {
        public Vector3 desiredOrientation;

        private float _desiredAngularAcceleration;
        private bool _hasOutput;

        public float desiredAngularAcceleration
        {
            get
            {
                return _desiredAngularAcceleration;
            }

            set
            {
                _desiredAngularAcceleration = value;
                _hasOutput = true;
            }
        }

        public bool hasOutput
        {
            get { return _hasOutput; }
        }

        public void Clear()
        {
            this.desiredOrientation = Vector3.zero;
            this.desiredAngularAcceleration = 0f;
            _hasOutput = false;
        }
    }
}
