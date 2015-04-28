using System;
namespace NXT
{
    public class ScheduledEvent
    {
        private Action m_Callback;
        private Action<object> m_CallbackArg;
        private object m_Argument;
        private float m_EndTime;
        public Action Callback
        {
            get
            {
                return this.m_Callback;
            }
            set
            {
                this.m_Callback = value;
            }
        }
        public Action<object> CallbackArg
        {
            get
            {
                return this.m_CallbackArg;
            }
            set
            {
                this.m_CallbackArg = value;
            }
        }
        public object Argument
        {
            get
            {
                return this.m_Argument;
            }
            set
            {
                this.m_Argument = value;
            }
        }
        public float EndTime
        {
            get
            {
                return this.m_EndTime;
            }
            set
            {
                this.m_EndTime = value;
            }
        }
        public void Reset()
        {
            this.m_Callback = null;
            this.m_CallbackArg = null;
            this.m_Argument = null;
        }
    }
}
