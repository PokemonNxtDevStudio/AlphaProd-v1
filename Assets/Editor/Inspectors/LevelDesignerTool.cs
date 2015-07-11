using UnityEngine;

using UnityEditor;

using System.Collections;
using System.Collections.Generic;


public class LevelDesignerTool : EditorWindow
{
    public enum TabAtm
    {
        None,
        SetUP,
        Selection,
        Active
    }

    private static List<Object> prefabs = new List<Object>();
    private Object go = new Object();
    private static TabAtm _tab = LevelDesignerTool.TabAtm.SetUP;
    private Texture2D curAssetTexture;
    private static Object curObject = null;
    private static GameObject ParentOfAssets = null;
    private int childs = 0;

    private static bool _x = false;
    private static bool _y = false;
    private static bool _z = false;
    private static float _AX = 0;
    private static float _AY = 0;
    private static float _AZ = 0;

    private float BoxSizes = 80;

    [MenuItem("NXT/Level Designer Tool %L")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(LevelDesignerTool));
       // prefabs = new List<Object>() /*{ null,null,null,null,null,null,null,null,null}*/;
    }
    void OnGUI()
    {
        Tabs();
        SelectActive();
        Active();
    }    
    //[MenuItem("NXT/Spawn Asset #A")]
    public static void SpawnItem(Vector3 pos)
    {      
        if (_tab == TabAtm.Active && curObject != null)
        {                
           // Debug.Log("Creating Item");
            GameObject item = (GameObject)PrefabUtility.InstantiatePrefab(curObject);
            
            item.transform.position = pos;
           // Debug.Log("Added Asset at :"+ pos);
            if (_x)
                _AX = Random.Range(0, 360);
            else
                _AX = 0;
            if (_y)
                _AY = Random.Range(0, 360);
            else
                _AY = 0;
            if (_z)
                _AZ = Random.Range(0, 360);
            else
                _AZ = 0;

            item.transform.rotation = Quaternion.Euler(_AX, _AY, _AZ);

            if (ParentOfAssets != null)
                item.transform.parent = ParentOfAssets.transform;   
         
        }
        else
        {
            Debug.Log("Select Active Tab");
            _tab = TabAtm.SetUP;
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
            if (GUILayout.Button("Clean Assets", GUILayout.Height(30)))
            {
                prefabs.Clear();
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

                    prefabs[i] = EditorGUILayout.ObjectField(prefabs[i], typeof(Object), false);
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

                        if (GUILayout.Button(AssetPreview.GetAssetPreview(prefabs[i]), GUILayout.Height(BoxSizes)/*, GUILayout.Width(BoxSizes)*/))
                        {
                            curAssetTexture = AssetPreview.GetAssetPreview(prefabs[i]);
                            curObject = prefabs[i];
                            _tab = TabAtm.Active;
                            
                        }
                    }
                    
                }
            }          
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

                if (ParentOfAssets != null)
                    childs = ParentOfAssets.transform.childCount;
                else
                    childs = 0;
                GUILayout.Label("Childs :" + childs); 
                GUILayout.Label("Current Asset To Spawn = " + curObject.name, "Box",GUILayout.Height(25),GUILayout.Width(300));
                GUILayout.BeginHorizontal();
                GUILayout.Label("Random :", "box");
                _x = GUILayout.Toggle(_x, "X", GUILayout.Width(50));
                _y = GUILayout.Toggle(_y, "Y", GUILayout.Width(50));
                _z = GUILayout.Toggle(_z, "Z", GUILayout.Width(50));
                GUILayout.EndHorizontal();
                GUILayout.Box(curAssetTexture,GUILayout.Height(150),GUILayout.Width(150));
                /*if(Input.GetKeyDown(KeyCode.S) )
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
            
                }*/
            }
        }
    }

}
