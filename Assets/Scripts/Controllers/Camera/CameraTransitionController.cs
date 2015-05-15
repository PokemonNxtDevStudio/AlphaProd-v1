using UnityEngine;
using System.Collections;

public class CameraTransitionController : MonoBehaviour {
	[SerializeField]
	private Transform[] m_cameraTargets;
	private Transform m_currentTarget;
	// Use this for initialization
	void Start () {
		if (m_cameraTargets.Length > 0)
			m_currentTarget = m_cameraTargets [0];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)) {
			//cycle targets

		}
	}

	private void SetTarget(Transform __target){

	}
}
