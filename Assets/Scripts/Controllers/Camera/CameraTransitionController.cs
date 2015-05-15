using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class CameraTransitionController : MonoBehaviour {
	[SerializeField]
	private Transform[] m_cameraTargets;
	[SerializeField]
	private EaseType ease;
	private int currentTarget=0;
	private Tweener tween;
	// Use this for initialization
	void Start () {
		if (m_cameraTargets.Length > 0)
			SetTarget (m_cameraTargets[currentTarget], true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)) {
			//cycle targets
			currentTarget = currentTarget+1 < m_cameraTargets.Length ? currentTarget+1 : 0;
			SetTarget (m_cameraTargets[currentTarget], false);
		}
	}

	private void SetTarget(Transform __target, bool __noAnim = false){
		transform.parent = __target;
		if(tween != null) tween.Kill();
		tween = null;

		if (__noAnim) {
			transform.localPosition = Vector3.zero;
		} else {
			Vector3 localPos = transform.localPosition;
			tween = HOTween.To (gameObject.transform, .5f, new TweenParms ().Prop ( "localPosition", new Vector3 ( 0, 0, 0 )).Ease(ease));
		}
	}
}
