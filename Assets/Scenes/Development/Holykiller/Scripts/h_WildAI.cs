using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SphereCollider))]
public class h_WildAI : MonoBehaviour
{    
    private bool _alive = true;
    [SerializeField]
    private WildAIState _curState = WildAIState.Roaming;

    private WildAIState _roaming = WildAIState.Roaming;
    private WildAIState _attacking = WildAIState.Attack;

    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private Animator anim;

    [Header("Wander")]
    private Vector3 _SpawnPoint;
    [SerializeField]
    [Range(10, 100)]
    private int MaxRangeOfRoaming = 20;
    [SerializeField]
    private int MaxWalkDistance = 20;
    [SerializeField]
    private int MinWalkDistance = 4;
    [SerializeField]
    [Range(2, 11)]
    private int WonderPeriodicityInSecs = 3;
    //[SerializeField]
   // private h_Wandering _wandering
    private NavMeshAgent agent;

    

    

    [Header("Combat")]
    [SerializeField]
    private GameObject _mainTarget;

    private List<GameObject> targets = new List<GameObject>();
    [SerializeField]
    private bool _isAggressive = false;

    float normalSpeed = 8;


    void Start ()
    {
        anim = GetComponent<Animator>();
        _SpawnPoint = gameObject.transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        //_wandering = GetComponent<h_Wandering>();
        StartCoroutine("WildAI");
        agent.updatePosition = false;
    }
    IEnumerator WildAI()
    {
        while(_alive)
        {
            switch(_curState)
            {
                case WildAIState.idle:
                    break;
                case WildAIState.Roaming:
                    
                    if (_mainTarget != null)
                    {
                        _curState = _attacking;
                    }
                    else
                    {
                        if(agent.pathPending == false)
                        {
                            int ran = Random.Range(0, 10);
                            // 0 not to move
                            if (ran == 0)
                            {
                                //Debug.Log("WAITING");
                                yield return new WaitForSeconds(WonderPeriodicityInSecs);
                            }
                                                          
                            // 1 move to the next point
                            else
                            {
                                DoWandering();
                            }
                                
                        }
                        yield return new WaitForSeconds(WonderPeriodicityInSecs);
                    }
                  //  Debug.Log("Wainting " + WonderPeriodicityInSecs + " segs For next WONDERING");                   
                    break;
                case WildAIState.Attack:
                    //AttackAction();
                    yield return new WaitForSeconds(3);
                    break;
            }
        }
    }


   /* private void AttackAction()
    {
        
        else
        {
            
            if (IsMoving() == false)
            {

                agent.SetDestination(_mainTarget.transform.position);
                //_moving = false;
                Debug.Log("Not Moving Go TO Target");
                _locAgent.Moving = true;
            }
            
            

            
        }
    }*/
    void Update()
    {
        AnimUpdate();
                
       /* 
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Tackle();      
        }*/
        
    }

    void Tackle()
    {
        anim.SetTrigger("Tackle");

        agent.acceleration = 16;
        Vector3 front = transform.forward;//.TransformDirection(Vector3.forward);
        //front += Vector3.forward * 2f;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(front, out hit,2,1))
        {
            Debug.Log("next positon " + hit.position);
            agent.SetDestination(hit.position);
        }
        //Vector3 front = Physics.Raycast(origin, Vector3.forward, 2f,);
            //Vector3.forward * Time.deltaTime ;
       // agent.Move(front * Time.deltaTime);
        Invoke("ResetSpeed", 2f);
    }

    void ResetSpeed()
    {
        agent.acceleration = normalSpeed;
        
    }



    private void AnimUpdate()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

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

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

        // Update animation parameters
        anim.SetBool("move", shouldMove);
        anim.SetFloat("DirX", velocity.x);
        anim.SetFloat("DirY", velocity.y);
        
    }
    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = agent.nextPosition;
    }

    private bool IsMoving()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {

                    return false;
                }
            }
        }
        return true;
    }


    #region Wandering

    public void DoWandering()
    {
        if (!agent.hasPath)
        {
            agent.SetDestination(gameObject.transform.position + Vector3.forward);
            //Debug.Log("Dint had a path");
        }
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    Vector3 nextPos;

                    //Check if its with in the limited range of roaming
                    float curdistance = Vector3.Distance(gameObject.transform.position, _SpawnPoint);
                    //  Debug.Log("CurDistands From the SpawnPoint " + curdistance);
                    if (curdistance > MaxRangeOfRoaming)
                    {
                        nextPos = _SpawnPoint;

                    }
                    else
                    {
                        int maxMove = Random.Range(MinWalkDistance, MaxWalkDistance);
                        Vector3 randomDir = Random.insideUnitSphere * maxMove;
                        randomDir += transform.position;
                        NavMeshHit hit;
                        NavMesh.SamplePosition(randomDir, out hit, maxMove, 1);
                        //Vector3 finalPosition = hit.position;
                        nextPos = hit.position;

                    }
                   
                    agent.SetDestination(nextPos);
                    
                    //anim.SetFloat("DirX", agent.desiredVelocity.magnitude);


                }
            }
        }
    }

    #endregion

    public void ReceiveDMG(GameObject attacker)
    {
        if(_mainTarget == null)
        {
            _mainTarget = attacker;
            _curState = _attacking;
        }
        if(!targets.Contains(attacker))
            targets.Add(attacker);
        if (_curState != _attacking)
            _curState = _attacking;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAggressive)
        {
            if (other.tag == "Pokemon")
            {
                targets.Add(other.gameObject);
                if (_mainTarget == null)
                {
                    _mainTarget = other.gameObject;
                    // _targetInRange = true;
                    _curState = _attacking;
                }
            }
        }
        #region Future
        //For Later if pokemon are able to attack the player   
        /*
        else if(other.tag == "Player")
        {
            bool otherPokeInRange = false;
            int indexOfPoke = 0;
           

            for(int i = 0; i < targets.Count;i++)
            {
                if(targets[i].tag == "Pokemon")
                {
                    otherPokeInRange = true;
                    indexOfPoke = i;
                    return;
                }
            }
            targets.Add(other.transform);
            if (MainTarget == null)
            {
                MainTarget = other.transform;                           
            }                
        }*/
        #endregion
    }

    private void OnTriggerExit(Collider other)
    {
        if(_mainTarget != null)
        {
            if (other.gameObject == _mainTarget.gameObject)
            {
                if (targets.Contains(other.gameObject))
                {
                    targets.Remove(other.gameObject);
                    //Look for another target
                    if(targets.Count > 1)
                    {
                        int i = Random.Range(0, targets.Count);
                        _mainTarget = targets[i];
                        _curState = _attacking;
                    }
                    else
                    {
                        _mainTarget = null;
                        _curState = _roaming;
                    }                    
                    // _targetInRange = true;                    
                }
            }
        }
        else
        {
            if(targets.Contains(other.gameObject))
                targets.Remove(other.gameObject);
        }        
    }
    
}

public enum WildAIState
{
    idle,
    Attack,
    //Run,
    Roaming,
}
