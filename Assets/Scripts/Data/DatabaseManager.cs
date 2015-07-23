using NXT.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{


    public PokeAssetDatabase pokeAssetDatabase;
    public ItemAssetDatabase itemAssetDatabase;

    private List<Pokemon> pokeList { get { return pokeAssetDatabase.PokemonsList; } }
    private MoveData[] moveList { get { return pokeAssetDatabase.moveList; } }

    private List<InventoryItem> itemList { get { return itemAssetDatabase.ItemsList; } }
    public static DatabaseManager instance;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }



    void Start()
    {

    }
    #region items
    /// <summary>
    /// Gets item by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public InventoryItem GetItemByID(int ID)
    {
        InventoryItem tem = null;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].ID == ID)
            {
                tem = itemList[i];
                break;
            }
        }

        return tem;
    }
    /// <summary>
    /// Gets item by item name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public InventoryItem GetItemByName(string name)
    {
        InventoryItem tem = null;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].Name == name)
            {
                tem = itemList[i];
                break;
            }
        }

        return tem;
    }
    #endregion
    public Pokemon GetPokemonByID(string name)
    {
        Pokemon pokemon = null;

        for (int i = 0; i < pokeList.Count; i++)
        {
            if (pokeList[i].Name == name)
            {
                pokemon = pokeList[i];
                break;
            }
        }
        return pokemon;

    }

    public Pokemon GetPokemonByID(int id,int lvl)
    {
        Pokemon pokemon = null;

        for (int i = 0; i < pokeList.Count; i++)
        {
            if (pokeList[i].ID == id)
            {
                pokemon = pokeList[i];
                break;
            }
        }
        return pokemon;

    }
    public PokeData GetPokeDataByID(int id, int lvl)
    {
        return GetPokemonByID(id, lvl).pokeData;

    }

}

