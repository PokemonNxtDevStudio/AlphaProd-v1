using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using UnityEditor;
using TeamName.Inventory.Models;
using NXT.Inventory;
#endif
[Serializable]
public class ItemAssetDatabase : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> m_ItemsList = new List<InventoryItem>();
   
    [HideInInspector]
    public List<InventoryItem> ItemsList { get { return m_ItemsList; } set { m_ItemsList = value; } }

    public InventoryItem[] items;
    /// <summary>
    /// ItemGroup for this assetDatabase
    /// </summary>
    public ItemGroup[] itemGroup= new ItemGroup[] { new ItemGroup() { ID = 0, name = "None", cooldownTime = 0.0f } };
    /*public InventoryItem[] items;
	
    /// <summary>
    /// Get the specified SpellInfo by index.
    /// </summary>
    /// <param name="index">Index.</param>
    public InventoryItem Get(int index)
    {
        return (items[index]);
    }	
    /// <summary>
    /// Gets the specified SpellInfo by ID.
    /// </summary>
    /// <returns>The SpellInfo or NULL if not found.</returns>
    /// <param name="ID">The spell ID.</param>
    public InventoryItem GetByID(int ID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].ID == ID)
                return items[i];
        }		
        return null;
    }*/
#if UNITY_EDITOR
    public void AddToList(InventoryItem item)
    {
        if(!ItemsList.Contains(item))
        {
            ItemsList.Add(item);
            EditorUtility.SetDirty(this);
        }
    }
#endif

    public InventoryItem GetByIDInList(int ID)
    {
        InventoryItem tem = null;
        for (int i = 0; i < ItemsList.Count;i++ )
        {
            if(ItemsList[i].ID == ID)
            {
                tem = ItemsList[i];
            }
        }
           
        return tem;
    }
}
