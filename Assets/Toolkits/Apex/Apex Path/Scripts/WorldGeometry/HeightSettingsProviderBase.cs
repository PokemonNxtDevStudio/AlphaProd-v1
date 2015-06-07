/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using System.Collections;
    using Apex.DataStructures;
    using Apex.Services;
    using Apex.Steering;
    using Apex.Utilities;
    using UnityEngine;

    public abstract class HeightSettingsProviderBase : IHeightSettingsProvider
    {
        protected readonly CellMatrix _matrix;

        protected HeightSettingsProviderBase(CellMatrix matrix)
        {
            _matrix = matrix;
        }

        public abstract IEnumerator AssignHeightSettings(MatrixBounds bounds);

        protected Vector3[] GetPerpendicularOffsets(int dx, int dz)
        {
            Vector3 ppd;
            var obstacleSensitivityRange = Mathf.Min(_matrix.cellSize / 2f, _matrix.obstacleSensitivityRange);

            if (dx != 0 && dz != 0)
            {
                var offSet = obstacleSensitivityRange / Consts.SquareRootTwo;
                ppd = new Vector3(offSet * -dx, 0.0f, offSet * dz);
            }
            else
            {
                ppd = new Vector3(obstacleSensitivityRange * dz, 0.0f, obstacleSensitivityRange * dx);
            }

            return new Vector3[]
            {
                Vector3.zero,
                ppd,
                ppd * -1
            };
        }

        protected ISampleHeightsSimple GetHeightSampler()
        {
            if (GameServices.heightStrategy.heightMode == HeightSamplingMode.HeightMap)
            {
                return _matrix;
            }

            return GameServices.heightStrategy.heightSampler;
        }
    }
}
