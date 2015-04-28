using System;
using System.Reflection;
using UnityEngine;
namespace NXT
{
    public abstract class SharedProperty
    {
        protected string propertyName;
    }
    public class SharedProperty<T> : SharedProperty
    {
        private Func<T> m_Getter;
        private Action<T> m_Setter;
        public SharedProperty(PropertyInfo property, object owner, string name)
        {
            this.propertyName = name;
            if (property.GetGetMethod(true) != null)
            {
                this.m_Getter = (Func<T>)Delegate.CreateDelegate(typeof(Func<T>), owner, property.GetGetMethod(true));
            }
            if (property.GetSetMethod(true) != null)
            {
                this.m_Setter = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), owner, property.GetSetMethod(true));
            }
        }
        public T Get()
        {
            if (this.m_Getter == null)
            {
                Debug.LogError("Unable to get " + this.propertyName + ": Getter does not exist");
                return default(T);
            }
            return this.m_Getter();
        }
        public void Set(T value)
        {
            if (this.m_Setter == null)
            {
                Debug.LogError("Unable to set " + this.propertyName + ": Setter does not exist");
                return;
            }
            this.m_Setter(value);
        }
    }
}
