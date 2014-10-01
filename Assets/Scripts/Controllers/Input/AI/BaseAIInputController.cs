using UnityEngine;

namespace PokemonNXT.Controllers {

    /// <summary>
    /// Every AI controller should inherits from this class.
    /// 
    /// In the future, the AI controllers may include seperate model instances that run AI algorithms.
    /// Those should output what the controllers need in order to 
    /// </summary>
    public abstract class BaseAIInputController: BaseController {

        public abstract Vector3 Direction { get; protected set; }

        protected abstract void Update();
    }
}