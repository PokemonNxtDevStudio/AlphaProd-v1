using UnityEngine;
using System.Collections;
using Pathfinding;

public class h_WanderingPF : MonoBehaviour
{
    private Seeker seeker;
    public GameObject target;
    private MotorController motor;


    private Path path;
    private float nextWPDistance = 3;
    private int curWP = 0;

    private Animator AnimatorCtrl;
    private Rigidbody rb;
    // private CharacterController cct;



    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;

    //private Transform myTransform;

    void Start ()
    {
        seeker = GetComponent<Seeker>();
        motor = GetComponent<MotorController>();
        AnimatorCtrl = GetComponent<Animator>();
        //cct = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //myTransform = transform;

    }
	

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            seeker.StartPath(transform.position, target.transform.position, OnPathComlete);
        }

        if (path == null)
            return;

      /*  if(rb.velocity.magnitude < 1)
        {
            if(AnimatorCtrl.GetFloat("DirX") > 0 || AnimatorCtrl.GetFloat("DirX") < 0 || 
                AnimatorCtrl.GetFloat("DirY") > 0 || AnimatorCtrl.GetFloat("DirX") < 0)
            {
                AnimatorCtrl.SetFloat("DirX", 0);
                AnimatorCtrl.SetFloat("DirY", 0);
            }
        }*/
        if(curWP >= path.vectorPath.Count)
        {
            Debug.Log("END of the Path Reached");
            
            return;
        }

        Vector3 worldDeltaPosition = path.vectorPath[path.vectorPath.Count-1] - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        // bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        //anim.SetBool("move", shouldMove);
        AnimatorCtrl.SetFloat("DirX", velocity.x);
        AnimatorCtrl.SetFloat("DirY", velocity.y);

        

        Vector3 dir = (path.vectorPath[curWP] - transform.position).normalized;
        // dir *= 250 *Time.deltaTime;
        //cct.SimpleMove(dir);
        Transform myTransform = transform;
        myTransform.LookAt(path.vectorPath[curWP]);

        transform.rotation = Quaternion.Lerp(transform.rotation, myTransform.rotation, Time.deltaTime * 10);

        transform.rotation.SetLookRotation(path.vectorPath[curWP]);
        //transform.LookAt(path.vectorPath[curWP]);
        motor.Move(dir);
        //transform.LookAt(dir);
        if(Vector3.Distance(transform.position,path.vectorPath[curWP])< nextWPDistance)
        {
            curWP++;
            
            return;
        }

    }

    public void OnPathComlete(Path p)
    {
        if(!p.error)
        {
            path = p;
            curWP = 0;
            //Debug.Log("Get next point");
        }
        
    }
}
