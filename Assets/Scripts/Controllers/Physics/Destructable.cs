using System;
using UnityEngine;


namespace NXT.PhysX
{
    public abstract class Destructable : MonoBehaviour
    {
        [SerializeField]
        private float m_DamageAmount = 100f;
        [SerializeField]
        private float m_ImpactForce = 5f;
        [SerializeField]
        private string m_DamageEvent;
        [SerializeField]
        private GameObject m_Explosion;
        [SerializeField]
        private GameObject m_Decal;
        [SerializeField]
        private GameObject m_Dust;
        private GameObject m_GameObject;
        private Transform m_Transform;
        protected virtual void Awake()
        {
            this.m_GameObject = base.gameObject;
            this.m_Transform = base.transform;
        }
        protected void Destruction(Collision collisionObj)
        {
            Vector3 vector = Vector3.zero;
            Vector3 vector2 = Vector3.up;
            if (collisionObj == null)
            {
                vector = this.m_Transform.position;
                vector2 = this.m_Transform.up;
            }
            else
            {
                vector = collisionObj.contacts[0].point;
                vector2 = collisionObj.contacts[0].normal;
            }
            Quaternion quaternion = Quaternion.LookRotation(vector2);
            if (this.m_Explosion != null)
            {
                ObjectPool.Instantiate(this.m_Explosion, this.m_Transform.position, this.m_Transform.rotation);
            }
            else if (collisionObj != null)
            {
                if (!string.IsNullOrEmpty(this.m_DamageEvent))
                {
                    EventHandler.ExecuteEvent<float, Vector3, Vector3>(collisionObj.gameObject, this.m_DamageEvent, this.m_DamageAmount, this.m_Transform.position, this.m_Transform.forward * -this.m_ImpactForce);
                }
                /*
                Health componentInParent;
                if ((componentInParent = collisionObj.get_transform().GetComponentInParent<Health>()) != null)
                {
                    componentInParent.Damage(this.m_DamageAmount, this.m_Transform.position, this.m_Transform.get_forward() * -this.m_ImpactForce);
                }*/
            }
         /*   if (this.m_Decal != null && (collisionObj == null || DecalManager.CanAddDecal(collisionObj.gameObject.get_layer())))
            {
                DecalManager.Add(this.m_Decal, vector + vector2 * 0.02f, this.m_Decal.get_transform().get_rotation() * quaternion, (collisionObj == null) ? null : collisionObj.get_transform());
            }
          */
            if (this.m_Dust != null)
            {
                ObjectPool.Instantiate(this.m_Dust, vector, this.m_Dust.transform.rotation * quaternion);
            }
            ObjectPool.Destroy(this.m_GameObject);
            base.enabled =(false);
        }
    }
}
