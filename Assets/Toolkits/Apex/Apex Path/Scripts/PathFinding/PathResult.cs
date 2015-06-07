/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    using Apex.DataStructures;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// The result of a <see cref="IPathRequest"/>
    /// </summary>
    public class PathResult
    {
        private static readonly Path _pathEmpty = new Path();

        private Vector3[] _pendingWaypoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathResult"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="path">The path.</param>
        /// <param name="pathCost">The cost of the path, i.e. its length and combined cost of the cells involved</param>
        /// <param name="originalRequest">The original request.</param>
        public PathResult(PathingStatus status, Path path, int pathCost, IPathRequest originalRequest)
            : this()
        {
            this.status = status;
            this.path = path ?? _pathEmpty;
            this.pathCost = pathCost;
            this.originalRequest = originalRequest;
            this.pendingWaypoints = originalRequest.pendingWaypoints;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathResult"/> class.
        /// </summary>
        protected PathResult()
        {
            this.pendingWaypoints = Consts.EmptyVectorArray;
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
            set;
        }

        /// <summary>
        /// Gets or sets the error information. Consult this if the path request fails for unknown reasons.
        /// </summary>
        /// <value>
        /// The error information.
        /// </value>
        public string errorInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public Path path
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path cost. The cost is a number that represents the length of the path combined with the cost of the nodes along it.
        /// </summary>
        /// <value>
        /// The path cost.
        /// </value>
        public int pathCost
        {
            get;
            set;
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
            get
            {
                return _pendingWaypoints;
            }

            set
            {
                if (value == null)
                {
                    _pendingWaypoints = Consts.EmptyVectorArray;
                }
                else
                {
                    _pendingWaypoints = value;
                }
            }
        }
    }
}
