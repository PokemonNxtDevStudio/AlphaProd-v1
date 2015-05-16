using UnityEngine;
using System.Collections;

public class TrainerInputHandler : MonoBehaviour {
	[SerializeField] private Camera camera;
	[SerializeField] private float speed;
	private MotorController motor;
	void Start(){
		speed = speed == 0 ? .2f : speed;
		motor = GetComponent<MotorController> ();
	}

	// Update is called once per frame
	void Update(){
		readInputs ();
	}

	void readInputs(){
		//handle cases for triggering release/capture/cast/inventory/etc
		if (motor) {
			if (Input.GetKey(KeyCode.W)) {
//				motor.MoveCharacter(SharedConstants.Movement.forward);
				Vector3 facingAngle = camera.transform.eulerAngles;
				Vector3 facePos = camera.transform.position;
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, facingAngle.y, transform.eulerAngles.z);
				camera.transform.eulerAngles = new Vector3(facingAngle.x, facingAngle.y, facingAngle.z); 
				camera.transform.position = new Vector3(facePos.x, facePos.y, facePos.z); 
				motor.Move(Vector3.forward*speed);
			}
			if (Input.GetKeyUp(KeyCode.S)) {
				motor.Move(Vector3.back*speed);
//				motor.MoveCharacter(SharedConstants.Movement.back);
			}
			if (Input.GetKeyUp(KeyCode.D)) {
				motor.Move(Vector3.right*speed);
//				motor.MoveCharacter(SharedConstants.Movement.right);
			}
			if (Input.GetKeyUp(KeyCode.A)) {
				motor.Move(Vector3.left*speed);
//				motor.MoveCharacter(SharedConstants.Movement.left);
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
