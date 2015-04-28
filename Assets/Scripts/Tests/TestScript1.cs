using UnityEngine;
using System.Collections;
using Client;
using System.IO;

public class TestScript1 : MonoBehaviour {

	// Use this for initialization
    BinaryReader reader = null;
	void Awake () {

        
	}
	void Start()
    {
        MMOManager.Instance.Connect();
       // MMOManager.Instance.InstantiateNetworkObject(MMOManager.Instance.player.ID, 0, MMOManager.Instance.player.TrainerAssetID, reader);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
