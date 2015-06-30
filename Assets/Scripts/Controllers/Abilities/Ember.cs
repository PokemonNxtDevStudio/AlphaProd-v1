using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NXT.Controllers;
using NXT;

public class Ember : MoveBehavior
{

    private GameObject EmberFBX;
    private string path = "Prefabs/Effects/Pokemon_Moves/Fire/Ember_v2";
    private PokeController pcontrol;
    private bool canSpawn = true;
    private float cooldown = 4.5f;
    private float counter = 0;
    
    private float speed = 10;

    public override void Start()
    {
        ///EmberFBX = Resources.Load(path) as GameObject;
        pcontrol = gameObject.GetComponent<PokeController>();
        
    }

    public override void UseMove()
    {
        if(canSpawn == true)
        {
            EmberFBX = Instantiate(Resources.Load(path), pcontrol.SpawnPointMouth.position, pcontrol.SpawnPointMouth.rotation) as GameObject;
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
            EmberFBX.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
        }
    }

    IEnumerator Counter()
    {

        yield return new WaitForSeconds(cooldown);
        Destroy(EmberFBX);

        canSpawn = true;
        
        StopCoroutine("Counter");

    }


}