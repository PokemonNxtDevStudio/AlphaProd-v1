using UnityEngine;

namespace PokemonNXT.Controllers {

    /// <summary>
    /// Any Collision Unity callback should be implemented in classes that inherit from this class
    /// Lets leave this here for now and the time will tell if this is really helpful or not
    /// </summary>
    public class BaseCollisionController: BaseController {

        public LayerMask IgnoredLayers; //or TriggerLayers ?

    }
}