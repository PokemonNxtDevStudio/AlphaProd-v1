
using System;
using System.Collections.Generic;
using UnityEngine;
using NXT.Controllers;
namespace NXT
{
    [RequireComponent(typeof(RigidbodyCharacterController))]
    public class ControllerHandler : MonoBehaviour
    {
        [SerializeField]
        private const string m_HorizontalInputName = "Horizontal";
        [SerializeField]
        private const string m_VerticalInputName = "Vertical";
        [SerializeField]
        private const string m_JumpInputName = "Jump";
        [SerializeField]
        private const string m_SprintInputName = "Sprint";
        [SerializeField]
        private const string m_CrouchInputName = "Crouch";
        [SerializeField]
        private const string m_ActionInputName = "Action";
        private float m_HorizontalMovement;
        private float m_VerticalMovement;
        private List<Ability> m_AbilityInputComponents;
        private List<string> m_AbilityInputNames;
        private bool m_AllowGameplayInput = true;
        private SharedMethod<bool> m_IsAI = null;
        private GameObject m_GameObject;
        private Transform m_Transform;
        private RigidbodyCharacterController m_Controller;
        private Transform m_CameraTransform;
        private void Awake()
        {
            this.m_GameObject = base.gameObject;
            this.m_Transform = base.transform;
            this.m_Controller = base.GetComponent<RigidbodyCharacterController>();
            this.m_CameraTransform = Utility.FindCamera().transform;
        }
        private void OnEnable()
        {
            EventHandler.RegisterEvent(this.m_GameObject, "OnDeath", new Action(this.OnDeath));
        }
        private void OnDisable()
        {
            EventHandler.UnregisterEvent(this.m_GameObject, "OnDeath", new Action(this.OnDeath));
            this.m_Controller.StopMovement();
        }
        private void Start()
        {
            SharedManager.InitializeSharedFields(this.m_GameObject, this);
            EventHandler.RegisterEvent<bool>(this.m_GameObject, "OnAllowGameplayInput", new Action<bool>(this.AllowGameplayInput));
            base.enabled =(!this.m_IsAI.Invoke());
        }
        private void Update()
        {
            if (this.m_AllowGameplayInput)
            {
                this.m_HorizontalMovement = Input.GetAxis("Horizontal");
                this.m_VerticalMovement = Input.GetAxis("Vertical");
            }
            else
            {
                this.m_HorizontalMovement = (this.m_VerticalMovement = 0f);
            }
            if (this.m_AbilityInputComponents != null)
            {
                for (int i = 0; i < this.m_AbilityInputComponents.Count; i++)
                {
                    if (Input.GetButtonDown(this.m_AbilityInputNames[i]))
                    {
                        if (!this.m_AbilityInputComponents[i].IsActive && this.m_AbilityInputComponents[i].InputStartType == Ability.InputStartTypes.ButtonDown)
                        {
                            this.m_Controller.TryStartAbility(this.m_AbilityInputComponents[i]);
                        }
                        else if (this.m_AbilityInputComponents[i].InputStopType == Ability.InputStopTypes.ButtonToggle)
                        {
                            this.m_Controller.TryStopAbility(this.m_AbilityInputComponents[i]);
                        }
                    }
                    else if (Input.GetButtonUp(this.m_AbilityInputNames[i]) && this.m_AbilityInputComponents[i].InputStopType == Ability.InputStopTypes.ButtonUp)
                    {
                        this.m_Controller.TryStopAbility(this.m_AbilityInputComponents[i]);
                    }
                }
            }
        }
        private void FixedUpdate()
        {
            Quaternion lookRotation;
            if (this.m_Controller.Movement == RigidbodyCharacterController.MovementType.TopDown)
            {
                Vector3 vector = Input.mousePosition - Camera.main.WorldToScreenPoint(this.m_Transform.position);
                vector.z = vector.y;
                vector.y = 0f;
                lookRotation = Quaternion.LookRotation(vector);
            }
            else
            {
                lookRotation = this.m_CameraTransform.rotation;
            }
            this.m_Controller.Move(this.m_HorizontalMovement, this.m_VerticalMovement, lookRotation);
        }
        public void RegisterAbility(Ability ability, string inputName)
        {
            if (ability.InputStartType == Ability.InputStartTypes.None && ability.InputStopType == Ability.InputStopTypes.None)
            {
                return;
            }
            if (this.m_AbilityInputComponents == null)
            {
                this.m_AbilityInputComponents = new List<Ability>();
                this.m_AbilityInputNames = new List<string>();
            }
            this.m_AbilityInputComponents.Add(ability);
            this.m_AbilityInputNames.Add(inputName);
        }
        private void OnDeath()
        {
            base.enabled =(false);
            EventHandler.RegisterEvent(this.m_GameObject, "OnRespawn", new Action(this.OnRespawn));
        }
        private void OnRespawn()
        {
            base.enabled =(true);
            EventHandler.UnregisterEvent(this.m_GameObject, "OnRespawn", new Action(this.OnRespawn));
        }
        private void AllowGameplayInput(bool allow)
        {
            this.m_AllowGameplayInput = allow;
        }
    }
}