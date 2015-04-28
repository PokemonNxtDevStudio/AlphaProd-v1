using System;
using System.Collections.Generic;
using UnityEngine;

namespace NXT
{
    public class Scheduler : MonoBehaviour
    {
        private static Scheduler s_Instance;
        private static bool m_Initialized;
        private List<ScheduledEvent> m_ActiveEvents = new List<ScheduledEvent>();
        private static Scheduler Instance
        {
            get
            {
                if (!Scheduler.m_Initialized)
                {
                    Debug.LogError("Error: Scheduler is null. Ensure the Scheduler component has been added to a GameObject.");
                }
                return Scheduler.s_Instance;
            }
        }
        public List<ScheduledEvent> ActiveEvents
        {
            get
            {
                return this.m_ActiveEvents;
            }
        }
        private void Awake()
        {
            Scheduler.s_Instance = this;
            Scheduler.m_Initialized = true;
        }
        private void Update()
        {
            for (int i = this.m_ActiveEvents.Count - 1; i > -1; i--)
            {
                if (this.m_ActiveEvents[i].EndTime <= Time.time)
                {
                    this.Execute(i);
                }
            }
        }


      
        public static ScheduledEvent Schedule(float delay, Action callback)
        {
            return Scheduler.Instance.AddEventInternal(delay, callback);
        }
        private ScheduledEvent AddEventInternal(float delay, Action callback)
        {
            if (delay == 0f)
            {
                callback();
                return null;
            }
            ScheduledEvent scheduledEvent = ObjectPool.Get<ScheduledEvent>();
            scheduledEvent.Reset();
            scheduledEvent.EndTime = Time.time + delay;
            scheduledEvent.Callback = callback;
            this.m_ActiveEvents.Add(scheduledEvent);
            return scheduledEvent;
        }
        public static ScheduledEvent Schedule(float delay, Action<object> callback, object arg)
        {
            return Scheduler.Instance.AddEventInternal(delay, callback, arg);
        }
        private ScheduledEvent AddEventInternal(float delay, Action<object> callbackArg, object arg)
        {
            if (delay == 0f)
            {
                callbackArg(arg);
                return null;
            }
            ScheduledEvent scheduledEvent = ObjectPool.Get<ScheduledEvent>();
            scheduledEvent.Reset();
            scheduledEvent.EndTime = Time.time + delay;
            scheduledEvent.CallbackArg = callbackArg;
            scheduledEvent.Argument = arg;
            this.m_ActiveEvents.Add(scheduledEvent);
            return scheduledEvent;
        }
        public static void Cancel(ScheduledEvent scheduledEvent)
        {
            Scheduler.Instance.CancelEventInternal(scheduledEvent);
        }
        private void CancelEventInternal(ScheduledEvent scheduledEvent)
        {
            if (this.m_ActiveEvents.Contains(scheduledEvent))
            {
                this.m_ActiveEvents.Remove(scheduledEvent);
                ObjectPool.Return<ScheduledEvent>(scheduledEvent);
                scheduledEvent = null;
            }
        }
        private void Execute(int index)
        {
            ScheduledEvent scheduledEvent = this.m_ActiveEvents[index];
            this.m_ActiveEvents.RemoveAt(index);
            if (scheduledEvent.Callback != null)
            {
                scheduledEvent.Callback();
            }
            else
            {
                scheduledEvent.CallbackArg(scheduledEvent.Argument);
            }
            ObjectPool.Return<ScheduledEvent>(scheduledEvent);
        }
    }
}
