using UnityEngine;
using System.Collections;

public class Projectile : Destructible {
	public float force = 10;
	public int speed = 1;
	public delegate void CollisionCallback(GameObject __obj);
	public event CollisionCallback collided;
	// Use this for initialization
	protected virtual void Awake () {
		//do throw here
		base.Awake ();

	}
	
	// Update is called once per frame
	protected virtual void Update () {
		GetComponent<Rigidbody> ().AddForce (transform.forward*force);
	}
//	throwItem(force)
//		overrideDestroy()
			
	public virtual void OnColliderHit(Collider __collision){
		collided (__collision.gameObject);
		DoDeath ();
	}

	public override void DoDeath(){
		base.DoDeath ();
		//death animations can go here too
		GameObject.Destroy(gameObject);
	}
}
