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
	void OnCollision(Collider collision){
		//handlec quicktrigger with shops and such
//		switch (collision.tag) {
//			SharedConstants.
//		}
	}
}
