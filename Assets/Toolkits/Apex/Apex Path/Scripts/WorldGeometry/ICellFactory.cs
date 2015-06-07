/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using UnityEngine;

    public interface ICellFactory
    {
        Cell Create(CellMatrix parent, Vector3 position, int matrixPosX, int matrixPosZ, bool blocked);
    }
}
