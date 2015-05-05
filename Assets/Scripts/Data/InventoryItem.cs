using UnityEngine;
using UnityEngine.UI;
using System;

[System.Serializable]
public class AssetItem
{
    public int ID;
    public string Name;

}

[System.Serializable]
public class InventoryItem : AssetItem
{
	public ItemType ItemType;
	public Sprite icon;
    public float BuyingPrice;
    public float SellingPrice;
    public int StacksUpTo;
    public int StacksAtm;
    public string Description;
    public InventoryItem(InventoryItem item)
    {
        ID = item.ID;
        Name = item.Name;
        ItemType = item.ItemType;
        icon = item.icon;
        BuyingPrice = item.BuyingPrice;
        SellingPrice = item.SellingPrice;
        StacksUpTo = item.StacksUpTo;
        StacksAtm = item.StacksAtm;
        Description = item.Description;
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