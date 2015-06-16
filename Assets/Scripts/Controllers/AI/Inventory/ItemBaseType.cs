using System;
using UnityEngine;
namespace NXT.Inventory
{
    public abstract class ItemBaseType : ScriptableObject
    {
        public abstract int GetCapacity();
    }
}
