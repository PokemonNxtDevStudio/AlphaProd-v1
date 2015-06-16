using UnityEngine;
using System.Collections;
using UnityEditor;
using TeamName.Editors.Database;
using System.Collections.Generic;
using TeamName.Editors;

public class DatabaseMainEditor : EditorWindow
{




    public static DatabasePreferencesEditor settingsEditor { get; set; }
    public static List<ICustomEditorWindow> editors = new List<ICustomEditorWindow>(1);

    public static EditorWindowContainer itemEditor { get; set; }

    private int toolbarIndex;


    protected string[] editorNames
    {
        get
        {
            string[] items = new string[editors.Count];
            for (int i = 0; i < editors.Count; i++)
            {
                items[i] = editors[i].ToString();
            }

            return items;
        }
    }

    private void OnEnable()
    {
        minSize = new Vector2(600.0f, 400.0f);
        //toolbarIndex = 0;

        //if (InventoryEditorUtil.selectedDatabase == null)
        //    return;

        CreateEditors();
    }
    public virtual void CreateEditors()
    {
        editors.Clear();

        itemEditor = new EditorWindowContainer("Poke Items editor", this);
        itemEditor.requiresDatabase = true;
        itemEditor.childEditors.Add(new ItemEditor("Item", "Items", this));

        editors.Add(itemEditor);
        settingsEditor = new DatabasePreferencesEditor("Preferences editor");
        editors.Add(settingsEditor);
    }

    [MenuItem("NXT/Database System/Database Manager", false, -99)] // Always at the top
    public static void ShowWindow()
    {
        GetWindow<DatabaseMainEditor>(false, "Database Manager", true);
        //GetWindow(typeof(ItemManagerEditor), true, "Item manager", true);
    }

    protected virtual bool CheckDatabase()
    {
        if (EditorUtils.selectedDatabase == null)
        {
            ShowItemDatabasePicker();
            return false;
        }

        return true;
    }
    protected virtual void ShowItemDatabasePicker()
    {
        EditorGUILayout.LabelField("Found the following databases in your project folder:", EditorStyles.largeLabel);

        var dbs = AssetDatabase.FindAssets("t:" + typeof(ItemAssetDatabase).Name);
        foreach (var db in dbs)
        {
            EditorGUILayout.BeginHorizontal();

            if (EditorUtils.GetItemDatabase(true, false) != null && AssetDatabase.GUIDToAssetPath(db) == AssetDatabase.GetAssetPath(EditorUtils.GetItemDatabase(true, false)))
                GUI.color = Color.green;

            EditorGUILayout.LabelField(AssetDatabase.GUIDToAssetPath(db), DatabaseEditorStyles.labelStyle);
            if (GUILayout.Button("Select", GUILayout.Width(100)))
            {
                EditorUtils.selectedDatabase = (ItemAssetDatabase)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(db), typeof(ItemAssetDatabase));
                OnEnable(); // Re-do editors
            }

            GUI.color = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        if (dbs.Length == 0)
        {
            EditorGUILayout.LabelField("No Item databases found, first create one in your assets folder.");
        }
    }

    protected virtual void DrawToolbar()
    {
        EditorGUILayout.BeginHorizontal();
        GUI.color = Color.grey;
        if (GUILayout.Button("Select Database", DatabaseEditorStyles.toolbarStyle, GUILayout.Width(100)))
        {
            EditorUtils.selectedDatabase = null;
            toolbarIndex = 0;
        }
        GUI.color = Color.white;

        int before = toolbarIndex;
        toolbarIndex = GUILayout.Toolbar(toolbarIndex, editorNames, DatabaseEditorStyles.toolbarStyle);
        if (before != toolbarIndex)
            editors[toolbarIndex].Focus();

        EditorGUILayout.EndHorizontal();
    }
    public void OnGUI()
    {
        DrawToolbar();

        if (CheckDatabase() == false && editors[toolbarIndex].requiresDatabase)
        {
            //drawFooter();
            return;
        }
        editors[toolbarIndex].Draw();
        //drawFooter();
    }


    void drawFooter()
    {
        EditorGUILayout.BeginHorizontal();
        GUI.color = Color.grey;
        EditorGUI.LabelField(new Rect(5, 380, 1000, 100), "*************************Created for PokemonNXT by Teamname *************************************************", EditorStyles.label);
        EditorGUILayout.EndHorizontal();
    }
    // Use this for initialization

}
