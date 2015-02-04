using System.Collections.Generic;

using UnityEngine;
namespace PokemonNXT.Controllers
{
    class CharactorAnimatorCtrl : BaseAnimatorController
    {

        //_velocity = new Vector3 (_input_x * input_modifier, -antiBunny, _input_y * input_modifier);
        //_velocity = _t.TransformDirection (_velocity) * _speed;

        // Animation

        //TODO if grounded
        void Awake()
        {
            base.Awake();
        }
        void Update()
        {
            Vector3 posDxDz = target.position - lastPosition;
            moveSpeed = (posDxDz).magnitude;
   
            lastPosition = target.position;
            moveDir = transform.InverseTransformDirection(transform.forward);
           // moveDir = (posDxDz - faceDirectionLocal).normalized;
            // Get the angle between the facing direction and character forward
          

   

            SetFloat("DirX", moveDir.x);
            SetFloat("DirY", moveDir.z);
            //_last_position = _t.position;
         
        }
    }
}