using UnityEngine;
using System.Collections;

public class TrainerInputHandler : MonoBehaviour {
	[SerializeField] private Camera camera;
	[SerializeField] private float speed;
	private MotorController motor;
	private TrainerController trainerController;

	//events to subscribe to
	public delegate void inputAction(KeyCode keys);
	public static event inputAction Movement;
	public static event inputAction COMMANDS;
	public static event inputAction UI;

	void Start(){
		speed = speed == 0 ? .2f : speed;
		motor = GetComponent<MotorController> ();
		trainerController = GetComponent<TrainerController> ();
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
				faceCamera();
				Movement(KeyCode.W);
//				motor.Move(Vector3.forward*speed);
			}
			if (Input.GetKeyUp(KeyCode.S)) {
				Movement(KeyCode.S);
//				motor.Move(Vector3.back*speed);
			}
			if (Input.GetKeyUp(KeyCode.D)) {
				Movement(KeyCode.D);
//				motor.Move(Vector3.right*speed);
			}
			if (Input.GetKeyUp(KeyCode.A)) {
				Movement(KeyCode.A);
//				motor.Move(Vector3.left*speed);
			}
		}

		if (Input.GetKeyUp(KeyCode.I))
			UI(KeyCode.I);
		checkTrainerCommands ();
		checkItemUse ();
	}

	public void checkItemUse() {

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
			UI(KeyCode.Alpha1);
		//cast skill 2
		if (Input.GetKeyUp (KeyCode.Alpha2))
			UI(KeyCode.Alpha2);
		//cast skill 3
		if (Input.GetKeyUp (KeyCode.Alpha3))
			UI(KeyCode.Alpha3);
		//cast skill 4
		if (Input.GetKeyUp (KeyCode.Alpha4))
			UI(KeyCode.Alpha4);
		//cast skill 5
		if (Input.GetKeyUp (KeyCode.Alpha5))
			UI(KeyCode.Alpha5);
	}

	void OnCollision(Collider collision){
		//handlec quicktrigger with shops and such
//		switch (collision.tag) {
//			SharedConstants.
//		}
	}

	//make sure trainer always faces camera
	private void faceCamera(){
		Vector3 facingAngle = camera.transform.eulerAngles;
		Vector3 facePos = camera.transform.position;
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, facingAngle.y, transform.eulerAngles.z);
		camera.transform.eulerAngles = new Vector3(facingAngle.x, facingAngle.y, facingAngle.z); 
		camera.transform.position = new Vector3(facePos.x, facePos.y, facePos.z); 
	}
}
