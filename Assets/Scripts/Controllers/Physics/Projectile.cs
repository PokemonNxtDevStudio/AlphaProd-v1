using System;
using UnityEngine;
using NXT;
namespace NXT.PhysX
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : Destructable
    {
        [SerializeField]
        private float m_Speed;
        [SerializeField]
        private float m_Lifespan;
        private ScheduledEvent m_ScheduledActivation;
        private Vector3 m_MovementForce;
        private Rigidbody m_Rigidbody;
        private TrailRenderer m_TrailRenderer;
        protected override void Awake()
        {
            base.Awake();
            this.m_Rigidbody = base.GetComponent<Rigidbody>();
            this.m_TrailRenderer = base.GetComponent<TrailRenderer>();
        }
        public void Initialize(Vector3 direction, Vector3 torque)
        {
            base.enabled =(true);
            this.m_MovementForce = direction * this.m_Speed;
            if (this.m_MovementForce != Vector3.zero)
            {
              //  this.m_Rigidbody.AddForce(this.m_MovementForce, 2);
            }
            this.m_Rigidbody.AddRelativeTorque(torque);
        }
        private void OnEnable()
        {
            if (this.m_TrailRenderer)
            {
                this.m_TrailRenderer.enabled =(true);
            }
            this.m_ScheduledActivation = Scheduler.Schedule(this.m_Lifespan, new Action(this.LifespanElapsed));
        }
        private void OnDisable()
        {
            Scheduler.Cancel(this.m_ScheduledActivation);
            if (this.m_TrailRenderer)
            {
                this.m_TrailRenderer.enabled =(false);
            }
        }
        private void FixedUpdate()
        {
            this.m_Rigidbody.AddForce(this.m_MovementForce - this.m_Rigidbody.velocity, ForceMode.Impulse);
        }
        private void LifespanElapsed()
        {
            base.Destruction(null);
        }
        public void OnCollisionEnter(Collision collision)
        {
            if (!base.enabled)
            {
                return;
            }
            base.Destruction(collision);
        }
    }
}
