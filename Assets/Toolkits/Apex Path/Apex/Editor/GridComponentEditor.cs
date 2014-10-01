namespace Apex.Editor
{
    using System.Linq;
    using Apex.WorldGeometry;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(GridComponent), false)]
    public class GridComponentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("These settings cannot be edited in play mode.", MessageType.Info);
                return;
            }

            var g = this.target as GridComponent;

            //If data is baked, only offer an option to edit
            if (g.bakedData != null)
            {
                EditorGUILayout.LabelField("The grid has been baked. To change it press the Edit button below.");

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Edit"))
                {
                    EditorUtilities.RemoveAsset(g.bakedData);
                    g.bakedData = null;
                    EditorUtility.SetDirty(g);
                }

                if (GUILayout.Button("Re-bake Grid"))
                {
                    BakeGrid(g);
                }

                GUILayout.EndHorizontal();
                return;
            }

            var friendlyName = EditorGUILayout.TextField("Friendly Name", g.friendlyName);
            if (friendlyName != g.friendlyName)
            {
                g.friendlyName = friendlyName;
                EditorUtility.SetDirty(g);
            }

            var linkOrigin = EditorGUILayout.Toggle("Link Origin to Transform", g.linkOriginToTransform);
            if (linkOrigin != g.linkOriginToTransform)
            {
                g.linkOriginToTransform = linkOrigin;
                EditorUtility.SetDirty(g);
            }

            if (!linkOrigin)
            {
                var origin = EditorGUILayout.Vector3Field("Origin", g.origin);
                if (origin != g.origin)
                {
                    g.origin = origin;
                    EditorUtility.SetDirty(g);
                }
            }

            DrawDefaultInspector();

            if (g.obstacleSensitivityRange > (g.cellSize / 2.0f))
            {
                g.obstacleSensitivityRange = (g.cellSize / 2.0f);
                EditorUtility.SetDirty(g);
                Debug.LogWarning("The Obstacle Sensitivity Range of a grid cannot exceed half its Cell Size, this has been corrected.");
            }

            EditorGUILayout.Separator();
            var autoInit = EditorGUILayout.Toggle("Automatic Initialization", g.automaticInitialization);
            if (autoInit != g.automaticInitialization)
            {
                g.automaticInitialization = autoInit;
                g.enabled = autoInit;
                EditorUtility.SetDirty(g);
            }

            EditorGUILayout.Separator();
            var storeBakedDataAsAsset = EditorGUILayout.Toggle("Store Grid data as asset", g.storeBakedDataAsAsset);
            if (storeBakedDataAsAsset != g.storeBakedDataAsAsset)
            {
                g.storeBakedDataAsAsset = storeBakedDataAsAsset;
                EditorUtility.SetDirty(g);
            }

            if (GUILayout.Button("Bake Grid"))
            {
                BakeGrid(g);
            }
        }

        private static void BakeGrid(GridComponent g)
        {
            var builder = g.GetBuilder();

            var matrix = CellMatrix.Create(builder, true);

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

            EditorUtility.SetDirty(g);

            Debug.Log(string.Format("The grid {0} was successfully baked.", g.friendlyName));
        }
    }
}
