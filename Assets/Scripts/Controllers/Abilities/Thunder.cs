using UnityEngine;
using NXT;

public class Thunder : MoveBehavior
{

    void Start()
    {
        EventHandler.RegisterEvent<Vector3>(this.gameObject, EventAOE.EXECUTE, ExcecuteThunder);
    }

    /// <summary>
    /// Current for the player: Look at AOEMonitor Implementation
    /// </summary>
    public override void UseMove()
    {
        EventHandler.ExecuteEvent(this.gameObject, EventAOE.TRIGGER_ON);
    }

    void ExcecuteThunder(Vector3 hit)
    {
  
        Collider[] hits = Physics.OverlapSphere(hit, 5);
       for(int i =0 ; i <hits.Length; i++)
       {
           Debug.Log("Hit " + hits[i].name);
       }
    }



}

