using System;
using UnityEngine;
namespace NXT.Inventory
{
    public class QuickUseItemType : ItemBaseType
    {
        [SerializeField]
        private int m_Capacity = 2147483647;
        [SerializeField]
        private Sprite m_ItemSprite;
        public int Capacity
        {
            set
            {
                this.m_Capacity = value;
            }
        }
        public Sprite ItemSprite
        {
            get
            {
                return this.m_ItemSprite;
            }
            set
            {
                this.m_ItemSprite = value;
            }
        }
        public override int GetCapacity()
        {
            return this.m_Capacity;
        }
    }
}
