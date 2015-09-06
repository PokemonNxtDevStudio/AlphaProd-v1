using UnityEngine;
using System.Collections;


public class h_Wandering : MonoBehaviour
{
    [Header("Wander")]

    private Vector3 _SpawnPoint;

    [SerializeField]
    [Range(10, 100)]
    private int MaxRangeOfRoaming = 20;
    [SerializeField]
    private int MaxWalkDistance = 20;
    [SerializeField]
    private int MinWalkDistance = 4;

    private NavMeshAgent agent;
    

    void Start ()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        _SpawnPoint = gameObject.transform.position;
    }

    public void DoWandering()
    {
        if (!agent.hasPath)
        {
            agent.SetDestination(gameObject.transform.position + Vector3.forward);
            Debug.Log("Dint had a path");
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
                }
            }
        }  
    }
}
