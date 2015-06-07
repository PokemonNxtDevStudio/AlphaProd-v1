namespace Apex.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Apex.WorldGeometry;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(GridPortalComponent))]
    public class GridPortalComponentEditor : Editor
    {
        private static List<KeyValuePair<string, Type>> _actionsList;
        private static string[] _actionsNames;

        private SerializedProperty _portalName;
        private SerializedProperty _type;
        private SerializedProperty _direction;
        private SerializedProperty _exclusiveTo;
        private SerializedProperty _relativeToTransform;
        private SerializedProperty _portalOne;
        private SerializedProperty _portalTwo;
        private SerializedProperty _portalOneColor;
        private SerializedProperty _portalTwoColor;
        private SerializedProperty _connectionColor;

        private int _idHash;
        private bool _inPortalMode;
        private Vector3 _portalRectStart;
        private Vector3 _portalRectEnd;
        private bool _shiftDown;
        private bool _gridConnectMode;

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("These settings cannot be edited in play mode.", MessageType.Info);
                return;
            }

            this.serializedObject.Update();
            var relativeCurrent = _relativeToTransform.boolValue;

            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_portalName, new GUIContent("Portal Name", "The portal's name, which can be used to look up the portal through the GridManager."));
            EditorGUILayout.PropertyField(_exclusiveTo, new GUIContent("Exclusive To", "If set only units that have one or more of the specified attributes can use the portal."));
            EditorGUILayout.PropertyField(_type, new GUIContent("Type", "Connector portals are mainly for connecting grids, but can also be sued inside one grid. Shortcuts are as there name suggests, short cuts and are evaluated differently."));
            EditorGUILayout.PropertyField(_direction, new GUIContent("Direction", "Two-way portals are usable from both end points, one-way portals are only usable from one end point."));

            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_relativeToTransform, new GUIContent("Relative To Transform", "Controls whether the portal end points are seen as relative to their parent transform. IF relative they will move with the transform otherwise they will be static and remain where they were placed initially."));
            if (_relativeToTransform.boolValue != relativeCurrent)
            {
                var p = this.target as GridPortalComponent;
                if (_relativeToTransform.boolValue)
                {
                    var curVal = _portalOne.boundsValue;
                    curVal.center = p.transform.InverseTransformPoint(curVal.center);
                    _portalOne.boundsValue = curVal;

                    curVal = _portalTwo.boundsValue;
                    curVal.center = p.transform.InverseTransformPoint(curVal.center);
                    _portalTwo.boundsValue = curVal;
                }
                else
                {
                    var curVal = _portalOne.boundsValue;
                    curVal.center = p.transform.TransformPoint(curVal.center);
                    _portalOne.boundsValue = curVal;

                    curVal = _portalTwo.boundsValue;
                    curVal.center = p.transform.TransformPoint(curVal.center);
                    _portalTwo.boundsValue = curVal;
                }
            }

            EditorGUILayout.PropertyField(_portalOneColor, new GUIContent("Portal One Color", "The Gizmo color of portal one."));
            EditorGUILayout.PropertyField(_portalTwoColor, new GUIContent("Portal Two Color", "The Gizmo color of portal two."));
            EditorGUILayout.PropertyField(_connectionColor, new GUIContent("Connection Color", "The Gizmo color of the portal connection."));
            this.serializedObject.ApplyModifiedProperties();

            ShowPortalCommands();

            ShowActionSelector();
        }

        private void OnEnable()
        {
            _portalName = this.serializedObject.FindProperty("portalName");
            _type = this.serializedObject.FindProperty("type");
            _direction = this.serializedObject.FindProperty("direction");
            _exclusiveTo = this.serializedObject.FindProperty("_exclusiveTo");
            _relativeToTransform = this.serializedObject.FindProperty("relativeToTransform");
            _portalOne = this.serializedObject.FindProperty("portalOne");
            _portalTwo = this.serializedObject.FindProperty("portalTwo");
            _portalOneColor = this.serializedObject.FindProperty("portalOneColor");
            _portalTwoColor = this.serializedObject.FindProperty("portalTwoColor");
            _connectionColor = this.serializedObject.FindProperty("connectionColor");

            _idHash = this.GetHashCode();
        }

        private void OnDestroy()
        {
            if (this.target == null)
            {
                EditorUtilities.CleanupComponentMaster();
            }
        }

        private void ShowPortalCommands()
        {
            EditorGUILayout.Separator();
            if (_inPortalMode)
            {
                EditorGUILayout.HelpBox("Using the Left mouse button, click and drag to place Portal One.\n\nUsing the Left mouse button and Shift, click and drag to place Portal Two.\n\nHolding Control or Command with either option will expand the portal along the nearest perimeter.", MessageType.Info);
                EditorGUILayout.Separator();

                if (GUILayout.Button("Done (Esc)"))
                {
                    _inPortalMode = false;
                }
            }
            else if (GUILayout.Button("Edit Portals"))
            {
                _inPortalMode = true;
                if (SceneView.sceneViews.Count > 0)
                {
                    ((SceneView)SceneView.sceneViews[0]).Focus();
                }
            }
        }

        private void ShowActionSelector()
        {
            var portal = this.target as GridPortalComponent;

            object pa = portal.As<IPortalAction>();
            if (pa == null)
            {
                pa = portal.As<IPortalActionFactory>();
            }

            if (pa == null)
            {
                if (_actionsList == null)
                {
                    _actionsList = new List<KeyValuePair<string, Type>>();

                    var asm = Assembly.GetAssembly(typeof(GridPortalComponent));
                    foreach (var actionType in asm.GetTypes().Where(t => (typeof(IPortalActionFactory).IsAssignableFrom(t) || typeof(IPortalAction).IsAssignableFrom(t)) && t.IsClass && !t.IsAbstract))
                    {
                        var actionName = actionType.Name;

                        var acm = Attribute.GetCustomAttribute(actionType, typeof(AddComponentMenu)) as AddComponentMenu;
                        if (acm != null)
                        {
                            var startIdx = acm.componentMenu.LastIndexOf('/') + 1;
                            actionName = acm.componentMenu.Substring(startIdx);
                        }

                        var pair = new KeyValuePair<string, Type>(actionName, actionType);
                        _actionsList.Add(pair);
                    }

                    _actionsList.Sort((a, b) => a.Key.CompareTo(b.Key));
                    _actionsNames = _actionsList.Select(p => p.Key).ToArray();
                }

                EditorGUILayout.Separator();
                var style = new GUIStyle(GUI.skin.label);
                style.normal.textColor = Color.yellow;
                EditorGUILayout.LabelField("Select a Portal Action", style);
                var selectedActionIdx = EditorGUILayout.Popup(-1, _actionsNames);
                if (selectedActionIdx >= 0)
                {
                    portal.gameObject.AddComponent(_actionsList[selectedActionIdx].Value);
                }
            }
        }

        private void HandlePortalDefinition()
        {
            if (!_inPortalMode)
            {
                return;
            }

            var p = this.target as GridPortalComponent;
            int id = GUIUtility.GetControlID(_idHash, FocusType.Passive);
            var groundRect = new Plane(Vector3.up, new Vector3(0f, 0f, 0f));

            var evt = Event.current;
            if (evt.type == EventType.MouseDown && evt.button == 0)
            {
                GUIUtility.hotControl = id;

                evt.Use();

                if (!EditorUtilities.MouseToWorldPoint(groundRect, out _portalRectStart))
                {
                    GUIUtility.hotControl = 0;
                }

                _shiftDown = (evt.modifiers & EventModifiers.Shift) > 0;
                _gridConnectMode = (evt.modifiers & EventModifiers.Control) > 0 || (evt.modifiers & EventModifiers.Command) > 0;
                _portalRectEnd = _portalRectStart;
                return;
            }
            else if (evt.type == EventType.KeyDown && evt.keyCode == KeyCode.Escape)
            {
                GUIUtility.hotControl = id;
                evt.Use();
                return;
            }
            else if (evt.type == EventType.KeyUp && evt.keyCode == KeyCode.Escape)
            {
                GUIUtility.hotControl = 0;
                evt.Use();
                _inPortalMode = false;
                this.Repaint();

                return;
            }
            else if (GUIUtility.hotControl != id)
            {
                return;
            }

            if (evt.type == EventType.MouseDrag)
            {
                evt.Use();

                if (!EditorUtilities.MouseToWorldPoint(groundRect, out _portalRectEnd))
                {
                    _portalRectEnd = _portalRectStart;
                }
            }
            else if (evt.type == EventType.MouseUp)
            {
                GUIUtility.hotControl = 0;
                evt.Use();

                _portalRectStart.y = _portalRectEnd.y = Mathf.Max(_portalRectStart.y, _portalRectEnd.y);

                var grid = GridManager.instance.GetGridComponent(_portalRectStart);
                if (grid == null)
                {
                    grid = GridManager.instance.GetGridComponent(_portalRectEnd);
                    if (grid == null)
                    {
                        return;
                    }
                }

                var startToEnd = (_portalRectEnd - _portalRectStart);
                var portalBounds = new Bounds(
                    _portalRectStart + (startToEnd * 0.5f),
                    new Vector3(Mathf.Abs(startToEnd.x), Mathf.Abs(startToEnd.y) + 0.1f, Mathf.Abs(startToEnd.z)));

                _gridConnectMode = _gridConnectMode || (evt.modifiers & EventModifiers.Control) > 0 || (evt.modifiers & EventModifiers.Command) > 0;
                portalBounds = EditorUtilities.SnapToGrid(grid, portalBounds, _gridConnectMode);

                if (p.relativeToTransform)
                {
                    portalBounds.center = p.transform.InverseTransformPoint(portalBounds.center);
                }

                _shiftDown = _shiftDown || (evt.modifiers & EventModifiers.Shift) > 0;
                if (_shiftDown)
                {
                    p.portalTwo = portalBounds;
                }
                else
                {
                    p.portalOne = portalBounds;
                }

                EditorUtility.SetDirty(p);
            }
            else if (evt.type == EventType.Repaint)
            {
                Handles.color = _shiftDown ? p.portalTwoColor : p.portalOneColor;
                var c1 = new Vector3(_portalRectStart.x, _portalRectStart.y, _portalRectEnd.z);
                var c2 = new Vector3(_portalRectEnd.x, _portalRectEnd.y, _portalRectStart.z);
                Handles.DrawDottedLine(_portalRectStart, c1, 10f);
                Handles.DrawDottedLine(c1, _portalRectEnd, 10f);
                Handles.DrawDottedLine(_portalRectEnd, c2, 10f);
                Handles.DrawDottedLine(c2, _portalRectStart, 10f);
            }
        }

        private void OnSceneGUI()
        {
            HandlePortalDefinition();
        }
    }
}
