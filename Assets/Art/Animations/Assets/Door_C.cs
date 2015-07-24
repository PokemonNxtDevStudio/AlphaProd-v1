using UnityEngine;
using System.Collections;


public class Door_C : MonoBehaviour 
{

    private Animator anim;
    private BoxCollider col;

	void Start ()
    {
        anim = gameObject.GetComponent<Animator>();
        
        
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("Player enter");
            anim.SetBool("Open", true);
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
           // Debug.Log("Player Exit");
            anim.SetBool("Open", false);
        }
    }
    


}
