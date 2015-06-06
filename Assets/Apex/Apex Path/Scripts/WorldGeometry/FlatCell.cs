/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using Apex.Units;
    using UnityEngine;

    public class FlatCell : Cell
    {
        public static readonly ICellFactory factory = new FlatCellFacory();

        public FlatCell(CellMatrix parent, Vector3 position, int matrixPosX, int matrixPosZ, bool blocked)
            : base(parent, position, matrixPosX, matrixPosZ, blocked)
        {
        }

        /// <summary>
        /// Determines whether the cell is walkable from all directions.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <returns><c>true</c> if the cell is walkable, otherwise <c>false</c></returns>
        public override bool isWalkableFromAllDirections(IUnitProperties unitProps)
        {
            return isWalkable(unitProps.attributes);
        }

        /// <summary>
        /// Determines whether the cell is walkable from the specified neighbour.
        /// </summary>
        /// <param name="neighbour">The neighbour.</param>
        /// <param name="mask">The attribute mask used to determine walk-ability.</param>
        /// <returns><c>true</c> if the cell is walkable, otherwise <c>false</c></returns>
        public override bool isWalkableFrom(IGridCell neighbour, IUnitProperties unitProps)
        {
            return isWalkable(unitProps.attributes);
        }

        private class FlatCellFacory : ICellFactory
        {
            public Cell Create(CellMatrix parent, Vector3 position, int matrixPosX, int matrixPosZ, bool blocked)
            {
                return new FlatCell(parent, position, matrixPosX, matrixPosZ, blocked);
            }
        }
    }
}
