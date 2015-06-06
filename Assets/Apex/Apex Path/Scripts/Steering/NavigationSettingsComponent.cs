/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering
{
    using Apex.Services;
    using Apex.Steering;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    [ExecuteInEditMode]
    [AddComponentMenu("Apex/Navigation/Steering/Navigation Settings")]
    [ApexComponent("Game World")]
    public partial class NavigationSettingsComponent : MonoBehaviour
    {
        [Label("Mode", "The way by which height sampling and height navigation is performed.")]
        public HeightSamplingMode heightSampling = HeightSamplingMode.HeightMap;

        [Label("Height Map Detail", "The detail level of the height map, Normal is recommended. High is more accurate but somewhat slower to generate.")]
        public HeightMapDetailLevel heightMapDetail = HeightMapDetailLevel.Normal;

        [MinCheck(0.05f, label = "Granularity", tooltip = "The distance between height samples.")]
        public float heightSamplingGranularity = 0.1f;

        [RangeX(0f, 89f, label = "Ledge Threshold", tooltip = "The max angle at which a piece of geometry is considered a ledge. A climb or drop is defined as movement from one ledge to another.")]
        public float ledgeThreshold = 10f;

        [Tooltip("Controls whether units define their own height navigation capabilities or use a global setting.")]
        public bool useGlobalHeightNavigationSettings = true;

        public HeightNavigationCapabilities unitsHeightNavigationCapability = new HeightNavigationCapabilities
        {
            maxClimbHeight = 0.5f,
            maxDropHeight = 1f,
            maxSlopeAngle = 30f
        };

        private void OnEnable()
        {
            Refresh();
        }

        /// <summary>
        /// For internal use, do not call this.
        /// </summary>
        public void Refresh() 
        {
            GameServices.heightStrategy = new HeightStrategy(
                this.heightSampling,
                this.heightSamplingGranularity,
                this.ledgeThreshold,
                this.useGlobalHeightNavigationSettings,
                this.unitsHeightNavigationCapability,
                this.heightMapDetail);

            RefreshPartial();
        }

        partial void RefreshPartial();
    }
}
