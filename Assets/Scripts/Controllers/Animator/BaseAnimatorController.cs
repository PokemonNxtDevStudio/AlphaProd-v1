using System.Collections.Generic;

using UnityEngine;

namespace NXT.Controllers {
    /// <summary>
    /// Wrapper of the Unity AnimatorController component for encapsulation purposes
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class BaseAnimatorController: BaseController {        

        public Animator Animator;
        private Dictionary<string, int> m_StateNamesHash;
        public float moveSpeed;
        public Vector3 moveDir;
        public Transform target;
       
        public  Vector3 lastPosition = Vector3.zero;
        private Ability[] m_ActiveMove;
    
        public void SetBool(string key, bool b) {
            Animator.SetBool(key, b);            
        }
        public void SetBool(int key, bool b) {
            Animator.SetBool(key, b);
        }


        public void Trigger(string key) {
            Animator.SetTrigger(key);
        }
        public void Trigger(int key) {
            Animator.SetTrigger(key);
        }


        public void SetInteger(string key, int i) {
            Animator.SetInteger(key, i);
        }
        public void SetInteger(int key, int i) {
            Animator.SetInteger(key, i);
        }


        public void SetFloat(string key, float f) {
            Animator.SetFloat(key, f);
        }
        public void SetFloat(string key, float f, float dampTime, float deltaTime) {
            Animator.SetFloat(key, f, dampTime, deltaTime);
        }
        public void SetFloat(int key, float f) {
            Animator.SetFloat(key, f);
        }
        public void SetFloat(int key, float f, float dampTime, float deltaTime) {
            Animator.SetFloat(key, f, dampTime, deltaTime);
        }


        protected override void Awake() {
            Animator = GetComponent<Animator>();
            target = this.transform;
            base.Awake();
        }

        public bool ChangeAnimatorStates(MoveBehavior moveBehavior, int layer, string destinationState, float transitionDuration, float normalizedTime)
        {
            int nameHash;
            if (this.Animator.IsInTransition(layer))
            {
                AnimatorStateInfo animatorStateInfo = this.Animator.GetNextAnimatorStateInfo(layer);
                nameHash = animatorStateInfo.fullPathHash;
            }
            else
            {
                AnimatorStateInfo animatorStateInfo = this.Animator.GetCurrentAnimatorStateInfo(layer);
                nameHash = animatorStateInfo.fullPathHash;
            }

            if (string.IsNullOrEmpty(destinationState) || nameHash == Animator.StringToHash(destinationState))
                return false;
            AnimatorStateInfo animatorStateInfo1 = this.Animator.GetCurrentAnimatorStateInfo(layer);

            float f = (float)(transitionDuration / Animator.GetCurrentAnimatorStateInfo(layer).length);
            if (float.IsInfinity(f))
                f = 0.0f;
            this.Animator.CrossFade(this.GetStateNameHash(destinationState), f, layer, normalizedTime);
            //this.m_ActiveMove[layer] = moveBehavior;
            return true;
        }
        private int GetStateNameHash(string stateName)
        {
            int num1;
            if (this.m_StateNamesHash.TryGetValue(stateName, out num1))
                return num1;
            int num2 = Animator.StringToHash(stateName);
            this.m_StateNamesHash.Add(stateName, num2);
            return num2;
        }
    }
}