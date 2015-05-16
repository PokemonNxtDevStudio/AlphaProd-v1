using UnityEngine;
using System.Collections;

public class TrainerInputHandler : MonoBehaviour {
	[SerializeField]
	private MoterInputHandler motor;

	// Update is called once per frame
	void Update(){
		readInputs ();
	}

	void readInputs(){
		//handle cases for triggering release/capture/cast/inventory/etc
		if (motor) {
			if (Input.GetKeyUp(KeyCode.W)) {
				motor.MoveCharacter(SharedConstants.Movement.forward);
			}
			if (Input.GetKeyUp(KeyCode.S)) {
				motor.MoveCharacter(SharedConstants.Movement.back);
			}
			if (Input.GetKeyUp(KeyCode.D)) {
				motor.MoveCharacter(SharedConstants.Movement.right);
			}
			if (Input.GetKeyUp(KeyCode.A)) {
				motor.MoveCharacter(SharedConstants.Movement.left);
			}
		}

		if (Input.GetKeyUp(KeyCode.I)) {
			//open inventeory
		}
		if (Input.GetKeyUp(KeyCode.R)) {
			//Release pokemon if no pokemon
			//or
			//Return pokemon if pokemon is out
		}

		if (Input.GetKeyUp (KeyCode.Alpha1)) {
			//cast skill 1
		}
		if (Input.GetKeyUp (KeyCode.Alpha2)){
			//cast skill 2
		}
		if (Input.GetKeyUp (KeyCode.Alpha3)){
			//cast skill 3
		}
		if (Input.GetKeyUp (KeyCode.Alpha4)){
			//cast skill 4
		}
		if (Input.GetKeyUp (KeyCode.Alpha5)){
			//cast skill 5
		}

	}
	void OnCollision(Collider collision){
		//handlec quicktrigger with shops and such
//		switch (collision.tag) {
//			SharedConstants.
//		}
	}
}
