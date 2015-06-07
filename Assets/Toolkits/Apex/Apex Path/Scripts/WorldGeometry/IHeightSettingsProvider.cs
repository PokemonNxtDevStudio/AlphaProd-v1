/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using System.Collections;
    using Apex.DataStructures;

    public interface IHeightSettingsProvider
    {
        IEnumerator AssignHeightSettings(MatrixBounds bounds);
    }
}
