using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;

public class TrainerAI : MonoBehaviour {
	
	private MotorController motor;
	private AIPath pathfinder;
	private Transform _target;
	public BehaviorTree behaviourTree;


	public Transform target{
		get {
			return _target;
		}
		set {
			_target = value;
			((SharedTransform) behaviourTree.GetVariable ("target")).Value = _target;
			pathfinder.target = _target;
		}
	}
	// Use this for initialization
	void Awake () {
		Debug.Log ("Starto!");
		motor = GetComponent<MotorController> ();
		pathfinder = GetComponent<AIPath> ();
//		behaviourTree.StartWhenEnabled = false;
	}

	private void OnEnable(){
		motor.moveOverrde = true;
		pathfinder.enabled = true;
		behaviourTree.enabled = true;
//		behaviourTree.Start ();
//		behaviourTree.s
	}
	private void OnDisable(){
		motor.moveOverrde = false;
		pathfinder.enabled = false;
		behaviourTree.enabled = false;
//		behaviourTree.Sto ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
