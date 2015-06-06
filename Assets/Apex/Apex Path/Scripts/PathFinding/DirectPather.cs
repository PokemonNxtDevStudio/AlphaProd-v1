/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.PathFinding
{
    using System.Collections.Generic;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// Handles off grid navigation and other pre path finding tasks.
    /// </summary>
    public class DirectPather : IDirectPather
    {
        /// <summary>
        /// Resolves the direct path or delegates the request on to path finding.
        /// </summary>
        /// <param name="req">The request.</param>
        /// <returns>A path request to use in path finding or null if the path was resolved.</returns>
        public IPathRequest ResolveDirectPath(IPathRequest req)
        {
            var to = req.to;
            var from = req.from;
            var toGrid = req.toGrid;
            var fromGrid = req.fromGrid;
            var unitProps = req.requesterProperties;

            if (fromGrid == null)
            {
                fromGrid = req.fromGrid = GridManager.instance.GetGrid(from);
            }

            if (toGrid == null)
            {
                toGrid = req.toGrid = GridManager.instance.GetGrid(to);

                //Just treat the to grid as the from grid in this case so the destination can be closest point on from grid
                if (toGrid == null)
                {
                    toGrid = req.toGrid = fromGrid;
                }
            }

            //If no grids were resolved for this request it means the request involves two points outside the grid(s) that do not cross any grid(s), so we can move directly between them
            if (fromGrid == null && toGrid == null)
            {
                req.Complete(new DirectPathResult(from, to, req));
                return null;
            }
            else if (fromGrid == null || toGrid == null)
            {
                req.Complete(DirectPathResult.Fail(PathingStatus.NoRouteExists, req));
                return null;
            }

            //Is the start node on a grid
            Cell fromCell = fromGrid.GetCell(from, true);

            //If the from cell is blocked, we have to get out to a free cell first
            if (!fromCell.isWalkable(unitProps.attributes))
            {
                //Check for a free cell to escape to before resuming the planned path. If no free cell is found we are stuck.
                //The unit may still get stuck even if a free cell is found, in case there is a physically impassable blockade
                var escapeCell = fromGrid.GetNearestWalkableCell(from, from, true, req.pathFinderOptions.maxEscapeCellDistanceIfOriginBlocked, unitProps);
                if (escapeCell == null)
                {
                    req.Complete(DirectPathResult.Fail(PathingStatus.NoRouteExists, req));
                    return null;
                }

                //Move to the free cell and then on with the planned path
                req.Complete(DirectPathResult.CreateWithPath(from, escapeCell.position, to, req));
                return null;
            }

            return req;
        }
    }
}
