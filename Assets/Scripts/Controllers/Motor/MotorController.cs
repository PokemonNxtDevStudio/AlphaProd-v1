using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NXT.Controllers;

public enum MotorState
{
    Input,
    AI
}
    public class MotorController :MonoBehaviour
    {

        public MotorState motorState = MotorState.Input;
        public BaseAnimatorController AnimatorCtrl;
        public float JumpHeight = 2.0f;
        public float Gravity = 10.0f; 
        public float baseSpeed = .2f;
        public float MaxVelocityChange = 10f;
		public bool moveOverrde;
		public bool CanJump;

	
		private bool Grounded = true;
		public Camera camera;
		private float m_speed;
		private Rigidbody rigidbody;

        protected virtual float JumpSpeed {
            // From the jump height and gravity we deduce the upwards speed 
            // for the character to reach at the apex.
            get { return Mathf.Sqrt(2 * JumpHeight * Gravity); }
        }

        public virtual void Start()
        {
            m_speed = baseSpeed;
            camera = Camera.main;
			rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {  //if(obj!=null && obj.IsMine)
			if (moveOverrde)
				return;
            //obj.UpdatePosition(transform.position);
			if (Input.GetAxis("Vertical") >= 1)  {
                FaceCamera();
            }
			
			if (!Grounded) {
				Grounded = rigidbody.velocity.y == 0;
				if(Grounded) AnimatorCtrl.SetBool(Animator.StringToHash("Jump"), false);
			}

		
            if (motorState == MotorState.Input)
            {


               	Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
				if(Grounded) {
					AnimatorCtrl.SetFloat("DirX", Input.GetAxis("Horizontal"));
					AnimatorCtrl.SetFloat("DirY", Input.GetAxis("Vertical"));
				} else {
					AnimatorCtrl.SetFloat("DirX", 0);
					AnimatorCtrl.SetFloat("DirY", 0);
//					AnimatorCtrl.SetFloat("DirZ", 1);
				}
//			AnimatorCtrl.SetFloat
            }
        }
        public void SetState(MotorState ms)
        {
            this.motorState = ms;
        }
        public void FaceCamera()
        {
            Vector3 facingAngle = camera.transform.eulerAngles;
            Vector3 facePos = camera.transform.position;
            //smoothing + optiomiation
            //Olday way, doesnt resolve rotations on 2/4 quadrant, need use Quaternions
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, Vector3.up * facingAngle.y, 7 * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(camera.transform.eulerAngles.y * Vector3.up), Time.deltaTime * 7.0f);


			
            camera.transform.eulerAngles = new Vector3(facingAngle.x, facingAngle.y, facingAngle.z);
            camera.transform.position = new Vector3(facePos.x, facePos.y, facePos.z); 
        }
        public virtual void Interpolate(Vector3 newPos,Vector3 rot)
        {

            //transform.position = Vector3.Lerp(transform.position, newPos, interpolationSmoothing * Time.deltaTime);
        
        }

        public void RestoreBaseSpeed()
        {
            baseSpeed = m_speed;
        }
        public void IncreaseSpeedByFactor(float factor,float max)
        {
            baseSpeed *= factor;
            baseSpeed = Mathf.Clamp(baseSpeed, 0, max);
        }
        public virtual void Move(Vector3 InputDirection) {
            if(!InputDirection.Equals(Vector3.zero)) {
                Vector3 targetVelocity = transform.TransformDirection(InputDirection);
                targetVelocity *= baseSpeed;

                Vector3 velocity = GetComponent<Rigidbody>().velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -MaxVelocityChange, MaxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -MaxVelocityChange, MaxVelocityChange);
                velocityChange.y = 0;
                
                //We do not apply force when not required
               // if(velocityChange.magnitude > 0.1f)
                    GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);

                if(CanJump && Grounded && Input.GetKey(KeyCode.Space)) {
					Grounded = false;
				
					AnimatorCtrl.SetBool(Animator.StringToHash("Jump"), true);
					rigidbody.velocity = new Vector3(velocity.x, JumpSpeed, velocity.z);
                }
//			Grounded
                //Stay on the ground bitch
				//                rigidbody.AddForce(new Vector3(0, -Gravity * GetComponent<Rigidbody>().mass, 0));
            
            }
            //@You can uncomment the following two lines and comment the content of the UpdateAnimator method in Ctrl/Handlers/Net/CharacterNetCtrlHandler.cs
            //AnimatorCtrl.SetFloat("DIRY", InputDirection.z, 0.15f, Time.deltaTime);
            //AnimatorCtrl.SetFloat("DIRX", InputDirection.x, 0.15f, Time.deltaTime);
        }

        //public virtual void Jump
    }
    

