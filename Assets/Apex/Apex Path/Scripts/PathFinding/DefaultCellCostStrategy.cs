/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Apex.WorldGeometry;

    public class DefaultCellCostStrategy : ICellCostStrategy
    {
        public int GetCellCost(IGridCell cell, object unitProperties)
        {
            return cell.cost;
        }
    }
}
