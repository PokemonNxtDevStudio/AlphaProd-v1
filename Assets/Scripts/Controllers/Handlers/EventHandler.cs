
/*Event Handler Framework
 * 
 * Version1.0 Written my Reuben P w/ google.
 */
using System;
using System.Collections.Generic;

namespace NXT
{
    public class EventHandler
    {
        private static Dictionary<object, Dictionary<string, Delegate>> s_EventTable = new Dictionary<object, Dictionary<string, Delegate>>();

        private static void RegisterEvent(object obj, string eventName, Delegate handler)
        {
            Dictionary<string, Delegate> dictionary;
            if (!EventHandler.s_EventTable.TryGetValue(obj, out dictionary))
            {
                dictionary = new Dictionary<string, Delegate>();
                EventHandler.s_EventTable.Add(obj, dictionary);
            }
            Delegate a;
            if (dictionary.TryGetValue(eventName, out a))
                dictionary[eventName] = Delegate.Combine(a, handler);
            else
                dictionary.Add(eventName, handler);
        }

        private static Delegate GetDelegate(object obj, string eventName)
        {
            Dictionary<string, Delegate> dictionary;
            Delegate @delegate;
            if (EventHandler.s_EventTable.TryGetValue(obj, out dictionary) && dictionary.TryGetValue(eventName, out @delegate))
                return @delegate;
            return (Delegate)null;
        }

        private static void UnregisterEvent(object obj, string eventName, Delegate handler)
        {
            Dictionary<string, Delegate> dictionary;
            Delegate source;
            if (!EventHandler.s_EventTable.TryGetValue(obj, out dictionary) || !dictionary.TryGetValue(eventName, out source))
                return;
            dictionary[eventName] = Delegate.Remove(source, handler);
        }

        public static void RegisterEvent(object obj, string eventName, Action handler)
        {
            EventHandler.RegisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void RegisterEvent<T>(object obj, string eventName, Action<T> handler)
        {
            EventHandler.RegisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void RegisterEvent<T, U>(object obj, string eventName, Action<T, U> handler)
        {
            EventHandler.RegisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void RegisterEvent<T, U, V>(object obj, string eventName, Action<T, U, V> handler)
        {
            EventHandler.RegisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void ExecuteEvent(object obj, string eventName)
        {
            Action action = EventHandler.GetDelegate(obj, eventName) as Action;
            if (action == null)
                return;
            action();
        }

        public static void ExecuteEvent<T>(object obj, string eventName, T arg1)
        {
            Action<T> action = EventHandler.GetDelegate(obj, eventName) as Action<T>;
            if (action == null)
                return;
            action(arg1);
        }

        public static void ExecuteEvent<T, U>(object obj, string eventName, T arg1, U arg2)
        {
            Action<T, U> action = EventHandler.GetDelegate(obj, eventName) as Action<T, U>;
            if (action == null)
                return;
            action(arg1, arg2);
        }

        public static void ExecuteEvent<T, U, V>(object obj, string eventName, T arg1, U arg2, V arg3)
        {
            Action<T, U, V> action = EventHandler.GetDelegate(obj, eventName) as Action<T, U, V>;
            if (action == null)
                return;
            action(arg1, arg2, arg3);
        }

        public static void UnregisterEvent(object obj, string eventName, Action handler)
        {
            EventHandler.UnregisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void UnregisterEvent<T>(object obj, string eventName, Action<T> handler)
        {
            EventHandler.UnregisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void UnregisterEvent<T, U>(object obj, string eventName, Action<T, U> handler)
        {
            EventHandler.UnregisterEvent(obj, eventName, (Delegate)handler);
        }

        public static void UnregisterEvent<T, U, V>(object obj, string eventName, Action<T, U, V> handler)
        {
            EventHandler.UnregisterEvent(obj, eventName, (Delegate)handler);
        }
    }
}
