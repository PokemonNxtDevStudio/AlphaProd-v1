using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PokemonNXT.Controllers {

    /// <summary>
    /// @TODO: This Controller should be split into peaces
    /// AI model -> run logic
    /// AIInputController -> includes the AI input and runs it on update
    /// CameraMotorCtrl -> includes a Move method that will receive the AIInputController's Direction
    /// CameraCtrlHandler -> local handler that receives the AIInput and updates the CameraMotorCtrl
    /// </summary>
    public class ThirdPersonCameraCtrl: BaseController {
        // Feel free to change them to public if they need to be accessed by other classes
        public Transform target;
        public Transform targetToFollow;
        public Transform target3;
        public LayerMask collisionLayers = new LayerMask();
        public float collisionAlpha = 0.15f;
        public float collisionFadeSpeed = 10;
        public GameObject SmoothCamera = null;
        public bool allowRotation = true;
        public bool lockCursor = true;
        public bool invertX = false;
        public bool invertY = false;
        public bool stayBehindTarget = false;

        public Vector2 targetOffset = new Vector2();
        //Y Offset by the distance
        private float _targetOffsetYOnMinDis = 2f;
       // private float _targetOffsetYOnMaxDis = 10f;

        public bool rotateObjects = true;
        public List<Transform> objectsToRotate;

        public bool fadeObjects = true;
        public float fadeDistance = 1.5f;
        public List<Renderer> objectsToFade;

        public Vector2 originRotation = new Vector2();
        public bool returnToOrigin = true;
        public float returnSmoothing = 3;
        public Vector3 CollisionSmooth;
        [SerializeField]
        private float
                distance = -4f;
        [SerializeField]
        private float
                minDistance = 5f;
        [SerializeField]
        private float
                maxDistance = 25f;
        public float collisionDistance;
        public Vector2 sensitivity = new Vector2(3, 3);

        public float zoomSpeed = 2.5f;
        public float zoomSmoothing = 7f;

        public float zoomAltDelay = 0.5f;

        private float minAngle = -45;
        private float maxAngle = 45;

        //private List<Material> _faded_mats = new List<Material>();   // I have yet to implement this properly
        //private List<Material> _current_faded_mats = new List<Material>(); // I have yet to implement this properly

        private float wantedDistance;
        private Quaternion _rotation;
        private Vector2 inputRotation;
        //private bool isFirstPerson = false;       //TDDO need to get read to these bool states.
        //private bool isThirdPerson = true;
        private static bool isPlayerControl = true;
        public Transform _transform;
        // addition rotation smooth components
        public float mouseSmoothingFactor = 0.08f;

        //*******Mouse Variables***********
        private float mouseXSmooth = 0f;
        private float mouseXVel;
        private float mouseYSmooth = 0f;
        private float mouseYVel;
        //**********************************
        //******** Camera State************
        public enum CameraState {
            ThirdPerson = 1,
            FirstPerson = 2,
            FreeZoom = 3
        }
        public CameraState currentCameraState = CameraState.ThirdPerson;
        //**************************
        public static bool Controllable {            // incase we would be need to modifier our setters later.
            get { return isPlayerControl; }
            set { isPlayerControl = value; }
        }


        protected override void Awake() {
            //skip Deactivation
        }

        protected void Start() {

            ThirdPersonCamera();
            _transform = transform;
            wantedDistance = distance;
            inputRotation = originRotation;

            // If no target set, warn the user
            if(!target) {
                Debug.LogWarning("Set Target BITCH");
            }
        }

        protected void Update() {
            if(Input.GetKeyDown(KeyCode.T)) {

                // currentCameraState = CameraState.ThirdPerson;
            }
            if(Input.GetKeyDown(KeyCode.F)) {

                // currentCameraState = CameraState.ThirdPerson;

                //minDistance = 5f;
            }

            if(Input.GetKeyDown(KeyCode.Home)) {
                currentCameraState = CameraState.ThirdPerson;
                ThirdPersonCamera();
                //minDistance = 5f;
            }

            if(Input.GetKeyDown(KeyCode.End)) {
                currentCameraState = CameraState.FirstPerson;
                FirstPersonCamera();

            }
            if(Input.GetKeyDown(KeyCode.H)) {
                //isPokemonControl = true;
                SmoothCamera = new GameObject("MadefromCode");
                SmoothCamera.transform.position = target.position;
                //Camera.main.GetComponent<ThirdPersonCameraControl> ().target = SmoothCamera.transform;
                targetToFollow = GameObject.FindGameObjectWithTag("Player").transform;

                //transitionCamera.transform.position = positionCurrent.transform.position;
            }

            if(target) {
                // Fade the target according to Fade Distance (if enabled)
                foreach(Renderer objectToFade in objectsToFade) {
                    if(objectToFade) {
                        foreach(Material material in objectToFade.materials) {
                            Color c = material.color;
                            c.a = Mathf.Clamp(distance - fadeDistance, 0, 1);  //Transparent it up! Distance is LERPED already so it doesnt need more lerping

                            if(!fadeObjects) {
                                c.a = 1;
                            }

                            material.color = c;
                        }
                    }
                }

                if(isPlayerControl) {
                    // Lock the cursor (if enabled) when user click-drags mouse
                    if(lockCursor) {
                        if(Input.GetMouseButton(0) || Input.GetMouseButton(1)) {
                            if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
                                if (/*!Screen.lockCursor*/Cursor.lockState == CursorLockMode.None)
                                {
                                    //Screen.lockCursor = true;
                                }
                            }

                            return;
                        }
                    }
                }
            }
            //if (Screen.lockCursor) {
            //Screen.lockCursor = false;
            //}
        }


        void LateUpdate() {
            //Screen.lockCursor = true;
            Cursor.lockState = CursorLockMode.Locked;

            if(currentCameraState == CameraState.FirstPerson || currentCameraState == CameraState.ThirdPerson|| currentCameraState == CameraState.FreeZoom)
                SmoothFollow(target);
        }


        #region Methods
        private void SmoothFollow(Transform target) {

            if(target) {
                if(isPlayerControl) {
                    // Zoom control
                    if(Input.GetAxis("Mouse ScrollWheel") < 0) {
                        wantedDistance += zoomSpeed;
                    }
                    else if(Input.GetAxis("Mouse ScrollWheel") > 0) {
                        wantedDistance -= zoomSpeed;
                    }
                }

                // Prevent wanted distance from going below or above min and max distance
                wantedDistance = Mathf.Clamp(wantedDistance, minDistance, maxDistance);

                // If user clicks, change position based on drag direction and sensitivity
                // Stop at 90 degrees above / below object
                if(allowRotation) { //&& (Input.GetMouseButton (0) || Input.GetMouseButton (1))) {
                    if(isPlayerControl) {
                        if(invertX) {
                            inputRotation.x -= Input.GetAxis("Mouse X") * sensitivity.x;
                        }
                        else {
                            inputRotation.x += Input.GetAxis("Mouse X") * sensitivity.x;

                        }

                        //ClampRotation ();

                        if(invertY) {
                            inputRotation.y += Input.GetAxis("Mouse Y") * sensitivity.y;
                        }
                        else {
                            inputRotation.y -= Input.GetAxis("Mouse Y") * sensitivity.y;
                        }

                        inputRotation.y = Mathf.Clamp(inputRotation.y, minAngle, maxAngle);
                        mouseXSmooth = Mathf.SmoothDamp(mouseXSmooth, inputRotation.x, ref mouseXVel, mouseSmoothingFactor);
                        mouseYSmooth = Mathf.SmoothDamp(mouseYSmooth, inputRotation.y, ref mouseYVel, mouseSmoothingFactor);
                        _rotation = Quaternion.Euler(mouseYSmooth, mouseXSmooth, 0);

                        // Force the target's y rotation to face forward (if enabled) when right clicking
                        if(rotateObjects) {
                            //if (Input.GetMouseButton (1)) {
                            foreach(Transform o in objectsToRotate) {
                                o.rotation = Quaternion.Euler(0, mouseXSmooth, 0);
                            }
                            //}
                        }

                        // If user is right clicking, set the default position to the current position
                        //if (Input.GetMouseButton (1)) {
                        //originRotation = inputRotation;
                        //ClampRotation ();
                        //}
                    }
                }
                else {
                    // Keeps the camera behind the target when not controlling it (if enabled)
                    if(stayBehindTarget) {
                        originRotation.x = target.eulerAngles.y;
                        ClampRotation();
                    }

                    // If Return To Origin, move camera back to the default position
                    if(returnToOrigin && Input.GetKeyDown(KeyCode.C)) {
                        inputRotation = Vector3.Lerp(inputRotation, originRotation, returnSmoothing * Time.deltaTime);
                    }

                    _rotation = Quaternion.Euler(mouseYSmooth, mouseXSmooth, 0);
                }

                // Lerp from current distance to wanted distance
                if(currentCameraState == CameraState.FirstPerson)

                    wantedDistance = -4f;
                if(currentCameraState == CameraState.ThirdPerson)
                    wantedDistance = 5.5f;
                distance = Mathf.Clamp(Mathf.Lerp(distance, wantedDistance, Time.deltaTime * zoomSmoothing), minDistance, maxDistance);

                // Set wanted position based off rotation and distance
                Vector3 wanted_position = _rotation * new Vector3(targetOffset.x, 0, -wantedDistance - 0.2f) + target.position + new Vector3(0, targetOffset.y, 0);
                Vector3 current_position = _rotation * new Vector3(targetOffset.x, 0, 0) + target.position + new Vector3(0, targetOffset.y, 0);


                // Linecast to test if there are objects between the camera and the target using collision layers
                RaycastHit hit;

                if(Physics.Linecast(current_position, wanted_position, out hit, collisionLayers)) {
                    distance = Vector3.Distance(current_position, hit.point) - 0.2f;
                    //Screen.lockCursor = true;
                }



                // Set the position and rotation of the camera
                _transform.position = _rotation * new Vector3(targetOffset.x, 0.0f, -distance) + target.position + new Vector3(0.0f, targetOffset.y, 0);
                _transform.rotation = _rotation;
            }
        }
        private void ClampRotation() {
            if(originRotation.x < -180) {
                originRotation.x += 360;
            }
            else if(originRotation.x > 180) {
                originRotation.x -= 360;
            }

            if(inputRotation.x - originRotation.x < -180) {
                inputRotation.x += 360;
            }
            else if(inputRotation.x - originRotation.x > 180) {
                inputRotation.x -= 360;
            }
        }
        private void ThirdPersonCamera() {
            minDistance = -5f;
            targetOffset.y = _targetOffsetYOnMinDis;
        }
        private void FirstPersonCamera() {
            //isFirstPerson = true;
            //isThirdPerson = false;
            targetOffset.y = 2;


        }
        #region helpermethods


        #endregion
        #endregion
    }
}