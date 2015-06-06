/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering
{
    using Apex.Units;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    public sealed class SpherecastSampler : ISampleHeights
    {
        private const float _lookAhead = 0.01f;

        public HeightSamplingMode samplingStrategy
        {
            get { return HeightSamplingMode.Spherecast; }
        }

        public float SampleHeight(Vector3 position)
        {
            var g = GridManager.instance.GetGrid(position);
            var matrix = g != null ? g.cellMatrix : null;

            return SampleHeight(position, matrix);
        }

        public float SampleHeight(Vector3 position, CellMatrix matrix)
        {
            float plotRange;

            if (matrix != null)
            {
                position.y = matrix.origin.y + matrix.upperBoundary;
                plotRange = matrix.upperBoundary + matrix.lowerBoundary;
            }
            else
            {
                plotRange = Mathf.Infinity;
            }

            RaycastHit hit;
            if (Physics.Raycast(position, Vector3.down, out hit, plotRange, Layers.terrain))
            {
                return hit.point.y;
            }

            return Consts.InfiniteDrop;
        }

        public bool TrySampleHeight(Vector3 position, out float height)
        {
            var g = GridManager.instance.GetGrid(position);
            var matrix = g != null ? g.cellMatrix : null;

            return TrySampleHeight(position, matrix, out height);
        }

        public bool TrySampleHeight(Vector3 position, CellMatrix matrix, out float height)
        {
            float plotRange;

            if (matrix != null)
            {
                position.y = matrix.origin.y + matrix.upperBoundary;
                plotRange = matrix.upperBoundary + matrix.lowerBoundary;
            }
            else
            {
                plotRange = Mathf.Infinity;
            }

            RaycastHit hit;
            if (Physics.Raycast(position, Vector3.down, out hit, plotRange, Layers.terrain))
            {
                height = hit.point.y;
                return true;
            }

            height = Consts.InfiniteDrop;
            return false;
        }

        public float GetProposedHeight(IUnitFacade unit, CellMatrix matrix, Vector3 velocityNormal)
        {
            var radius = unit.radius + _lookAhead;
            var start = unit.basePosition;
            var baseHeight = start.y;
            var maxClimb = unit.heightNavigationCapability.maxClimbHeight;

            //Put the start at the unit's head or max climb above its center
            start.y = matrix != null ? matrix.origin.y + matrix.upperBoundary : start.y + Mathf.Max(unit.height, radius + maxClimb);

            RaycastHit hit;
            if (!Physics.SphereCast(start, radius, Vector3.down, out hit, Mathf.Infinity, Layers.terrain))
            {
                return Consts.InfiniteDrop;
            }
            
            var sampledHeight = hit.point.y;

            var diff = sampledHeight - baseHeight;

            if (diff <= maxClimb)
            {
                return sampledHeight + unit.baseToPositionOffset;
            }

            return SampleHeight(unit.position, matrix) + unit.baseToPositionOffset;
        }
    }
}
