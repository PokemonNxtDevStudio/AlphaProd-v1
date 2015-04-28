using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NXT
{
    public static class SharedManager
    {
        private const string SharedPropertyString = "SharedProperty_";
        private const string SharedMethodString = "SharedMethod_";
        private const string ConstantString = "m_";
        private static Dictionary<GameObject, List<Component>> ownerList = new Dictionary<GameObject, List<Component>>();
        public static void Register(Component ownerComponent)
        {
            GameObject gameObject = ownerComponent.gameObject;
            List<Component> list = null;
            SharedManager.ownerList.TryGetValue(gameObject, out list);
            if (list == null)
            {
                list = new List<Component>();
            }
            list.Add(ownerComponent);
            if (!SharedManager.ownerList.ContainsKey(gameObject))
            {
                SharedManager.ownerList.Add(gameObject, list);
            }
        }
        public static void InitializeSharedFields(GameObject targetGameObject, object targetObject)
        {
            List<Component> list;
            SharedManager.ownerList.TryGetValue(targetGameObject, out list);
            if (list == null)
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Component component = list[i];
                FieldInfo[] allFields = SharedManager.GetAllFields(targetObject.GetType());
                for (int j = 0; j < allFields.Length; j++)
                {
                    Type baseType = allFields[j].FieldType.BaseType;
                    if (baseType != null)
                    {
                        if (baseType.Equals(typeof(SharedProperty)))
                        {
                            string text = allFields[j].Name;
                            if (text.StartsWith("m_"))
                            {
                                text = text.Substring(2);
                            }
                            string text2 = "SharedProperty_" + text;
                            PropertyInfo property;
                            if ((property = SharedManager.GetProperty(component.GetType(), text2)) != null)
                            {
                                allFields[j].SetValue(targetObject, Activator.CreateInstance(allFields[j].FieldType, new object[]
								{
									property,
									component,
									text2
								}));
                            }
                        }
                        else if (baseType.Equals(typeof(SharedMethod)))
                        {
                            string text3 = allFields[j].Name;
                            if (text3.StartsWith("m_"))
                            {
                                text3 = text3.Substring(2);
                            }
                            string text4 = "SharedMethod_" + text3;
                            MethodInfo method;
                            if ((method = SharedManager.GetMethod(component.GetType(), text4)) != null)
                            {
                                allFields[j].SetValue(targetObject, Activator.CreateInstance(allFields[j].FieldType, new object[]
								{
									method,
									component,
									text4
								}));
                            }
                        }
                    }
                }
            }
        }
        private static FieldInfo[] GetAllFields(Type type)
        {
            List<FieldInfo> list = new List<FieldInfo>();
            SharedManager.GetAllFields(type, ref list);
            return list.ToArray();
        }
        private static void GetAllFields(Type type, ref List<FieldInfo> fieldList)
        {
            if (type == null)
            {
                return;
            }
            BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            FieldInfo[] fields = type.GetFields(bindingAttr);
            for (int i = 0; i < fields.Length; i++)
            {
                fieldList.Add(fields[i]);
            }
            SharedManager.GetAllFields(type.BaseType, ref fieldList);
        }
        private static PropertyInfo GetProperty(Type type, string propertyName)
        {
            if (type == null)
            {
                return null;
            }
            PropertyInfo property;
            if ((property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) != null)
            {
                return property;
            }
            return SharedManager.GetProperty(type.BaseType, propertyName);
        }
        private static MethodInfo GetMethod(Type type, string methodName)
        {
            if (type == null)
            {
                return null;
            }
            MethodInfo method;
            if ((method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) != null)
            {
                return method;
            }
            return SharedManager.GetMethod(type.BaseType, methodName);
        }
    }
}
