using UnityEngine;
using System.Collections.Generic;

namespace PokemonNXT.Controllers {

    /// <summary>
    /// Used to handle multiple controllers attached onto a purely local gameobject (not synced over the network)
    /// When the components' microarchitecture of gameobjects starts to become a mess due to many dependencies
    /// an class that inherits from this script may help a lot with the health of our code
    /// </summary>
    public abstract class BaseControllerHandler: BaseController {

        public InputState InputState;

        #region Referenced controllers
        private BaseAIInputController _aiInputCtrl;
        private BaseUserInputController _userInputCtrl;
        private BaseMotorController _motorCtrl;
        private BaseAnimatorController _animatorCtrl;
        private BaseCollisionController _collisionCtrl;
        #endregion

        #region Accessors
        //Hopefully, this protected will not be needed to change to public.
        //If it does, swap all "set" with "protected set"
        protected BaseAIInputController AIInputCtrl {
            get { return _aiInputCtrl; }
            set { _aiInputCtrl = value; }
        }
        protected BaseUserInputController UserInputCtrl {
            get { return _userInputCtrl; }
            set { _userInputCtrl = value; }
        }
        protected BaseMotorController MotorCtrl {
            get { return _motorCtrl; }
            set { _motorCtrl = value; }
        }
        protected BaseAnimatorController AnimatorCtrl {
            get { return _animatorCtrl; }
            set { _animatorCtrl = value; }
        }
        protected BaseCollisionController CollisionCtrl {
            get { return _collisionCtrl; }
            set { _collisionCtrl = value; }
        }
        #endregion

        protected abstract void UpdateAnimator();         

        #region Unity API
        protected override void Awake() {
            AIInputCtrl = GetComponent<BaseAIInputController>();
            UserInputCtrl = GetComponent<BaseUserInputController>();
            MotorCtrl = GetComponent<BaseMotorController>();
            AnimatorCtrl = GetComponent<BaseAnimatorController>();
            CollisionCtrl = GetComponent<BaseCollisionController>();
        }

        protected virtual void Start() {
            if(AIInputCtrl) AIInputCtrl.Activate();
            if(UserInputCtrl) UserInputCtrl.Activate();
            if(MotorCtrl) MotorCtrl.Activate();
            if(AnimatorCtrl) AnimatorCtrl.Activate();
            if(CollisionCtrl) CollisionCtrl.Activate();
        }

        protected virtual void Update() {
            if(InputState == InputState.AI)
                MotorCtrl.Move(AIInputCtrl.Direction);
            else if(InputState == InputState.User)
                MotorCtrl.Move(UserInputCtrl.Direction);
            UpdateAnimator();
        }
        #endregion
    }
}