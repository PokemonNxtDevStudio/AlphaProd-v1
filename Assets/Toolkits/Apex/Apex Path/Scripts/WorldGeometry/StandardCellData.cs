/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using System;
    using UnityEngine;

    public sealed class StandardCellData : CellMatrixData
    {
        [HideInInspector]
        [SerializeField]
        private NeighbourPosition[] _heightBlockStatus;

        protected override void PrepareForInitialization(CellMatrix matrix)
        {
            _heightBlockStatus = new NeighbourPosition[matrix.columns * matrix.rows];
        }

        protected override void RecordCellData(Cell c, int cellIdx)
        {
            var cell = c as StandardCell;
            _heightBlockStatus[cellIdx] = cell.heightBlockedFrom;
        }

        protected override void InjectCellData(Cell c, int cellIdx)
        {
            var cell = c as StandardCell;
            cell.heightBlockedFrom = _heightBlockStatus[cellIdx];
        }
    }
}
