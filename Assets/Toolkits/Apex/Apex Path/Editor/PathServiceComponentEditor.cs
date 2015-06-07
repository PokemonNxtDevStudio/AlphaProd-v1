namespace Apex.Editor
{
    using Apex.PathFinding;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(PathServiceComponent), false)]
    public class PathServiceComponentEditor : Editor
    {
        private SerializedProperty _engineType;
        private SerializedProperty _moveCost;
        private SerializedProperty _initialHeapSize;
        private SerializedProperty _runAsync;
        private SerializedProperty _useThreadPoolForAsyncOperations;
        private SerializedProperty _maxMillisecondsPerFrame;

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("These settings cannot be edited in play mode.", MessageType.Info);
                return;
            }

            this.serializedObject.Update();
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_engineType, new GUIContent("Engine Type", "The pathing engine to use."));
            EditorGUILayout.PropertyField(_moveCost, new GUIContent("Move Cost", "The algorithm used for calculating move costs."));
            EditorGUILayout.PropertyField(_initialHeapSize, new GUIContent("Initial Heap Size", "Memory allocation optimization. You only need to change this if you get warnings that ask you to do so."));
            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_runAsync, new GUIContent("Run Async", "Whether the path finding will run in a separate thread."));
            if (_runAsync.boolValue)
            {
                EditorGUILayout.PropertyField(_useThreadPoolForAsyncOperations, new GUIContent("Use Thread Pool For Async Operations", "Whether to use a thread pool if available instead of a dedicated thread. The recommendation is to use a dedicated thread."));
            }
            else
            {
                EditorGUILayout.PropertyField(_maxMillisecondsPerFrame, new GUIContent("Max Milliseconds Per Frame", "The maximum number of milliseconds to user for path finding per frame when the path finder runs in the main thread."));
            }

            this.serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _engineType = this.serializedObject.FindProperty("engineType");
            _moveCost = this.serializedObject.FindProperty("moveCost");
            _initialHeapSize = this.serializedObject.FindProperty("initialHeapSize");
            _runAsync = this.serializedObject.FindProperty("runAsync");
            _useThreadPoolForAsyncOperations = this.serializedObject.FindProperty("useThreadPoolForAsyncOperations");
            _maxMillisecondsPerFrame = this.serializedObject.FindProperty("maxMillisecondsPerFrame");
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
