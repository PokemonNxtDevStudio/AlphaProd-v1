using UnityEngine;
using System.Collections;

public class LinearProjectile : Projectile
{
    public delegate void CollisionCallback(GameObject __obj);
    public event CollisionCallback collided;

    private Rigidbody rb;
    private Vector3 direction;
    private GameObject onDestroyVFX;
    // Use this for initialization
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //do throw here
        base.Awake();

    }

    // Update is called once per frame
    protected override void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = ray.direction.normalized;
        rb.velocity = dir * speed;
        //GetComponent<Rigidbody> ().AddForce (transform.forward*force);
    }
    //	throwItem(force)
    //		overrideDestroy()

    public void OnCollisionEnter(Collision collision)
    {

        Debug.Log("HIT these object " + collision.gameObject.name);
        //collided(collision.gameObject);
        //DoDeath();
    }

    public override void DoDeath()
    {
     
        
        //todo ember explodes
        base.DoDeath();
        //death animations can go here too
        GameObject.Destroy(gameObject);
    }
}
