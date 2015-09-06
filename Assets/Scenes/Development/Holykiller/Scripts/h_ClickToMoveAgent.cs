using UnityEngine;

public class h_ClickToMoveAgent : MonoBehaviour
{
    private NavMeshAgent agent;
    public int MoveWithClick = 0;
    void Start ()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }
	void Update ()
    {
        if (Input.GetMouseButtonDown(MoveWithClick))
        {
            cameraTOMapRayCast();
        }
    }

    private void cameraTOMapRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
            agent.SetDestination(hit.point);        
    }
}
