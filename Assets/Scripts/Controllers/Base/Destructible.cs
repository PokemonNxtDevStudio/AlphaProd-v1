using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {
	public float timeBeforeDeath;
	// Use this for initialization
	protected virtual void Awake () {
		Debug.Log ("before death...!" + timeBeforeDeath);
		if(timeBeforeDeath > 0) 
			StartCoroutine ("TimeToDie");
	}
	
	// Update is called once per frame
	IEnumerator TimeToDie(){
		yield return new WaitForSeconds (timeBeforeDeath);
		DoDeath ();

	}

	public virtual void DoDeath(){
		StopAllCoroutines ();
		Debug.Log (gameObject + "destroyed");
	}


}
