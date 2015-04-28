using UnityEngine;
using Client;
namespace PokemonNXT.Controllers {
    
    /// <summary>
    /// Any behavior that implements movement related functionality should inherit from this class.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BaseAnimatorController))]
    public abstract class BaseMotorController: BaseController {
    //    /// <summary>
    //    /// This will control the various modes. We should create an ENUM folder for this 
    //    /// </summary>
    //    public enum Mode {
    //        Auto,
    //        // Select based on which component is attached to the same GameObject
    //        Basic,
    //        // Transform movement based on targetVelocity and drag - gravity is ignored
    //        Physics,
    //        // Rigidbody movement with drag, gravity and groundedness check
    //        Navigation
    //        // AI pathfinding movement
    //    };
    //    public Mode mode;
        //public Transform transform;
        public BaseAnimatorController AnimatorCtrl;        
        public LayerMask GroundLayers;
        public float Gravity = 10.0f; 
        public bool CanJump = false;
        public float JumpHeight = 2.0f;
        public float MaxVelocityChange = 10f;
        public float baseSpeed = 10;
        public float interpolationSmoothing = 5;
        private float _currentSpeed;
        // Networking Attribute: To check if destination is Valid
        //private bool validDestination;
        public MMObject obj;
        public float CurrentSpeed {
            get { return _currentSpeed; }
            protected set { _currentSpeed = value; }             
        }
        public bool Grounded {
            get { return Physics.Raycast(transform.position, Vector3.down, 0.1f); }
        }
        //public bool ValidDestination {
        //    get {  return validDestination; }
        //}        

        //public abstract void Move(System.Object movementData);
        //public abstract void Move(Vector3 deltaPosition, Quaternion deltaRotation);
        public abstract void Move(Vector3 InputDirection);
        public abstract void Interpolate(Vector3 newPos,Vector3 rot);

        protected virtual void Awake()
        {
           
        }
        protected virtual void Start() {
           
            CurrentSpeed = baseSpeed;
            //ValidDestination = false;
            AnimatorCtrl = GetComponent<BaseAnimatorController>();
        }
    }
}