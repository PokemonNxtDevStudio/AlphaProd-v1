
using System;
using System.Collections.Generic;
using UnityEngine;




public class TrainerController : MonoBehaviour
{



    private PokeParty pokeParty; 
    public List<String> pokemon;

    void Start()
    {
        pokemon.Add("Pikachu");
        pokemon.Add("Raichu");
        pokemon.Add("SDSDDSaichu");
        pokemon.Add("Pikachu");
        pokemon.Add("Raichu");
        pokemon.Add("SDSDDSaichu");
    }

    public void ReleasePokemon(int i)
    {
        Debug.Log("Releasing " + pokemon[i]);
    }
   
        


    //TODO:

    /*
    void ReleasePokemon(int pokeSlot)
    {
     * 
     * if(CanRelease(pokeSlot)
     *    ThrowPokeball
     }


        */
 }

