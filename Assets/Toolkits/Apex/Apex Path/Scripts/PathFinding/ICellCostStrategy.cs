/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Apex.WorldGeometry;

    public interface ICellCostStrategy
    {
        int GetCellCost(IGridCell cell, object unitProperties);

        //void ApplyCellCost(IGridCell cell, int cost);

        //void RemoveCellCost(IGridCell cell, int cost);
    }
}
