using UnityEngine;
using System.Collections;

public class PlayVFX : MonoBehaviour 
{
    public ParticleSystem FVXToPlay;


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
        if(Input.GetButtonDown("Fire1")/*GetKeyDown(/*KeyCode.Alpha0)*/)
        {
            if(FVXToPlay != null)
            {
                FVXToPlay.Play();
                //Debug.Log("Playing " + FVXToPlay.name);
            }
        }
	}
}
