/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering
{
    using System;
    using Apex.Units;
    using Apex.WorldGeometry;
    using UnityEngine;

    public sealed class HeightMapSampler : ISampleHeights
    {
        private float[] _heightCandidates = new float[5];

        public HeightSamplingMode samplingStrategy
        {
            get { return HeightSamplingMode.HeightMap; }
        }

        public float SampleHeight(Vector3 position)
        {
            return SampleHeight(position, null);
        }

        public float SampleHeight(Vector3 position, CellMatrix matrix)
        {
            var heightMap = HeightMapManager.instance.GetHeightMap(position);

            return heightMap.SampleHeight(position);
        }

        public bool TrySampleHeight(Vector3 position, out float height)
        {
            return TrySampleHeight(position, null, out height);
        }

        public bool TrySampleHeight(Vector3 position, CellMatrix matrix, out float height)
        {
            var heightMap = HeightMapManager.instance.GetHeightMap(position);

            return heightMap.TrySampleHeight(position, out height);
        }

        public float GetProposedHeight(IUnitFacade unit, CellMatrix matrix, Vector3 velocityNormal)
        {
            var heightMap = HeightMapManager.instance.GetHeightMap(unit.position);

            var lookAhead = heightMap.granularity;
            var unitRadius = unit.radius;
            var offsetFactor = (unitRadius + lookAhead);
            var perpOffset = new Vector3(-velocityNormal.z * offsetFactor, 0f, velocityNormal.x * offsetFactor);

            var center = unit.position;
            var backLeft = center - (velocityNormal * unitRadius) - perpOffset;
            var backRight = center - (velocityNormal * unitRadius) + perpOffset;
            var frontLeft = center + (velocityNormal * (unitRadius + lookAhead)) - perpOffset;
            var frontRight = center + (velocityNormal * (unitRadius + lookAhead)) + perpOffset;

            _heightCandidates[0] = heightMap.SampleHeight(backLeft);
            _heightCandidates[1] = heightMap.SampleHeight(backRight);
            _heightCandidates[2] = heightMap.SampleHeight(frontLeft);
            _heightCandidates[3] = heightMap.SampleHeight(frontRight);
            _heightCandidates[4] = heightMap.SampleHeight(center);

            var maxHeight = Mathf.Max(_heightCandidates);

            //Ground pos cannot be less than actual ground level. The reason it can be higher is if the unit is in free fall, in which case gravity will handle the rest.
            var groundPos = Mathf.Max(center.y - unit.baseToPositionOffset, _heightCandidates[4]);

            var diff = maxHeight - groundPos;
            return diff <= unit.heightNavigationCapability.maxClimbHeight ? maxHeight + unit.baseToPositionOffset : _heightCandidates[4] + unit.baseToPositionOffset;
        }
    }
}
