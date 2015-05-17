using UnityEngine;
using System.Collections;

public class Projectile : Destructible {
	public Vector3 direction;
	public float force = 10;
	public int speed = 1;
	public delegate void CollisionCallback(GameObject __obj);
	public event CollisionCallback collided;
	// Use this for initialization
	protected virtual void Awake () {
		//do throw here
		base.Awake ();
		
		GetComponent<Rigidbody> ().AddForce (direction*force);
	}
	
	// Update is called once per frame
	void Update () {
	
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
		GameObject.Destroy(this);
	}
}
