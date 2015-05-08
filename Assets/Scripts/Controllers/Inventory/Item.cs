using System;
using UnityEngine;
using NXT;
using NXT.Controllers;
namespace NXT.Inventory
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField]
        protected ItemBaseType m_ItemType;
        private SharedMethodArg<SphereCollider> m_SetItemCollider;
        private SphereCollider m_SphereCollider;
        public ItemBaseType ItemType
        {
            get
            {
                return this.m_ItemType;
            }
            set
            {
                this.m_ItemType = value;
            }
        }
        protected virtual void Awake()
        {
            this.m_SphereCollider = base.GetComponent<SphereCollider>();
            if (this.m_SphereCollider != null)
            {
                EventHandler.RegisterEvent<bool>(base.transform.GetComponentInParent<RigidbodyCharacterController>().gameObject, "OnControllerEnableItemCollider", new Action<bool>(this.EnableItemCollider));
            }
        }
        protected virtual void Start()
        {
            SharedManager.InitializeSharedFields(base.transform.GetComponentInParent<RigidbodyCharacterController>().gameObject, this);
            if (this.m_SphereCollider != null && this.m_SetItemCollider != null)
            {
                this.m_SetItemCollider.Invoke(this.m_SphereCollider);
                this.m_SphereCollider.enabled =(false);
            }
        }
        protected virtual void OnEnable()
        {
            if (this.m_SetItemCollider != null)
            {
                this.m_SetItemCollider.Invoke(this.m_SphereCollider);
                this.m_SphereCollider.enabled =(false);
            }
        }
        protected virtual void OnDisable()
        {
            if (this.m_SetItemCollider != null)
            {
                this.m_SetItemCollider.Invoke(null);
            }
        }
        public virtual void Init(Inventory inventory)
        {
        }
        private void EnableItemCollider(bool enable)
        {
            if (this.m_SetItemCollider != null)
            {
                this.m_SetItemCollider.Invoke((!enable) ? null : this.m_SphereCollider);
            }
            else
            {
                this.m_SphereCollider.enabled=(enable);
            }
        }
    }
}
