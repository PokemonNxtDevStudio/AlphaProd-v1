using UnityEngine;

namespace PokemonNXT {

    public class DontDestroyOnLoad: BaseBehavior {

        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}