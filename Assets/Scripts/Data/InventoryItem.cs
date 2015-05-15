using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class AssetItem
{
    protected int m_id;
    public int ID { get { return m_id; } set { m_id = value; } }

    protected string m_name;
    public string Name { get { return m_name; } set { m_name = value; } }

    public Sprite m_icon;
    public Sprite Icon { get { return m_icon; } set { m_icon = value; } }

    protected string m_description;
    public string Description { get { return m_description; } set { m_description = value; } }
   
}

[System.Serializable]
public class InventoryItem : AssetItem
{
    private ItemType m_itemtype;
    public ItemType ItemType { get { return m_itemtype; } set { m_itemtype = value; } }
    private float m_buyingprice;
    public float BuyingPrice { get { return m_buyingprice; } set { m_buyingprice = value; } }
    private float m_sellingprice;
    public float SellingPrice { get { return m_sellingprice; } set { m_sellingprice = value; } }
    private int m_stackupto;
    public int StacksUpTo { get { return m_stackupto; } set { m_stackupto = value; } }
    private int m_stacksatm = 1;
    public int StacksAtm { get { return m_stacksatm; } set { m_stacksatm = value; } }

    public InventoryItem(int iid,string name,Sprite icon,ItemType type,float buy,float sell,int stackupto,string description)
    {
        m_id = iid;
        m_name = name;
        m_icon = icon;
        m_itemtype = type;
        m_buyingprice = buy;
        m_sellingprice = sell;
        m_stackupto = stackupto;
        m_description = description;
    }
    public InventoryItem(InventoryItem item)
    {
        m_id = item.ID;
        m_name = item.Name;
        m_itemtype = item.ItemType;
        m_icon = item.Icon;
        m_buyingprice = item.BuyingPrice;
        m_sellingprice = item.SellingPrice;
        m_stackupto = item.StacksUpTo;
        m_stacksatm = item.StacksAtm;
        m_description = item.Description;
    }
}
[System.Serializable]
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