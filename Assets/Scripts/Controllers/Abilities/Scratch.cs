using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NXT.Controllers;
using NXT;

public class Scratch : MoveBehavior
{


    bool isCasted;


    public override void Start()
    {

    }


    public void Update()
    {
        /*
        if (!isCasted)
        {
            if (animController.GetCurrentAnimatorStateInfo(0).IsName("Scratch") && animController.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                RaycastHit[] hits;

                hits = Physics.RaycastAll(gameObject.transform.position + new Vector3(0, 2, 3), Vector3.forward, 1000, LayerMask.NameToLayer("PokeLayer"));

                if (hits.Count() > 0)
                {
                    for (int i = 0; i < hits.Count(); i++)
                    {
                        if (hits[i].collider.gameObject.pokeController.isWild())
                        {
                            hits[i].collider.gameObject.pokeController.Damage(MoveData.damage);
                            InstantiteHitEffect(MoveData.vfx, hits[i].point);
                            isCasted = true;
                            break;
                        }
                    }
                }
            }
        }
         */
    }
    public override void UseMove()
    {

        //animation.trigger("Scratch");
    
        Debug.Log("casting tail whip");
    }


}
