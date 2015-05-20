using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PokeAssetDatabase : ScriptableObject
{
    [SerializeField]
    private List<Pokemon> m_PokemonsList = new List<Pokemon>();
    [HideInInspector]
    private List<Pokemon> PokemonsList { get { return m_PokemonsList; } /*set { m_PokemonsList = value; }*/ }
   /* public Pokemon[] Pokemons;

    /// <summary>
    /// Get the specified SpellInfo by index.
    /// </summary>
    /// <param name="index">Index.</param>
    public Pokemon Get(int index)
    {
        return (Pokemons[index]);
    }
    /// <summary>
    /// Gets the specified SpellInfo by ID.
    /// </summary>
    /// <returns>The SpellInfo or NULL if not found.</returns>
    /// <param name="ID">The spell ID.</param>
    public Pokemon GetByID(int ID)
    {
        for (int i = 0; i < Pokemons.Length; i++)
        {
            if (Pokemons[i].ID == ID)
                return Pokemons[i];
        }
        return null;
    }
    */
    public void AddToList(Pokemon poketoadd)
    {
        if(!PokemonsList.Contains(poketoadd))
        {
            PokemonsList.Add(poketoadd);
            EditorUtility.SetDirty(this);

        }
    }
    public Pokemon GetByIDInList(int ID)
    {
        Pokemon tem = null;
        for(int i = 0; i < PokemonsList.Count ; i++)
        {
            if (PokemonsList[i].ID == ID)
            {
                tem = PokemonsList[i];
            }
        }
        return tem;
    }
}