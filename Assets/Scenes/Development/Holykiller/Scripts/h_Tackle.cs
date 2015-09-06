using UnityEngine;
using System.Collections;

public class h_Tackle : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;

    float normalSpeed = 8;
    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {           
            anim.SetTrigger("Tackle");
           
            agent.acceleration = 16;
            agent.SetDestination(transform.position + Vector3.forward * 2.5f);
            Invoke("ResetSpeed", 2f);
        }

    }


    void ResetSpeed()
    {
        agent.acceleration = normalSpeed;
       
    }
}
