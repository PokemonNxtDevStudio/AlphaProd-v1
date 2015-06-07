/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.WorldGeometry
{
    using System;
    using Apex.Common;
    using Apex.Messages;
    using Apex.Services;
    using UnityEngine;

    /// <summary>
    /// Component for setting up <see cref="GridPortal" />s at design time.
    /// </summary>
    [AddComponentMenu("Apex/Navigation/Portals/Portal")]
    [ApexComponent("Game World")]
    public class GridPortalComponent : ExtendedMonoBehaviour, IHandleMessage<GridStatusMessage>
    {
        /// <summary>
        /// The portal name
        /// </summary>
        public string portalName;

        /// <summary>
        /// The type of the portal, which determines how it affects path finding.
        /// </summary>
        public PortalType type;

        /// <summary>
        /// The direction of the portal, i.e. one- or two-way
        /// </summary>
        public PortalDirection direction;

        /// <summary>
        /// The bounds of the first portal
        /// </summary>
        public Bounds portalOne;

        /// <summary>
        /// The bounds of the second portal
        /// </summary>
        public Bounds portalTwo;

        /// <summary>
        /// Controls whether the portal end points are seen as relative to their parent transform
        /// </summary>
        public bool relativeToTransform;

        /// <summary>
        /// The color of portal one when drawing portal gizmos.
        /// </summary>
        public Color portalOneColor = new Color(0f, 150f / 255f, 211f / 255f, 100f / 255f);

        /// <summary>
        /// The color of portal one when drawing portal gizmos.
        /// </summary>
        public Color portalTwoColor = new Color(0f, 211f / 255f, 150f / 255f, 100f / 255f);

        /// <summary>
        /// The color used to show the connection between portals.
        /// </summary>
        public Color connectionColor = new Color(155f, 150f / 255f, 100f / 255f, 255f / 255f);

        [SerializeField, AttributeProperty]
        private int _exclusiveTo;
        private GridPortal _portal;
        private byte _activeGrids;

        /// <summary>
        /// Gets or sets the attribute mask that defines which units can use this portal.
        /// If set to a value other than <see cref="AttributeMask.None"/> only units with at least one of the specified attributes can use the portal.
        /// </summary>
        /// <value>
        /// The exclusive mask.
        /// </value>
        public AttributeMask exclusiveTo
        {
            get
            {
                return _exclusiveTo;
            }

            set
            {
                _exclusiveTo = value;
                if (_portal != null)
                {
                    _portal.exclusiveTo = value;
                }
            }
        }

        private Bounds actualPortalOne
        {
            get
            {
                return this.relativeToTransform ? new Bounds(this.transform.TransformPoint(this.portalOne.center), this.portalOne.size) : this.portalOne;
            }
        }

        private Bounds actualPortalTwo
        {
            get
            {
                return this.relativeToTransform ? new Bounds(this.transform.TransformPoint(this.portalTwo.center), this.portalTwo.size) : this.portalTwo;
            }
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="action">The action.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, Bounds portalOne, Bounds portalTwo, out TAction action) where TAction : Component, IPortalAction
        {
            return Create<TAction>(host, type, PortalDirection.Twoway, portalOne, portalTwo, false, string.Empty, out action);
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="relativeToTransform">Controls whether the given bounds are treated as relative to the host transform.</param>
        /// <param name="action">The action.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, Bounds portalOne, Bounds portalTwo, bool relativeToTransform, out TAction action) where TAction : Component, IPortalAction
        {
            return Create<TAction>(host, type, PortalDirection.Twoway, portalOne, portalTwo, relativeToTransform, string.Empty, out action);
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <param name="action">The action.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, Bounds portalOne, Bounds portalTwo, string portalName, out TAction action) where TAction : Component, IPortalAction
        {
            return Create<TAction>(host, type, PortalDirection.Twoway, portalOne, portalTwo, false, portalName, out action);
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="relativeToTransform">Controls whether the given bounds are treated as relative to the host transform.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <param name="action">The action.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, Bounds portalOne, Bounds portalTwo, bool relativeToTransform, string portalName, out TAction action) where TAction : Component, IPortalAction
        {
            return Create<TAction>(host, type, PortalDirection.Twoway, portalOne, portalTwo, relativeToTransform, portalName, out action);
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="direction">The direction of the portal</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="relativeToTransform">Controls whether the given bounds are treated as relative to the host transform.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <param name="action">The action.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, PortalDirection direction, Bounds portalOne, Bounds portalTwo, bool relativeToTransform, string portalName, out TAction action) where TAction : Component, IPortalAction
        {
            action = host.AddComponent<TAction>();

            var p = host.AddComponent<RuntimeGridPortalComponent>();
            p.Configure(type, direction, portalOne, portalTwo, relativeToTransform, portalName);

            return p;
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, Bounds portalOne, Bounds portalTwo) where TAction : Component, IPortalAction
        {
            return Create<TAction>(host, type, PortalDirection.Twoway, portalOne, portalTwo, false, string.Empty);
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="relativeToTransform">Controls whether the given bounds are treated as relative to the host transform.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, Bounds portalOne, Bounds portalTwo, bool relativeToTransform) where TAction : Component, IPortalAction
        {
            return Create<TAction>(host, type, PortalDirection.Twoway, portalOne, portalTwo, relativeToTransform, string.Empty);
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, Bounds portalOne, Bounds portalTwo, string portalName) where TAction : Component, IPortalAction
        {
            return Create<TAction>(host, type, PortalDirection.Twoway, portalOne, portalTwo, false, portalName);
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="relativeToTransform">Controls whether the given bounds are treated as relative to the host transform.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, Bounds portalOne, Bounds portalTwo, bool relativeToTransform, string portalName) where TAction : Component, IPortalAction
        {
            return Create<TAction>(host, type, PortalDirection.Twoway, portalOne, portalTwo, relativeToTransform, portalName);
        }

        /// <summary>
        /// Creates a runtime Portal instance.
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <param name="host">The host GameObject.</param>
        /// <param name="type">The portal type.</param>
        /// <param name="direction">The direction of the portal.</param>
        /// <param name="portalOne">The bounds of the first portal.</param>
        /// <param name="portalTwo">The bounds of the second portal.</param>
        /// <param name="relativeToTransform">Controls whether the given bounds are treated as relative to the host transform.</param>
        /// <param name="portalName">Name of the portal.</param>
        /// <returns>The portal</returns>
        public static GridPortalComponent Create<TAction>(GameObject host, PortalType type, PortalDirection direction, Bounds portalOne, Bounds portalTwo, bool relativeToTransform, string portalName) where TAction : Component, IPortalAction
        {
            host.AddComponent<TAction>();

            var p = host.AddComponent<RuntimeGridPortalComponent>();
            p.Configure(type, direction, portalOne, portalTwo, relativeToTransform, portalName);

            return p;
        }

        /// <summary>
        /// Called on Awake
        /// </summary>
        protected virtual void Awake()
        {
            if (GridManager.instance.GetGrid(this.actualPortalOne.center) != null)
            {
                _activeGrids++;
            }

            if (GridManager.instance.GetGrid(this.actualPortalTwo.center) != null)
            {
                _activeGrids++;
            }

            if (_activeGrids == 2)
            {
                Initialize();
            }

            GameServices.messageBus.Subscribe(this);
        }

        /// <summary>
        /// Called on Start and OnEnable, but only one of the two, i.e. at startup it is only called once.
        /// </summary>
        protected override void OnStartAndEnable()
        {
            if (_portal != null)
            {
                _portal.enabled = true;
            }
        }

        private void OnDisable()
        {
            if (_portal != null)
            {
                _portal.enabled = false;
            }
        }

        private void OnDestroy()
        {
            GameServices.messageBus.Unsubscribe(this);
        }

        private void OnDrawGizmos()
        {
            if (!this.enabled)
            {
                return;
            }

            var p1 = this.actualPortalOne;
            var p2 = this.actualPortalTwo;

            Gizmos.color = this.portalOneColor;
            Gizmos.DrawCube(p1.center, p1.size);
            Gizmos.color = this.portalTwoColor;
            Gizmos.DrawCube(p2.center, p2.size);

            Gizmos.color = this.connectionColor;
            Gizmos.DrawLine(p1.center, p2.center);

            if (this.direction == PortalDirection.Twoway)
            {
                Gizmos.DrawSphere(p1.center, 0.5f);
                Gizmos.DrawSphere(p2.center, 0.5f);
            }
            else
            {
                Gizmos.DrawSphere(p1.center, 0.5f);
            }
        }

        private void Initialize()
        {
            var action = this.As<IPortalAction>();
            if (action == null)
            {
                var fact = this.As<IPortalActionFactory>();
                if (fact != null)
                {
                    action = fact.Create();
                }
            }

            if (action == null)
            {
                Debug.LogError("A portal must have an accompanying portal action component, please add one.");
                this.enabled = false;
                return;
            }

            if (this.portalOne.size.sqrMagnitude == 0f || this.portalTwo.size.sqrMagnitude == 0f)
            {
                Debug.LogError("A portal's end points must have a size greater than 0.");
                this.enabled = false;
                return;
            }

            try
            {
                _portal = GridPortal.Create(this.portalName, this.type, this.direction, this.actualPortalOne, this.actualPortalTwo, this.exclusiveTo, action);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                this.enabled = false;
            }
        }

        void IHandleMessage<GridStatusMessage>.Handle(GridStatusMessage message)
        {
            var gridBounds = message.gridBounds;
            bool containsOne = gridBounds.Contains(this.actualPortalOne.center);
            bool containsTwo = gridBounds.Contains(this.actualPortalTwo.center);
            int increment = 0;

            if (containsOne || containsTwo)
            {
                increment = (containsOne && containsTwo) ? 2 : 1;
            }

            if (increment > 0)
            {
                switch (message.status)
                {
                    case GridStatusMessage.StatusCode.DisableComplete:
                    {
                        _activeGrids -= (byte)increment;
                        break;
                    }

                    case GridStatusMessage.StatusCode.InitializationComplete:
                    {
                        _activeGrids += (byte)increment;
                        break;
                    }
                }

                if (_activeGrids == 2)
                {
                    Initialize();

                    if (_portal != null)
                    {
                        _portal.enabled = this.enabled;
                    }
                }
                else if (_portal != null)
                {
                    GridManager.instance.UnregisterPortal(_portal.name);
                    _portal.enabled = false;
                    _portal = null;
                }
            }
        }

        private class RuntimeGridPortalComponent : GridPortalComponent
        {
            internal void Configure(PortalType type, PortalDirection direction, Bounds portalOne, Bounds portalTwo, bool relativeToTransform, string portalName)
            {
                this.type = type;
                this.direction = direction;
                this.portalOne = portalOne;
                this.portalTwo = portalTwo;
                this.relativeToTransform = relativeToTransform;
                this.portalName = portalName;

                base.Awake();
                OnStartAndEnable();
            }

            protected override void Awake()
            {
                /* NOOP */
            }
        }
    }
}
