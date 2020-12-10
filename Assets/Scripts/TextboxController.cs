using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;



public class TextboxController : MonoBehaviour, IChatClientListener
{

    public Text drawingTips;
    ChatClient chatClient;
    public string roomName = "Guild";
    public InputField input;




    // Start is called before the first frame update
    void Start() {
        if (StaticPlayerData.username == null)
        {
            StaticPlayerData.username = "user" + Environment.TickCount%99;
        }
        drawingTips.text = "Waiting on drawing tips!";
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "us";
        this.chatClient.Connect("7f873d11-eec7-4421-a8dd-311e26a71171", "1", new AuthenticationValues(StaticPlayerData.username));
        Debug.Log("created chat client");
        if (roomName == "")
        {
            roomName = "Guild";
        }
        else
        {
            roomName = NetworkController.roomName;
        }
    }

    
    // Update is called once per frame
    void Update() {  
        this.chatClient.Service();
    }

    public void setDrawingTips(string tips) {
        drawingTips.text = tips;
    }

    public void submit()
    {
        send(input.text); //need to add censor here when words arrive
        input.text = "";
    }

    public void send(string message, string[] censor)
    {
        foreach (string word in censor)
        {
            message = message.Replace(word, "#");
        }
        this.chatClient.PublishMessage(roomName, message);
    }

    public void send(string message)
    {
        this.chatClient.PublishMessage(roomName, message);
    }

    #region IChatClientListener implementation

    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
        this.chatClient.SetOnlineStatus(ChatUserStatus.Online, "hello");

        ChatChannel channel = null;
        bool found = this.chatClient.TryGetChannel(roomName, out channel);
        if (!found)
        {
            this.chatClient.Subscribe(roomName);
            Debug.Log("Failed to find channel: " + roomName + ". Creating now.");
            
        }

    }

    public void OnChatStateChange(ChatState state)
    {
        //throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        ChatChannel channel = null;
        this.chatClient.TryGetChannel(channelName, out channel);
        Debug.Log(channel.ToStringMessages());
        setDrawingTips(channel.ToStringMessages());
        //this.chatClient.PublishMessage(channelName, "connected " + Environment.TickCount%99);
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        Debug.Log(message);
        ChatChannel channel = null;
        this.chatClient.TryGetChannel(channelName, out channel);
        //setDrawingTips(channel.ToStringMessages());
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (string channel in channels)
        {
            this.chatClient.PublishMessage(channel, "connected.");
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        //throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string user, string user1)
    {
        //throw new System.NotImplementedException();
    }
    public void OnUserUnsubscribed(string user, string user1)
    {
        //throw new System.NotImplementedException();
    }

    #endregion

}
