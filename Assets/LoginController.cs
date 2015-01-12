﻿using UnityEngine;
using System.Collections;
using Client;
using System.IO;
public class LoginController : MonoBehaviour {


    public UILabel Username;
    public UILabel Password;
    private ClientCore client = new ClientCore();
	// Use this for initialization
	void Start () {
  
        client.InitializeClient(NetworkConfig.HOST, NetworkConfig.PORT);
        client.onLoginResponse += OnLoginResponse;
        client.Connect();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnLoginResponse(BinaryReader reader)
    {
        switch ((LoginResponseEC)reader.ReadByte())
        {
            case LoginResponseEC.LoginSuccess: OnLogin(reader); break;

        }
    }
    void OnLogin(BinaryReader reader)
    {
        //MMOManager.Instance.player.ID = reader.ReadInt32();
        //MMOManager.Instance.player.Username = reader.ReadString();
        Application.LoadLevel(1);
        
    }
    public void Login()
    {
        PacketBuffer buffer = client.tcProtoClient.CreatePacket(PacketTypes.Special);  //Header
        buffer.StartWriting(true).WriteHeader((byte)SpecialRequest.LoginRequest).WriteString(Username.text).WriteString("FuckYou232").WriteString("test@232email.ca");
        client.tcProtoClient.SendPacket();
    }
}
