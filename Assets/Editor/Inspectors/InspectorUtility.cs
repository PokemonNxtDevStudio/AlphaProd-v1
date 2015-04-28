using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace NXT
{
    /// <summary>
    /// Utility class for the Third Person Controller inspectors.
    /// </summary>
    public static class InspectorUtility
    {
        private static GUIStyle m_BoldFoldout;
        public static GUIStyle BoldFoldout
        {
            get
            {
                if (m_BoldFoldout == null)
                {
                    m_BoldFoldout = new GUIStyle(EditorStyles.foldout);
                    m_BoldFoldout.fontStyle = FontStyle.Bold;
                }
                return m_BoldFoldout;
            }
        }

        private static Dictionary<string, SerializedProperty> m_PropertyStringMap;

        /// <summary>
        /// Initializes the property string map.
        /// </summary>
        public static void Initialize()
        {
            m_PropertyStringMap = new Dictionary<string, SerializedProperty>();
        }

        /// <summary>
        /// Uses a dictionary to lookup a property from a string key.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The found SerializedProperty.</returns>
        public static SerializedProperty PropertyFromName(SerializedObject serializedObject, string name)
        {
            SerializedProperty property = null;
            if (m_PropertyStringMap.TryGetValue(name, out property))
            {
                return property;
            }

            property = serializedObject.FindProperty(name);
            if (property == null)
            {
                Debug.LogError("Unable to find property " + name);
                return null;
            }
            m_PropertyStringMap.Add(name, property);
            return property;
        }

        /// <summary>
        /// Draws a float slider which has a min and max label beside it.
        /// </summary>
        /// <param name="name">The name of the slider.</param>
        /// <param name="minValue">The current minimum value.</param>
        /// <param name="maxValue">The current maximum value.</param>
        /// <param name="min">The minimum value that can be selected.</param>
        /// <param name="max">The maximum value that can be selected.</param>
        public static void DrawMinMaxLabeledFloatSlider(string name, ref float minValue, ref float maxValue, float min, float max)
        {
            EditorGUILayout.BeginHorizontal();
            minValue = EditorGUILayout.FloatField(name, minValue);
            EditorGUILayout.MinMaxSlider(ref minValue, ref maxValue, min, max);
            maxValue = EditorGUILayout.FloatField(maxValue, GUILayout.Width(40));
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws +/- buttons to add or remove from a SerializedProperty.
        /// </summary>
        /// <param name="property">The property array.</param>
        /// <returns>True if an element was added or removed.</returns>
        public static bool DrawAddRemoveArrayButtons(SerializedProperty property)
        {
            var changed = false;
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUI.enabled = property.arraySize > 0;
            if (GUILayout.Button("-", EditorStyles.miniButtonLeft, GUILayout.Width(20)))
            {
                property.DeleteArrayElementAtIndex(property.arraySize - 1);
                changed = true;
            }
            GUI.enabled = true;
            if (GUILayout.Button("+", EditorStyles.miniButtonRight, GUILayout.Width(20)))
            {
                property.InsertArrayElementAtIndex(property.arraySize);
                changed = true;
            }
            EditorGUILayout.EndHorizontal();
            return changed;
        }

        /// <summary>
        /// Draws a float field with an infinity toggle to the right of it.
        /// </summary>
        /// <param name="property">The float property.</param>
        public static void DrawFloatInfinityField(SerializedProperty property)
        {
            EditorGUILayout.BeginHorizontal();
            var prevInfinity = (property.floatValue == float.PositiveInfinity);
            GUI.enabled = !prevInfinity;
            EditorGUILayout.PropertyField(property);
            GUI.enabled = true;
            var infinity = EditorGUILayout.ToggleLeft("Infinity", prevInfinity, GUILayout.Width(73));
            if (prevInfinity != infinity)
            {
                if (infinity)
                {
                    property.floatValue = float.PositiveInfinity;
                }
                else
                {
                    property.floatValue = 1;
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws an int field with an infinity toggle to the right of it.
        /// </summary>
        /// <param name="property">The int property.</param>
        public static void DrawIntInfinityField(SerializedProperty property)
        {
            EditorGUILayout.BeginHorizontal();
            var prevInfinity = (property.intValue == int.MaxValue);
            GUI.enabled = !prevInfinity;
            EditorGUILayout.PropertyField(property);
            GUI.enabled = true;
            var infinity = EditorGUILayout.ToggleLeft("Infinity", prevInfinity, GUILayout.Width(73));
            if (prevInfinity != infinity)
            {
                if (infinity)
                {
                    property.intValue = int.MaxValue;
                }
                else
                {
                    property.intValue = 1;
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}