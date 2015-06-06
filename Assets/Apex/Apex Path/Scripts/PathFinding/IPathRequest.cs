/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    using Apex.DataStructures;
    using Apex.Units;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// Interface for path requests
    /// </summary>
    public interface IPathRequest
    {
        /// <summary>
        /// Gets or sets where to move from.
        /// </summary>
        Vector3 from { get; set; }

        /// <summary>
        /// Gets or sets where to move to.
        /// </summary>
        Vector3 to { get; set; }

        /// <summary>
        /// Gets or sets the grid on which the navigation starts. Do not set this explicitly, the engine handles that.
        /// </summary>
        IGrid fromGrid { get; set; }

        /// <summary>
        /// Gets or sets the destination grid. Do not set this explicitly, the engine handles that.
        /// </summary>
        IGrid toGrid { get; set; }

        /// <summary>
        /// Gets or sets the requester of this path, i.e. the entity that needs a path.
        /// </summary>
        INeedPath requester { get; set; }

        /// <summary>
        /// Gets or sets the requester's properties.
        /// </summary>
        /// <value>
        /// The requester properties.
        /// </value>
        IUnitProperties requesterProperties { get; set; }

        /// <summary>
        /// Gets or sets the options used during the path finding process.
        /// </summary>
        /// <value>
        /// The path finder options.
        /// </value>
        IPathFinderOptions pathFinderOptions { get; set; }
        
        /// <summary>
        /// Gets or sets the type of this request.
        /// </summary>
        RequestType type { get; set; }

        /// <summary>
        /// Gets or sets the pending waypoints that are not covered by this request. Do not set this explicitly, the engine handles that.
        /// </summary>
        /// <value>
        /// The pending waypoints.
        /// </value>
        Vector3[] pendingWaypoints { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        bool isValid { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has decayed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has decayed; otherwise, <c>false</c>.
        /// </value>
        bool hasDecayed { get; }

        /// <summary>
        /// Completes this request
        /// </summary>
        /// <param name="result">The result.</param>
        void Complete(PathResult result);
    }
}
