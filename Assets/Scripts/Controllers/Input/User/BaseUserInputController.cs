using UnityEngine;

namespace NXT.Controllers {

    /// <summary>
    /// Any controller that is responsible for receiving input from the user should inherit from this class
    /// UserInput classes contain an Unity callback Update that updates the input and stores it into as many
    /// public properties as needed. Those can be accessed by the Controller handler and used to update other
    /// Controllers of the gameobject.
    /// 
    /// Another approach for handling the user input would be an InputManager global singleton for any gameobject
    /// that is controller by the user. However, that approach requires all the Input.GetStuff() methods for our
    /// gameplay to run all the time through the game.
    /// 
    /// On the other hand, seperating the User input to many controllers we only need any input necessary for
    /// the gameobject that is currently controlled by the player. Therefore less statements and better performance.
    /// </summary>
    public abstract class BaseUserInputController: BaseController {

        public abstract Vector3 Direction { get; protected set; }

        protected abstract void Update();
    }
}