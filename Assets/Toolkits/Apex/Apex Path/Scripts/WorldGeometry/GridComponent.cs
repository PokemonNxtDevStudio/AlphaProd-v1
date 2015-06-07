/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using System;
    using System.Collections.Generic;
    using Apex.DataStructures;
    using Apex.Messages;
    using Apex.Services;
    using Apex.Utilities;
    using UnityEngine;

    /// <summary>
    /// Component for configuration of a <see cref="Grid"/>
    /// </summary>
    [ExecuteInEditMode]
    [AddComponentMenu("Apex/Navigation/Basics/Grid")]
    [ApexComponent("Game World")]
    public partial class GridComponent : MonoBehaviour
    {
        /// <summary>
        /// Size along the x-axis.
        /// </summary>
        [MinCheck(1)]
        public int sizeX = 10;

        /// <summary>
        /// Size along the z-axis.
        /// </summary>
        [MinCheck(1)]
        public int sizeZ = 10;

        /// <summary>
        /// The sub sections across the x-axis
        /// </summary>
        [MinCheck(1)]
        public int subSectionsX = 2;

        /// <summary>
        /// The sub sections across the z-axis
        /// </summary>
        [MinCheck(1)]
        public int subSectionsZ = 2;

        /// <summary>
        /// The sub sections cell overlap
        /// </summary>
        [MinCheck(0)]
        public int subSectionsCellOverlap = 2;

        /// <summary>
        /// The cell size
        /// </summary>
        [MinCheck(0.1f)]
        public float cellSize = 2;

        /// <summary>
        /// Whether or not to generate a height map to enable units to follow a terrain of differing heights.
        /// </summary>
        public bool generateHeightmap = true;

        /// <summary>
        /// Gets the type of the height lookup. Dictionaries are fast but memory hungry. Quad Tree stats depend on height density.
        /// </summary>
        public HeightLookupType heightLookupType = HeightLookupType.QuadTree;

        /// <summary>
        /// Gets the height lookup maximum depth. Only applicable to Quad Trees.
        /// </summary>
        [MinCheck(1)]
        public int heightLookupMaxDepth = 5;

        /// <summary>
        /// The distance below the grid's origin that defines the lower boundary of the grid
        /// </summary>
        [MinCheck(0f)]
        public float lowerBoundary = 1.0f;

        /// <summary>
        /// The distance above the grid's origin that defines the lower boundary of the grid
        /// </summary>
        [MinCheck(0f)]
        public float upperBoundary = 10.0f;

        /// <summary>
        /// The obstacle sensitivity range, meaning any obstacle within this range of the cell center will cause the cell to be blocked.
        /// </summary>
        [MinCheck(0f)]
        public float obstacleSensitivityRange = 0.5f;

        /// <summary>
        /// The obstacle and ground detection mode used when determining the terrain and obstacles of the grid.
        /// </summary>
        public ColliderDetectionMode obstacleAndGroundDetection = ColliderDetectionMode.Mixed;

        /// <summary>
        /// The baked grid data
        /// </summary>
        [SerializeField, HideInInspector]
        public CellMatrixData bakedData;

        //In order to prevent causing a breaking change with the introduction of the new editor,
        //the properties that has always been exposed as properties remain that way
        [SerializeField]
        private Vector3 _origin;

        [SerializeField]
        private Vector3 _originOffset;

        [SerializeField]
        private string _friendlyName;

        [SerializeField]
        private bool _linkOriginToTransform = true;

        [SerializeField]
        private bool _storeBakedDataAsAsset;

        [SerializeField]
        private bool _automaticInitialization = true;

        private Bounds _bounds;

        /// <summary>
        /// Gets or sets the friendly name of the grid. Used in messages and such.
        /// </summary>
        public string friendlyName
        {
            get { return _friendlyName; }
            set { _friendlyName = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to link origin to the transform.
        /// </summary>
        public bool linkOriginToTransform
        {
            get { return _linkOriginToTransform; }
            set { _linkOriginToTransform = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to store baked data as an asset. This is an editor setting and should not be manipulated manually.
        /// </summary>
        public bool storeBakedDataAsAsset
        {
            get { return _storeBakedDataAsAsset; }
            set { _storeBakedDataAsAsset = value; }
        }

        /// <summary>
        /// Controls whether the grid is automatically initialized when enabled. If set to <c>false</c> the grid must be manually initialized by calling <see cref="Initialize(int, Action&lt;GridComponent&gt;)"/>
        /// </summary>
        public bool automaticInitialization
        {
            get { return _automaticInitialization; }
            set { _automaticInitialization = value; }
        }

        /// <summary>
        /// The origin, i.e. center, of the grid
        /// </summary>
        public Vector3 origin
        {
            get
            {
                if (_linkOriginToTransform)
                {
                    return this.transform.position + _originOffset;
                }

                return _origin;
            }

            set
            {
                if (!_linkOriginToTransform)
                {
                    _origin = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the origin offset. This will offset the origin if <see cref="linkOriginToTransform"/> is true.
        /// </summary>
        /// <value>
        /// The origin offset.
        /// </value>
        public Vector3 originOffset
        {
            get { return _originOffset; }
            set { _originOffset = value; }
        }

        /// <summary>
        /// Gets the grid.
        /// </summary>
        /// <value>
        /// The grid.
        /// </value>
        public IGrid grid
        {
            get;
            private set;
        }

        internal Bounds bounds
        {
            get
            {
                //While the grid has yet to be initialized we have to recalculate the bounds since the grid component may be moved around before being initialized
                if (this.grid == null)
                {
                    CalculateBounds();
                }

                return _bounds;
            }
        }

        /// <summary>
        /// Creates a runtime Grid instance. The grid is disabled and will need to be initialized.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="cfg">The configuration.</param>
        /// <returns>The grid instance.</returns>
        public static GridComponent Create(GameObject host, GridConfig cfg)
        {
            Ensure.ArgumentNotNull(host, "host");
            Ensure.ArgumentNotNull(cfg, "cfg");

            var g = host.AddComponent<RuntimeGridComponent>();
            g.Configure(cfg);

            return g;
        }

        /// <summary>
        /// Creates a runtime Grid instance and initializes it.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="cfg">The configuration.</param>
        /// <param name="maxMillisecondsUsedPerFrame">The maximum milliseconds used per frame while initializing.</param>
        /// <param name="callback">Callback that will be called once initialization is complete. The callback will receive a reference to the created component for convenience.</param>
        /// <returns>The grid instance. Note initialization is not complete at the time of return.</returns>
        public static GridComponent CreateAndInitialize(GameObject host, GridConfig cfg, int maxMillisecondsUsedPerFrame, Action<GridComponent> callback)
        {
            Ensure.ArgumentNotNull(host, "host");
            Ensure.ArgumentNotNull(cfg, "cfg");

            var g = host.AddComponent<RuntimeGridComponent>();
            g.ConfigureAndInitialize(cfg, maxMillisecondsUsedPerFrame, callback);

            return g;
        }

        /// <summary>
        /// Gets a builder for initializing the grid. This is a framework method, and should not be called.
        /// </summary>
        /// <returns>The builder configured for this grid.</returns>
        public GridBuilder GetBuilder()
        {
            return new GridBuilder
            {
                origin = this.origin,
                sizeX = this.sizeX,
                sizeZ = this.sizeZ,
                cellSize = this.cellSize,
                obstacleSensitivityRange = this.obstacleSensitivityRange,
                generateHeightmap = this.generateHeightmap,
                heightLookupType = this.heightLookupType,
                heightLookupMaxDepth = this.heightLookupMaxDepth,
                lowerBoundary = this.lowerBoundary,
                upperBoundary = this.upperBoundary,
                obstacleAndGroundDetection = this.obstacleAndGroundDetection,
                subSectionsX = this.subSectionsX,
                subSectionsZ = this.subSectionsZ,
                subSectionsCellOverlap = this.subSectionsCellOverlap,
                bounds = this.bounds
            };
        }

        /// <summary>
        /// Initializes the grid. This is only intended for use if <see cref="automaticInitialization"/> is set to <c>false</c>.
        /// The grid will be initialized over a number of frames, as to smooth out the initialization.
        /// </summary>
        /// <param name="maxMillisecondsUsedPerFrame">The maximum milliseconds used per frame while initializing.</param>
        /// <param name="callback">Callback that will be called once initialization is complete. The callback will receive a reference to this component for convenience.</param>
        public void Initialize(int maxMillisecondsUsedPerFrame, Action<GridComponent> callback)
        {
            if (this.grid != null)
            {
                return;
            }

            var builder = GetBuilder();

            //This is the callback used for posting the final status message
            Action msgCallback = () =>
            {
                if (callback != null)
                {
                    callback(this);
                }
            };

            //This is the callback for the initialization routine
            Action<IGrid> cb = (g) =>
            {
                this.grid = g;
                RegisterWithManagers();

                this.enabled = true;

                var msg = new GridStatusMessage
                {
                    gridBounds = g.bounds,
                    status = GridStatusMessage.StatusCode.InitializationComplete
                };

                GameServices.messageBus.PostBalanced(msg, maxMillisecondsUsedPerFrame, msgCallback);
            };

            if (this.bakedData != null)
            {
                builder.Create(this.bakedData, maxMillisecondsUsedPerFrame, cb);
            }
            else
            {
                builder.Create(maxMillisecondsUsedPerFrame, cb);
            }
        }

        /// <summary>
        /// Disables the grid and releases all memory. If <see cref="automaticInitialization"/> is <c>true</c>, the grid will be reinitialized if re-enabled, otherwise <see cref="Initialize(int, Action&lt;GridComponent&gt;)"/> must be called to re-enable and re-initialize the grid.
        /// <param name="maxMillisecondsUsedPerFrame">The maximum milliseconds used per frame while disabling</param>
        /// </summary>
        public void Disable(int maxMillisecondsUsedPerFrame)
        {
            if (this.grid == null)
            {
                return;
            }

            this.enabled = false;
            this.grid = null;

            var msg = new GridStatusMessage
            {
                gridBounds = this.bounds,
                status = GridStatusMessage.StatusCode.DisableComplete
            };

            GameServices.messageBus.PostBalanced(msg, maxMillisecondsUsedPerFrame);
        }

        /// <summary>
        /// Editor function, do not use this.
        /// </summary>
        public void ResetGrid()
        {
            this.grid = null;
        }

        /// <summary>
        /// Determines whether the bounds of the grid contains the specified position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns>
        ///   <c>true</c> if the position is contained; otherwise false.
        /// </returns>
        public bool Contains(Vector3 pos)
        {
            return this.bounds.Contains(pos);
        }

        internal void EnsureGrid()
        {
            if (this.grid == null)
            {
                var builder = GetBuilder();

                if (this.bakedData != null)
                {
                    this.grid = builder.Create(this.bakedData);

                    if (!this.storeBakedDataAsAsset)
                    {
                        //No need to hang on to this anymore.
                        this.bakedData = null;
                    }
                }
                else
                {
                    this.grid = builder.Create();
                }
            }
        }

        internal Bounds EnsureForEditor(bool refresh, Bounds area)
        {
            if (!Application.isPlaying)
            {
                //We do it this way rather than incorporate it below, since recalc of bounds requires grid to be null
                if (refresh && this.transform.hasChanged)
                {
                    this.transform.hasChanged = false;
                    this.grid = null;
                }

                if (this.grid == null)
                {
                    var builder = GetBuilder();

                    this.grid = builder.CreateForEditor(this.bakedData);
                }
            }

            var gb = this.bounds;
            gb.Expand(-this.cellSize);

            var bl = new Vector3(Mathf.Max(area.min.x, gb.min.x), 0f, Mathf.Max(area.min.z, gb.min.z));
            var tr = new Vector3(Mathf.Min(area.max.x, gb.max.x), 0f, Mathf.Min(area.max.z, gb.max.z));
            area.SetMinMax(bl, tr);

            if (refresh && !Application.isPlaying)
            {
                this.grid.cellMatrix.UpdateForEditor(area, this.bakedData);
            }

            return area;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected void Init()
        {
            GridManager.instance.RegisterGridComponent(this);
        }

        /// <summary>
        /// Called on Awake
        /// </summary>
        protected virtual void Awake()
        {
            Init();
        }

        /// <summary>
        /// Called when enabled.
        /// </summary>
        protected virtual void OnEnable()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                Init();
                return;
            }

            if (this.automaticInitialization)
            {
                EnsureGrid();
                RegisterWithManagers();
            }
        }

        private void OnDisable()
        {
            if (this.grid != null && Application.isPlaying)
            {
                GridManager.instance.UnregisterGrid(this.grid);
                HeightMapManager.instance.UnregisterMap(this.grid.cellMatrix);
            }
        }

        private void OnDestroy()
        {
            GridManager.instance.UnregisterGridComponent(this);

            if (Application.isPlaying)
            {
                Disable(5);
            }
        }

        private void OnValidate()
        {
            //This is only called in the editor and is done to ensure a refresh of the grid when values change.
            if (!Application.isPlaying)
            {
                this.grid = null;
            }
        }

        private void CalculateBounds()
        {
            var yoffset = (this.upperBoundary - this.lowerBoundary) * 0.5f;
            var boundsCenter = this.origin;
            boundsCenter.y += yoffset;

            _bounds = new Bounds(boundsCenter, new Vector3(this.sizeX * this.cellSize, this.upperBoundary + this.lowerBoundary, this.sizeZ * this.cellSize));
        }

        private void RegisterWithManagers()
        {
            GridManager.instance.RegisterGrid(this.grid);

            var matrix = this.grid.cellMatrix;
            HeightMapManager.instance.RegisterMap(matrix);
        }

        private class RuntimeGridComponent : GridComponent
        {
            private bool _configured;

            internal void Configure(GridConfig cfg)
            {
                this.automaticInitialization = false;
                this.linkOriginToTransform = false;
                this.origin = cfg.origin;
                this.sizeX = cfg.sizeX;
                this.sizeZ = cfg.sizeZ;
                this.cellSize = cfg.cellSize;
                this.generateHeightmap = cfg.generateHeightmap;
                this.heightLookupType = cfg.heightLookupType;
                this.heightLookupMaxDepth = cfg.heightLookupMaxDepth;
                this.lowerBoundary = cfg.lowerBoundary;
                this.upperBoundary = cfg.upperBoundary;
                this.obstacleSensitivityRange = cfg.obstacleSensitivityRange;
                this.subSectionsCellOverlap = cfg.subSectionsCellOverlap;
                this.subSectionsX = cfg.subSectionsX;
                this.subSectionsZ = cfg.subSectionsZ;
                this.friendlyName = cfg.friendlyName;

                Init();

                this.enabled = false;

                _configured = true;
            }

            internal void ConfigureAndInitialize(GridConfig cfg, int maxMillisecondsUsedPerFrame, Action<GridComponent> callback)
            {
                Configure(cfg);

                Initialize(
                    maxMillisecondsUsedPerFrame,
                    callback);
            }

            protected override void Awake()
            {
                /* NOOP */
            }

            protected override void OnEnable()
            {
                if (_configured)
                {
                    base.OnEnable();
                }
            }
        }
    }
}
