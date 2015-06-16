using System;
using UnityEngine;
namespace NXT.Inventory
{
    public class PrimaryItemType : ItemBaseType
    {
        [Serializable]
        public class UseableConsumableItem
        {
            [SerializeField]
            private ConsumableItemType m_ItemType;
            [SerializeField]
            private int m_Capacity = 2147483647;
            public ConsumableItemType ItemType
            {
                get
                {
                    return this.m_ItemType;
                }
                set
                {
                    this.m_ItemType = value;
                }
            }
            public int Capacity
            {
                get
                {
                    return this.m_Capacity;
                }
                set
                {
                    this.m_Capacity = value;
                }
            }
            public UseableConsumableItem(ConsumableItemType itemType, int capacity)
            {
                this.m_ItemType = itemType;
                this.m_Capacity = capacity;
            }
        }
        [Serializable]
        public class CrosshairsType
        {
            [SerializeField]
            private Sprite m_Center;
            [SerializeField]
            private float m_Offset;
            [SerializeField]
            private Sprite m_Left;
            [SerializeField]
            private Sprite m_Top;
            [SerializeField]
            private Sprite m_Right;
            [SerializeField]
            private Sprite m_Bottom;
            [SerializeField]
            private float m_AccuracyLossPercent = 0.05f;
            public Sprite Center
            {
                get
                {
                    return this.m_Center;
                }
            }
            public float Offset
            {
                get
                {
                    return this.m_Offset;
                }
            }
            public Sprite Left
            {
                get
                {
                    return this.m_Left;
                }
            }
            public Sprite Top
            {
                get
                {
                    return this.m_Top;
                }
            }
            public Sprite Right
            {
                get
                {
                    return this.m_Right;
                }
            }
            public Sprite Bottom
            {
                get
                {
                    return this.m_Bottom;
                }
            }
            public float AccuracyLossPercent
            {
                get
                {
                    return this.m_AccuracyLossPercent;
                }
            }
        }
        [SerializeField]
        private string m_ItemName;
        [SerializeField]
        private PrimaryItemType.UseableConsumableItem m_ConsumableItem;
        [SerializeField]
        private string m_LowerAnimatorUseState;
        [SerializeField]
        private PrimaryItemType.CrosshairsType m_Crosshairs;
        [SerializeField]
        private Sprite m_ItemSprite;
        public PrimaryItemType.CrosshairsType Crosshairs
        {
            get
            {
                return this.m_Crosshairs;
            }
            set
            {
                this.m_Crosshairs = value;
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
        public string ItemName
        {
            get
            {
                return this.m_ItemName;
            }
            set
            {
                this.m_ItemName = value;
            }
        }
        public PrimaryItemType.UseableConsumableItem ConsumableItem
        {
            get
            {
                return this.m_ConsumableItem;
            }
            set
            {
                this.m_ConsumableItem = value;
            }
        }
        public string LowerAnimatorUseState
        {
            get
            {
                return this.m_LowerAnimatorUseState;
            }
            set
            {
                this.m_LowerAnimatorUseState = value;
            }
        }
        public override int GetCapacity()
        {
            return 2147483647;
        }
    }
}
