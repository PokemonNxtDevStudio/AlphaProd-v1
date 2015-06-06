/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using System.Collections;
    using Apex.DataStructures;

    public class FlatCellHeightSettingsProvider : HeightSettingsProviderBase
    {
        public FlatCellHeightSettingsProvider(CellMatrix matrix)
            : base(matrix)
        {           
        }

        public override IEnumerator AssignHeightSettings(MatrixBounds bounds)
        {
            yield break;
        }
    }
}
