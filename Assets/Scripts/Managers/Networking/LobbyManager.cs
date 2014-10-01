using UnityEngine;
using System.Collections;
///<summary>
///Manages networking functionality before the main gameplay
///</summary>
namespace PokemonNXT.Managers {
    using Templates;
    using Net;

    public class LobbyManager: SingletonNet<LobbyManager> {        

        private LobbyManager() { }
        
        private void Awake() {
            PokemonNXT.Info("Connecting");
            if (!PhotonNetwork.connected)
                PhotonNetwork.ConnectUsingSettings("v1.0");
            PokemonNXT.Info("Connected: " + PhotonNetwork.connected);
        }

        private void OnJoinedLobby() {
            PokemonNXT.Info("Joining random existing room");
            if (PhotonNetwork.connected)
                PhotonNetwork.JoinRandomRoom();
        }

        private void OnPhotonRandomJoinFailed() {
            PokemonNXT.Info("Joining room failed, attempt to create a new one");
            if(PhotonNetwork.connected)
                PhotonNetwork.CreateRoom("Only room");
        }

        private void OnJoinedRoom() {
            PokemonNXT.Info("Joined room successfully");
            SceneManager.Instance.LoadDevTestScene();
        }
    }
}