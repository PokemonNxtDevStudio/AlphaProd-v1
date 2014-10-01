using UnityEngine;
using System.Collections;

namespace PokemonNXT.Net.Controllers {

    /// <summary>
    /// //@IFDO: Later on we need to check if it better to use a global photonView per client that is used by every net controller
    ///          However, if we just deactivate and activate the photonView accordingly, everything is probably gonna be fine. Well tests will tell.
    /// 
    /// All Networking Controller classes must inherit from this class.
    /// NetControllers are used for objects that are needed to be serialized over the network (should be updated on every frame)
    /// Usually this kind of Controllers are going to be NetControllerHandlers that refer to all the local behaviors
    /// and manage their functionality based on if the gameobject is owned by the local client or by a remote one.
    /// 
    /// When a Networking Controller is being held by a Networking Controller handler the base Awake callback
    /// should be called by it in order to disable the script. Then it is the job of the controller handler
    /// to Activate or Deactivate the controller based on when it is needed.
    /// All initializations should take place in the Start unity callback or on the overriden Awake method
    /// </summary>
    [RequireComponent(typeof(PhotonView))]
    public abstract class BaseNetController: BaseNetBehavior {

        #region Interpolation / Extrapolation attributes
        private double _lastSyncTime,
                       _timeSinceLastSync,
                       _timeSinceLastSyncPlusPing;        
        #endregion

        #region Accessors
        protected double TimeSinceLastSyncPlusPing {
            get { return _timeSinceLastSyncPlusPing; }
            set { _timeSinceLastSyncPlusPing = value; }
        }
        protected double TimeSinceLastSync {
            get { return _timeSinceLastSync; }
            set { _timeSinceLastSync = value; }
        }
        protected double LastSyncTime {
            get { return _lastSyncTime; }
            set { _lastSyncTime = value; }
        }
        #endregion

        #region (De)Activators
        public virtual void Activate() {
            photonView.enabled = true;
            photonView.observed = this;            
            name += "::" + photonView.viewID.ToString();
            
            if(photonView.synchronization == ViewSynchronization.Off) {
                photonView.synchronization = ViewSynchronization.Unreliable;
                PokemonNXT.Warn("BaseNetController", "Activate", "The network controller of gameobject: " + name
                              + " has its obseve option OFF but it was manually set to UNRELIABLE");
            }
            enabled = true;
        }

        public virtual void Deactivate() {
            photonView.enabled = false;
            enabled = false;
        }
#       endregion

        #region Networking Helpers
        protected abstract void SendData(PhotonStream stream, PhotonMessageInfo info);
        protected abstract void ReceiveData(PhotonStream stream, PhotonMessageInfo info);
        #endregion

        protected virtual void Awake() {
            enabled = false;
        }

        protected void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if(stream.isWriting)
                SendData(stream, info);
            else {
                _lastSyncTime = info.timestamp;
                _timeSinceLastSync = (float)(PhotonNetwork.time - _lastSyncTime);
                _timeSinceLastSyncPlusPing = _timeSinceLastSync + PingInSeconds;
                ReceiveData(stream, info);
            }
        }
    }
}