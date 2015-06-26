using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	[TaskDescription("Wander using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}WanderIcon.png")]
	public class WanderAStar : Action
	{
		[Tooltip("The speed of the agent")]
		public SharedFloat speed;
		[Tooltip("Angular speed of the agent")]
		public SharedFloat angularSpeed = 10;
		[Tooltip("How far ahead of the current position to look ahead for a wander")]
		public SharedFloat wanderDistance = 20;
		[Tooltip("The amount that the agent rotates direction")]
		public SharedFloat wanderRate = 2;
		
		// A cache of the NavMeshAgent
		private AIPath aiPath;
		private MotorController motor;
		private GameObject wanderTarget;
		
		public override void OnAwake()
		{
			// cache for quick lookup
			aiPath = gameObject.GetComponent<AIPath>();
			motor = gameObject.GetComponent<MotorController>();
			wanderTarget = new GameObject ();
			wanderTarget.transform.parent = transform.parent;
			wanderTarget.name = gameObject.name + " Target";
		}
		
		public override void OnStart()
		{
			// set the speed, angular speed, and destination then enable the agent
			aiPath.speed = speed.Value;
			aiPath.turningSpeed = angularSpeed.Value;
			aiPath.enabled = true;
			aiPath.target = Target();
			Debug.Log ("new target is!:" + aiPath.target);
		}
		
		// There is no success or fail state with wander - the agent will just keep wandering
		public override TaskStatus OnUpdate()
		{
			aiPath.target = Target();
			
			//		transform.position = Vector3.MoveTowards(transform.position, target.Value.position, motor.baseSpeed * Time.deltaTime);
			motor.Move (( transform.worldToLocalMatrix.MultiplyVector(transform.forward)));
			return TaskStatus.Running;
		}
		
		public override void OnEnd()
		{
			// Disable the nav mesh
			aiPath.enabled = false;
		}
		
		// Return targetPosition if targetTransform is null
		private Vector3 getTarget()
		{
			// point in a new random direction and then multiply that by the wander distance
			var direction = transform.forward + Random.insideUnitSphere * wanderRate.Value;
			return transform.position + direction.normalized * wanderDistance.Value;
		}

		private Transform Target()
		{
			// point in a new random direction and then multiply that by the wander distance
			wanderTarget.transform.position = getTarget ();
			return wanderTarget.transform;
		}
		
		// Reset the public variables
		public override void OnReset()
		{
			wanderDistance = 20;
			wanderRate = 2;
		}
	}
}