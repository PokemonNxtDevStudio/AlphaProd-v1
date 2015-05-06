using System;
using System.Collections.Generic;
using UnityEngine;
using NXT;

namespace NXT.Controllers
{
    [RequireComponent(typeof(CameraHandler))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private bool m_ThirdPersonView = true;
        [SerializeField]
        private GameObject m_Character;
        [SerializeField]
        private Transform m_Anchor;
        [SerializeField]
        private Vector2 m_CrosshairsLocation = new Vector2(0.5f, 0.4f);
        [SerializeField]
        private float m_MinPitchLimit = -85f;
        [SerializeField]
        private float m_MaxPitchLimit = 85f;
        [SerializeField]
        private float m_MinYawLimit = -1f;
        [SerializeField]
        private float m_MaxYawLimit = -1f;
        [SerializeField]
        private float m_MoveSmoothing = 0.1f;
        [SerializeField]
        private float m_TurnSmoothing = 0.05f;
        [SerializeField]
        private float m_TurnSpeed = 1.5f;
        [SerializeField]
        private bool m_CanTurnInAir = true;
        [SerializeField]
        private Vector3 m_CameraOffset = new Vector3(0.7f, 0.1f, -2f);
        [SerializeField]
        private float m_NormalFOV = 60f;
        [SerializeField]
        private float m_RotationSpeed = 1.5f;
        [SerializeField]
        private float m_ViewDistance = 10f;
        [SerializeField]
        private float m_ViewStep = 5f;
        [SerializeField]
        private bool m_AllowZoom;
        [SerializeField]
        private float m_ZoomTurnSmoothing = 0.01f;
        [SerializeField]
        private Vector3 m_ZoomCameraOffset = new Vector3(0.4f, 0f, -3.66f);
        [SerializeField]
        private float m_ZoomFOV = 20f;
        [SerializeField]
        private float m_FOVSpeed = 5f;
        [SerializeField]
        private float m_ScopeTurnSmoothing = 0.01f;
        [SerializeField]
        private Vector3 m_ScopeCameraOffset = new Vector3(0.4f, 0f, -3.66f);
        [SerializeField]
        private float m_ScopeFOV = 10f;
        [SerializeField]
        private float m_DisableRendererDistance = 1f;
        [SerializeField]
        private float m_CollisionRadius = 0.01f;
        [SerializeField]
        private float m_RecoilSpring = 0.01f;
        [SerializeField]
        private float m_RecoilDampening = 0.05f;
        [SerializeField]
        private Transform m_DeathAnchor;
        [SerializeField]
        private bool m_UseDeathOrbit = true;
        [SerializeField]
        private float m_DeathRotationSpeed = 5f;
        [SerializeField]
        private float m_DeathOrbitMoveSpeed = 5f;
        [SerializeField]
        private float m_DeathOrbitDistance = 5f;
        private float m_Pitch;
        private float m_Yaw;
        private float m_StartPitch;
        private bool m_IsScoped;
        private bool m_IsZoomed;
        private bool m_Focus;
        private bool m_LimitYaw;
        private bool m_StaticHeight;
        private float m_SmoothX;
        private float m_SmoothY;
        private float m_SmoothXvelocity;
        private float m_SmoothYvelocity;
        private Vector3 m_SmoothPositionVelocity;
        private Vector3 m_LookPoint;
        private float m_DisableRendererDistanceSqr;
        private bool m_ApplyColliderOffset;
        private Vector3 m_AnchorStartOffset;
        private float m_StaticYPosition;
        private bool m_CharacterHasDied;
        private Vector3 m_PrevTargetPosition;
        private bool m_PrevRenderersEnabled;
        private RaycastHit m_RaycastHit;
        private bool m_RestrictRotation;
        private Ray m_TargetRay;
        private float m_Recoil;
        private float m_TargetRecoil;
        private static Camera m_Camera;
        private CameraHandler m_CameraHandler;
        private Transform m_Transform;
        private Transform m_CharacterTransform;
        private CapsuleCollider m_CharacterCapsuleCollider;
        private List<Renderer> m_Renderers;
        private float SharedProperty_Recoil
        {
            get
            {
                return this.m_Recoil;
            }
            set
            {
                this.m_TargetRecoil = value;
            }
        }
        public GameObject Character
        {
            get
            {
                return this.m_Character;
            }
        }
        private void Awake()
        {
            this.m_Transform = base.transform;
            CameraController.m_Camera = base.GetComponent<Camera>();
            this.m_CameraHandler = base.GetComponent<CameraHandler>();
            SharedManager.Register(this);
            if (this.m_Character == null)
            {
                Debug.LogWarning("Warning: No character has been assigned to the Camera Controller. It will automatically be assigned to the GameObject with the Player tag.");
                this.m_Character = GameObject.FindGameObjectWithTag("Player");
            }
            this.m_CharacterTransform = this.m_Character.transform;
            this.m_CharacterCapsuleCollider = this.m_Character.GetComponent<CapsuleCollider>();
            Renderer[] componentsInChildren = this.m_Character.GetComponentsInChildren<Renderer>(true);
            this.m_Renderers = new List<Renderer>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                if (componentsInChildren[i].sharedMaterials.Length > 0)
                {
                    this.m_Renderers.Add(componentsInChildren[i]);
                }
            }
            if (this.m_Anchor == null)
            {
                this.m_Anchor = this.m_CharacterTransform;
                this.m_ApplyColliderOffset = true;
            }
            this.m_AnchorStartOffset = this.m_Anchor.position - this.m_CharacterTransform.position;
            this.m_StartPitch = (this.m_Pitch = this.m_Transform.eulerAngles.x);
            this.m_Yaw = this.m_CharacterTransform.eulerAngles.y;
//            Transform arg_16E_0 = this.m_Transform;//**************************************************************************
            Quaternion rotation = Quaternion.Euler(this.m_Pitch, this.m_Yaw, 0f);
            this.m_Transform.rotation = rotation ;
            //arg_16E_0.rotation =(rotation);//**************************************************************************
            this.m_DisableRendererDistanceSqr = this.m_DisableRendererDistance * this.m_DisableRendererDistance;
            this.Move(100f, true);
        }


        
        private void OnEnable()
        {
            EventHandler.RegisterEvent<bool>(this.m_Character, "OnItemShowScope", new Action<bool>(this.OnStartScopeFocus));
            EventHandler.RegisterEvent<bool>(this.m_Character, "OnControllerGrounded", new Action<bool>(this.OnCharacterGrounded));
            EventHandler.RegisterEvent(this.m_Character, "OnControllerLeaveCover", new Action(this.OnCharacterLeaveCover));
            EventHandler.RegisterEvent<bool>(this.m_Character, "OnAnimatorPopFromCover", new Action<bool>(this.OnCharacterPopFromCover));
            EventHandler.RegisterEvent<bool>(this.m_Character, "OnControllerRoll", new Action<bool>(this.OnStaticHeight));
            EventHandler.RegisterEvent(this.m_Character, "OnDeath", new Action(this.OnCharacterDeath));
            EventHandler.RegisterEvent(this.m_Character, "OnRespawn", new Action(this.OnCharacterSpawn));
           
        }
        private void OnDisable()
        {
            EventHandler.UnregisterEvent<bool>(this.m_Character, "OnItemShowScope", new Action<bool>(this.OnStartScopeFocus));
            EventHandler.UnregisterEvent<bool>(this.m_Character, "OnControllerGrounded", new Action<bool>(this.OnCharacterGrounded));
            EventHandler.UnregisterEvent(this.m_Character, "OnControllerLeaveCover", new Action(this.OnCharacterLeaveCover));
            EventHandler.UnregisterEvent<bool>(this.m_Character, "OnAnimatorPopFromCover", new Action<bool>(this.OnCharacterPopFromCover));
            EventHandler.UnregisterEvent<bool>(this.m_Character, "OnControllerRoll", new Action<bool>(this.OnStaticHeight));
            EventHandler.UnregisterEvent(this.m_Character, "OnDeath", new Action(this.OnCharacterDeath));
            EventHandler.UnregisterEvent(this.m_Character, "OnRespawn", new Action(this.OnCharacterSpawn));
        }
        private void Start()
        {
            SharedManager.InitializeSharedFields(this.m_Character, this);
        }
        public void LateUpdate()
        {
            if (this.m_CharacterHasDied)
            {
                if (this.m_UseDeathOrbit)
                {
                    this.DeathOrbitMovement();
                }
                else
                {
                    this.LookAtCharacter((!(this.m_DeathAnchor != null)) ? this.m_CharacterTransform : this.m_DeathAnchor, this.m_DeathRotationSpeed);
                }
            }
            else
            {
                if (this.m_CameraHandler.Focus != this.m_Focus)
                {
                    EventHandler.ExecuteEvent<bool>(this.m_Character, "OnCameraFocus", this.m_CameraHandler.Focus);
                    this.m_Focus = this.m_CameraHandler.Focus;
                }
                this.m_IsZoomed = (this.m_AllowZoom && this.m_Focus);
                if (this.m_ThirdPersonView)
                {
                    this.UpdateInput();
                }
                if (this.m_IsZoomed || this.m_MoveSmoothing == 0f)
                {
                    this.Move(Time.deltaTime, false);
                    if (this.m_ThirdPersonView)
                    {
                        this.UpdateRecoil();
                        this.CheckForCharacterClipping();
                    }
                    else
                    {
                        this.LookAtCharacter(this.m_Anchor, this.m_RotationSpeed);
                    }
                }
            }
        }
        private void FixedUpdate()
        {
            if (this.m_CharacterHasDied)
            {
                return;
            }
            if (this.m_ThirdPersonView)
            {
                this.Rotate();
            }
            if (!this.m_IsZoomed && this.m_MoveSmoothing != 0f)
            {
                this.Move(Time.fixedDeltaTime, false);
                if (this.m_ThirdPersonView)
                {
                    this.UpdateRecoil();
                    this.CheckForCharacterClipping();
                }
                else
                {
                    this.LookAtCharacter(this.m_Anchor, this.m_RotationSpeed);
                }
            }
        }
        private void UpdateInput()
        {
            float yaw = this.m_CameraHandler.Yaw;
            float pitch = this.m_CameraHandler.Pitch;
            float num;
            if (this.m_IsScoped)
            {
                num = this.m_ScopeTurnSmoothing;
            }
            else if (this.m_IsZoomed)
            {
                num = this.m_ZoomTurnSmoothing;
            }
            else
            {
                num = this.m_TurnSmoothing;
            }
            if (num > 0f)
            {
                this.m_SmoothX = Mathf.SmoothDamp(this.m_SmoothX, yaw, ref this.m_SmoothXvelocity, num);
                this.m_SmoothY = Mathf.SmoothDamp(this.m_SmoothY, pitch, ref this.m_SmoothYvelocity, num);
            }
            else
            {
                this.m_SmoothX = yaw;
                this.m_SmoothY = pitch;
            }
        }
        private void Rotate()
        {
            if (this.m_RestrictRotation)
            {
                return;
            }
            this.m_Yaw += this.m_SmoothX * this.m_TurnSpeed;
            this.m_Pitch += this.m_SmoothY * this.m_TurnSpeed * -1f;
            this.m_Pitch = Utility.ClampAngle(this.m_Pitch, this.m_MinPitchLimit, this.m_MaxPitchLimit);
            float num;
            if (this.m_IsScoped)
            {
                num = this.m_ScopeTurnSmoothing;
            }
            else if (this.m_IsZoomed)
            {
                num = this.m_ZoomTurnSmoothing;
            }
            else
            {
                num = this.m_TurnSmoothing;
            }
            if (this.m_LimitYaw)
            {
                float num2 = Utility.RestrictInnerAngle(this.m_CharacterTransform.eulerAngles.y + this.m_MinYawLimit);
                float num3 = Utility.RestrictInnerAngle(this.m_CharacterTransform.eulerAngles.y + this.m_MaxYawLimit);
                if (num3 < num2)
                {
                    num3 += 360f;
                }
                this.m_Yaw = Mathf.SmoothDamp(this.m_Yaw, Mathf.Clamp(this.m_Yaw, num2, num3), ref this.m_SmoothXvelocity, num);
            }
            this.m_Transform.rotation = Quaternion.Euler(this.m_Pitch, this.m_Yaw, 0f);
        }
        private void Move(float deltaTime, bool immediateMove)
        {
            this.m_LookPoint = this.m_Anchor.position;
            if (this.m_IsScoped)
            {
                CameraController.m_Camera.fieldOfView = (this.m_ScopeFOV);
                this.m_LookPoint += this.m_ScopeCameraOffset.x * this.m_Transform.right + this.m_ScopeCameraOffset.y * this.m_CharacterTransform.up+ this.m_ScopeCameraOffset.z * this.m_Transform.forward;
            }
            else if (this.m_IsZoomed)
            {
                CameraController.m_Camera.fieldOfView =(Mathf.Lerp(CameraController.m_Camera.fieldOfView, this.m_ZoomFOV, this.m_FOVSpeed * deltaTime));
                this.m_LookPoint += this.m_ZoomCameraOffset.x * this.m_Transform.right + this.m_ZoomCameraOffset.y * this.m_CharacterTransform.up + this.m_ZoomCameraOffset.z * this.m_Transform.forward;
            }
            else
            {
                CameraController.m_Camera.fieldOfView = (Mathf.Lerp(CameraController.m_Camera.fieldOfView, this.m_NormalFOV, this.m_FOVSpeed * deltaTime));
                this.m_LookPoint += this.m_CameraOffset.x * this.m_Transform.right + this.m_CameraOffset.y * this.m_CharacterTransform.up + this.m_CameraOffset.z * this.m_Transform.forward;
            }
            Vector3 vector = this.m_LookPoint;
            if (this.m_ThirdPersonView)
            {
                Vector3 vector2 = this.m_Anchor.position+ ((!this.m_ApplyColliderOffset) ? Vector3.zero: this.m_CharacterCapsuleCollider.center);
                Vector3 vector3 = vector - vector2;
                if (Physics.SphereCast(vector2, this.m_CollisionRadius, vector3.normalized, out this.m_RaycastHit, vector3.magnitude, 2013265897))
                {
                    vector = vector2 + vector3.normalized * this.m_RaycastHit.distance;
                }
            }
            else
            {
                Vector3 vector4 = Quaternion.Euler(this.m_MinPitchLimit, 0f, 0f) * -Vector3.forward;
                float num = 0f;
                while (Physics.SphereCast(this.m_LookPoint, this.m_CollisionRadius, vector4.normalized, out this.m_RaycastHit, this.m_ViewDistance, 2013265897))
                {
                    if (this.m_MinPitchLimit + num >= this.m_MaxPitchLimit)
                    {
                        vector4 = Quaternion.Euler(this.m_MaxPitchLimit, 0f, 0f) * -Vector3.forward;
                        break;
                    }
                    num += this.m_ViewStep;
                    vector4 = Quaternion.Euler(this.m_MinPitchLimit + num, 0f, 0f) * -Vector3.forward;
                }
                vector = this.m_LookPoint + vector4 * this.m_ViewDistance;
            }
            if (this.m_StaticHeight)
            {
                vector.y = this.m_StaticYPosition;
            }
            this.m_Transform.position =(Vector3.SmoothDamp(this.m_Transform.position, vector, ref this.m_SmoothPositionVelocity, (!this.m_IsZoomed && !immediateMove) ? this.m_MoveSmoothing : 0f));
        }
        private void UpdateRecoil()
        {
            if (Mathf.Abs(this.m_TargetRecoil - this.m_Recoil) > 0.001f)
            {
                float num = 0f;
                this.m_Recoil = Mathf.SmoothDamp(this.m_Recoil, this.m_TargetRecoil, ref num, this.m_RecoilSpring);
                EventHandler.ExecuteEvent<float>(this.m_Character, "OnCameraUpdateRecoil", this.m_Recoil);
            }
            else if (this.m_Recoil != 0f)
            {
                float num2 = 0f;
                this.m_TargetRecoil = (this.m_Recoil = Mathf.SmoothDamp(this.m_Recoil, 0f, ref num2, this.m_RecoilDampening));
                EventHandler.ExecuteEvent<float>(this.m_Character, "OnCameraUpdateRecoil", this.m_Recoil);
                if (this.m_Recoil < 0.001f)
                {
                    this.m_Recoil = 0f;
                }
            }
        }
        private void CheckForCharacterClipping()
        {
            bool flag = this.m_Anchor.InverseTransformPoint(this.m_Transform.position).sqrMagnitude > this.m_DisableRendererDistanceSqr;
            if (flag != this.m_PrevRenderersEnabled)
            {
                for (int i = 0; i < this.m_Renderers.Count; i++)
                {
                    this.m_Renderers[i].enabled = flag;
                }
                this.m_PrevRenderersEnabled = flag;
            }
        }
        private void DeathOrbitMovement()
        {
            Transform transform = (!(this.m_DeathAnchor != null)) ? this.m_Anchor : this.m_DeathAnchor;
            Quaternion quaternion = Quaternion.identity;
            if ((double)(this.m_PrevTargetPosition - transform.position).sqrMagnitude < 0.01)
            {
                quaternion = Quaternion.AngleAxis(this.m_DeathRotationSpeed * Time.fixedDeltaTime, Vector3.up);
            }
            Vector3 normalized = (this.m_Transform.position - transform.position).normalized;
            float num = this.m_DeathOrbitDistance;
            if (Physics.SphereCast(transform.position, this.m_CollisionRadius, normalized.normalized, out this.m_RaycastHit, num, 2013265897))
            {
                num = this.m_RaycastHit.distance;
            }
            Vector3 vector = transform.position + quaternion * normalized * num;
            this.m_Transform.position =(Vector3.MoveTowards(this.m_Transform.position, vector, this.m_DeathOrbitMoveSpeed));
            this.m_Transform.rotation =(Quaternion.LookRotation(-normalized));
            this.m_PrevTargetPosition = transform.position;
        }
        private void LookAtCharacter(Transform anchor, float roationSpeed)
        {
            Quaternion quaternion = Quaternion.LookRotation(anchor.position - this.m_Transform.position);
            this.m_Transform.rotation = (Quaternion.Slerp(this.m_Transform.rotation, quaternion, roationSpeed * Time.fixedDeltaTime));
        }
        private void OnStartScopeFocus(bool scope)
        {
            this.m_IsScoped = scope;
            if (this.m_IsScoped)
            {
                CameraController.m_Camera.fieldOfView =(this.m_ScopeFOV);
            }
            else
            {
                CameraController.m_Camera.fieldOfView =(this.m_NormalFOV);
                this.Move(0f, true);
            }
        }
        private void OnCharacterGrounded(bool grounded)
        {
            if (!this.m_CanTurnInAir)
            {
                this.m_RestrictRotation = !grounded;
            }
        }
        private void OnCharacterLeaveCover()
        {
            this.m_LimitYaw = false;
        }
        private void OnCharacterPopFromCover(bool popped)
        {
            this.m_LimitYaw = popped;
            if (this.m_LimitYaw)
            {
                float num = Utility.RestrictInnerAngle(this.m_CharacterTransform.eulerAngles.y + this.m_MinYawLimit);
                float num2 = Utility.RestrictInnerAngle(this.m_CharacterTransform.eulerAngles.y + this.m_MaxYawLimit);
                if (num2 < num)
                {
                    num2 += 360f;
                }
                if (this.m_Yaw < num || this.m_Yaw > num2)
                {
                    this.m_Yaw = Utility.RestrictInnerAngle(this.m_Yaw);
                }
            }
        }
        private void OnStaticHeight(bool staticHeight)
        {
            this.m_StaticHeight = staticHeight;
            this.m_StaticYPosition = this.m_Transform.position.y;
        }
        private void OnCharacterDeath()
        {
            this.m_CharacterHasDied = true;
            this.m_TargetRecoil = (this.m_Recoil = 0f);
            this.m_PrevTargetPosition = -((!(this.m_DeathAnchor != null)) ? this.m_Anchor : this.m_DeathAnchor).position;
            EventHandler.ExecuteEvent<float>(this.m_Character, "OnCameraUpdateRecoil", this.m_Recoil);
        }
        private void OnCharacterSpawn()
        {
            this.m_CharacterHasDied = false;
            this.m_PrevRenderersEnabled = false;
            this.m_Yaw = this.m_CharacterTransform.eulerAngles.y;
            this.m_Pitch = this.m_StartPitch;
            this.ImmediatePosition();
        }
        public void ImmediatePosition()
        {
            Vector3 position = this.m_CharacterTransform.position + this.m_AnchorStartOffset + this.m_CameraOffset;
            this.m_Transform.rotation = (Quaternion.Euler(this.m_StartPitch, this.m_Yaw, 0f));
            this.m_Transform.position =(position);
            CameraController.m_Camera.fieldOfView =(this.m_NormalFOV);
        }
        private Ray LookRay(bool applyRecoil)
        {
            if (this.m_ThirdPersonView)
            {
                Vector3 zero = Vector3.zero;
                zero.x = (float)Screen.width * this.m_CrosshairsLocation.x;
                zero.y = (float)Screen.height * this.m_CrosshairsLocation.y;
                this.m_TargetRay = CameraController.m_Camera.ScreenPointToRay(zero);
                this.m_TargetRay.origin=(this.m_TargetRay.origin - this.m_TargetRay.direction * ((!this.m_IsZoomed) ? this.m_CameraOffset.z : this.m_ZoomCameraOffset.z));
                if (applyRecoil && this.m_Recoil != 0f)
                {
                    Vector3 direction = this.m_TargetRay.direction;
                    direction.y += this.m_Recoil;
                    this.m_TargetRay.direction =(direction);
                }
            }
            else
            {
                this.m_TargetRay.direction =(this.m_CharacterTransform.forward);
                this.m_TargetRay.origin=(this.m_CharacterTransform.position);
            }
            return this.m_TargetRay;
        }
        public Vector3 SharedMethod_TargetLookPosition(bool applyRecoil)
        {
            Ray ray = this.LookRay(applyRecoil);
            if (this.m_ThirdPersonView && Physics.Raycast(ray, out this.m_RaycastHit, float.PositiveInfinity, 2013265897))
            {
                return this.m_RaycastHit.point;
            }
            return ray.GetPoint(10000f);
        }
        public Vector3 SharedMethod_TargetLookDirection(bool applyRecoil)
        {
            return this.LookRay(applyRecoil).direction;
        }
        public float SharedMethod_TargetPitchNormalized(bool applyRecoil)
        {
            if (!this.m_ThirdPersonView)
            {
                return 0f;
            }
            Ray ray = this.LookRay(applyRecoil);
            float num = Utility.RestrictPositiveAngle(Quaternion.LookRotation(ray.direction).eulerAngles.x);
            if (ray.direction.y < 0f)
            {
                return -num / this.m_MaxPitchLimit;
            }
            return (Utility.RestrictAngle(num) - 360f) / this.m_MinPitchLimit;
        }
    }
}
