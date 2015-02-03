using UnityEngine;
using Client;
using System.Collections;
using System.IO;

public class ChatController : MonoBehaviour {

	// Use this for initialization

    Player player;
    private ClientCore client;
    public UILabel PlayerName;
   public UIInput ChatMessage;
    public UITextList textList;
	void Start () {
        
        player = MMOManager.Instance.player;

        client = MMOManager.Instance.clientCore;
        client.onChatResponse += OnChatResponse;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// At this pointer buffer contains the Player ID + Player Name
    /// </summary>
    /// <param name="buffer"></param>
    void OnChatResponse(PacketBuffer buffer)
    {
        BinaryReader reader = buffer.StartReading();
        if (textList != null)
        {   
                textList.Add("Player " +reader.ReadInt32() +": "+reader.ReadString());
                ChatMessage.value = "";
                ChatMessage.isSelected = false;
        }
    }
    public void SendChat()
    {
        PacketBuffer buffer = client.clientSocket.CreatePacket(PacketTypes.Special);  //Header
        buffer.StartWriting(true).WriteHeader((byte)SpecialRequest.SendChat).WriteString(NGUIText.StripSymbols(ChatMessage.value));
        client.clientSocket.SendPacket();
    }
}
