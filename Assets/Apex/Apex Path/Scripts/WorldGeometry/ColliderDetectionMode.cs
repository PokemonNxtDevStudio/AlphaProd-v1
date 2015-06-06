/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    public enum ColliderDetectionMode
    {
        /// <summary>
        /// Detects colliders using a capsule check
        /// </summary>
        CheckCapsule,

        /// <summary>
        /// Detects colliders using a sphere cast
        /// </summary>
        SphereCast,

        /// <summary>
        /// This mode will use capsules to detect obstacles, and use spheres for terrain detection.
        /// </summary>
        Mixed
    }
}
