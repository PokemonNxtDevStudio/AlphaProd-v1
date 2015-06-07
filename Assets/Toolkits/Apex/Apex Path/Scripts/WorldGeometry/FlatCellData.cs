/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using System;
    using UnityEngine;

    public sealed class FlatCellData : CellMatrixData
    {
        protected override void PrepareForInitialization(CellMatrix matrix)
        {
            /* NOOP */
        }

        protected override void RecordCellData(Cell c, int cellIdx)
        {
            /* NOOP */
        }

        protected override void InjectCellData(Cell c, int cellIdx)
        {
            /* NOOP */
        }
    }
}
