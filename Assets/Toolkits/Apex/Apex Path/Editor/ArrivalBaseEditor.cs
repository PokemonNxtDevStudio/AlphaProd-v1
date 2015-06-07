/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Editor
{
    using UnityEditor;
    using UnityEngine;

    public abstract class ArrivalBaseEditor : SteeringComponentEditor
    {
        private SerializedProperty _slowingDistance;
        private SerializedProperty _autoCalculateSlowingDistance;
        private SerializedProperty _arrivalDistance;
        private SerializedProperty _slowingAlgorithm;

        protected override void CreateUI()
        {
            base.CreateUI();

            EditorUtilities.Section("Arrival");
            EditorGUILayout.PropertyField(_slowingAlgorithm, new GUIContent("Slowing Algorithm", "The algorithm used to slow the unit for arrival."));
            EditorGUILayout.PropertyField(_autoCalculateSlowingDistance, new GUIContent("Auto Calculate Slowing Distance", "Determines whether the slowing distance is automatically calculated based on the unit's speed and deceleration capabilities."));
            if (!_autoCalculateSlowingDistance.boolValue)
            {
                EditorGUI.indentLevel += 1;
                EditorGUILayout.PropertyField(_slowingDistance, new GUIContent("Slowing Distance", "The distance within which the unit will start to slow down for arrival."));
                EditorGUI.indentLevel -= 1;
            }

            EditorGUILayout.PropertyField(_arrivalDistance, new GUIContent("Arrival Distance", "The distance from the final destination where the unit will stop."));

            EditorGUI.indentLevel -= 1; 
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _slowingDistance = this.serializedObject.FindProperty("slowingDistance");
            _autoCalculateSlowingDistance = this.serializedObject.FindProperty("autoCalculateSlowingDistance");
            _arrivalDistance = this.serializedObject.FindProperty("arrivalDistance");
            _slowingAlgorithm = this.serializedObject.FindProperty("slowingAlgorithm");
        }
    }
}