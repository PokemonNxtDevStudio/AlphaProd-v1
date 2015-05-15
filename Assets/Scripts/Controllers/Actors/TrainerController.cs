
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

	void Update() {

	}

    public void ReleasePokemon(int __slot)
    {
		Debug.Log("Releasing " + pokemon[__slot]);
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

