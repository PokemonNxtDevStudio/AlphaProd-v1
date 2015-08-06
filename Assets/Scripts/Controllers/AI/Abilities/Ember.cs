using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NXT.Controllers;
using NXT;

public class Ember : MoveBehavior
{

    private GameObject EmberFBX;//TODO MOVEDATA
    private string path = "Prefabs/Effects/Pokemon_Moves/Fire/Ember_v2";  //TODO MOVEDATA
//    private PokeController pcontrol;
    private bool canSpawn = true;
//    private float cooldown = .5f;
//    private float counter = 0;
    private float rotateAngle;
//    private float speed = 10;
    private Transform mouthAnchor;
    public override void Start()
    {
        base.Start();

        mouthAnchor = GetComponent<AnchorCache>().mouthAnchor;
        if (!mouthAnchor)
            Debug.Log("Please add mouthanchor");            
        ///EmberFBX = Resources.Load(path) as GameObject;
//        pcontrol = gameObject.GetComponent<PokeController>();
        
    }

    public override void UseMove()
    {

//        Vector3 facingAngle = Camera.main.transform.eulerAngles;
        //Vector3 facePos = Camera.main.transform.position;
        //smoothing + optiomiation
        //Olday way, doesnt resolve rotations on 2/4 quadrant, need use Quaternions
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.up * facingAngle.y, 7 * Time.deltaTime);




        //Camera.main.transform.eulerAngles = new Vector3(facingAngle.x, facingAngle.y, facingAngle.z);
        rotateAngle = Camera.main.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(rotateAngle * Vector3.up);
       // Camera.main.transform.position = new Vector3(facePos.x, facePos.y, facePos.z); 
        if(canSpawn == true)
        {

            animController.CrossFade(Animator.StringToHash("Ember"), 0.2f, 0, 0);
          
            //EmberFBX.transform.position = spawnPoint.position;
            
            canSpawn = false;
            StartCoroutine("Counter");

            Debug.Log("casting Ember!");

        }
        
    }


    private void Update()
    {

        

    
        if(EmberFBX != null)
        {
            //Debug.Log("spawned");
           //  EmberFBX.transform.localPosition += ( Vector3.forward * Time.deltaTime * speed);
            //EmberFBX.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
        }
    }

    IEnumerator Counter()
    {

        while (true)
        {
          
            if (animController.GetCurrentAnimatorStateInfo(0).IsName("Ember") && animController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.35f)
            {
                EmberFBX = Instantiate(Resources.Load(path), mouthAnchor.position, mouthAnchor.rotation) as GameObject;
                break;
            }
            yield return null;
        }
       // Destroy(EmberFBX);

        canSpawn = true;
        yield return null;
 

    }


}