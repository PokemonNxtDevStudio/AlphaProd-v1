using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]

public class ItemAssetDatabase : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> m_ItemsList = new List<InventoryItem>();
   
    [HideInInspector]
    public List<InventoryItem> ItemsList { get { return m_ItemsList; } /*set { m_ItemsList = value; } */}
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
    public void AddToList(InventoryItem item)
    {
        if(!ItemsList.Contains(item))
        {
            ItemsList.Add(item);
            EditorUtility.SetDirty(this);
        }
    }
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