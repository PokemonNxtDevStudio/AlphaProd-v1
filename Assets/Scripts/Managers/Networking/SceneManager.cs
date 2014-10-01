using UnityEngine;
///<summary>
///Manages the scene hierarchy and the level loading
///</summary>
namespace PokemonNXT.Managers {
    using Templates;

    public class SceneManager: SingletonNet<SceneManager>{

        private const int LOGIN = 0;
        private const int DEV_TEST = 1;

        private SceneManager() { }

        public void LoadLoginScene() {
            PhotonNetwork.LoadLevel(LOGIN);
        }

        public void LoadDevTestScene() {
            PhotonNetwork.LoadLevel(DEV_TEST);
        }

        public bool LevelIsLoading {
            get { return Application.isLoadingLevel; }
        }

        private void OnLevelWasLoaded(int level) {
            if(level == LOGIN) {
                ;//dont give a fuck for now
            }
            else if(level == DEV_TEST) {
                GameManager.Instance.InitDevTest();
            }
        }
    }
}