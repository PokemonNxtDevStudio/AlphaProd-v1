/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Units
{
    using Apex.Common;
    using Apex.Services;
    using Apex.Steering;
    using Apex.Utilities;
    using Apex.WorldGeometry;
    using UnityEngine;

    /// <summary>
    /// Basic unit properties component.
    /// </summary>
    [AddComponentMenu("Apex/Units/Unit")]
    [ApexComponent("Unit Properties")]
    public partial class UnitComponent : AttributedComponent, IUnitProperties
    {
        private Transform _transform;
        private bool _isSelected;
        private bool? _selectPending;
        private HeightNavigationCapabilities _effectiveHeightCapabilities;

        [SerializeField]
        private int _determination = 1;

        [SerializeField]
        private bool _isSelectable = false;

        [SerializeField]
        private HeightNavigationCapabilities _heightCapabilities = new HeightNavigationCapabilities
        {
            maxClimbHeight = 0.5f,
            maxDropHeight = 1f,
            maxSlopeAngle = 30f
        };

        /// <summary>
        /// The visual used to indicate whether the unit is selected or not.
        /// </summary>
        public GameObject selectionVisual;

        /// <summary>
        /// The radius of the unit.
        /// </summary>
        [MinCheck(0f, label = "Radius", tooltip = "The radius of the unit.")]
        public float radius = 0.5f;

        /// <summary>
        /// The field of view in degrees
        /// </summary>
        [RangeX(0f, 360f, label = "Field of View", tooltip = "The field of view in degrees.")]
        public float fieldOfView = 200f;

        /// <summary>
        /// If the unit is not properly grounded at y = 0, set this offset such that when the unit is in a grounded position, its transform.y - yAxisoffset == 0.
        /// This is only relevant if your unit has no rigidbody with gravity.
        /// </summary>
        [MinCheck(0f, label = "Ground Offset", tooltip = "The distance the unit will hover above the ground as seen from the bottom of its collider/mesh.")]
        public float yAxisoffset = 0.0f;

        /// <summary>
        /// Gets the position of the component.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector3 position
        {
            get { return _transform.position; }
        }

        /// <summary>
        /// Gets the forward vector of the unit, i.e. the direction its nose is pointing (provided it has a nose).
        /// </summary>
        /// <value>
        /// The forward vector.
        /// </value>
        public Vector3 forward
        {
            get { return _transform.forward; }
        }

        float IUnitProperties.radius
        {
            get { return this.radius; }
        }

        float IUnitProperties.fieldOfView
        {
            get { return this.fieldOfView; }
        }

        public bool isSelectable
        {
            get { return _isSelectable; }
            set { _isSelectable = value; }
        }

        public int determination
        {
            get { return _determination; }
            set { _determination = value; }
        }

        public bool isSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _selectPending = null;

                if (_isSelected != value)
                {
                    _isSelected = value;
                    if (this.selectionVisual != null)
                    {
                        this.selectionVisual.SetActive(value);
                    }
                }
            }
        }

        public float baseToPositionOffset
        {
            get;
            private set;
        }

        public float height
        {
            get;
            private set;
        }

        public Vector3 basePosition
        {
            get
            {
                var tmp = _transform.position;
                tmp.y -= this.baseToPositionOffset;
                return tmp;
            }
        }

        public HeightNavigationCapabilities heightNavigationCapability
        {
            get { return _effectiveHeightCapabilities; }
        }

        public void RecalculateBasePosition()
        {
            var rb = this.GetComponent<Rigidbody>();
            var coll = this.GetComponent<Collider>();
            var renderer = this.GetComponent<Renderer>();

            if (rb != null && rb.useGravity)
            {
                yAxisoffset = 0.0f;
            }

            float totalOffset;
            if (coll != null)
            {
                //We need to adjust the collider to take the collider overlap into consideration
                var posOffset = (this.position.y - coll.bounds.center.y);
                totalOffset = coll.bounds.extents.y + posOffset + this.yAxisoffset;
                this.height = coll.bounds.size.y + this.yAxisoffset;
                if (rb != null && rb.useGravity)
                {
					totalOffset -= (2 * Physics.defaultContactOffset);
                }
            }
            else if (renderer != null)
            {
                var posOffset = (this.position.y - renderer.bounds.center.y);
                totalOffset = renderer.bounds.extents.y + posOffset + this.yAxisoffset;
                this.height = renderer.bounds.size.y + this.yAxisoffset;
            }
            else
            {
                totalOffset = this.yAxisoffset;
            }

            this.baseToPositionOffset = totalOffset;
        }

        /// <summary>
        /// Marks the unit as pending for selection. This is used to indicate a selection is progress, before the actual selection occurs.
        /// </summary>
        /// <param name="pending">if set to <c>true</c> the unit is pending for selection otherwise it is not.</param>
        public void MarkSelectPending(bool pending)
        {
            if (pending != _selectPending)
            {
                _selectPending = pending;

                if (this.selectionVisual != null)
                {
                    this.selectionVisual.SetActive(pending);
                }
            }
        }

        private void Awake()
        {
            this.WarnIfMultipleInstances<IUnitProperties>();

            _transform = this.transform;

            var heightStrat = GameServices.heightStrategy;
            _effectiveHeightCapabilities = heightStrat.useGlobalHeightNavigationSettings ? heightStrat.unitsHeightNavigationCapability : _heightCapabilities;
            GameServices.gameStateManager.RegisterUnit(this.gameObject);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            //Ensure selection is toggled off
            _isSelected = true;
            this.isSelected = false;

            //Get base pos
            RecalculateBasePosition();

            //Make sure units do not start embedded in the ground
            RaycastHit groundHit;
            var basePos = this.basePosition;
            var topPos = basePos;
            topPos.y += this.height;
            if (Physics.Raycast(topPos, Vector3.down, out groundHit, float.PositiveInfinity, Layers.terrain))
            {
                var diff = groundHit.point.y - basePos.y;
                if (diff > 0f)
                {
                    var pos = _transform.position;
                    pos.y += diff;
                    _transform.position = pos;
                }
            }
        }
        
        private void OnDestroy()
        {
            GameServices.gameStateManager.UnregisterUnit(this.gameObject);
        }

        public void CloneFrom(UnitComponent unitComp)
        {
            _isSelectable = unitComp.isSelectable;
            this.selectionVisual = unitComp.selectionVisual;

            _heightCapabilities = unitComp._heightCapabilities;
            _effectiveHeightCapabilities = unitComp._effectiveHeightCapabilities;

            this.radius = unitComp.radius;
            this.fieldOfView = unitComp.fieldOfView;
            this.baseToPositionOffset = unitComp.baseToPositionOffset;
            
            this.height = unitComp.height;
            this.yAxisoffset = unitComp.yAxisoffset;
        }
    }
}
