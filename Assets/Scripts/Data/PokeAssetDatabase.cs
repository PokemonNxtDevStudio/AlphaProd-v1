﻿using UnityEngine;
using System.Collections;

public class PokeAssetDatabase : ScriptableObject
{

    public Pokemon[] Pokemons;

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
}