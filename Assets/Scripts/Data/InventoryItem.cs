using UnityEngine;
using UnityEngine.UI;
using System;


[Serializable]
public class InventoryItem : AssetItem
{
    [SerializeField]
    private ItemType m_itemtype;
    public ItemType ItemType { get { return m_itemtype; } set { m_itemtype = value; } }
    
    [SerializeField]
    private float m_buyingprice;
    public float BuyingPrice { get { return m_buyingprice; } set { m_buyingprice = value; } }
   
    [SerializeField]
    private float m_sellingprice;
    public float SellingPrice { get { return m_sellingprice; } set { m_sellingprice = value; } }

    [SerializeField]
    private int m_stackupto;
    public int StacksUpTo { get { return m_stackupto; } set { m_stackupto = value; } }
    
    [SerializeField]
    private int m_stacksatm = 1;
    public int StacksAtm { get { return m_stacksatm; } set { m_stacksatm = value; } }

    public InventoryItem(int iid,string name,Sprite icon,ItemType type,float buy,float sell,int stackupto,string description)
    {
        ID = iid;
        Name = name;
        Icon = icon;
        ItemType = type;
        BuyingPrice = buy;
        SellingPrice = sell;
        StacksUpTo = stackupto;
        Description = description;
    }
    public InventoryItem(InventoryItem item)
    {
        ID = item.ID;
        Name = item.Name;
        ItemType = item.ItemType;
        Icon = item.Icon;
        BuyingPrice = item.BuyingPrice;
        SellingPrice = item.SellingPrice;
        StacksUpTo = item.StacksUpTo;
        StacksAtm = item.StacksAtm;
        Description = item.Description;
    }
}
[Serializable]
public enum ItemType
{
    None,
    Pokeball,
    GeneralItem,
    Potion,
    MtTm,
    Berry,
    KeyItem
}