/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering
{
    using System;
    using Apex.Utilities;

    [Serializable]
    public struct HeightNavigationCapabilities
    {
        [MinCheck(0f)]
        public float maxSlopeAngle;

        [MinCheck(0f)]
        public float maxClimbHeight;

        [MinCheck(0f)]
        public float maxDropHeight;

        public static bool operator ==(HeightNavigationCapabilities lhs, HeightNavigationCapabilities rhs)
        {
            return lhs.maxSlopeAngle.Equals(rhs.maxSlopeAngle) && lhs.maxClimbHeight.Equals(rhs.maxClimbHeight) && lhs.maxClimbHeight.Equals(rhs.maxClimbHeight);
        }

        public static bool operator !=(HeightNavigationCapabilities lhs, HeightNavigationCapabilities rhs)
        {
            return !(lhs.maxSlopeAngle.Equals(rhs.maxSlopeAngle) && lhs.maxClimbHeight.Equals(rhs.maxClimbHeight) && lhs.maxClimbHeight.Equals(rhs.maxClimbHeight));
        }

        public override int GetHashCode()
        {
            return this.maxSlopeAngle.GetHashCode() ^ this.maxClimbHeight.GetHashCode() << 2 ^ this.maxDropHeight.GetHashCode() >> 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is HeightNavigationCapabilities))
            {
                return false;
            }

            var rhs = (HeightNavigationCapabilities)other;
            return this.maxSlopeAngle.Equals(rhs.maxSlopeAngle) && this.maxClimbHeight.Equals(rhs.maxClimbHeight) && this.maxClimbHeight.Equals(rhs.maxClimbHeight);
        }
    }
}
