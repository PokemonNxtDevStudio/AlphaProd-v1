using System.Collections.Generic;

using UnityEngine;

namespace PokemonNXT.Controllers {
    /// <summary>
    /// Wrapper of the Unity AnimatorController component for encapsulation purposes
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class BaseAnimatorController: BaseController {        

        public Animator Animator;


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
            base.Awake();
        }
    }
}