using System.Collections.Generic;

using UnityEngine;

using Photon;

using PokemonNXT.Controllers;

/// <summary>
/// Synchronizes position and rotation by default
/// 
/// Helpful info
/// Interpolation: estimating values within a series e.g. 1, 2, 3, x, y, 6, 7 (x = 4, y = 5) 
/// Extrapolation: estimating values outside a known series e.g. 2, 4, 6, x, y (x = 8, y = 10)
///
/// </summary>

namespace PokemonNXT.Net.Controllers {

    /// <summary>
    /// Used to handle multiple controllers attached onto a network gameobject
    /// Some of those controllers is needed to run only on the client of the owner when others need
    /// to run for any network object at any client.
    /// 
    /// When the components' microarchitecture of gameobjects starts to become a mess due to many dependencies
    /// an class that inherits from this script may help a lot with the health of our code
    /// </summary>
    public abstract class BaseNetControllerHandler: BaseNetController {

        #region Public Attributes
        public InputState InputState;
        //if client and remote object have distance smaller than this -> interpolation occurs
        //else -> extarpolation occurs (laggy transition to fix position)
        public float MaxPositionInterpolationDistance = 2f;
        #endregion

        #region Private Attributes
        #region Referenced Controllers
        private BaseAIInputController _aiInputCtrl;
        private BaseUserInputController _userInputCtrl;
        private BaseMotorController _motorCtrl;
        private BaseAnimatorController _animatorCtrl;
        private BaseCollisionController _collisionCtrl;
        #endregion
        #region Interpolation / Extrapolation Temps
        protected Vector3 RemotePosition;
        protected Quaternion RemoteRotation;
        #endregion
        #endregion

        #region Accessors
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

        #region Holding Controller Helpers
        protected virtual void ActivateHoldingControllers() {
            if(AIInputCtrl && IsClientObject) AIInputCtrl.Activate();
            if(UserInputCtrl && IsClientObject) UserInputCtrl.Activate();
            if(MotorCtrl) MotorCtrl.Activate();//&& IsClientObject
            if(AnimatorCtrl) AnimatorCtrl.Activate();
            if(CollisionCtrl && IsClientObject) CollisionCtrl.Activate();
        }

        protected abstract void UpdateAnimator();
        #endregion

        #region Networking Helpers
        protected override void SendData(PhotonStream stream, PhotonMessageInfo info) {
            stream.SendNext(pos);
            stream.SendNext(transform.rotation);
        }

        protected override void ReceiveData(PhotonStream stream, PhotonMessageInfo info) {
            RemotePosition = (Vector3)stream.ReceiveNext();
            RemoteRotation = (Quaternion)stream.ReceiveNext();
        }

        protected virtual void SyncWithRemote() {            
            ExtrapolatePosition();
            InterpolateRotation();
        }

        protected abstract void ExtrapolatePosition();
        protected abstract void InterpolateRotation();
        #endregion

        #region Unity API
        protected override void Awake() {
            Activate();
            AIInputCtrl = GetComponent<BaseAIInputController>();
            UserInputCtrl = GetComponent<BaseUserInputController>();
            MotorCtrl = GetComponent<BaseMotorController>();
            AnimatorCtrl = GetComponent<BaseAnimatorController>();
            CollisionCtrl = GetComponent<BaseCollisionController>();
        }

        protected virtual void Start() {
            TimeSinceLastSyncPlusPing = TimeSinceLastSync = 0.001f;
            LastSyncTime = PhotonNetwork.time;
            RemotePosition = transform.position;
            RemoteRotation = Quaternion.identity;

            ActivateHoldingControllers();
        }

        protected abstract void Update();
        #endregion
    }
}