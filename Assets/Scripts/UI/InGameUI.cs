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
            ActiveWindow(Transition.Out, TransitionType.Zoom);
        }
	}
}
