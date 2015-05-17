using UnityEngine;
using System.Collections;

public class Projectile : Destructible {
	public int speed;
	public Vector3 force;
	public delegate void CollisionCallback(GameObject __obj);
	public event CollisionCallback collided;
	// Use this for initialization
	void Awake () {
		//do throw here
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
		//death animations can go here too
		GameObject.Destroy(this);
	}
}
