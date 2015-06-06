namespace Apex.Editor
{
    using Apex.DataStructures;
    using Apex.Services;
    using Apex.Steering;
    using Apex.WorldGeometry;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(GridComponent), true), CanEditMultipleObjects]
    public class GridComponentEditor : Editor
    {
        private SerializedProperty _friendlyName;
        private SerializedProperty _linkOriginToTransform;
        private SerializedProperty _origin;
        private SerializedProperty _originOffset;
        private SerializedProperty _sizeX;
        private SerializedProperty _sizeZ;
        private SerializedProperty _cellSize;
        private SerializedProperty _obstacleSensitivityRange;
        private SerializedProperty _subSectionsX;
        private SerializedProperty _subSectionsZ;
        private SerializedProperty _subSectionsCellOverlap;
        private SerializedProperty _generateHeightMap;
        private SerializedProperty _heightLookupType;
        private SerializedProperty _heightLookupMaxDepth;
        private SerializedProperty _lowerBoundary;
        private SerializedProperty _upperBoundary;
        private SerializedProperty _storeBakedDataAsAsset;
        private SerializedProperty _automaticInitialization;
        private SerializedProperty _obstacleAndGroundDetection;

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("These settings cannot be edited in play mode.", MessageType.Info);
                return;
            }

            this.serializedObject.Update();

            int baked = 0;
            var editedObjects = this.serializedObject.targetObjects;
            for (int i = 0; i < editedObjects.Length; i++)
            {
                var g = editedObjects[i] as GridComponent;
                if (g.bakedData != null)
                {
                    baked++;
                }
            }

            EditorGUILayout.Separator();

            if (baked > 0 && baked < editedObjects.Length)
            {
                EditorGUILayout.LabelField("A mix of baked and unbaked grids cannot be edited at the same time.");
                return;
            }

            //If data is baked, only offer an option to edit or rebake
            if (baked == editedObjects.Length)
            {
                EditorGUILayout.LabelField("The grid has been baked. To change it press the Edit button below.");

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Edit"))
                {
                    foreach (var o in editedObjects)
                    {
                        var g = o as GridComponent;
                        EditorUtilities.RemoveAsset(g.bakedData);
                        g.bakedData = null;
                        g.ResetGrid();
                        EditorUtility.SetDirty(g);
                    }
                }

                if (GUILayout.Button("Re-bake Grid"))
                {
                    foreach (var o in editedObjects)
                    {
                        var g = o as GridComponent;
                        BakeGrid(g);
                    }
                }

                GUILayout.EndHorizontal();
                return;
            }

            EditorGUILayout.PropertyField(_friendlyName, new GUIContent("Friendly Name", "An optional friendly name for the grid that will be used in messages and such."));

            EditorUtilities.Section("Layout");

            EditorGUILayout.PropertyField(_linkOriginToTransform, new GUIContent("Link Origin to Transform", "Link the center of the grid to the position of the game object."));

            if (!_linkOriginToTransform.hasMultipleDifferentValues)
            {
                if (_linkOriginToTransform.boolValue)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(_originOffset, new GUIContent("Offset", "The offset in relation to the linked transform."), true);
                    EditorGUI.indentLevel--;
                }
                else
                {
                    EditorGUILayout.PropertyField(_origin, new GUIContent("Origin", "The center of the grid."), true);
                }
            }

            EditorGUILayout.PropertyField(_sizeX, new GUIContent("Size X", "Number of cells along the x-axis."));
            EditorGUILayout.PropertyField(_sizeZ, new GUIContent("Size Z", "Number of cells along the z-axis."));
            EditorGUILayout.PropertyField(_cellSize, new GUIContent("Cell Size", "The size of each grid cell, expressed as the length of one side."));
            EditorGUILayout.PropertyField(_lowerBoundary, new GUIContent("Lower Boundary", "How far below the grid's plane does the grid have influence."));
            EditorGUILayout.PropertyField(_upperBoundary, new GUIContent("Upper Boundary", "How far above the grid's plane does the grid have influence."));
            EditorGUILayout.PropertyField(_obstacleSensitivityRange, new GUIContent("Obstacle Sensitivity Range", "How close to the center of a cell must an obstacle be to block the cell."));
            EditorGUILayout.PropertyField(_obstacleAndGroundDetection, new GUIContent("Obstacle and Ground Detection", "Controls the method used to detect terrain and obstacles. Choose whichever works for your type of terrain."));

            EditorUtilities.Section("Subsections");

            EditorGUILayout.PropertyField(_subSectionsX, new GUIContent("Subsections X", "Number of subsections along the x-axis."));
            EditorGUILayout.PropertyField(_subSectionsZ, new GUIContent("Subsections Z", "Number of subsections along the z-axis."));
            EditorGUILayout.PropertyField(_subSectionsCellOverlap, new GUIContent("Subsections Cell Overlap", "The number of cells by which subsections overlap neighbouring subsections."));

            ShowHeightMapOptions();

            EditorUtilities.Section("Initialization");
            EditorGUILayout.PropertyField(_automaticInitialization, new GUIContent("Automatic Initialization", "Controls whether the grid is automatically initialized when enabled. If unchecked the grid must be manually initialized."));

            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(_storeBakedDataAsAsset, new GUIContent("Store Grid data as asset", "Store baked data in a separate asset file instead of storing to the scene, this enables prefab'ing."));

            this.serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Bake Grid"))
            {
                foreach (var o in editedObjects)
                {
                    var g = o as GridComponent;
                    BakeGrid(g);
                }
            }
        }

        private static void BakeGrid(GridComponent g)
        {
            var builder = g.GetBuilder();

            var matrix = CellMatrix.Create(builder);

            var data = g.bakedData;
            if (data == null)
            {
                data = CellMatrixData.Create(matrix);

                g.bakedData = data;
            }
            else
            {
                data.Refresh(matrix);
            }

            if (g.storeBakedDataAsAsset)
            {
                EditorUtilities.CreateOrUpdateAsset(data, g.friendlyName.Trim());
            }
            else
            {
                EditorUtility.SetDirty(data);
            }

            g.ResetGrid();
            EditorUtility.SetDirty(g);

            Debug.Log(string.Format("The grid {0} was successfully baked.", g.friendlyName));
        }

        private void ShowHeightMapOptions()
        {
            var heightMode = GameServices.heightStrategy.heightMode;
            if (heightMode == HeightSamplingMode.HeightMap)
            {
                EditorGUI.indentLevel--;
                EditorGUILayout.Separator();

                EditorGUILayout.PropertyField(_generateHeightMap, new GUIContent("Generate Height Map", "Controls whether the grid generates a height map to allow height sensitive navigation."));
                if (_generateHeightMap.boolValue)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(_heightLookupType, new GUIContent("Lookup Type", "Dictionaries are fast but dense. Quad Trees are sparse but slower and are very dependent on height distributions. Use Quad trees on maps with large same height areas."));
                    if (_heightLookupType.intValue == (int)HeightLookupType.QuadTree)
                    {
                        EditorGUILayout.PropertyField(_heightLookupMaxDepth, new GUIContent("Tree Depth", "The higher the allowed depth, the more sparse the tree will be but it will also get slower."));
                    }

                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                EditorUtilities.Section("Height Map");
                EditorGUILayout.HelpBox(string.Format("Height navigation mode has been set to {0} (on Game World), hence no height map will be created.", heightMode), MessageType.Info);
            }
        }

        private void OnEnable()
        {
            _friendlyName = this.serializedObject.FindProperty("_friendlyName");

            _linkOriginToTransform = this.serializedObject.FindProperty("_linkOriginToTransform");
            _origin = this.serializedObject.FindProperty("_origin");
            _originOffset = this.serializedObject.FindProperty("_originOffset");
            _sizeX = this.serializedObject.FindProperty("sizeX");
            _sizeZ = this.serializedObject.FindProperty("sizeZ");
            _cellSize = this.serializedObject.FindProperty("cellSize");
            _obstacleSensitivityRange = this.serializedObject.FindProperty("obstacleSensitivityRange");

            _subSectionsX = this.serializedObject.FindProperty("subSectionsX");
            _subSectionsZ = this.serializedObject.FindProperty("subSectionsZ");
            _subSectionsCellOverlap = this.serializedObject.FindProperty("subSectionsCellOverlap");

            _generateHeightMap = this.serializedObject.FindProperty("generateHeightmap");
            _heightLookupType = this.serializedObject.FindProperty("heightLookupType");
            _heightLookupMaxDepth = this.serializedObject.FindProperty("heightLookupMaxDepth");
            _lowerBoundary = this.serializedObject.FindProperty("lowerBoundary");
            _upperBoundary = this.serializedObject.FindProperty("upperBoundary");
            _obstacleAndGroundDetection = this.serializedObject.FindProperty("obstacleAndGroundDetection");

            _storeBakedDataAsAsset = this.serializedObject.FindProperty("_storeBakedDataAsAsset");
            _automaticInitialization = this.serializedObject.FindProperty("_automaticInitialization");
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
