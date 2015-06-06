/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Utilities
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class DebugTimer
    {
        private static Stack<Stopwatch> _watches = new Stack<Stopwatch>();

        [Conditional("UNITY_EDITOR")]
        public static void Start()
        {
            var sw = new Stopwatch();
            _watches.Push(sw);
            sw.Start();
        }

        [Conditional("UNITY_EDITOR")]
        public static void EndTicks(string label)
        {
            var sw = _watches.Pop();

            UnityEngine.Debug.Log(string.Format(label, sw.ElapsedTicks));
        }

        [Conditional("UNITY_EDITOR")]
        public static void EndMilliseconds(string label)
        {
            var sw = _watches.Pop();

            UnityEngine.Debug.Log(string.Format(label, sw.ElapsedMilliseconds));
        }
    }
}
