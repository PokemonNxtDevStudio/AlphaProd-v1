using UnityEngine;
using System.Collections;

public class InGameUI : UIWindow
{

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (cInput.GetKeyDown("Use"))
        {
            Debug.Log("Showing ui");
        }
	}
}
