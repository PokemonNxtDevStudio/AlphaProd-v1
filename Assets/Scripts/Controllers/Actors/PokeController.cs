﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Reflection;
using NXT;
namespace NXT.Controllers
{
    public class PokeController : MonoBehaviour
    {

        public Pokemon m_Pokemon;
        private Animator m_Anim;
        public List<MoveData> moves = new List<MoveData>();



        private Dictionary<int, MoveBehavior> currentMoveBehaviors = new Dictionary<int, MoveBehavior>();
        protected virtual void Start()
        {

            //moves = m_Pokemon.Moves;
            for (int i = 0; i < moves.Count; i++)
            {
                Type t = Type.GetType("TailWhip");
                currentMoveBehaviors.Add(0, ((MoveBehavior)this.gameObject.AddComponent(t)).SetMoveData(moves[i]));
            }
            Type t1 = Type.GetType("Tackle");
            currentMoveBehaviors.Add(0, (MoveBehavior)this.gameObject.AddComponent(t1));

            Type t2 = Type.GetType("Thunder");
            currentMoveBehaviors.Add(0, (MoveBehavior)this.gameObject.AddComponent(t2));

            Type t3 = Type.GetType("TailWhip");
            currentMoveBehaviors.Add(0, (MoveBehavior)this.gameObject.AddComponent(t3));
        }


        protected void CastMove(int id)
        {
            currentMoveBehaviors[id].UseMove();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                CastMove(0);
            if (Input.GetKeyDown(KeyCode.W))
                CastMove(1);
            if (Input.GetKeyDown(KeyCode.E))
                CastMove(2);



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

    }
}
