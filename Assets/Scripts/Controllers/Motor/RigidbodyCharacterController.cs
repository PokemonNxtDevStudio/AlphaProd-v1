//using Opsive.ThirdPersonController.Abilities;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using NXT;
namespace NXT.Controllers
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
    public class RigidbodyCharacterController : MonoBehaviour
    {
        public enum MovementType
        {
            Combat,
            Adventure,
            TopDown
        }
        [SerializeField]
        private RigidbodyCharacterController.MovementType m_MovementType;
        [SerializeField]
        private float m_RotationSpeed = 6f;
        [SerializeField]
        private float m_FastRotationSpeed = 15f;
        [SerializeField]
        private float m_AirAcceleration = 0.5f;
        [SerializeField]
        private float m_Dampening = 0.15f;
        [SerializeField]
        private float m_AirDampening = 0.15f;
        [SerializeField]
        private float m_GroundStickiness = 5f;
        [SerializeField]
        private float m_SkinWidth = 0.08f;
        [SerializeField]
        private float m_SkinStickiness = 0.2f;
        [SerializeField]
        private float m_SkinMovingPlatformStickiness = 0.5f;
        [SerializeField]
        private bool m_AlwaysAim;
        [SerializeField]
        private float m_ItemUseRotationThreshold = 0.1f;
        [SerializeField]
        private float m_ItemForciblyUseDuration = 0.3f;
        [SerializeField]
        private PhysicMaterial m_GroundedFrictionMaterial;
        [SerializeField]
        private PhysicMaterial m_AirFrictionMaterial;
        [SerializeField]
        private Ability[] m_Abilities;
        private Vector3 m_InputVector;
        private Quaternion m_LookRotation;
        private bool m_Aim;
        private bool m_IsAiming;
        private bool m_ForceAim;
        private bool m_IsForcedAiming;
        private bool m_IsAI;
        private bool m_Grounded;
        private bool m_Moving;
        private Vector3 m_Velocity;
        private Vector3 m_RootMotionForce;
        private Vector3 m_PrevRootMotionForce;
        private Quaternion m_RootMotionRotation = Quaternion.identity;
        private float m_PrevYRotation;
        private Vector3 m_AirVelocity;
        private Vector3 m_PrevAirVelocity;
        private float m_PrevGroundHeight;
        private ScheduledEvent m_ForcedItemUseEvent;
        private WaitForEndOfFrame m_EndOfFrame = new WaitForEndOfFrame();
        private RaycastHit m_RaycastHit;
        private Transform m_Platform;
        private Vector3 m_PlatformPosition;
        private Vector3 m_PlatformRelativePosition;
        private float m_PrevPlatformAngle;
        private GameObject m_GameObject;
        private Transform m_Transform;
        private Rigidbody m_Rigidbody;
        private CapsuleCollider m_CapsuleCollider;
        private Animator m_Animator;
        private AnimatorMonitor m_AnimatorMonitor;
        public RigidbodyCharacterController.MovementType Movement
        {
            get
            {
                return this.m_MovementType;
            }
            set
            {
                this.m_MovementType = value;
            }
        }
        public bool Moving
        {
            get
            {
                return this.m_Moving;
            }
            set
            {
                if (this.m_Moving != value)
                {
                    this.m_Moving = value;
                    this.m_AnimatorMonitor.DetermineStates();
                }
            }
        }
        public float RotationSpeed
        {
            set
            {
                this.m_RotationSpeed = value;
            }
        }
        public float AirAcceleration
        {
            set
            {
                this.m_AirAcceleration = value;
            }
        }
        public bool AlwaysAim
        {
            get 
            {
                return this.m_AlwaysAim;
            }
            set
            {
                this.m_AlwaysAim = value;
                if (this.m_AnimatorMonitor != null)
                {
                    this.m_AnimatorMonitor.DetermineStates();
                }
            }
        }
        public PhysicMaterial GroundedFrictionMaterial
        {
            set
            {
                this.m_GroundedFrictionMaterial = value;
            }
        }
        public PhysicMaterial AirFrictionMaterial
        {
            set
            {
                this.m_AirFrictionMaterial = value;
            }
        }
        public float SkinWidth
        {
            get
            {
                return this.m_SkinWidth;
            }
        }
        public bool Grounded
        {
            get
            {
                return this.m_Grounded;
            }
        }
        public bool Aim
        {
            get
            {
                return this.m_Aim || this.m_ForceAim || this.m_AlwaysAim;
            }
        }
        public Vector3 InputVector
        {
            get
            {
                return this.m_InputVector;
            }
        }
        public Quaternion LookRotation
        {
            get
            {
                return this.m_LookRotation;
            }
        }
        public Vector3 RootMotionForce
        {
            get
            {
                return this.m_RootMotionForce;
            }
            set
            {
                this.m_RootMotionForce = value;
            }
        }
        public Quaternion RootMotionRotation
        {
            get
            {
                return this.m_RootMotionRotation;
            }
            set
            {
                this.m_RootMotionRotation = value;
            }
        }
        public Vector3 Velocity
        {
            get
            {
                return this.m_Velocity;
            }
        }
        public Ability[] Abilities
        {
            get
            {
                return this.m_Abilities;
            }
        }
        private void Awake()
        {
            this.m_GameObject = base.gameObject;
            this.m_Transform = base.transform;
            this.m_Rigidbody = base.GetComponent<Rigidbody>();
            this.m_CapsuleCollider = base.GetComponent<CapsuleCollider>();
            this.m_Animator = base.GetComponent<Animator>();
            this.m_AnimatorMonitor = base.GetComponent<AnimatorMonitor>();
            SharedManager.Register(this);
            this.m_IsAI = this;// (base.GetComponent<PlayerInput>() == null);
            this.m_PrevGroundHeight = this.m_Transform.position.y;
            this.SetPosition(this.m_Transform.position);
            this.SetRotation(this.m_Transform.rotation);
        }
        private void OnEnable()
        {
            EventHandler.RegisterEvent<bool>(this.m_GameObject, "OnCameraFocus", new Action<bool>(this.OnFocus));
            EventHandler.RegisterEvent(this.m_GameObject, "OnAnimatorAiming", new Action(this.OnAiming));
            EventHandler.RegisterEvent(this.m_GameObject, "OnItemStartUse", new Action(this.OnItemStartUse));
            EventHandler.RegisterEvent(this.m_GameObject, "OnItemStopUse", new Action(this.OnItemStopUse));
            EventHandler.RegisterEvent(this.m_GameObject, "OnDeath", new Action(this.OnDeath));
        }
        private void OnDisable()
        {
            EventHandler.UnregisterEvent<bool>(this.m_GameObject, "OnCameraFocus", new Action<bool>(this.OnFocus));
            EventHandler.UnregisterEvent(this.m_GameObject, "OnAnimatorAiming", new Action(this.OnAiming));
            EventHandler.UnregisterEvent(this.m_GameObject, "OnItemStartUse", new Action(this.OnItemStartUse));
            EventHandler.UnregisterEvent(this.m_GameObject, "OnItemStopUse", new Action(this.OnItemStopUse));
            EventHandler.UnregisterEvent(this.m_GameObject, "OnDeath", new Action(this.OnDeath));
        }
        private bool SharedMethod_IsAI()
        {
            return this.m_IsAI;
        }
        public void Move(float horizontalMovement, float forwardMovement, Quaternion lookRotation)
        {
            this.m_Velocity = this.m_Rigidbody.velocity;
            bool flag = false;
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                this.m_Abilities[i].UpdateAbility();
                if (!flag && this.m_Abilities[i].IsActive && this.m_Abilities[i].Move(ref horizontalMovement, ref forwardMovement, lookRotation))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                return;
            }
            this.m_InputVector.x = horizontalMovement;
            this.m_InputVector.z = forwardMovement;
            this.m_LookRotation = lookRotation;
            this.CheckGround();
            this.CheckForExternalForces();
            this.SetPhysicMaterial();
            this.UpdateVelocities();
            this.UpdateRotation();
            this.UpdateMovement();
            this.UpdatePlatformMovement();
            this.UpdateAnimator();
            this.m_Rigidbody.velocity = (this.m_Velocity);
            for (int j = 0; j < this.m_Abilities.Length; j++)
            {
                if (!this.m_Abilities[j].IsActive && this.m_Abilities[j].InputStartType == Ability.InputStartTypes.None && this.TryStartAbility(this.m_Abilities[j]))
                {
                    break;
                }
            }
        }
        public void CheckGround()
        {
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive && this.m_Abilities[i].CheckGround())
                {
                    return;
                }
            }
            float num = this.m_SkinStickiness;
            if (this.m_Platform != null && this.m_Velocity.y < 0.1f)
            {
                num = this.m_SkinMovingPlatformStickiness;
            }
            bool flag = Physics.SphereCast(this.m_Transform.position + this.m_CapsuleCollider.center, this.m_CapsuleCollider.radius / 2f, Vector3.down, out this.m_RaycastHit, this.m_CapsuleCollider.height / 2f + this.m_SkinWidth + num, 2147483643);
            if (flag)
            {
                if (this.m_RaycastHit.transform.gameObject.layer== 29)
                {
                    if (this.m_Platform == null)
                    {
                        this.m_Platform = this.m_RaycastHit.transform;
                        this.m_PlatformPosition = this.m_Transform.position;
                        this.m_PlatformRelativePosition = this.m_Platform.InverseTransformPoint(this.m_Transform.position);
                        this.m_PrevPlatformAngle = this.m_Platform.eulerAngles.y;
                    }
                }
                else
                {
                    this.m_Platform = null;
                }
                if (this.m_Velocity.y <= 0f)
                {
                    Vector3 position = this.m_Transform.position;
                    position.y = this.m_RaycastHit.point.y + this.m_SkinWidth;
                    this.m_Transform.position =(Vector3.MoveTowards(this.m_Transform.position, position, this.m_GroundStickiness * Time.deltaTime));
                }
            }
            else if (!flag)
            {
                this.m_Platform = null;
                if (this.m_Grounded)
                {
                    this.m_PrevGroundHeight = this.m_Transform.position.y;
                }
            }
            if (this.m_Grounded != flag)
            {
                EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnControllerGrounded", flag);
                if (!this.m_Grounded)
                {
                    this.m_AirVelocity = (this.m_PrevAirVelocity = Vector3.zero);
                    EventHandler.ExecuteEvent<float>(this.m_GameObject, "OnControllerLand", this.m_PrevGroundHeight - this.m_Transform.position.y);
                }
            }
            this.m_Grounded = flag;
            this.m_Rigidbody.useGravity = (!this.m_Grounded);
        }
        private void CheckForExternalForces()
        {
            float num = 0f;
            float num2 = 0f;
            if (this.m_Grounded)
            {
                num = ((this.m_Velocity.x == 0f) ? 1f : Mathf.Clamp01(this.m_PrevRootMotionForce.x / this.m_Velocity.x));
                this.m_Velocity.x = this.m_Velocity.x * (1f - num) / (1f + this.m_Dampening) + this.m_PrevRootMotionForce.x * num;
                num2 = ((this.m_Velocity.z == 0f) ? 1f : Mathf.Clamp01(this.m_PrevRootMotionForce.z / this.m_Velocity.z));
                this.m_Velocity.z = this.m_Velocity.z * (1f - num2) / (1f + this.m_Dampening) + this.m_PrevRootMotionForce.z * num2;
            }
            else
            {
                this.m_Velocity.x = this.m_Velocity.x / (1f + this.m_AirDampening);
                this.m_Velocity.z = this.m_Velocity.z / (1f + this.m_AirDampening);
            }
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive)
                {
                    this.m_Abilities[i].CheckForExternalForces(num, num2);
                }
            }
        }
        private void SetPhysicMaterial()
        {
            if (this.m_Grounded)
            {
                if (this.m_GroundedFrictionMaterial != null)
                {
                    this.m_CapsuleCollider.material =(this.m_GroundedFrictionMaterial);
                }
            }
            else if (this.m_AirFrictionMaterial != null)
            {
                this.m_CapsuleCollider.material =(this.m_AirFrictionMaterial);
            }
        }
        private void UpdateVelocities()
        {
            if (this.m_Grounded)
            {
                this.UpdateGroundedVelocities();
            }
            else
            {
                this.UpdateAirborneVelocities();
            }
        }
        private void UpdateGroundedVelocities()
        {
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive && this.m_Abilities[i].UpdateGroundedVelocities())
                {
                    return;
                }
            }
            this.m_Velocity.y = 0f;
        }
        private void UpdateAirborneVelocities()
        {
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive && this.m_Abilities[i].UpdateAirborneVelocities())
                {
                    return;
                }
            }
            this.m_AirVelocity.y = 0f;
            Vector3 vector = this.m_LookRotation * Vector3.forward;
            vector.y = 0f;
            this.m_AirVelocity += Quaternion.LookRotation(vector.normalized) * this.m_InputVector.normalized * this.m_AirAcceleration;
            this.m_AirVelocity -= this.m_PrevAirVelocity;
            if (this.m_AirVelocity.sqrMagnitude < 0.01f)
            {
                this.m_AirVelocity = Vector3.zero;
            }
            this.m_Velocity += this.m_AirVelocity;
            this.m_PrevAirVelocity = this.m_AirVelocity;
        }
        private void UpdateRotation()
        {
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive && this.m_Abilities[i].UpdateRotation())
                {
                    return;
                }
            }
            if (this.m_MovementType == RigidbodyCharacterController.MovementType.Adventure)
            {
                if (this.m_InputVector != Vector3.zero)
                {
                    Quaternion quaternion = Quaternion.Euler(0f, Quaternion.LookRotation(this.m_LookRotation * this.m_InputVector.normalized).eulerAngles.y, 0f);
                    this.m_Transform.rotation =(Quaternion.Slerp(this.m_Transform.rotation, quaternion, this.m_RotationSpeed * Time.deltaTime));
                }
            }
            else
            {
                Vector3 eulerAngles = this.m_Transform.eulerAngles;
                eulerAngles.y = this.m_LookRotation.eulerAngles.y;
                this.m_Transform.rotation =(Quaternion.Slerp(this.m_Transform.rotation, Quaternion.Euler(eulerAngles), ((!this.m_Aim && !this.m_ForceAim) ? this.m_RotationSpeed : this.m_FastRotationSpeed) * Time.deltaTime));
            }
        }
        private void UpdateMovement()
        {
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive && this.m_Abilities[i].UpdateMovement())
                {
                    return;
                }
            }
            this.m_Velocity.x = this.m_Velocity.x + (this.m_RootMotionForce.x - this.m_PrevRootMotionForce.x);
            this.m_Velocity.z = this.m_Velocity.z + (this.m_RootMotionForce.z - this.m_PrevRootMotionForce.z);
            this.m_PrevRootMotionForce = this.m_RootMotionForce;
        }
        private void UpdatePlatformMovement()
        {
            if (this.m_Platform == null)
            {
                return;
            }
            Vector3 vector = this.m_Transform.position + this.m_Platform.TransformPoint(this.m_PlatformRelativePosition) - this.m_PlatformPosition;
            this.m_Rigidbody.MovePosition(vector);
            this.m_PlatformPosition = vector;
            this.m_PlatformRelativePosition = this.m_Platform.InverseTransformPoint(vector);
            Vector3 eulerAngles = this.m_Transform.eulerAngles;
            eulerAngles.y -= Mathf.DeltaAngle(this.m_Platform.eulerAngles.y, this.m_PrevPlatformAngle);
            this.m_Rigidbody.MoveRotation(Quaternion.Euler(eulerAngles));
            this.m_PrevPlatformAngle = this.m_Platform.eulerAngles.y;
            this.m_PrevYRotation = eulerAngles.y;
        }
        private void UpdateAnimator()
        {
            this.m_Animator.applyRootMotion =(this.m_Grounded);
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive && this.m_Abilities[i].UpdateAnimator())
                {
                    return;
                }
            }
            float num = 0f;
            float num2;
            if (this.m_MovementType == RigidbodyCharacterController.MovementType.Combat)
            {
                num2 = this.m_InputVector.z;
                num = this.m_InputVector.x;
            }
            else if (this.m_MovementType == RigidbodyCharacterController.MovementType.Adventure)
            {
                num2 = this.m_InputVector.magnitude;
            }
            else
            {
                Vector3 vector = this.m_Transform.InverseTransformDirection(this.m_InputVector.x, 0f, this.m_InputVector.z);
                num = vector.x;
                num2 = vector.z;
            }
            //this.m_Animator.SetFloat(AnimatorMonitor.VerticalInputHash, num2, 0.2f, Time.deltaTime);
            //this.m_Animator.SetFloat(AnimatorMonitor.HorizontalInputHash, num, 0.2f, Time.deltaTime);
            //this.m_Animator.SetFloat(AnimatorMonitor.YawHash, (!this.m_Aim) ? Mathf.DeltaAngle(this.m_PrevYRotation, this.m_Transform.eulerAngles.y) : 0f, 0.1f, Time.deltaTime);
            this.m_PrevYRotation = this.m_Transform.eulerAngles.y;
            if (!this.m_Moving)
            {
                this.Moving = (this.m_InputVector.sqrMagnitude > 0.01f);
            }
            else
            {
                this.Moving = (this.m_Velocity.sqrMagnitude> 0.01f);
            }
        }
        private void OnAnimatorMove()
        {
            if (this.m_Grounded)
            {
                this.m_RootMotionForce = this.m_Animator.deltaPosition/ ((1f + this.m_Dampening) * Time.deltaTime);
                this.m_RootMotionRotation *= this.m_Animator.deltaRotation;
            }
            else
            {
                this.m_RootMotionForce = Vector3.zero;
            }
        }
        public void SetPosition(Vector3 position)
        {
            Rigidbody arg_15_0 = this.m_Rigidbody;
            this.m_Transform.position =(position);
            arg_15_0.position =(position);
        }
        public void SetRotation(Quaternion rotation)
        {
            Rigidbody arg_15_0 = this.m_Rigidbody;
            Quaternion rotation2 = rotation;
            this.m_Transform.rotation =(rotation2);
            arg_15_0.rotation =(rotation2);
            this.m_PrevYRotation = rotation.eulerAngles.y;
        }
        public void StopMovement()
        {
            this.m_Rigidbody.velocity = (Vector3.zero);
            this.m_Rigidbody.angularVelocity =(Vector3.zero);
        }
        public bool TryStartAbility(Ability ability)
        {
            if (!ability.IsActive && ability.CanStartAbility())
            {
                if (!ability.IsConcurrentAblity())
                {
                    int i = 0;
                    while (i < this.m_Abilities.Length)
                    {
                        if (this.m_Abilities[i].IsActive && !this.m_Abilities[i].IsConcurrentAblity())
                        {
                            if (i < ability.Index || !this.m_Abilities[i].CanStartAbility(ability))
                            {
                                return false;
                            }
                            this.m_Abilities[i].StopAbility();
                            break;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < this.m_Abilities.Length; j++)
                    {
                        if (this.m_Abilities[j].IsActive && !this.m_Abilities[j].CanStartAbility(ability))
                        {
                            return false;
                        }
                    }
                }
                ability.StartAbility();
                return true;
            }
            return false;
        }
        public void TryStopAbility(Ability ability)
        {
            if (ability.IsActive)
            {
                ability.StopAbility();
            }
        }
        private bool SharedMethod_TryUseItem()
        {
            Vector3 eulerAngles = this.m_Transform.eulerAngles;
            eulerAngles.y = this.m_LookRotation.eulerAngles.y;
            if ((!this.m_IsAiming && !this.m_IsForcedAiming) || (this.m_MovementType != RigidbodyCharacterController.MovementType.Combat && Mathf.Abs(Mathf.DeltaAngle(this.m_Transform.eulerAngles.y, eulerAngles.y)) > this.m_ItemUseRotationThreshold + 1.401298E-45f))
            {
                base.StartCoroutine(this.PrepareToUseItem());
                return false;
            }
            return true;
        }
        [DebuggerHidden]
        private IEnumerator PrepareToUseItem()
		{
			//RigidbodyCharacterController.<PrepareToUseItem>c__Iterator0 <PrepareToUseItem>c__Iterator = new RigidbodyCharacterController.<PrepareToUseItem>c__Iterator0();
			//<PrepareToUseItem>c__Iterator.<>f__this = this;
			//return <PrepareToUseItem>c__Iterator;

            yield return null;
		}
        private void OnItemStartUse()
        {
            Scheduler.Cancel(this.m_ForcedItemUseEvent);
            this.m_ForcedItemUseEvent = null;
        }
        private void OnItemStopUse()
        {
            if (this.m_ForceAim)
            {
                Scheduler.Cancel(this.m_ForcedItemUseEvent);
                this.m_ForcedItemUseEvent = Scheduler.Schedule(this.m_ItemForciblyUseDuration, new Action(this.StopForceUse));
            }
        }
        private void StopForceUse()
        {
            this.m_ForceAim = false;
            this.m_IsForcedAiming = false;
            this.m_ForcedItemUseEvent = null;
            this.m_AnimatorMonitor.DetermineStates();
            EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnControllerAim", false);
        }
        private void OnFocus(bool focus)
        {
            if (!this.SharedMethod_CanUseItem())
            {
                return;
            }
            bool flag = focus != this.m_Aim;
            this.m_Aim = focus;
            if (flag)
            {
                this.m_AnimatorMonitor.DetermineStates();
            }
            if (!focus && !this.m_AlwaysAim)
            {
                this.m_IsAiming = false;
                EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnControllerAim", false);
            }
        }
        private void OnAiming()
        {
            if (this.m_ForceAim && !this.m_IsForcedAiming)
            {
                this.m_IsForcedAiming = true;
                this.m_AnimatorMonitor.DetermineStates();
                EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnControllerAim", true);
            }
            if ((this.m_Aim || this.m_AlwaysAim) && !this.m_IsAiming)
            {
                this.m_IsAiming = true;
                this.m_AnimatorMonitor.DetermineStates();
                EventHandler.ExecuteEvent<bool>(this.m_GameObject, "OnControllerAim", true);
            }
        }
        private bool SharedMethod_CanInteractItem()
        {
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive && !this.m_Abilities[i].CanInteractItem())
                {
                    return false;
                }
            }
            return true;
        }
        private bool SharedMethod_CanUseItem()
        {
            for (int i = 0; i < this.m_Abilities.Length; i++)
            {
                if (this.m_Abilities[i].IsActive && !this.m_Abilities[i].CanUseItem())
                {
                    return false;
                }
            }
            return true;
        }
        private void OnDeath()
        {
            for (int i = this.m_Abilities.Length - 1; i > -1; i--)
            {
                if (this.m_Abilities[i].IsActive)
                {
                    this.m_Abilities[i].StopAbility();
                }
            }
            this.m_CapsuleCollider.enabled =(false);
            EventHandler.RegisterEvent(this.m_GameObject, "OnRespawn", new Action(this.OnRespawn));
        }
        private void OnRespawn()
        {
            this.m_CapsuleCollider.enabled =(true);
            this.m_Rigidbody.isKinematic = (false);
            this.m_Rigidbody.velocity =(Vector3.zero);
            EventHandler.UnregisterEvent(this.m_GameObject, "OnRespawn", new Action(this.OnRespawn));
        }
    }
}
