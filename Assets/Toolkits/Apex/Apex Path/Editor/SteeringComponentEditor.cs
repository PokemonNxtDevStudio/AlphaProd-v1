/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Editor
{
    using UnityEditor;
    using UnityEngine;

    public abstract class SteeringComponentEditor : Editor
    {
        private SerializedProperty _weight;
        private SerializedProperty _priority;

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            CreateUI();

            this.serializedObject.ApplyModifiedProperties();
        }

        protected virtual void CreateUI()
        {
            EditorGUILayout.Separator();

            if (Application.isPlaying)
            {
                EditorGUILayout.LabelField("Priority", _priority.intValue.ToString());
            }
            else
            {
                EditorGUILayout.PropertyField(_priority, new GUIContent("Priority", "The priority of this steering behaviour relative to others. Only behaviours with the highest priority will influence the steering of the unit, provided they have any steering output."));
            }
            
            EditorGUILayout.PropertyField(_weight, new GUIContent("Weight", "The weight this component's input will have in relation to other steering components."));
        }

        protected virtual void OnEnable()
        {
            _weight = this.serializedObject.FindProperty("weight");
            _priority = this.serializedObject.FindProperty("priority");
        }

        protected virtual void OnDestroy()
        {
            if (this.target == null)
            {
                EditorUtilities.CleanupComponentMaster();
            }
        }
    }
}
