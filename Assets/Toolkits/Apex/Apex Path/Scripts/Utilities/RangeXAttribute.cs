/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Utilities
{
    using System;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class RangeXAttribute : PropertyAttribute
    {
        public readonly float min;
        public readonly float max;

        public RangeXAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// Gets or sets the tooltip.
        /// </summary>
        public string tooltip { get; set; }
    }
}
