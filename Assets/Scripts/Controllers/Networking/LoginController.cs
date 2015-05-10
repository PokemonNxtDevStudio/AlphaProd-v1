
using UnityEngine;
using System.Collections;
using Client;
using System.IO;
public class LoginController : MonoBehaviour {


//    public UILabel Username;
    //public UILabel Password;
    //public UILabel Email;
    private ClientCore client;
    public bool testMode = false;  //Logs in with test data
	// Use this for initialization
	void Start () {

        client = MMOManager.Instance.clientCore;
        client.InitializeClient(NetworkConfig.HOST, NetworkConfig.PORT);
        client.Connect();
        client.onLoginResponse += OnLoginResponse;
   
        client.onConnectionResponse += OnConnectionResponse;
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnConnectionResponse(PacketBuffer buffer)
    {
        if (testMode)
            Login();
    }
    public void OnLoginResponse(PacketBuffer buffer)
    {
        switch ((LoginResponseEC)buffer.StartReading().ReadByte())
        {
            case LoginResponseEC.LoginSuccess: OnLogin(buffer); break;

        }
    }
    void OnLogin(PacketBuffer buffer)
    {
    
        //TRAINER_DATA		TrainerID (Integer)				Prefab Index(short)	AssetID (Integer)				Username (String) ...				LOCATION    
        BinaryReader reader = buffer.StartReading();
        MMOManager.Instance.player = new Player();
        reader.ReadString();
        MMOManager.Instance.player.ID = reader.ReadInt32();
        reader.ReadInt16();
        //MMOManager.Instance.player.TrainerAssetID = reader.ReadInt32();
        //MMOManager.Instance.player.Username = reader.ReadString();
       
        if(!testMode)
        Application.LoadLevelAsync("MovementV1");
        
    }
    public void Login()
    {
        PacketBuffer buffer = client.clientSocket.CreatePacket(PacketTypes.Special);  //Header
        if(testMode)
        {
            
            buffer.StartWriting(true).WriteHeader((byte)SpecialRequest.LoginRequest).WriteString("Test").WriteString("FuckYou232").WriteString("");
            client.clientSocket.SendPacket();
            return;
        }

        //buffer.StartWriting(true).WriteHeader((byte)SpecialRequest.LoginRequest).WriteString(Username.text).WriteString(Password.text).WriteString(Email.text);
        client.clientSocket.SendPacket();
    }

    
}
