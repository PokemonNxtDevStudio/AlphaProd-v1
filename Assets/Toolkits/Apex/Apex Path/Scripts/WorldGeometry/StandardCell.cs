/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using Apex.Units;
    using UnityEngine;

    public sealed class StandardCell : Cell
    {
        public static readonly ICellFactory factory = new StandardCellFacory();

        private NeighbourPosition _heightBlockedFrom;

        public StandardCell(CellMatrix parent, Vector3 position, int matrixPosX, int matrixPosZ, bool blocked)
            : base(parent, position, matrixPosX, matrixPosZ, blocked)
        {
        }

        /// <summary>
        /// Gets or sets the mask of height blocked neighbours, i.e. neighbours that are not walkable from this cell to to the slope angle between them being too big.
        /// </summary>
        /// <value>
        /// The height blocked neighbours mask.
        /// </value>
        public NeighbourPosition heightBlockedFrom
        {
            get { return _heightBlockedFrom; }
            set { _heightBlockedFrom = value; }
        }

        internal NeighbourPosition heightIntializedFrom
        {
            get;
            set;
        }

        /// <summary>
        /// Determines whether the cell is walkable from all directions.
        /// </summary>
        /// <param name="mask">The mask.</param>
        /// <returns><c>true</c> if the cell is walkable, otherwise <c>false</c></returns>
        public override bool isWalkableFromAllDirections(IUnitProperties unitProps)
        {
            if (!isWalkable(unitProps.attributes))
            {
                return false;
            }

            return (_heightBlockedFrom == NeighbourPosition.None);
        }

        /// <summary>
        /// Determines whether the cell is walkable from the specified neighbour.
        /// </summary>
        /// <param name="neighbour">The neighbour.</param>
        /// <param name="mask">The attribute mask used to determine walk-ability.</param>
        /// <returns><c>true</c> if the cell is walkable, otherwise <c>false</c></returns>
        public override bool isWalkableFrom(IGridCell neighbour, IUnitProperties unitProps)
        {
            if (!isWalkable(unitProps.attributes))
            {
                return false;
            }

            var pos = neighbour.GetRelativePositionTo(this);

            return (_heightBlockedFrom & pos) == 0;
        }

        private class StandardCellFacory : ICellFactory
        {
            public Cell Create(CellMatrix parent, Vector3 position, int matrixPosX, int matrixPosZ, bool blocked)
            {
                return new StandardCell(parent, position, matrixPosX, matrixPosZ, blocked);
            }
        }
    }
}
