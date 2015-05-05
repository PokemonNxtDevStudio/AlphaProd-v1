using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _delete1 : MonoBehaviour {

	// Use this for initialization


    public List<ItemBehavior> items;

    private TrainerController t;
	void Start ()
	{
	    t = GetComponent<TrainerController>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.H))
	    {
            items[0].DoMethod(t);
	    }
        if (Input.GetKeyDown(KeyCode.P))
        {
            items[1].DoMethod(t);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            items[2].DoMethod(t);
        }
	
	}
}
