using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveTowards : Action {
	// min distance to object
	public float minDistance = 0.1f; 
	// The transform that the object is moving towards
	public SharedTransform target;
	// motor
	public MotorController motor;

	private Vector3 updatedPosition;

	public void OnStart(){
//		target = GlobalVariables.Instance.GetVariable("MyVariable");
		Debug.Log ("target!" + target);
	}

	public override TaskStatus OnUpdate()
	{
		// Return a task status of success once we've reached the target
		if (Vector3.SqrMagnitude(transform.position - target.Value.position) < minDistance) {
			return TaskStatus.Success;
		}
		// We haven't reached the target yet so keep moving towards it
		
//		transform.position = Vector3.MoveTowards(transform.position, target.Value.position, motor.baseSpeed * Time.deltaTime);
		Vector3 targetDir = target.Value.position - transform.position;
//		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 1, 0.0F);
//		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
		
//		updatedPosition = Vector3.MoveTowards(transform.position, target.Value.position, motor.baseSpeed * Time.deltaTime);
//		motor.Move (updatedPosition);
		return TaskStatus.Running;
	}
}
