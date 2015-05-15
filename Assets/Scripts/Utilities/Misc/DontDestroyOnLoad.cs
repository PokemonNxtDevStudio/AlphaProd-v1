using UnityEngine;

namespace NXT {

    public class DontDestroyOnLoad: BaseBehavior {

        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}