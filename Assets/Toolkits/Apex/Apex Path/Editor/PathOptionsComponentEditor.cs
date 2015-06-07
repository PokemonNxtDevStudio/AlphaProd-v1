namespace Apex.Editor
{
    using Apex.PathFinding;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(PathOptionsComponent), false), CanEditMultipleObjects]
    public class PathOptionsComponentEditor : Editor
    {
        private SerializedProperty _pathingPriority;
        private SerializedProperty _usePathSmoothing;
        private SerializedProperty _allowCornerCutting;
        private SerializedProperty _preventDiagonalMoves;
        private SerializedProperty _navigateToNearestIfBlocked;
        private SerializedProperty _maxEscapeCellDistanceIfOriginBlocked;
        private SerializedProperty _nextNodeDistance;
        private SerializedProperty _requestNextWaypointDistance;
        private SerializedProperty _announceAllNodes;
        private SerializedProperty _replanMode;
        private SerializedProperty _replanInterval;

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("These settings cannot be edited in play mode.", MessageType.Info);
                return;
            }

            this.serializedObject.Update();

            EditorUtilities.Section("Path Finder Options");
            EditorGUILayout.PropertyField(_pathingPriority, new GUIContent("Pathing Priority", "The priority with which this unit's path requests should be processed."));
            EditorGUILayout.PropertyField(_usePathSmoothing, new GUIContent("Use Path Smoothing", "Whether to use path smoothing to create more natural paths."));
            EditorGUILayout.PropertyField(_allowCornerCutting, new GUIContent("Allow Corner Cutting", "Whether to allow the path to cut corners. Corner cutting has slightly better performance, but produces less natural routes."));
            EditorGUILayout.PropertyField(_preventDiagonalMoves, new GUIContent("Prevent Diagonal Moves", "Whether the unit is allowed to move to diagonal neighbours."));
            EditorGUILayout.PropertyField(_navigateToNearestIfBlocked, new GUIContent("Navigate To Nearest If Blocked", "Whether the unit will navigate to the nearest possible position if the actual destination is blocked or otherwise inaccessible."));
            EditorGUILayout.PropertyField(_maxEscapeCellDistanceIfOriginBlocked, new GUIContent("Max Escape Cell Distance If Origin Blocked", "The maximum escape cell distance if the unit's starting position is blocked."));

            EditorUtilities.Section("Path Following");
            EditorGUILayout.PropertyField(_nextNodeDistance, new GUIContent("Next Node Distance", "The distance from the current destination node on the path at which the unit will switch to the next node."));
            EditorGUILayout.PropertyField(_requestNextWaypointDistance, new GUIContent("Request Next Waypoint Distance", "The distance from the current way point at which the next way point will be requested."));
            EditorGUILayout.PropertyField(_announceAllNodes, new GUIContent("Announce All Nodes", "Whether to raise a navigation event for each node reached along a path."));

            EditorUtilities.Section("Replanning");
            EditorGUILayout.PropertyField(_replanMode, new GUIContent("Replan Mode", "The replan mode."));
            EditorGUILayout.PropertyField(_replanInterval, new GUIContent("Replan Interval", "The time between replans, the exact meaning depends on the replan mode."));

            this.serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            _pathingPriority = this.serializedObject.FindProperty("_pathingPriority");
            _usePathSmoothing = this.serializedObject.FindProperty("_usePathSmoothing");
            _allowCornerCutting = this.serializedObject.FindProperty("_allowCornerCutting");
            _preventDiagonalMoves = this.serializedObject.FindProperty("_preventDiagonalMoves");
            _navigateToNearestIfBlocked = this.serializedObject.FindProperty("_navigateToNearestIfBlocked");
            _maxEscapeCellDistanceIfOriginBlocked = this.serializedObject.FindProperty("_maxEscapeCellDistanceIfOriginBlocked");
            _nextNodeDistance = this.serializedObject.FindProperty("_nextNodeDistance");
            _requestNextWaypointDistance = this.serializedObject.FindProperty("_requestNextWaypointDistance");
            _announceAllNodes = this.serializedObject.FindProperty("_announceAllNodes");
            _replanMode = this.serializedObject.FindProperty("_replanMode");
            _replanInterval = this.serializedObject.FindProperty("_replanInterval");
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
