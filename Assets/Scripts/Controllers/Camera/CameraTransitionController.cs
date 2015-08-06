using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class CameraTransitionController : MonoBehaviour {
	[SerializeField] private EaseType ease;
	[SerializeField] private float offsetY;
	[SerializeField] private Transform target;


//	private int currentTarget=0;
	private Tweener tween;
	// Use this for initialization
	void Start () {
//		if (m_cameraTargets.Length > 0)
		SetTarget (target, 0, true);
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyUp (KeyCode.Q)) {
			//cycle targets
//			currentTarget = currentTarget+1 < m_cameraTargets.Length ? currentTarget+1 : 0;
//			SetTarget (target);
//		}
	}

	public void SetTarget(Transform __target, float __delay = 0, bool __noAnim = false){
		transform.parent = __target;
		if (tween != null)
			tween.Kill ();
		tween = null;
		if (__noAnim) {
			transform.localPosition = new Vector3(0,offsetY,0);
		} else {
//			Vector3 localPos = transform.localPosition;
			tween = HOTween.To (gameObject.transform, .5f, new TweenParms ().Prop ( "localPosition", new Vector3 ( 0, offsetY, 0 )).Ease(ease).Delay(__delay));
		}
	}
}
