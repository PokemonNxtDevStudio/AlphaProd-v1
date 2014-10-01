using UnityEngine;

namespace PokemonNXT.Controllers {

    public class CharacterUserInputCtrl: BaseUserInputController {

        public override Vector3 Direction { get; protected set; }

        protected virtual void Start() {
            Direction = Vector3.zero;
        }

        protected override void Update() {
            Direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}
