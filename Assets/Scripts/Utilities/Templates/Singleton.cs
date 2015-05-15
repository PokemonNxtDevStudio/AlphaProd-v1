using UnityEngine;

//@IFDO: Add functionality for creating object by itself if it does not exist, instead of logging error

/// <summary>
/// 
/// ----------------------- Usage ---------------------------
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// 
/// ---------------- Usage for inheritance ------------------
/// public class BaseSingleton<T>: PokeNXTSingleton<T> where T: BaseSingleton<T> { } 
/// public class InheritingSingleton : BaseSingleton<InheritingSingleton> {
///     protected SoundEffectsHelper() { }
/// }
/// </summary>

namespace NXT.Templates {

    [RequireComponent(typeof(DontDestroyOnLoad))]
    public class Singleton<T>: BaseBehavior where T: BaseBehavior {

        private static T _instance;
        private static object _lock = new object();
        private static bool _applicationIsQuitting = false;        

        public static T Instance {
            get {
                if(_applicationIsQuitting) {
                    PokemonNXT.Warn("[Singleton] Instance '" + typeof(T) + "' already destroyed on application quit." + " Won't create again. Returning null.");
                    return null;
                }

                lock(_lock) {
                    if(_instance == null) {
                        _instance = (T)FindObjectOfType(typeof(T));
                        PokemonNXT.Assert(_instance != null, "[Singleton]: Singletons need to be manually added to a gameobject");
                        PokemonNXT.IfWarn(FindObjectsOfType(typeof(T)).Length != 1, "[Singleton] There are more than one Singletons of this type."
                                                                                         +"Returnig first instance found");
                    }
                    return _instance;
                }
            }
        }

        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        ///   it will create a buggy ghost object that will stay on the Editor scene
        ///   even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        public virtual void OnDestroy() {
            _applicationIsQuitting = true;
        }
    }
}