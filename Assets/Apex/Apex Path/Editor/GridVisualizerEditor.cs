namespace Apex.Editor
{
    using Apex.Debugging;
    using Apex.Services;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(GridVisualizer), false), CanEditMultipleObjects]
    public class GridVisualizerEditor : Editor
    {
        private SerializedProperty _drawMode;
        private SerializedProperty _modelHeightNavigationCapabilities;
        private SerializedProperty _modelAttributes;
        private SerializedProperty _drawSubSections;
        private SerializedProperty _drawAllGrids;
        private SerializedProperty _editorRefreshDelay;
        private SerializedProperty _drawDistanceThreshold;
        private SerializedProperty _gridLinesColor;
        private SerializedProperty _descentOnlyColor;
        private SerializedProperty _ascentOnlyColor;
        private SerializedProperty _obstacleColor;
        private SerializedProperty _subSectionsColor;
        private SerializedProperty _boundsColor;
        private SerializedProperty _drawAlways;

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(_drawMode, new GUIContent("Draw Mode"));
            if (_drawMode.intValue == (int)GridVisualizer.GridMode.Accessibility)
            {
                if (!GameServices.heightStrategy.useGlobalHeightNavigationSettings)
                {
                    EditorGUILayout.PropertyField(_modelHeightNavigationCapabilities, new GUIContent("Model Settings", "Change these to see how the grid looks to units with these settings."));
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(_modelAttributes);
                    EditorGUI.indentLevel--;
                }

                if (!Application.isPlaying)
                {
                    EditorGUILayout.HelpBox("Please note that in order to show height map data, the grid(s) must be baked.", MessageType.Info);
                }
            }

            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_drawAlways, new GUIContent("Draw Always"));
            EditorGUILayout.PropertyField(_drawAllGrids, new GUIContent("Draw All Grids", "Draw all grids in the scene, i.e. only one visualizer needed."));
            EditorGUILayout.PropertyField(_drawSubSections, new GUIContent("Draw Sub Sections", "Draw a crude representation of the grid's sub sections."));
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_editorRefreshDelay, new GUIContent("Editor Refresh Delay", "The delay between updates after changes have been made to the scene."));
            EditorGUILayout.PropertyField(_drawDistanceThreshold, new GUIContent("Draw Distance Threshold", "How zoomed out can you be and still see the grid visuals."));
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_gridLinesColor, new GUIContent("Grid Lines Color"));
            EditorGUILayout.PropertyField(_ascentOnlyColor, new GUIContent("Ascent Only Color"));
            EditorGUILayout.PropertyField(_descentOnlyColor, new GUIContent("Descent Only Color"));
            EditorGUILayout.PropertyField(_obstacleColor, new GUIContent("Obstacle Color"));
            EditorGUILayout.PropertyField(_subSectionsColor, new GUIContent("Sub Sections Color"));
            EditorGUILayout.PropertyField(_boundsColor, new GUIContent("Bounds Color"));

            this.serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _drawMode = this.serializedObject.FindProperty("drawMode");
            _modelHeightNavigationCapabilities = this.serializedObject.FindProperty("modelHeightNavigationCapabilities");
            _modelAttributes = this.serializedObject.FindProperty("modelAttributes");
            _drawSubSections = this.serializedObject.FindProperty("drawSubSections");
            _drawAllGrids = this.serializedObject.FindProperty("drawAllGrids");
            _editorRefreshDelay = this.serializedObject.FindProperty("editorRefreshDelay");
            _drawDistanceThreshold = this.serializedObject.FindProperty("drawDistanceThreshold");
            _gridLinesColor = this.serializedObject.FindProperty("gridLinesColor");
            _descentOnlyColor = this.serializedObject.FindProperty("descentOnlyColor");
            _ascentOnlyColor = this.serializedObject.FindProperty("ascentOnlyColor");
            _obstacleColor = this.serializedObject.FindProperty("obstacleColor");
            _subSectionsColor = this.serializedObject.FindProperty("subSectionsColor");
            _boundsColor = this.serializedObject.FindProperty("boundsColor");
            _drawAlways = this.serializedObject.FindProperty("drawAlways");
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
