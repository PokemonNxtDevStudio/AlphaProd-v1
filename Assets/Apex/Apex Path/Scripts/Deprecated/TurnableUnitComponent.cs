/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering
{
    using System.Collections.Generic;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// Turns a unit to face a direction. Where to face is governed by attached <see cref="IProvideFacingOrientation"/> components.
    /// </summary>
    [AddComponentMenu("")]
    public class TurnableUnitComponent : MonoBehaviour
    {
        public Axis ignoreAxis = Axis.Y;

        private void Awake()
        {
            Debug.LogWarning("The TurnableUnitComponent component is no longer valid and should be removed. Use the 'Upgrade Scene' tool in the Tools -> Apex menu to fix up the scene.");
        }
    }
}
