/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.GameState
{
    using System.Collections.Generic;
    using Apex.Units;
    using UnityEngine;

    /// <summary>
    /// Class for managing various game state related data.
    /// </summary>
    public partial class GameStateManager
    {
        private Dictionary<GameObject, IUnitFacade> _units;
        private Selections _unitSelection;
        private IUnitFacadeFactory _unitFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameStateManager"/> class.
        /// </summary>
        public GameStateManager()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameStateManager"/> class.
        /// </summary>
        /// <param name="unitFactory">The unit factory.</param>
        public GameStateManager(IUnitFacadeFactory unitFactory)
        {
            _unitSelection = new Selections();
            _units = new Dictionary<GameObject, IUnitFacade>(new UnitEqualityComparer());
            _unitFactory = unitFactory;
        }

        /// <summary>
        /// Gets the unit selections.
        /// </summary>
        /// <value>
        /// The unit selections.
        /// </value>
        public Selections unitSelection
        {
            get { return _unitSelection; }
        }

        /// <summary>
        /// Gets the registered units. Please note that this is a live list.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public IEnumerable<IUnitFacade> units
        {
            get { return _units.Values; }
        }

        public IUnitFacade GetUnitFacade(GameObject unitGameObject, bool createIfMissing = true)
        {
            IUnitFacade unit;
            if (!_units.TryGetValue(unitGameObject, out unit) && createIfMissing)
            {
                unit = _unitFactory != null ? _unitFactory.CreateUnitFacade() : new UnitFacade();
                unit.Initialize(unitGameObject);
                _units.Add(unitGameObject, unit);
            }

            return unit;
        }

        public T GetUnitFacade<T>(GameObject unitGameObject, bool createIfMissing = true) where T : class, IUnitFacade, new()
        {
            IUnitFacade unit;
            if (!_units.TryGetValue(unitGameObject, out unit) && createIfMissing)
            {
                unit = new T();
                unit.Initialize(unitGameObject);
                _units.Add(unitGameObject, unit);
            }

            T actual = unit as T;
            if (actual == null)
            {
                actual = new T();
                actual.Initialize(unitGameObject);
                _units[unitGameObject] = actual;
            }

            return actual;
        }

        public void RegisterUnit(GameObject unitGameObject)
        {
            var unit = GetUnitFacade(unitGameObject);
            if (unit.isSelectable)
            {
                _unitSelection.RegisterSelectable(unit);
            }
        }

        public void UnregisterUnit(GameObject unitGameObject)
        {
            IUnitFacade unit;
            if (_units.TryGetValue(unitGameObject, out unit))
            {
                if (unit.transientGroup != null)
                {
                    unit.transientGroup.Remove(unit);
                }

                if (unit.isSelectable)
                {
                    _unitSelection.UnregisterSelectable(unit);
                }
            }

            _units.Remove(unitGameObject);
        }

        private class UnitEqualityComparer : IEqualityComparer<GameObject>
        {
            public bool Equals(GameObject x, GameObject y)
            {
                return object.ReferenceEquals(x, y);
            }

            public int GetHashCode(GameObject obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}