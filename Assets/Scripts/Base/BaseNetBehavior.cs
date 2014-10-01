using UnityEngine;

using Photon;

namespace PokemonNXT.Net {

    /// <summary>
    /// Every Photon.Monobehavior in the project must inherit from this class
    /// This is used just for encapsulation purposes.
    /// That way whenever we want as the project is progressing, we will be able
    /// to easily add global methods or fieldsfor our gameobjects' networking behaviors
    /// </summary>
    public class BaseNetBehavior: Photon.MonoBehaviour, IBaseBehavior {        

        #region Unity Transform Utils
        public Vector3 pos {
            set { gameObject.transform.position = value; }
            get { return gameObject.transform.position; }
        }
        public float x {
            set { pos = new Vector3(value, y, z); }
            get { return pos.x; }
        }
        public float y {
            set { pos = new Vector3(x, value, z); }
            get { return pos.y; }
        }
        public float z {
            set { pos = new Vector3(x, y, value); }
            get { return pos.z; }
        }
        #endregion

        #region Photon Utils
        protected float PingInSeconds { get { return PhotonNetwork.GetPing() * 0.001f; } }
        public bool IsClientObject { get { return photonView.isMine; } }
        public bool IsRemoteObject { get { return !photonView.isMine; } }
        public bool IsMasterClientObject { get { return PhotonNetwork.isMasterClient; } }        
        #endregion
    }
}