using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

//[CustomEditor(typeof(ItemCreator))]
public class ItemCreator : EditorWindow
{
    private string nameOfItem = "";
    private int uniqueId;
    private float buyPrice;
    private float sellPrice;
    private string Description = "";
    private bool IsStackable = false;
    public Sprite Icon;
    private ItemType typeOfItem = ItemType.None;
    private int id;
   
    private Rect NameLableRect = new Rect(10, 10, 70, 20);
    private Rect NameRect = new Rect(70, 10, 200, 20);

    private Rect IdLableRect = new Rect(10, 40, 70, 20);
    private Rect IdRect = new Rect(70, 40, 100, 20);

    private Rect BuyLableRect = new Rect(10, 70, 70, 20);
    private Rect BuyRect = new Rect(70, 70, 100, 20);
    
    private Rect SellLableRect = new Rect(10, 100, 70, 20);
    private Rect SellRect = new Rect(70, 100, 100, 20);

    private Rect DescriptionLabelRect = new Rect(10, 130, 70, 20);
    private Rect DescriptionRect = new Rect(70, 130, 200, 20);
    
    private Rect IsStackRect = new Rect(10, 160, 200, 20);

    private Rect IconLableRect = new Rect(10, 190, 70, 20);
    private Rect IconRect = new Rect(70, 190,190, 15);

    private Rect TypeRect = new Rect(10, 230, 250, 20);

    private Rect CreateButtonRect = new Rect(10, 300, 150, 50);

    [MenuItem("NXT/ItemCreation #I")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ItemCreator));
    }


    void OnGUI()
    {
        GUI.Label(NameLableRect, "Name:");        
        nameOfItem = GUI.TextField(NameRect, nameOfItem);

        GUI.Label(IdLableRect, "ID:");
        uniqueId = EditorGUI.IntField(IdRect, uniqueId);

        GUI.Label(BuyLableRect, "BuyPrice:");
        buyPrice = EditorGUI.FloatField(BuyRect, buyPrice);

        GUI.Label(SellLableRect, "SellPrice:");
        sellPrice = EditorGUI.FloatField(SellRect, sellPrice);

        GUI.Label(DescriptionLabelRect, "Descrip:");
        Description = GUI.TextField(DescriptionRect, Description);

        IsStackable = EditorGUI.Toggle(IsStackRect, "Is Stackable", IsStackable);

        //id = int.Parse(uniqueId);
        GUI.Label(IconLableRect, "Icon:");
        Icon = (Sprite)EditorGUI.ObjectField(IconRect,Icon, typeof(Sprite),false);

        typeOfItem = (ItemType)EditorGUI.EnumPopup(TypeRect,"Item Type",typeOfItem);   
    
        if(nameOfItem != "" && uniqueId != 0 && Icon != null && typeOfItem != ItemType.None)
        if(GUI.Button(CreateButtonRect,"Create Item"))
        {
            MakeTheNewItem();
        }
    }

    private void MakeTheNewItem()
    {
        HItem item = ScriptableObject.CreateInstance<HItem>();
        item.Name = nameOfItem;
        item.ID = uniqueId;
        item.Buyprice = buyPrice;
        item.Sellprice = sellPrice;
        item.Description = Description;
        item.Isstackable = IsStackable;
        item.Icon = Icon;
        item.Type = typeOfItem;

        AssetDatabase.CreateAsset(item, "Assets/DB/Items/" + nameOfItem+".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;

        HItemDB db = (HItemDB)AssetDatabase.LoadAssetAtPath("Assets/DB/PokemonItemsDB.asset", typeof(HItemDB));
        db.ItemsDataBase.Add(item);

    }
    /*
    [MenuItem("NXT/ScriptableObjecs/CreateItem")]
    public static void MakeAItem()
    {
        HItem item = ScriptableObject.CreateInstance<HItem>();
        AssetDatabase.CreateAsset(item, "Assets/DB/Items/NewItem.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = item;
    }
    [MenuItem("NXT/ScriptableObjecs/ItemDataBase")]
    public static void MakeAItemDataBase()
    {
        HItemDB DB = ScriptableObject.CreateInstance<HItemDB>();
        AssetDatabase.CreateAsset(DB, "Assets/DB/NewItemDataBase.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = DB;
    }
     * */
}
