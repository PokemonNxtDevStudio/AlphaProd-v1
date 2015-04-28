
using UnityEngine;
using System;
using NXT;
using NXT.Controllers;

namespace NXT
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField]
        private const string m_YawInputName = "Mouse X";
        [SerializeField]
        private const string m_PitchInputName = "Mouse Y";
        [SerializeField]
        private const string m_FocusInputName = "Fire2";
        private float m_Yaw;
        private float m_Pitch;
        private bool m_Focus;

        public float Yaw
        {
            get
            {
                return this.m_Yaw;
            }
        }

        public float Pitch
        {
            get
            {
                return this.m_Pitch;
            }
        }

        public bool Focus
        {
            get
            {
                return this.m_Focus;
            }
        }

        public CameraHandler()
        {

        }

        private void Update()
        {
            this.m_Yaw = Input.GetAxis("Mouse X");
            this.m_Pitch = Input.GetAxis("Mouse Y");
            this.m_Focus = Input.GetButton("Fire2");
        }

        private void Start()
        {
            EventHandler.RegisterEvent<bool>(GetComponent<CameraController>().Character, "OnAllowGameplayInput", new Action<bool>(this.AllowGameplayInput));
        }

        private void AllowGameplayInput(bool allow)
        {
            ((Behaviour)this).enabled = allow;
        }
    }

}