using System;
using System.Collections.Generic;
using UnityEngine;
using NXT.PhysX;

public class TrainerController : MonoBehaviour
{
	private PokeParty pokeParty; 
	private int pokeSlot = 0;
	private GameObject activePokemon;
	private TrainerAI trainerAI;
	public CameraTransitionController cameraController;
	//    public List<String> pokemon;
	public List<GameObject> pokemon;
	public GameObject releasePokeball;

    public ProjectileController projectileController;

    void Start() {
		trainerAI = GetComponent<TrainerAI> ();
    }

	void OnEnable(){
		TrainerInputHandler.COMMANDS += OnCommand;
		TrainerInputHandler.UI += OnUI;
	}

	private void OnCommand(KeyCode __command){
		switch (__command) {
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
	
	void OnUI(KeyCode __ui) {
		switch (__ui) {
			case KeyCode.R:
				TogglePokemon();
				break;
			case KeyCode.I:
				ToggleInventory();
				break;
		}
	}

    public void TogglePokemon() {
		Debug.Log ("actimon" + activePokemon);
		if (activePokemon == null) {
			Debug.Log("Releasing "+pokemon[pokeSlot]);
			ReleasePokemon ();
		} else if (activePokemon.activeSelf) {
			Debug.Log("returning pokemon ");
			trainerAI.enabled = false;
			ReturnPokemon();
		}
		//		releasePokeball.transform.position = Vector3.zero;
    }
	
	public void ReturnPokemon() {
		cameraController.SetTarget(gameObject.transform);
		PokemonSpawner.KillPokemon (activePokemon);
//		activePokemon = null;
	}

	public void ReleasePokemon() {
        projectileController.projectile = releasePokeball;
        ReleasePokeball pokeball = (ReleasePokeball) projectileController.Spawnprojectile();
		//active pokemon
		activePokemon = NXT.ObjectPool.Instantiate(pokemon[pokeSlot]);
		activePokemon.SetActive (false);

		//spawning and throwing pokeball
		//binding oncomplete
//		Debug.Log ("bindingo n this! " + pokeball);
		pokeball.releaseComplete = PokemonSpawned;
		pokeball.pokemon = pokemon [pokeSlot];
	}

	public void PokemonSpawned(Transform __transform){
		if (activePokemon == null)
			return;
		Debug.Log ("spawn dat pokemon!");

		//setting active and position
		activePokemon.transform.position = __transform.position;
		activePokemon.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
		activePokemon.SetActive (true);
		//moving camera
		cameraController.SetTarget(activePokemon.transform, .25f);

		//enabling AI
		
		trainerAI.enabled = true;
		trainerAI.target = activePokemon.transform;
	}
	
	public void UseSkill(int __skillSlotID) {
		Debug.Log("Using Skill from slot: "+__skillSlotID);
	}
	
	public void UseItem(int __slot) {
//		Debug.Log("usingItem ");
	}
	
	public void ToggleInventory() {
		Debug.Log("opening inventory ");
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

