using UnityEngine;
using System.Collections;

public class TrainerInputHandler : MonoBehaviour {

	//events to subscribe to
	public delegate void inputAction(KeyCode keys);
	public static event inputAction MOVEMENT;
	public static event inputAction COMMANDS;
	public static event inputAction UI;

	// Update is called once per frame
	void Update(){
		readInputs ();
	}

	void readInputs(){
		//handle cases for triggering release/capture/cast/inventory/etc
		if (Input.GetKey(KeyCode.W))
			MOVEMENT(KeyCode.W);
		if (Input.GetKeyUp(KeyCode.S))
			MOVEMENT(KeyCode.S);
		if (Input.GetKeyUp(KeyCode.D))
			MOVEMENT(KeyCode.D);
		if (Input.GetKeyUp(KeyCode.A))
			MOVEMENT(KeyCode.A);
		if (Input.GetKeyUp(KeyCode.I))
			UI(KeyCode.I);
		checkTrainerCommands ();
	}

	public void checkTrainerCommands(){
		if (Input.GetMouseButtonUp(0)) {
			UI(KeyCode.R);
			//Release pokemon if no pokemon
			//or
			//Return pokemon if pokemon is out
		}
		//cast skill 1
		if (Input.GetKeyUp (KeyCode.Alpha1))
			COMMANDS(KeyCode.Alpha1);
		//cast skill 2
		if (Input.GetKeyUp (KeyCode.Alpha2))
			COMMANDS(KeyCode.Alpha2);
		//cast skill 3
		if (Input.GetKeyUp (KeyCode.Alpha3))
			COMMANDS(KeyCode.Alpha3);
		//cast skill 4
		if (Input.GetKeyUp (KeyCode.Alpha4))
			COMMANDS(KeyCode.Alpha4);
		//cast skill 5
		if (Input.GetKeyUp (KeyCode.Alpha5))
			COMMANDS(KeyCode.Alpha5);
	}

	void OnCollision(Collider collision){
		//handlec quicktrigger with shops and such
//		switch (collision.tag) {
//			SharedConstants.
//		}
	}
}
