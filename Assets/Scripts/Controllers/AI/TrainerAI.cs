using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;

public class TrainerAI : MonoBehaviour {
	
	private MotorController motor;
	private Transform _target;
	public BehaviorTree behaviourTree;

	public Transform target{
		get {
			return _target;
		}
		set {
			_target = value;
			((SharedTransform) behaviourTree.GetVariable ("target")).Value = _target;
		}
	}
	// Use this for initialization
	void Awake () {
		Debug.Log ("Starto!");
		motor = GetComponent<MotorController> ();
//		behaviourTree.StartWhenEnabled = false;
	}

	private void OnEnable(){
		motor.moveOverrde = true;
		behaviourTree.enabled = true;
//		behaviourTree.Start ();
//		behaviourTree.s
	}
	private void OnDisable(){
		motor.moveOverrde = false;
		behaviourTree.enabled = false;
//		behaviourTree.Sto ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
