using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NXT.Controllers;



   public class MoveBehavior: MonoBehaviour
   {
       protected int m_MoveID{get;set;}
       private MoveData m_MoveData;
       private Pokemon m_Pokemon;

       public virtual void Start()
       {
           
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

       public static MoveBehavior AddComponent(GameObject target,MoveData moveData,Type T)
       {
           MoveBehavior mb= (MoveBehavior)target.AddComponent(T);
           mb.SetMoveData(moveData);
           return mb;
       }


   }

