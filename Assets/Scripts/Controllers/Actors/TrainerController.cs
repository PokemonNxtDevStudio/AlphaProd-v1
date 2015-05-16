using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainerController : MonoBehaviour
{
    private PokeParty pokeParty; 
    public List<String> pokemon;
	//public SharedConstants.CharacterType characterType;

    void Start()
    {
        pokemon.Add("Pikachu");
        pokemon.Add("Raichu");
        pokemon.Add("SDSDDSaichu");
        pokemon.Add("Pikachu");
        pokemon.Add("Raichu");
        pokemon.Add("SDSDDSaichu");
    }

	void OnEnable(){
//		TrainerInputHandler.Commands += OnCommand;
	}

	void Update() {
		
	}

	private void OnCommand(){

	}

    public void ReleasePokemon(int __slot)
    {
		Debug.Log("Releasing " + pokemon[__slot]);
    }
	
	public void UseSkill(int __skillID)
	{
//		Debug.Log("Using Skill ");
	}
	
	public void UseItem(int __slot)
	{
//		Debug.Log("usingItem ");
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

