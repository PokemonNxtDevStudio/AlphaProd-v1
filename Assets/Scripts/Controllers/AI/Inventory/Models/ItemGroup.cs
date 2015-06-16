using UnityEngine;
using System;

namespace TeamName.Inventory.Models
{
    /// <summary>
    /// ItemGrouping, mainly for cooldown or another level of grouping items for cooldown.
    /// </summary>
    [System.Serializable]
    public partial class ItemGroup
    {
        public uint ID;
        public string name;

        /// <summary>
        /// If you don't want a cooldown leave it at 0.0
        /// </summary>
        [Range(0, 999)]
        public float cooldownTime;

        [HideInInspector]
        [NonSerialized]
        public float lastUsageTime;


        /// <summary>
        /// Limit a category to specific types, for example potions -> consumables.
        /// </summary>
        [HideInInspector]
        public string[] _onlyAllowTypes = new string[0];
    }
}