/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering
{
    using Apex.Units;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    public sealed class RaycastSampler : ISampleHeights
    {
        private const float _lookAhead = 0.01f;
        private Vector3[] _samplePoints = new Vector3[5];

        public HeightSamplingMode samplingStrategy
        {
            get { return HeightSamplingMode.Raycast; }
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

                if (!matrix.bounds.Contains(position))
                {
                    height = Consts.InfiniteDrop;
                    return false;
                }
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
            var unitRadius = unit.radius;
            var perpOffset = new Vector3(-velocityNormal.z * unitRadius, 0f, velocityNormal.x * unitRadius);

            var center = unit.position;
            _samplePoints[0] = center - (velocityNormal * unitRadius) - perpOffset;
            _samplePoints[1] = center - (velocityNormal * unitRadius) + perpOffset;
            _samplePoints[2] = center + (velocityNormal * (unitRadius + _lookAhead)) - perpOffset;
            _samplePoints[3] = center + (velocityNormal * (unitRadius + _lookAhead)) + perpOffset;
            _samplePoints[4] = center;

            float maxClimb = unit.heightNavigationCapability.maxClimbHeight;

            float rayStart = matrix != null ? matrix.origin.y + matrix.upperBoundary : center.y + maxClimb;
            float sampledHeight = Consts.InfiniteDrop;
            float maxHeight = Consts.InfiniteDrop;

            RaycastHit hit;
            for (int i = 0; i < 5; i++)
            {
                _samplePoints[i].y = rayStart;

                if (Physics.Raycast(_samplePoints[i], Vector3.down, out hit, Mathf.Infinity, Layers.terrain))
                {
                    sampledHeight = hit.point.y;
                    if (sampledHeight > maxHeight)
                    {
                        maxHeight = sampledHeight;
                    }
                }
            }

            var diff = maxHeight - unit.basePosition.y;

            //Since sampledHeight will be the height at the center we can just use that directly here in case the max height is too high.
            return diff <= maxClimb ? maxHeight + unit.baseToPositionOffset : sampledHeight + unit.baseToPositionOffset;
        }
    }
}
