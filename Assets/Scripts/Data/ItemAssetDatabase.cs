using UnityEngine;
using System.Collections;

public class ItemAssetDatabase : ScriptableObject
{
	
	public InventoryItem[] items;
	
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
	}
}