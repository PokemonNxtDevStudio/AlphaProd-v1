using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainerController : MonoBehaviour
{
    private PokeParty pokeParty; 
	private int pokeSlot;
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
		TrainerInputHandler.COMMANDS += OnCommand;
	}

	void Update() {
		
	}

	private void OnCommand(KeyCode __command){
		switch (__command) {
			case KeyCode.R:
				TogglePokemon();
				break;
			case KeyCode.Alpha1:
				UseSkill(0);
				break;
			case KeyCode.Alpha2:
				UseSkill(1);
				break;
			case KeyCode.Alpha3:
				UseSkill(2);
				break;
			case KeyCode.Alpha4:
				UseSkill(3);
				break;
			case KeyCode.Alpha5:
				UseSkill(4);
				break;
		}
	}

    public void TogglePokemon()
    {
		Debug.Log("Releasing "+pokemon[pokeSlot]);
    }
	
	public void UseSkill(int __skillSlotID)
	{
//		Debug.Log("Using Skill ");
	}
	
	public void UseItem(int __slot)
	{
//		Debug.Log("usingItem ");
	}
	
	public void OpenInventory()
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

