/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using Apex.DataStructures;
    using UnityEngine;

    public interface IHeightLookup
    {
        bool hasHeights { get; }

        int heightCount { get; }

        bool Add(int x, int z, float height);

        bool TryGetHeight(int x, int z, out float height);

        void Cleanup();

        IHeightLookup PrepareForUpdate(MatrixBounds suggestedBounds, out MatrixBounds requiredBounds);

        void FinishUpdate(IHeightLookup updatedHeights);

        void Render(Vector3 position, float pointGranularity, Color drawColor);
    }
}
