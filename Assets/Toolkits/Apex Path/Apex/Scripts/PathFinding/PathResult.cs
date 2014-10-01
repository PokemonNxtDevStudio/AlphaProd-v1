/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    using Apex.DataStructures;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// The result of a <see cref="IPathRequest"/>
    /// </summary>
    public class PathResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PathResult"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="path">The path.</param>
        /// <param name="originalRequest">The original request.</param>
        public PathResult(PathingStatus status, StackWithLookAhead<IPositioned> path, IPathRequest originalRequest)
        {
            this.status = status;
            this.path = path;
            this.originalRequest = originalRequest;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathResult"/> class.
        /// </summary>
        protected PathResult()
        {
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public PathingStatus status
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public StackWithLookAhead<IPositioned> path
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the original request.
        /// </summary>
        /// <value>
        /// The original request.
        /// </value>
        public IPathRequest originalRequest
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the pending way points that are not covered by the path.
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
