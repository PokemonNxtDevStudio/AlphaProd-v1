/* Copyright © 2014 Apex Software. All rights reserved. */
namespace Apex.Editor
{
    using Apex.WorldGeometry;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(DynamicObstacle), false), CanEditMultipleObjects]
    public partial class DynamicObstacleEditor : Editor
    {
        private SerializedProperty _exceptions;
        private SerializedProperty _updateMode;
        private SerializedProperty _velocityPredictionFactor;
        private SerializedProperty _resolveVelocityFromParent;
        private SerializedProperty _stopUpdatingIfStationary;
        private SerializedProperty _stationaryThresholdSeconds;
        private SerializedProperty _useGridObstacleSensitivity;
        private SerializedProperty _customSensitivity;
        private SerializedProperty _customUpdateInterval;
        private SerializedProperty _supportDynamicGrids;
        private SerializedProperty _causesReplanning;

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            EditorUtilities.Section("Obstruction");
            EditorGUILayout.PropertyField(_exceptions, new GUIContent("Exceptions", "Units with one or more matching attributes will not consider this obstacle when path finding, e.g. useful for doors etc.."));

            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_updateMode, new GUIContent("Update Mode", "Controls when the obstacle updates its state, and thereby its associated grid."));
            EditorGUILayout.PropertyField(_customUpdateInterval, new GUIContent("Custom Update Interval", "Setting this to a value other than 0, will override the default update interval of the load balancer."));

            ExtensionOnGUI();

            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_useGridObstacleSensitivity, new GUIContent("Use Grid Obstacle Sensitivity", "Controls whether the obstacle sensitivity range of the grid will be used by this obstacle."));
            if (_useGridObstacleSensitivity.boolValue == false)
            {
                EditorGUILayout.PropertyField(_customSensitivity, new GUIContent("Custom Sensitivity", "The custom obstacle sensitivity range to use."));
            }

            EditorUtilities.Section("Grid interaction");
            EditorGUILayout.PropertyField(_supportDynamicGrids, new GUIContent("Support Dynamic Grids", "If checked, the Dynamic Obstacle will react to grids being disabled / initialized at runtime. If you use dynamic grid initialization this should be checked."));
            EditorGUILayout.PropertyField(_causesReplanning, new GUIContent("Causes Replanning", "Controls whether the dynamic obstacle will trigger path replanning for unit's in the grid sections it influences."));

            EditorUtilities.Section("Velocity");

            EditorGUILayout.PropertyField(_velocityPredictionFactor, new GUIContent("Velocity Prediction Factor", "Determines how far in the obstacles direction of movement, that cells will be considered blocked."));
            EditorGUILayout.PropertyField(_resolveVelocityFromParent, new GUIContent("Resolve Velocity from Parent", "Controls whether the obstacle will look for a velocity source on the parent game object if one is not found on its own."));
            EditorGUILayout.PropertyField(_stopUpdatingIfStationary, new GUIContent("Stop Updating If Stationary", "Setting this to true, will stop the dynamic updates if the obstacle remains stationary for 'Stationary Threshold Seconds' seconds."));
            if (_stopUpdatingIfStationary.boolValue == true)
            {
                EditorGUILayout.PropertyField(_stationaryThresholdSeconds, new GUIContent("Stationary Threshold Seconds", "The amount of seconds after which dynamic updates on the obstacle will stop."));
            }

            this.serializedObject.ApplyModifiedProperties();

            if (Application.isPlaying && GUI.changed)
            {
                var t = this.target as ISupportRuntimeStateChange;
                if (t != null)
                {
                    t.ReevaluateState();
                }
            }
        }

        partial void ExtensionEnable();

        partial void ExtensionOnGUI();

        private void OnEnable()
        {
            _exceptions = this.serializedObject.FindProperty("_exceptionsMask");
            _updateMode = this.serializedObject.FindProperty("updateMode");
            _velocityPredictionFactor = this.serializedObject.FindProperty("velocityPredictionFactor");
            _resolveVelocityFromParent = this.serializedObject.FindProperty("resolveVelocityFromParent");
            _stopUpdatingIfStationary = this.serializedObject.FindProperty("stopUpdatingIfStationary");
            _stationaryThresholdSeconds = this.serializedObject.FindProperty("stationaryThresholdSeconds");
            _useGridObstacleSensitivity = this.serializedObject.FindProperty("useGridObstacleSensitivity");
            _customSensitivity = this.serializedObject.FindProperty("customSensitivity");
            _customUpdateInterval = this.serializedObject.FindProperty("customUpdateInterval");
            _supportDynamicGrids = this.serializedObject.FindProperty("supportDynamicGrids");
            _causesReplanning = this.serializedObject.FindProperty("causesReplanning");

            ExtensionEnable();
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
