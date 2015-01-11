using UnityEngine;
using System.Collections;

public class SpriteTrigger : MonoBehaviour {


    public UISprite target;
	// Use this for initialization
	public void DisableSprite()
    {
        target.enabled = false;
    }
    public void EnableSprite()
    {
        target.enabled = true;
    }


}
