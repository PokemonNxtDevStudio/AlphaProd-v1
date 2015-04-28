using System;
using System.Reflection;
using UnityEngine;
namespace NXT
{
    public abstract class SharedMethod
    {
        protected string m_MethodName;
    }
    public class SharedMethod<R> : SharedMethod
    {
        private Func<R> m_Invoker;
        public SharedMethod(MethodInfo method, object owner, string name)
        {
            this.m_MethodName = name;
            this.m_Invoker = (Func<R>)Delegate.CreateDelegate(typeof(Func<R>), owner, method);
        }
        public R Invoke()
        {
            if (this.m_Invoker == null)
            {
                Debug.LogError("Unable to invoke " + this.m_MethodName + ": Method does not exist");
                return default(R);
            }
            return this.m_Invoker();
        }
    }

    public class SharedMethod<T, R> : SharedMethod
    {
        private Func<T, R> m_Invoker;
        public SharedMethod(MethodInfo method, object owner, string name)
        {
            this.m_MethodName = name;
            this.m_Invoker = (Func<T, R>)Delegate.CreateDelegate(typeof(Func<T, R>), owner, method);
        }
        public R Invoke(T value)
        {
            if (this.m_Invoker == null)
            {
                Debug.LogError("Unable to invoke " + this.m_MethodName + ": Method does not exist");
                return default(R);
            }
            return this.m_Invoker(value);
        }
    }
}