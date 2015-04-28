using System;
using System.Reflection;
using UnityEngine;

namespace NXT
{
    public class SharedMethodArg<T> : SharedMethod
    {
        private Action<T> m_Invoker;
        public SharedMethodArg(MethodInfo method, object owner, string name)
        {
            this.m_MethodName = name;
            this.m_Invoker = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), owner, method);
        }
        public void Invoke(T value)
        {
            if (this.m_Invoker == null)
            {
                Debug.LogError("Unable to invoke " + this.m_MethodName + ": Method does not exist");
                return;
            }
            this.m_Invoker(value);
        }
    }
}
