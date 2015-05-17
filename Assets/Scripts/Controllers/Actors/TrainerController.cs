using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainerController : MonoBehaviour
{
	[SerializeField] private Camera camera;
	private PokeParty pokeParty; 
	private MotorController motor;
	private int pokeSlot;
	public CameraTransitionController cameraController;
	//    public List<String> pokemon;
	public List<GameObject> pokemon;
	public GameObject releasePokeball;

    void Start()
    {
//        pokemon.Add("Pikachu");
//        pokemon.Add("Raichu");
//        pokemon.Add("SDSDDSaichu");
//        pokemon.Add("Pikachu");
//        pokemon.Add("Raichu");
//		pokemon.Add("SDSDDSaichu");
		motor = GetComponent<MotorController> ();

    }

	void OnEnable(){
		TrainerInputHandler.COMMANDS += OnCommand;
		TrainerInputHandler.MOVEMENT += OnMove;
		TrainerInputHandler.UI += OnUI;
	}

	void OnMove(KeyCode __dir) {
		switch (__dir) {
			case KeyCode.W:
				faceCamera ();
				motor.Move(Vector3.forward);
				break;
			case KeyCode.S:
				motor.Move(Vector3.back);
				break;
			case KeyCode.A:
				motor.Move(Vector3.left);
				break;
			case KeyCode.D:
				motor.Move(Vector3.right);
				break;
		}
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
		Debug.Log("Releasing "+pokemon[pokeSlot]);
		ReleasePokeball pokeball = ((GameObject)GameObject.Instantiate (releasePokeball, transform.position, transform.rotation)).GetComponent<ReleasePokeball>();
		pokeball.pokemon = pokemon [pokeSlot];
		pokeball.direction = Vector3.forward;
		pokeball.releaseComplete += PokemonSpawned;
		//		releasePokeball.transform.position = Vector3.zero;
    }

	public void PokemonSpawned(GameObject __pokemon, Transform __transform){
		Debug.Log ("spawn dat pokemon!");
		GameObject pokemon = (GameObject) GameObject.Instantiate (__pokemon.gameObject, __transform.position, __transform.rotation);
		cameraController.SetTarget(pokemon.transform);
//		cameraController.
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
	
	//make sure trainer always faces camera
	private void faceCamera(){
		Vector3 facingAngle = camera.transform.eulerAngles;
		Vector3 facePos = camera.transform.position;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, facingAngle.y, transform.eulerAngles.z);
		camera.transform.eulerAngles = new Vector3(facingAngle.x, facingAngle.y, facingAngle.z); 
		camera.transform.position = new Vector3(facePos.x, facePos.y, facePos.z); 
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

