﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using NXT;
namespace NXT.Controllers
{
    public class PokeController : MonoBehaviour//Entity
    {

        public Pokemon m_Pokemon;
        private Animator m_Anim;
        public List<MoveData> moves = new List<MoveData>();



        private Dictionary<int, MoveBehavior> currentMoveBehaviors = new Dictionary<int, MoveBehavior>();
        protected virtual void Start()
        {

            //HERE
            
            //moves = m_Pokemon.Moves;
            for (int i = 0; i < moves.Count; i++)
            {
                Type t = Type.GetType("TailWhip");
                currentMoveBehaviors.Add(0, ((MoveBehavior)this.gameObject.AddComponent(t)).SetMoveData(moves[i])); //SetModeAI
            }


            Type t1 = Type.GetType("Tackle");
            currentMoveBehaviors.Add(0, (MoveBehavior)this.gameObject.AddComponent(t1));

            Type t2 = Type.GetType("Thunder");
            currentMoveBehaviors.Add(1, (MoveBehavior)this.gameObject.AddComponent(t2));

            Type t3 = Type.GetType("TailWhip");
            currentMoveBehaviors.Add(2, (MoveBehavior)this.gameObject.AddComponent(t3));

            Type t4 = Type.GetType("Scratch");
            currentMoveBehaviors.Add(3, (MoveBehavior)this.gameObject.AddComponent(t4));
                
            Type t5= Type.GetType("Ember");
            currentMoveBehaviors.Add(4, (MoveBehavior)this.gameObject.AddComponent(t5));

            Cursor.lockState = CursorLockMode.Locked;
        }


        protected void CastMove(int id)
        {
            currentMoveBehaviors[id].UseMove();
        }
        void Update()
        {


            

            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Alpha1))
                CastMove(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                CastMove(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                CastMove(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                CastMove(3);
            if (Input.GetKeyDown(KeyCode.Alpha5))
                CastMove(4);

            if (Input.GetMouseButtonDown(1))
            {
                EventHandler.ExecuteEvent(this.gameObject, "RightClick");
            }
        }
        public void UsePP(int PP)
        {
            m_Pokemon.currentPP -= PP;
            if (m_Pokemon.currentPP < 0)
                m_Pokemon.PP = 0;
        }

        public virtual void OnDeath()
        {
            //TODO kill all references
            Destroy(gameObject);
        }

    }
}
