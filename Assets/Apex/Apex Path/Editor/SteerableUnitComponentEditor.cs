/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Editor
{
    using Apex.Steering;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(SteerableUnitComponent), false), CanEditMultipleObjects]
    public class SteerableUnitComponentEditor : Editor
    {
        private SerializedProperty _stopIfStuckForSeconds;
        private SerializedProperty _groundStickynessFactor;
        private SerializedProperty _gravity;
        private SerializedProperty _terminalVelocity;

        public override void OnInspectorGUI()
        {
            var c = this.target as SteerableUnitComponent;
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Current Speed", c.speed.ToString());
            EditorGUILayout.LabelField("Current Velocity", c.velocity.ToString());
            EditorGUILayout.LabelField("Actual Velocity", c.actualVelocity.ToString());

            this.serializedObject.Update();
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_stopIfStuckForSeconds);
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_groundStickynessFactor);
            EditorGUILayout.PropertyField(_gravity);
            EditorGUILayout.PropertyField(_terminalVelocity);
            this.serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _groundStickynessFactor = this.serializedObject.FindProperty("groundStickynessFactor");
            _stopIfStuckForSeconds = this.serializedObject.FindProperty("stopIfStuckForSeconds");
            _gravity = this.serializedObject.FindProperty("gravity");
            _terminalVelocity = this.serializedObject.FindProperty("terminalVelocity");
        }

        private void OnDestroy()
        {
            if (this.target == null)
            {
                EditorUtilities.CleanupComponentMaster();
            }
        }
    }
}
