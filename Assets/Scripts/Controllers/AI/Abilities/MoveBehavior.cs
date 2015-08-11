using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NXT.Controllers;


namespace NXT{
   public class MoveBehavior: MonoBehaviour
   {
       protected int m_MoveID{get;set;}
       private MoveData m_MoveData;
       private Pokemon m_Pokemon;
       public Animator animController;
       public virtual void Start()
       {
           if(m_MoveData == null)
          m_MoveData = new MoveData();
          EventHandler.RegisterEvent(this.gameObject, "RightClick", new Action(StopMove));
          animController = GetComponent<Animator>();
       }
       public MoveBehavior SetMoveData(MoveData moveData)
       {
           
           this.m_MoveData = moveData;
           return this;
       }
       public virtual void UseMove()
       {
           //Debug.Log("casting tail whip child");
       }

       protected virtual void StopMove()
       {

       }
       public static MoveBehavior AddComponent(GameObject target,MoveData moveData,Type T)
       {
           MoveBehavior mb= (MoveBehavior)target.AddComponent(T);
           mb.SetMoveData(moveData);
           return mb;
       }

        void OnDestroy()
    {
        EventHandler.UnregisterEvent(this.gameObject, "RightClick", StopMove);
    }


   }
}
