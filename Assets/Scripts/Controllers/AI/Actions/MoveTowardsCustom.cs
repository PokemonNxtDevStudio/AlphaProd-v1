using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveTowardsCustom : Action {
	// min distance to object
	public float minDistance = 0.1f; 
	// The transform that the object is moving towards
	public SharedTransform target;
	// motor
	public MotorController motor;
	public AIPath pathfinder;
	private Vector3 updatedPosition;

	public override void OnStart(){
//		target = GlobalVariables.Instance.GetVariable("MyVariable");
		Debug.Log ("target!!!!!!!!!!!!!!!!!!!!!" + target);
//		pathfinder.enabled = true;
//		pathfinder.target = target.Value;
//		thing.OnP
	}

	public override TaskStatus OnUpdate()
	{
		float distance = Vector3.Distance (transform.position, target.Value.position);

		// Return a task status of success once we've reached the target
		if (distance < minDistance) {
			return TaskStatus.Success;
		}
		Debug.Log ("MERP UPDATE"+target);
		// We haven't reached the target yet so keep moving towards it

//		transform.position = Vector3.MoveTowards(transform.position, target.Value.position, motor.baseSpeed * Time.deltaTime);
		motor.Move (( transform.worldToLocalMatrix.MultiplyVector(transform.forward) * Mathf.Clamp(distance-minDistance,0,1)));
		return TaskStatus.Running;
	}
}
