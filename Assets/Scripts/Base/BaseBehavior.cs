using UnityEngine;

namespace PokemonNXT {

    /// <summary>
    /// Every Monobehavior in the project must inherit from this class
    /// This is used just for encapsulation purposes.
    /// That way whenever we want as the project is progressing, we will be able
    /// to easily add global methods or fields for our gameobjects' behaviors
    /// </summary>
    public class BaseBehavior: MonoBehaviour, IBaseBehavior {

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
        public bool IsClientObject { get { return true; } }
        public bool IsRemoteObject { get { return false; } }
        public bool IsMasterClientObject { get { return false; } }
        #endregion
    }
}