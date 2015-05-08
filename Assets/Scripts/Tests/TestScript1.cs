using UnityEngine;
using System.Collections;
using Client;
using System.IO;
using System;
using Util;

public class TestScript1 : MonoBehaviour {

	// Use this for initialization
//    BinaryReader reader = null;
	void Awake () {

        
	}
	void Start()
    {

        NXT.EventHandler.RegisterEvent(this.gameObject, "OnShopRequest", new Action(OnShopRequest));
       // MMOManager.Instance.Connect();
       // MMOManager.Instance.InstantiateNetworkObject(MMOManager.Instance.player.ID, 0, MMOManager.Instance.player.TrainerAssetID, reader);
    }


    void OnShopRequest()
    {
      
    }
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(KeyCode.E))
	    {
            NXT.EventHandler.ExecuteEvent(this.gameObject, "OnShopRequest");
	    }
	
	}
}
