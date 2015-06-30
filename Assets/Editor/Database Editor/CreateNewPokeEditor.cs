using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TeamName.Editors;
using UnityEditor;
using UnityEngine;

public class CreateNewPokeEditor : EditorWindow
{


    private Editor itemPicker;

    private PokeComponentPicker picker { get; set; }

    private int currentStage = 0;

    public bool forceFocus;


    private Type type;

    private GameObject _prefab;

    private GameObject prefab
    {
        get
        {
            return _prefab;
        }
        set
        {
            if ((_prefab != null) && string.IsNullOrEmpty(AssetDatabase.GetAssetPath(_prefab)))
                DestroyImmediate(_prefab);

            _prefab = value;
        }
    }


    public Action<Type, GameObject, EditorWindow> callback { get; set; }
    // Use this for initialization

    public static EditorWindow Get(System.Action<System.Type, GameObject, EditorWindow> callback, string windowTitle = "Create new item")
    {
        var window = EditorWindow.GetWindow<CreateNewPokeEditor>(true, "Create a new Pokemon", true);
        window.minSize = new Vector2(400, 500);
        window.maxSize = new Vector2(400, 500);
        window.title = windowTitle;
        window.callback = callback;
        window.forceFocus = false;

        return window;
    }

    public void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        currentStage = 0;
        type = null;
        prefab = null;

        picker = PokeComponentPicker.Get();
        picker.Show(true);
        picker.Close(true);

        picker.OnPickObject += selectedType =>
        {
            type = selectedType;
            currentStage++;

            Repaint();
        };

        Focus();
    }

    public void OnGUI()
    {
        // Recompiled or something?? No callback found
        if (callback == null)
            Close();

        if (forceFocus)
            EditorWindow.FocusWindowIfItsOpen<PokeComponentPicker>();

        var r = new Rect(0, 0, 390, 18);

        GUI.color = Color.gray;

        if (currentStage == 0)
            GUI.color = Color.blue;

        if (GUI.Button(r, "Step 1", "GUIEditor.BreadcrumbLeft"))
        {
            Reset();
        }
        GUI.color = Color.gray;

        r.width /= 2;
        r.x += r.width;
        if (currentStage == 1)
            GUI.color = Color.white;

        if (GUI.Button(r, "Step 2", "GUIEditor.BreadcrumbMid"))
        { }

        GUI.color = Color.white;

        EditorGUILayout.BeginVertical();
        GUILayout.Space(30);

        if (currentStage == 0)
            Step1();
        if (currentStage == 1)
            Step2();

        EditorGUILayout.EndVertical();
    }


    public void Step1()
    {
        // Otherwise it repaints to late...
        if (Event.current.isKey)
        {
            picker.Repaint();
            Repaint();
        }


        // Draw inside ...
        picker.OnGUI();
    }

    public void Step2()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Selected type: " + type.Name, (GUIStyle)"BoldLabel");


        var style = new GUIStyle(DatabaseEditorStyles.boxStyle);
        style.alignment = TextAnchor.MiddleCenter;

        if (GUILayout.Button("No model", GUILayout.ExpandWidth(true), GUILayout.Height(30)))
        {
            // Use a box
            prefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //CreateItem(firstStepType, step2Model);
        }



        ShowOr();


        EditorGUILayout.BeginVertical();
        var boxStyle = new GUIStyle("HelpBox");
        boxStyle.stretchWidth = true;
        boxStyle.fixedHeight = 200;
        boxStyle.alignment = TextAnchor.MiddleCenter;
        var rect = GUILayoutUtility.GetRect(390, 390, 200, 200);
        rect.x += 5;
        rect.width = 390;


        #region Accepting drag for box

        switch (Event.current.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (rect.Contains(Event.current.mousePosition) == false)
                    break;

                DragAndDrop.visualMode = DragAndDropVisualMode.Link;

                if (Event.current.type == EventType.DragPerform)
                {
                    if (DragAndDrop.objectReferences.Length == 1)
                    {
                        prefab = DragAndDrop.objectReferences[0] as GameObject;
                        var sprite = DragAndDrop.objectReferences[0] as Sprite;

                        if (prefab != null)
                        {
                            DragAndDrop.AcceptDrag();
                        }
                        else
                        {
                            if (sprite != null)
                            {
                                DragAndDrop.AcceptDrag();

                                prefab = new GameObject("2D Sprite");
                                var spr = prefab.AddComponent<SpriteRenderer>();
                                spr.sprite = sprite;

                                var col = prefab.AddComponent<BoxCollider2D>();
                                col.isTrigger = true;
                            }
                        }
                    }

                }
                break;
        }

        #endregion


        if (prefab == null)
            GUI.Box(rect, "Drag object here", boxStyle);
        else
        {
            var rect2 = rect;
            rect2.width /= 2;

            Texture2D preview = AssetPreview.GetAssetPreview(prefab);
            if (preview != null)
            {
                EditorGUI.DrawPreviewTexture(rect2, preview);

                rect.width -= rect2.width;
                rect.x += rect2.width;
            }

            GUI.Box(rect, prefab.name, boxStyle);
        }
        GUI.color = Color.white;


        ShowOr();


        if (Event.current.commandName == "ObjectSelectorUpdated")
        {
            if (EditorGUIUtility.GetObjectPickerControlID() == 123)
            {
                prefab = (GameObject)EditorGUIUtility.GetObjectPickerObject();
                forceFocus = true;
            }
        }
        if (Event.current.commandName == "ObjectSelectorUpdated")
        {
            if (EditorGUIUtility.GetObjectPickerControlID() == 124)
            {
                var sprite = (Sprite)EditorGUIUtility.GetObjectPickerObject();

                prefab = new GameObject("2D Sprite");
                var spr = prefab.AddComponent<SpriteRenderer>();
                spr.sprite = sprite;

                var col = prefab.AddComponent<BoxCollider2D>();
                col.isTrigger = true;

                forceFocus = true;
            }
        }
        if (GUILayout.Button("Select model", GUILayout.ExpandWidth(true), GUILayout.Height(30)))
        {
            EditorGUIUtility.ShowObjectPicker<GameObject>(prefab, false, "", 123);
            forceFocus = false;
        }
        if (GUILayout.Button("Select sprite", GUILayout.ExpandWidth(true), GUILayout.Height(30)))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>(prefab, false, "", 124);
            forceFocus = false;
        }
        EditorGUILayout.EndVertical();

        if (prefab == null)
            GUI.enabled = false;

        GUI.color = Color.green;
        if (GUILayout.Button("Create item", (GUIStyle)"LargeButton"))
        {
            if (prefab != null)
                CreateItem(type, prefab);

        }
        GUI.color = Color.white;
        GUI.enabled = true;

        EditorGUILayout.EndVertical();
    }

    private void CreateItem(System.Type type, GameObject model)
    {
        if (callback != null)
            callback(type, model, this);

    }

    private void ShowOr()
    {
        GUILayout.BeginHorizontal();
        var r = GUILayoutUtility.GetRect(400, 400, 20, 20);
        r.width = 180;
        r.y += 8;
        GUI.Label(r, "", "sv_iconselector_sep");

        r.width = 30;
        r.y -= 8;
        r.x += 186;
        GUI.Label(r, "OR");

        r.width = 180;
        r.y += 8;
        r.x += 30;
        GUI.Label(r, "", "sv_iconselector_sep");
        GUILayout.EndHorizontal();
    }
}


