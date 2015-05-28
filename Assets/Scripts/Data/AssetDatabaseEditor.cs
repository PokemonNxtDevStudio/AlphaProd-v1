using UnityEngine;

using System.Collections;
using System.Security.Policy;

#if UNITY_EDITOR
using UnityEditor;
public class AssetDatabaseEditor
{
    static MockData mockdata = ScriptableObject.CreateInstance("MockData") as MockData;
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
   // [MenuItem("NXT/Asset Database/Items Database/Create Item Database")]
     [MenuItem("NXT/Asset Database/Create/Items Database")]
    public static void ItemsDatabase()
    {
        // Get the currently selected asset directory
        //string currentPath = GetSelectionFolder();
        // New asset name
		//string assetName = "ItemAssetDatabase.asset";
		ItemAssetDatabase asset = ScriptableObject.CreateInstance("ItemAssetDatabase") as ItemAssetDatabase;  //scriptable object
        mockdata.ItemMockData(asset);
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Database/ItemAssetDatabase.asset"));
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
        //EditorUtility.SetDirty(asset);
    }
    /*
    //Pokemons
    [MenuItem("NXT/Asset Database/Pokemon Database/Create Pokemon Database")] 
    public static void CreatePokemonsDatabase()
    {
        // Get the currently selected asset directory
//        string currentPath = GetSelectionFolder();
        // New asset name
        string assetName = "PokemonAssetDatabase.asset";
        PokeAssetDatabase asset = ScriptableObject.CreateInstance("PokeAssetDatabase") as PokeAssetDatabase;  //scriptable object
       //  mockdata.PokemonmockData(asset);
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath("Assets/Resources/Database/" + assetName));
        AssetDatabase.Refresh();
        Debug.Log("Pokemon Database Created and Updated");
    }
    [MenuItem("NXT/Asset Database/Moves Database/Create Moves Database")]
    public static void CreateMovesDatabase()
    {
        // Get the currently selected asset directory
        string currentPath = GetSelectionFolder();
        // New asset name
        string assetName = "MovesAssetDatabase.asset";
        MoveAssetDatabase asset = ScriptableObject.CreateInstance("MoveAssetDatabase") as MoveAssetDatabase;  //scriptable object

        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(currentPath + assetName));
        AssetDatabase.Refresh();
    }

    //[MenuItem("NXT/Asset Database/Pokemon Database/Get From SQL")]
    public static void CreatePokeDatabaseSQL()
    {
        // Get the currently selected asset directory
        string currentPath = GetSelectionFolder();
        // New asset name
        string assetName = "PokemonAssetDatabaseFromSQL.asset";
        PokeAssetDatabase asset = ScriptableObject.CreateInstance("PokeAssetDatabase") as PokeAssetDatabase;  //scriptable object
        GetFromSQL(asset);
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(currentPath + assetName));
        AssetDatabase.Refresh();
    }*/
    [MenuItem("NXT/Asset Database/Create/The Pokemon Database")]
    public static void CreatePokeDatabase()
    {
        
      //  MockData.instance. mockdata = new MockData();
        string path = "Assets/Resources/Database/";
        // New asset name
        string assetName1 = "MovesAssetDatabase.asset";
        MoveAssetDatabase asset1 = ScriptableObject.CreateInstance("MoveAssetDatabase") as MoveAssetDatabase;  //scriptable object
        mockdata.MoveMockData(asset1);
        AssetDatabase.CreateAsset(asset1, AssetDatabase.GenerateUniqueAssetPath(path + assetName1));
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
        //EditorUtility.SetDirty(asset1);
        // Get the currently selected asset directory
        //        string currentPath = GetSelectionFolder();
        // New asset name
        string assetName = "PokemonAssetDatabase.asset";
        PokeAssetDatabase asset = ScriptableObject.CreateInstance("PokeAssetDatabase") as PokeAssetDatabase;  //scriptable object
        mockdata.PokemonmockData(asset,asset1);
        AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(path + assetName));
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets(); 
        Debug.Log("Pokemon Database Created and Updated With the Moves");

      //  EditorUtility.SetDirty(asset);
   
        
    }

    [MenuItem("NXT/Asset Database/UpdateDataBase")]
    public static void UpdateItemsDataBase()
    {
        PokeAssetDatabase pdb = (PokeAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database/PokemonAssetDatabase.asset", typeof(PokeAssetDatabase));
        MoveAssetDatabase mdb = (MoveAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database/MovesAssetDatabase.asset", typeof(MoveAssetDatabase));
        ItemAssetDatabase idb = (ItemAssetDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database/ItemAssetDatabase.asset", typeof(ItemAssetDatabase));
        if (AssetDatabase.Contains(pdb))
        {
            AssetDatabase.DeleteAsset("Assets/Resources/Database/PokemonAssetDatabase.asset");
        }
        if(AssetDatabase.Contains(mdb))
        {
            AssetDatabase.DeleteAsset("Assets/Resources/Database/MovesAssetDatabase.asset");
        }
        if(AssetDatabase.Contains(idb))
        {
            AssetDatabase.DeleteAsset("Assets/Resources/Database/ItemAssetDatabase.asset");
        }
        ItemsDatabase();
        CreatePokeDatabase();
        Debug.Log("Databases Updated");
    }
    /*
    [MenuItem("NXT/Asset Database/Items Database/Update Items DataBase")]
    public static void UpdateItemsDataBase()
    {
        MockData mockdata = new MockData();
        mockdata.ItemMockData();
        Debug.Log("Items Database Updated");
    }
    [MenuItem("NXT/Asset Database/Moves Database/Update Moves DataBase")]
    public static void UpdateMoveDataBase()
    {
        MockData mockdata = new MockData();
        mockdata.MoveMockData();
        Debug.Log("Move Database Updated");
    }
    [MenuItem("NXT/Asset Database/Pokemon Database/Update Pokemon DataBase")]
    public static void UpdatePokemonDataBase()
    {
        MockData mockdata = new MockData();
        mockdata.PokemonmockData();
        Debug.Log("pokemon Database Updated");
    }*/
    /*[MenuItem("NXT/Asset Database/Update DataBase")]
    public static void UpdateDataBase()
    {
        mockdata.MoveMockData();
        Debug.Log("Move Database Updated");
        mockdata.PokemonmockData();
        Debug.Log("pokemon Database Updated");
    }
     * */
    //TODO DO same for itemsList

    //static void GetFromMockDB(PokeAssetDatabase asset)
   // {
        
   // }

    static void GetFromSQL(PokeAssetDatabase asset)
    {
    }

    //TODO GET ARRAY OF POKELIST FROM SQL
    //INSERT ONE BY ONE INTO POKEASSETDATABASE

}
#endif
