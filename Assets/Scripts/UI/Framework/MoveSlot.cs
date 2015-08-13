using UnityEngine;
using System.Collections;


[RequireComponent(typeof(ProgressBar))]
public class MoveSlot : IconSlot
{



    public ProgressBar cooldown; //using radial spin


    private MoveData moveData = null;

    // Use this for initialization
    void Start()
    {

        cooldown = GetComponent<ProgressBar>();
    }



    void SetData(MoveData moveData)
    {
        moveData = this.moveData;
       
    }
    public void UseMove()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
