using UnityEngine;

namespace PokemonNXT.Controllers {

    /// <summary>
    /// All Controller classes must inherit from this class.
    /// When a controller is being held by a controller handler the base Awake callback should be called by it in order to disable the script
    /// Then it is the job of the controller handler to Activate or Deactivate the controller based on when it is needed
    /// All initialization should take place in the Start unity callback
    /// </summary>
    public class BaseController: BaseBehavior {        

        public virtual void Activate() {
            enabled = true;
        }
        public virtual void Deactivate() {
            enabled = false;
        }

        protected virtual void Awake() {
            enabled = false;
        }
    }

}