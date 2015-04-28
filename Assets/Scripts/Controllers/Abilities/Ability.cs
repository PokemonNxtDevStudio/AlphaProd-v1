using System;
using UnityEngine;
using NXT;
using NXT.Controllers;
namespace NXT
{
    public abstract class Ability : MonoBehaviour
    {
        public enum AbilityTypes
        {
            None = -1,
            Interact,
            Cover,
            Vault
        }
        public enum InputStartTypes
        {
            None,
            ButtonDown
        }
        public enum InputStopTypes
        {
            None,
            ButtonUp,
            ButtonToggle
        }
        [HideInInspector, SerializeField]
        private int m_Index;
        [SerializeField]
        private string m_InputName;
        [SerializeField]
        private Ability.InputStartTypes m_InputStartType;
        [SerializeField]
        private Ability.InputStopTypes m_InputStopType;
        [SerializeField]
        private float m_TransitionDuration = 0.2f;
        private bool m_Active;
        protected Animator m_Animator;
        protected RigidbodyCharacterController m_Controller;
        protected AnimatorMonitor m_AnimatorMonitor;
        public bool IsActive
        {
            get
            {
                return this.m_Active;
            }
        }
        public int Index
        {
            get
            {
                return this.m_Index;
            }
            set
            {
                this.m_Index = value;
            }
        }
        public string InputName
        {
            get
            {
                return this.m_InputName;
            }
        }
        public Ability.InputStartTypes InputStartType
        {
            get
            {
                return this.m_InputStartType;
            }
        }
        public Ability.InputStopTypes InputStopType
        {
            get
            {
                return this.m_InputStopType;
            }
        }
        public float TransitionDuration
        {
            get
            {
                return this.m_TransitionDuration;
            }
        }
        protected virtual void Awake()
        {
            this.m_Controller = base.GetComponent<RigidbodyCharacterController>();
            this.m_Animator = base.GetComponent<Animator>();
            this.m_AnimatorMonitor = base.GetComponent<AnimatorMonitor>();
            if (!string.IsNullOrEmpty(this.m_InputName))
            {
                base.GetComponent<ControllerHandler>().RegisterAbility(this, this.m_InputName);
            }
        }
        public virtual bool IsConcurrentAblity()
        {
            return false;
        }
        public virtual void UpdateAbility()
        {
        }
        public virtual bool CanStartAbility()
        {
            return true;
        }
        public virtual bool CanStartAbility(Ability ability)
        {
            return true;
        }
        public virtual void StartAbility()
        {
            this.m_Active = true;
            this.m_Animator.SetInteger(AnimatorMonitor.StateHash, 0);
            this.m_AnimatorMonitor.DetermineStates();
        }
        public virtual string GetDestinationState(int layer)
        {
            return string.Empty;
        }
        public virtual void StopAbility()
        {
            if (!this.m_Active)
            {
                return;
            }
            this.m_Active = false;
            this.m_AnimatorMonitor.DetermineStates();
        }
        public virtual bool Move(ref float horizontalMovement, ref float forwardMovement, Quaternion lookRotation)
        {
            return false;
        }
        public virtual void CheckForExternalForces(float xPercent, float zPercent)
        {
        }
        public virtual bool CheckGround()
        {
            return false;
        }
        public virtual bool UpdateGroundedVelocities()
        {
            return false;
        }
        public virtual bool UpdateAirborneVelocities()
        {
            return false;
        }
        public virtual bool UpdateMovement()
        {
            return false;
        }
        public virtual bool UpdateRotation()
        {
            return false;
        }
        public virtual bool UpdateAnimator()
        {
            return false;
        }
        public virtual bool HasAnimatorControl(int layer)
        {
            return false;
        }
        public virtual bool CanInteractItem()
        {
            return true;
        }
        public virtual bool CanUseItem()
        {
            return true;
        }
    }
}
