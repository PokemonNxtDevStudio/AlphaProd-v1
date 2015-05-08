using System;
using System.Collections.Generic;
using UnityEngine;
namespace NXT
{
    public class ObjectPool : MonoBehaviour
    {
        private static ObjectPool s_Instance;
        private static bool m_Initialized;
        private Dictionary<int, Stack<GameObject>> m_GameObjectPool = new Dictionary<int, Stack<GameObject>>();
        private Dictionary<int, int> m_SpawnedGameObjects = new Dictionary<int, int>();
        private Dictionary<Type, object> m_GenericPool = new Dictionary<Type, object>();
        private static ObjectPool Instance
        {
            get
            {
                if (!ObjectPool.m_Initialized)
                {
                    Debug.LogError("Error: ObjectPool is null. Ensure the ObjectPool component has been added to a GameObject.");
                }
                return ObjectPool.s_Instance;
            }
        }
        private void Awake()
        {
            ObjectPool.s_Instance = this;
            ObjectPool.m_Initialized = true;
        }
        public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation)
        {
            return ObjectPool.Instantiate(original, position, rotation, null);
        }
        public static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation, Transform parent)
        {
            return ObjectPool.Instance.InstantiateInternal(original, position, rotation, parent);
        }
        private GameObject InstantiateInternal(GameObject original, Vector3 position, Quaternion rotation, Transform parent)
        {
            int instanceID = original.GetInstanceID();
            Stack<GameObject> stack;
            GameObject gameObject;
            if (this.m_GameObjectPool.TryGetValue(instanceID, out stack) && stack.Count > 0)
            {
                gameObject = stack.Pop();
                gameObject.transform.position =(position);
                gameObject.transform.rotation =(rotation);
                gameObject.transform.parent =(parent);
                gameObject.SetActive(true);
                this.m_SpawnedGameObjects.Add(gameObject.GetInstanceID(), instanceID);
                return gameObject;
            }
            gameObject = (GameObject)Instantiate(original, position, rotation);
            gameObject.transform.parent =(parent);
            this.m_SpawnedGameObjects.Add(gameObject.GetInstanceID(), instanceID);
            return gameObject;
        }
        public static void Destroy(GameObject spawnedObject)
        {
            ObjectPool.Instance.DestroyInternal(spawnedObject);
        }
        private void DestroyInternal(GameObject spawnedObject)
        {
            int instanceID = spawnedObject.GetInstanceID();
            if (!this.m_SpawnedGameObjects.ContainsKey(instanceID))
            {
                Debug.LogError(string.Concat(new object[]
				{
					"Unable to pool ",
					spawnedObject,
					" (instance ",
					instanceID,
					"): the GameObject was not instantiated with ObjectPool.Instantiate ",
					Time.time
				}));
                return;
            }
            int key = this.m_SpawnedGameObjects[instanceID];
            this.m_SpawnedGameObjects.Remove(instanceID);
            spawnedObject.SetActive(false);
            spawnedObject.transform.parent =(base.transform);
            Stack<GameObject> stack;
            if (this.m_GameObjectPool.TryGetValue(key, out stack))
            {
                stack.Push(spawnedObject);
            }
            else
            {
                stack = new Stack<GameObject>();
                stack.Push(spawnedObject);
                this.m_GameObjectPool.Add(key, stack);
            }
        }
        public static T Get<T>()
        {
            return ObjectPool.Instance.GetInternal<T>();
        }
        private T GetInternal<T>()
        {
            object obj;
            if (this.m_GenericPool.TryGetValue(typeof(T), out obj))
            {
                List<T> list = obj as List<T>;
                if (list.Count > 0)
                {
                    T result = list[0];
                    list.RemoveAt(0);
                    return result;
                }
            }
            return Activator.CreateInstance<T>();
        }
        public static void Return<T>(T obj)
        {
            ObjectPool.Instance.ReturnInternal<T>(obj);
        }
        private void ReturnInternal<T>(T obj)
        {
            object obj2;
            if (this.m_GenericPool.TryGetValue(typeof(T), out obj2))
            {
                List<T> list = obj2 as List<T>;
                list.Add(obj);
            }
            else
            {
                List<T> list2 = new List<T>();
                list2.Add(obj);
                this.m_GenericPool.Add(typeof(T), list2);
            }
        }
    }
}
