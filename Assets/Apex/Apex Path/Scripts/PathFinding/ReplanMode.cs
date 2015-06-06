/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    public enum ReplanMode
    {
        /// <summary>
        /// Replanning is done a a set interval
        /// </summary>
        AtInterval,

        /// <summary>
        /// Replanning is done when changes occur in the units immediate surroundings. Immediate surroundings are defined by the grid's <see cref="IGrid.gridSections"/>
        /// </summary>
        Dynamic,

        /// <summary>
        /// No replanning
        /// </summary>
        NoReplan
    }
}
