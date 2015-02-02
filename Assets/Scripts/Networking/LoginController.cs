
using UnityEngine;
using System.Collections;
using Client;
using System.IO;
public class LoginController : MonoBehaviour {


    public UILabel Username;
    public UILabel Password;
    private ClientCore client;
    public bool testMode = true;  //Logs in with test data
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

    void OnConnectionResponse(BinaryReader reader)
    {
        if (testMode)
            Login();
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
        MMOManager.Instance.player = new Player();
        reader.ReadString();
        MMOManager.Instance.player.ID = reader.ReadInt32();
        MMOManager.Instance.player.Username = reader.ReadString();
        MMOManager.Instance.player.TrainerAssetID = reader.ReadInt32();
        if(!testMode)
        Application.LoadLevelAsync(4);
        
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
       
        buffer.StartWriting(true).WriteHeader((byte)SpecialRequest.LoginRequest).WriteString(Username.text).WriteString("FuckYou232").WriteString("");
        client.clientSocket.SendPacket();
    }

    
}
