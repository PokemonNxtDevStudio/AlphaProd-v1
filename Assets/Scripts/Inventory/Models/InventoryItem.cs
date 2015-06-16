using System;
using UnityEngine;

namespace NXT.Inventory
{
    public class InventoryItem : AssetItem
    {
       // [SerializeField]
        //private ItemType m_itemtype;

        [SerializeField]
        private String m_Description;
        public String Description { get { return m_Description; } set { m_Description = value; } }

        //public ItemType ItemType { get { return m_itemtype; } set { m_itemtype = value; } }

        [SerializeField]
        private float m_buyingprice;
        public float BuyingPrice { get { return m_buyingprice; } set { m_buyingprice = value; } }

        [SerializeField]
        private float m_sellingprice;
        public float SellingPrice { get { return m_sellingprice; } set { m_sellingprice = value; } }

        [SerializeField]
        private int m_stackupto;
        public int StacksUpTo { get { return m_stackupto; } set { m_stackupto = value; } }  //maxStackCount

        [SerializeField]
        private int m_stacksatm = 1;
        public int StacksAtm { get { return m_stacksatm; } set { m_stacksatm = value; } }   //currentStackCount

    }

    public enum ItemType
    {

    }
}
