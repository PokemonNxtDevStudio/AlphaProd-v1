/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    using System;
    using Apex.DataStructures;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// Represents a path along which a unit can move.
    /// </summary>
    public class Path : StackWithLookAhead<IPositioned>, ICloneable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        public Path()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public Path(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="path">The path points.</param>
        public Path(params Vector3[] path)
            : base(path.Length)
        {
            Ensure.ArgumentNotNull(path, "path");

            for (int i = path.Length - 1; i >= 0; i--)
            {
                Push(path[i].AsPositioned());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="path">The path points.</param>
        public Path(IIndexable<Vector3> path)
            : base(path.count)
        {
            Ensure.ArgumentNotNull(path, "path");

            for (int i = path.count - 1; i >= 0; i--)
            {
                Push(path[i].AsPositioned());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Path"/> class.
        /// </summary>
        /// <param name="path">The path points.</param>
        public Path(IIndexable<IPositioned> path)
            : base(path.count)
        {
            Ensure.ArgumentNotNull(path, "path");

            for (int i = path.count - 1; i >= 0; i--)
            {
                Push(path[i]);
            }
        }

        public float CalculateSquaredLength()
        {
            float length = 0f;

            var lastIdx = this.count - 2;
            for (int i = 0; i <= lastIdx; i++)
            {
                //Seeing as a portal is never the first nor the last node, this is valid
                var first = this[i];
                var second = this[i + 1];
                if (second is IPortalNode)
                {
                    second = this[++i + 1];
                }

                length += (first.position - second.position).sqrMagnitude;
            }

            return length;
        }

        public float CalculateLength()
        {
            float length = 0f;

            var lastIdx = this.count - 2;
            for (int i = 0; i <= lastIdx; i++)
            {
                //Seeing as a portal is never the first nor the last node, this is valid
                var first = this[i];
                var second = this[i + 1];
                if (second is IPortalNode)
                {
                    second = this[++i + 1];
                }

                length += (first.position - second.position).magnitude;
            }

            return length;
        }

        /// <summary>
        /// Updates the path.
        /// </summary>
        /// <param name="path">The path points.</param>
        public void Update(params Vector3[] path)
        {
            Clear();

            for (int i = path.Length - 1; i >= 0; i--)
            {
                Push(path[i].AsPositioned());
            }
        }

        /// <summary>
        /// Updates the path.
        /// </summary>
        /// <param name="path">The path points.</param>
        public void Update(IIndexable<Vector3> path)
        {
            Clear();

            for (int i = path.count - 1; i >= 0; i--)
            {
                Push(path[i].AsPositioned());
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Path Clone()
        {
            return new Path(this);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
