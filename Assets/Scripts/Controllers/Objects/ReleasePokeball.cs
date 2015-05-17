using UnityEngine;
using System.Collections;

public class ReleasePokeball : Projectile {
	public delegate void ReleaseComplete(GameObject __pokemon, Transform __origin);
	public event ReleaseComplete releaseComplete;
	public GameObject pokemon;
	// Use this for initialization
	protected override void Awake () {
//		timeBeforeDeath = 1f;
		base.Awake ();
	}
	
	// Update is called once per frame
	override protected void Update () {
		base.Update ();
	}

	public override void DoDeath(){
		base.DoDeath ();
		releaseComplete (pokemon, transform);
	}
}
