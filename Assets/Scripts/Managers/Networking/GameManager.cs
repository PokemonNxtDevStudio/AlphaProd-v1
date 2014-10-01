using UnityEngine;

using PokemonNXT.Templates;
using PokemonNXT.Controllers;
///<summary>
///Instantiatiation & pooling of game objects
///</summary>
namespace PokemonNXT.Managers {    

    public class PlayerObjectPair {
        public PhotonPlayer Player;
        public GameObject Object;

        public PlayerObjectPair() {
            Player = null;
            Object = null;
        }
        public PlayerObjectPair(GameObject obj) {
            PokemonNXT.Assert(obj.GetPhotonView(), "PlayerObjectPair", "PlayerObjectPair(obj)", "The game object must have a PhotonView component attached");
            Object = obj;
            Player = obj.GetPhotonView().owner;
        }
        public PlayerObjectPair(PhotonPlayer player, GameObject obj) {
            PokemonNXT.Assert(obj.GetPhotonView(), "PlayerObjectPair", "PlayerObjectPair(player, obj)", "The game object must have a PhotonView component attached");
            Player = player;
            Object = obj;
        }
    }

    public class GameManager: SingletonNet<GameManager> {

        private PlayerObjectPair me;

        private GameManager() { }

        public PlayerObjectPair Me { get { return me; } }

        public void InitDevTest() {
            me = new PlayerObjectPair(
                PhotonNetwork.Instantiate(ResourcesPathHolder.PokemonPrefab("025 Pikachu"), Vector3.zero, Quaternion.identity, 0)
            );
            ThirdPersonCameraCtrl thirdPersonCamera = Camera.main.GetComponent<ThirdPersonCameraCtrl>();
            thirdPersonCamera.target = me.Object.transform;
            thirdPersonCamera.objectsToRotate.Add(me.Object.transform);
        }
    }
}