using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NXT.Controllers;

    public class MotorController :MonoBehaviour
    {
        public BaseAnimatorController AnimatorCtrl;
        public float JumpHeight = 2.0f;
        public float Gravity = 10.0f; 
        public float baseSpeed = .2f;
        public float MaxVelocityChange = 10f;
		public bool moveOverrde;
        protected virtual float JumpSpeed {
            // From the jump height and gravity we deduce the upwards speed 
            // for the character to reach at the apex.
            get { return Mathf.Sqrt(2 * JumpHeight * Gravity); }
        }

        public virtual void Start()
        {
            

        }
		
        void Update()
        {  //if(obj!=null && obj.IsMine)
            //obj.UpdatePosition(transform.position);

		if(!moveOverrde) Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
           AnimatorCtrl.SetFloat("DirX", Input.GetAxis("Horizontal"));
           AnimatorCtrl.SetFloat("DirY", Input.GetAxis("Vertical"));
        }
        public virtual void Interpolate(Vector3 newPos,Vector3 rot)
        {

            //transform.position = Vector3.Lerp(transform.position, newPos, interpolationSmoothing * Time.deltaTime);
        
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

                //if(CanJump && !Grounded && Input.GetKey(KeyCode.Space)) {
                //    rigidbody.velocity = new Vector3(velocity.x, JumpSpeed, velocity.z);
                //}
                //Stay on the ground bitch
                GetComponent<Rigidbody>().AddForce(new Vector3(0, -Gravity * GetComponent<Rigidbody>().mass, 0));
            
            }
            //@You can uncomment the following two lines and comment the content of the UpdateAnimator method in Ctrl/Handlers/Net/CharacterNetCtrlHandler.cs
            //AnimatorCtrl.SetFloat("DIRY", InputDirection.z, 0.15f, Time.deltaTime);
            //AnimatorCtrl.SetFloat("DIRX", InputDirection.x, 0.15f, Time.deltaTime);
        }

        //public virtual void Jump
    }
    

