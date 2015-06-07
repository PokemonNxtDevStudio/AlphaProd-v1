/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    using Apex.Units;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// Base class for path request types.
    /// </summary>
    public abstract class PathRequestBase
    {
        /// <summary>
        /// Gets or sets where to move from.
        /// </summary>
        public Vector3 from
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets where to move to.
        /// </summary>
        public Vector3 to
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the grid on which the navigation starts. Do not set this explicitly, the engine handles that.
        /// </summary>
        public IGrid fromGrid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets to grid.
        /// </summary>
        /// <value>
        /// To grid.
        /// </value>
        public IGrid toGrid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the requester of this path, i.e. the entity that needs a path.
        /// </summary>
        public virtual INeedPath requester
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the requester's properties.
        /// </summary>
        /// <value>
        /// The requester properties.
        public IUnitProperties requesterProperties
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the type of this request.
        /// </summary>
        public RequestType type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path finder options.
        /// </summary>
        /// <value>
        /// The path finder options.
        /// </value>
        public IPathFinderOptions pathFinderOptions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the pending way points that are not covered by this request. Do not set this explicitly, the engine handles that.
        /// </summary>
        /// <value>
        /// The pending way points.
        /// </value>
        public Vector3[] pendingWaypoints
        {
            get;
            set;
        }
    }
}
