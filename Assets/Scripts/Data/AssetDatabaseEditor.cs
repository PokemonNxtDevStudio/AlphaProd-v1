using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Security.Policy;

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
    [MenuItem("NXT/Asset Database/Create Item Database")]
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

    [MenuItem("NXT/Asset Database/Create Poke Database()")]
    public static void CreatePokeDatabase()
    {
        // Get the currently selected asset directory
        string currentPath = GetSelectionFolder();
        // New asset name
        string assetName = "Poke Asset Database.asset";
        PokeAssetDatabase asset = ScriptableObject.CreateInstance("PokeAssetDatabase") as PokeAssetDatabase;  //scriptable object

        GetFromMockDB(asset);
        //GetFromMockDB(asset);
        asset.items = new Pokemon[150];
        asset.items[0] = new Pokemon();
        asset.items[0].name = "Pikachu";
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(currentPath + assetName));
        AssetDatabase.Refresh();
    }

    [MenuItem("NXT/Asset Database/Poke Database()/Get From SQL")]
    public static void CreatePokeDatabaseSQL()
    {
        // Get the currently selected asset directory
        string currentPath = GetSelectionFolder();
        // New asset name
        string assetName = "Poke Asset Database.asset";
        PokeAssetDatabase asset = ScriptableObject.CreateInstance("PokeAssetDatabase") as PokeAssetDatabase;  //scriptable object
        GetFromSQL(asset);
        asset.items = new Pokemon[150];
        asset.items[0] = new Pokemon();
        asset.items[0].name = "Pikachu";
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(currentPath + assetName));
        AssetDatabase.Refresh();
    }
    //TODO DO same for itemsList



    static void GetFromMockDB(PokeAssetDatabase asset)
    {
        
    }

    static void GetFromSQL(PokeAssetDatabase asset)
    {

    }

    //TODO GET ARRAY OF POKELIST FROM SQL
    //INSERT ONE BY ONE INTO POKEASSETDATABASE

}
#endif
