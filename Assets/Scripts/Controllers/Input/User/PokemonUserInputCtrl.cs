using UnityEngine;

namespace PokemonNXT.Controllers {

    public class PokemonUserInputCtrl: CharacterUserInputCtrl {
        
        public bool Jump { get; protected set; }

        protected override void Start() {
            base.Start();
            Jump = false;
        }

        protected override void Update() {
            base.Update();
            Jump = Input.GetButtonUp("Jump");
        }
    }
}
