using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(LevelDesignerRay))]
public class LevelDesig : Editor
{
    private bool canSpawn = true;
    public void OnSceneGUI()
    {
        if (Event.current.shift &&Event.current.type == EventType.MouseDown/* && Event.current.isKey*/ && canSpawn)
        {
           
           // Debug.Log("fuckkkS");
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit = new RaycastHit();
           
            if (Physics.Raycast(ray, out hit))
            {            
                LevelDesignerTool.SpawnItem(hit.point);
                canSpawn = false;
            }
            
            Event.current.Use();
        }
        if(Event.current.type == EventType.MouseUp)
        {
            canSpawn = true;
        }
    }
}
