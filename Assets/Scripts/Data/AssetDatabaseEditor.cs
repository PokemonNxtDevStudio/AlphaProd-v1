using UnityEngine;
using UnityEditor;
using System.Collections;

#if UNITY_EDITOR
public class AssetDatabaseEditor
{

    private static string GetSelectionFolder()
    {
        if (Selection.activeObject != null)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
            if (!string.IsNullOrEmpty(path))
            {
                int dot = path.LastIndexOf('.');
                int slash = Mathf.Max(path.LastIndexOf('/'), path.LastIndexOf('\\'));
                if (slash > 0) return (dot > slash) ? path.Substring(0, slash + 1) : path + "/";
            }
        }
        return "Assets/Scripts/Data";
    }
    [MenuItem("NXT/Asset Database/Item Database")]
    public static void CreateDatabase()
    {
        // Get the currently selected asset directory
        string currentPath = GetSelectionFolder();
        // New asset name
		string assetName = "ItemAssetDatabase.asset";
		ItemAssetDatabase asset = ScriptableObject.CreateInstance("ItemAssetDatabase") as ItemAssetDatabase;  //scriptable object
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(currentPath + assetName));
        AssetDatabase.Refresh();
    }

}
#endif
