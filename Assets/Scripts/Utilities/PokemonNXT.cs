using UnityEngine;

namespace NXT {
    using Templates;

    public class PokemonNXT {

        private PokemonNXT _instance;

        private PokemonNXT() { }

        public PokemonNXT Instance {
            get {
                if(_instance == null)
                    _instance = new PokemonNXT();
                return _instance;
            }
        }

        public static void Info(string str) {
            Debug.Log(str);
        }
        public static void Info(string scriptName, string methodName, string str) {
            Debug.Log(str + " :: " + scriptName + " ::" + methodName);
        }
        public static void IfInfo(bool b, string str) {
            if(b) Debug.Log(str);
        }
        public static void IfInfo(bool b, string scriptName, string methodName, string str) {
            if(b) Debug.Log(str + " :: " + scriptName + " ::" + methodName);
        }

        public static void Warn(string str) {
            Debug.Log(str);
        }
        public static void Warn(string scriptName, string methodName, string str) {
            Debug.Log(str + " :: " + scriptName + " ::" + methodName);
        }
        public static void IfWarn(bool b, string str) {
            if(b) Debug.Log(str);
        }
        public static void IfWarn(bool b, string scriptName, string methodName, string str) {
            if(b) Debug.Log(str + " :: " + scriptName + " ::" + methodName);
        }

        public static void Error(string str) {
            Debug.LogError(str);
        }
        public static void Error(string scriptName, string methodName, string str) {
            Debug.LogError(str + " :: " + scriptName + " ::" + methodName);
        }
        public static void Assert(bool b, string str) {
            if(!b) Debug.LogError(str);
        }
        public static void Assert(bool b, string scriptName, string methodName, string str) {
            if(!b) Debug.LogError(str + " :: " + scriptName + " ::" + methodName);
        }
    }
}