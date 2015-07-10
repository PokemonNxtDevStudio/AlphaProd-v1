using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
public class LevelDesignerTool : EditorWindow
{
    private static List<Object> prefabs =  new List<Object>();

    public enum TabAtm
    {
        None,
        SetUP,
        Selection,
        Active
    }
    private Object go = new Object();

    private static TabAtm _tab = LevelDesignerTool.TabAtm.SetUP;
    private Texture2D curAssetTexture;

    private Rect pos1 = new Rect(10, 60, 500, 200);

    private static Object curObject = null;


    private static GameObject ParentOfAssets = null;

    [MenuItem("NXT/Level Designer Tool %L")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelDesignerTool));
       // prefabs = new List<Object>() /*{ null,null,null,null,null,null,null,null,null}*/;
    }

 

    void OnGUI()
    {
        Tabs();
        //SetUpSection();
        SelectActive();
        Active();

        
 
    }
    [MenuItem("NXT/Spawn Asset #A")]
    public static void SpawnItem()
    {
        if (_tab == TabAtm.Active)
        {
            if (curObject != null)
            {
                //Debug.Log("Cur Item not null");
                
                    Debug.Log("Creating Item");
                    GameObject item = (GameObject)PrefabUtility.InstantiatePrefab(curObject);
                    
                    Ray ray = SceneView.currentDrawingSceneView.camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        item.transform.position = hit.point;
                        Debug.Log(hit.point);
                    }
                    if (ParentOfAssets != null)
                        item.transform.parent = ParentOfAssets.transform;

            }
        }
        else
        {
            Debug.Log("Select Active Tab");
        }
    }

    private void Tabs()
    {
        GUILayout.BeginHorizontal();

        
        if (GUILayout.Button("Set up", GUILayout.Height(50)))
        {
            _tab = TabAtm.SetUP;
        }
       
        if (GUILayout.Button("Setting", GUILayout.Height(50)))
        {
            _tab = TabAtm.Selection;
        }
        
        if (GUILayout.Button("Active", GUILayout.Height(50)))
        {
            _tab = TabAtm.Active;
        }    
        
        
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical();
        SetUpSection();
        GUILayout.EndVertical();
        if (_tab == TabAtm.SetUP)
        {
            if (GUILayout.Button("Add Item", GUILayout.Height(50)))
            {
                prefabs.Add(go);
            }
        }
    }

   
    private void SetUpSection()
    {
        if (_tab == TabAtm.SetUP)
        {
            GUILayout.Label("Objects");
           
            if(prefabs.Count > 0)
            {
                for (int i = 0; i < prefabs.Count; i++)
                {
                    
                    prefabs[i] = EditorGUILayout.ObjectField(prefabs[i], typeof(Object),false);
                }
            }
        }
    }

    private void SelectActive()
    {
        if(_tab == TabAtm.Selection)
        {
            GUILayout.BeginVertical();
           // GUILayout.BeginArea(pos1); 
            if (prefabs.Count > 0)
            {
                for (int i = 0; i < prefabs.Count; i++)
                {
                    if(prefabs[i] != null)
                    {
                        
                        if(GUILayout.Button(AssetPreview.GetAssetPreview(prefabs[i]),GUILayout.Height(100)))
                        {
                            curAssetTexture = AssetPreview.GetAssetPreview(prefabs[i]);
                            curObject = prefabs[i];
                            _tab = TabAtm.Active;
                            
                        }
                    }
                    
                }
            }
           // GUILayout.EndArea(); 
            GUILayout.EndVertical();


        }
    }

    private void Active()
    {
        if(_tab == TabAtm.Active)
        {
            if(curObject != null)
            {
                //GUILayout.Label("Parent Object to = " + curObject.name, "Box", GUILayout.Height(25), GUILayout.Width(300));
                ParentOfAssets = (GameObject)EditorGUILayout.ObjectField("Parent Object to = ",ParentOfAssets, typeof(GameObject), true);
                GUILayout.Label("Current Asset To Spawn = " + curObject.name, "Box",GUILayout.Height(25),GUILayout.Width(300));
               
                GUILayout.Box(curAssetTexture,GUILayout.Height(150),GUILayout.Width(150));
                if(Input.GetKeyDown(KeyCode.S) )
                {
                    Debug.Log("Creating Item");
                    GameObject item =(GameObject) PrefabUtility.InstantiatePrefab(curObject);
                    if(ParentOfAssets != null)
                    item.transform.parent = ParentOfAssets.transform;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        item.transform.position = hit.point;                        
                    }
            
                }
            }
        }
    }

}
#endif