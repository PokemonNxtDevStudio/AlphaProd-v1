/* Copyright © 2014 Apex Software. All rights reserved. */

namespace Apex.Editor
{
    using Apex.Services;
    using Apex.Steering;
    using Apex.Units;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(UnitComponent), false), CanEditMultipleObjects]
    public class UnitComponentEditor : Editor
    {
        private SerializedProperty _isSelectable;
        private SerializedProperty _selectionVisual;
        private SerializedProperty _attributeMask;
        private SerializedProperty _heightCapabilities;
        private SerializedProperty _radius;
        private SerializedProperty _fieldOfView;
        private SerializedProperty _yAxisoffset;
        private SerializedProperty _determination;

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("These settings cannot be edited in play mode.", MessageType.Info);
                return;
            }

            this.serializedObject.Update();
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_attributeMask);

            EditorUtilities.Section("Selection");
            EditorGUILayout.PropertyField(_isSelectable);
            if (_isSelectable.boolValue)
            {
                EditorGUILayout.PropertyField(_selectionVisual);
            }

            // set indention level back to normal
            EditorGUI.indentLevel -= 1;

            var heightStrategy = GameServices.heightStrategy;
            if (heightStrategy.useGlobalHeightNavigationSettings || heightStrategy.heightMode == HeightSamplingMode.NoHeightSampling)
            {
                EditorUtilities.Section("Height Navigation");
                EditorGUILayout.HelpBox("Height navigation capabilities have been set globally on the Game World, which applies to all units.", MessageType.Info);
            }
            else
            {
                EditorGUILayout.Separator();
                EditorGUILayout.PropertyField(_heightCapabilities, new GUIContent("Height Navigation", "Represents the height navigation capabilities of the unit."), true);
            }

            EditorUtilities.Section("Misc");
            EditorGUILayout.PropertyField(_radius);
            EditorGUILayout.PropertyField(_fieldOfView);
            EditorGUILayout.PropertyField(_yAxisoffset);
            EditorGUILayout.PropertyField(_determination, new GUIContent("Determination", "The determination factor is used to evaluate whether this unit separates or avoids other units. Units with lower determination are basically ignored."));
            this.serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _isSelectable = this.serializedObject.FindProperty("_isSelectable");
            _selectionVisual = this.serializedObject.FindProperty("selectionVisual");
            _attributeMask = this.serializedObject.FindProperty("_attributeMask");
            _heightCapabilities = this.serializedObject.FindProperty("_heightCapabilities");
            _radius = this.serializedObject.FindProperty("radius");
            _fieldOfView = this.serializedObject.FindProperty("fieldOfView");
            _yAxisoffset = this.serializedObject.FindProperty("yAxisoffset");
            _determination = this.serializedObject.FindProperty("_determination");
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
