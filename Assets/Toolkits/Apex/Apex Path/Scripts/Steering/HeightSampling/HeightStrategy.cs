/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Steering
{
    using Apex.WorldGeometry;

    public sealed class HeightStrategy
    {
        private readonly ISampleHeights _sampler;

        public HeightStrategy(HeightSamplingMode mode, float sampleGranularity, float ledgeThreshold, bool useGlobalHeightNavigationSettings, HeightNavigationCapabilities unitsHeightNavigationCapability, HeightMapDetailLevel heightMapDetail)
        {
            switch (mode)
            {
                case HeightSamplingMode.HeightMap:
                {
                    _sampler = new HeightMapSampler();
                    break;
                }

                case HeightSamplingMode.Raycast:
                {
                    _sampler = new RaycastSampler();
                    break;
                }

                case HeightSamplingMode.Spherecast:
                {
                    _sampler = new SpherecastSampler();
                    break;
                }

                default:
                case HeightSamplingMode.NoHeightSampling:
                {
                    _sampler = new NullSampler();
                    break;
                }
            }

            this.heightMode = mode;
            this.sampleGranularity = sampleGranularity;
            this.ledgeThreshold = ledgeThreshold;
            this.useGlobalHeightNavigationSettings = useGlobalHeightNavigationSettings;
            this.unitsHeightNavigationCapability = unitsHeightNavigationCapability;
            this.heightMapDetail = heightMapDetail;
        }

        public ISampleHeights heightSampler
        {
            get { return _sampler; }
        }

        public HeightSamplingMode heightMode
        {
            get;
            private set;
        }

        public HeightMapDetailLevel heightMapDetail
        {
            get;
            private set;
        }

        public float sampleGranularity
        {
            get;
            private set;
        }

        public float ledgeThreshold
        {
            get;
            private set;
        }

        public bool useGlobalHeightNavigationSettings
        {
            get;
            private set;
        }

        public HeightNavigationCapabilities unitsHeightNavigationCapability
        {
            get;
            private set;
        }
    }
}
