using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class HItem : ScriptableObject
{
    public string Name;
    public int ID;
    public float Buyprice;
    public float Sellprice;
    public string Description;
    public bool Isstackable;
    public Sprite Icon;
    public ItemType Type;

    /*
    public string ItemName { get { return h_name; } set { h_name = value; } }
    public float BuyPrice { get { return h_buyprice; } set { h_buyprice = value; } }
    public float SellPrice { get { return h_sellprice; } set { h_sellprice = value; } }
    public string Description { get { return h_description; } set { h_description = value; } }
    public bool IsStackable { get { return h_isstackable; } set { h_isstackable = value; } }
    public Sprite Icon { get { return h_icon; } set { h_icon = value; } }
    public ItemType ItemType { get { return h_type; } set { h_type = value; } }

    public HItem(string Name,float Buy,float Sell,string Des,bool Stackable,Sprite icon,ItemType type)
    {
         
        h_name = Name;   
        h_buyprice = Buy;
        h_sellprice = Sell;
        h_description = Des;
        h_isstackable = Stackable;
        h_icon = icon;
        h_type = type;
    }
     * */
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