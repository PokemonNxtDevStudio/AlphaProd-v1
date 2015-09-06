using UnityEngine;
using System.Collections;

public class TerrainTrees : MonoBehaviour
{
    public Terrain t;
    public GameObject prefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    [ContextMenu("Add Colliders To Trees")]
    public void AddColliders()
    {
        for(int i = 0;i < t.terrainData.treeInstanceCount;i++)
        {
            
            
            Vector3 pos = Vector3.Scale(t.terrainData.treeInstances[i].position, t.terrainData.size) + t.transform.position;
            GameObject col = (GameObject)Instantiate(prefab,pos,Quaternion.identity);
            col.transform.SetParent(this.transform);
            col.gameObject.name = "Tree_Collider_" + (i+1);
            //col.transform.position = t.terrainData.treeInstances[i].position;
        }
    }
}
