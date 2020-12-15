using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Voice.Unity;


public class NetworkController : MonoBehaviourPunCallbacks, ILobbyCallbacks, IConnectionCallbacks, IMatchmakingCallbacks
{
    public static string roomName;
    public int roomSize = 2;
    public bool roomAvail = false;
    
    public static GameObject a;
    public Photon.Voice.Unity.VoiceConnection voiceConnection;
    public static int LEVEL;
    string[] strOpts = new string[1];
    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
    public static bool coach = false;
    public TMPro.TMP_Dropdown dropdown;
    public InputField input;
    public static string username;

    //DontDestroyOnLoad(transform.a);

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Controller");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        //this.voiceConnection = this.GetComponent<VoiceConnection>();
    }


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //connects to Photon master servers
        if(StaticPlayerData.Level.ToString() == null){
            LEVEL = 50;
        }
        else
        {
            LEVEL = StaticPlayerData.Level;
        }
    }

    public void setLevel(int gn)
    {
        if(gn == 0)
        {
            int lvl = dropdown.value;
            Debug.Log(lvl);
            if (lvl == 0)
            {
                LEVEL = 20;
            }
            else if (lvl == 2)
            {
                LEVEL = 70;
            }
            else
            {
                LEVEL = 50;
            }
        }
        else
        {
            if (LEVEL > 98)
            {
                LEVEL = 98;
            }else if(LEVEL < 10)
            {
                LEVEL = 10;
            }
        }
        Debug.Log(LEVEL);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

    //connect to lobby when start is pressed
    public void StartButton()
    {
        if(input.text != null)
        {
            username = input.text;
        }
        else
        {
            username = "user" + Environment.TickCount % 99;
        }
        Debug.Log(username);
        setLevel(0);
        Connect();
    }

    //this is where matchmaking would be done if PUN was well enough documented
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("updating room list");
        int closest = 100;
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("room Level " + room.CustomProperties["LEVEL"].ToString());
            Debug.Log(room.ToStringFull()); //this is for testing if I had a way to test
            if(room.IsOpen && room.IsVisible)
            {
                roomAvail = true;
                if(((Int32.Parse(room.CustomProperties["LEVEL"].ToString()) - LEVEL) <= 5) && ((Int32.Parse(room.CustomProperties["LEVEL"].ToString()) - LEVEL) >= -5)){
                    JoinARoom(room.Name);
                    break;
                }
                else {
                    int tmp = (Int32.Parse(room.CustomProperties["LEVEL"].ToString()) - LEVEL);
                    if (tmp < 0) {
                        tmp = tmp * -1;
                    }
                    if (tmp < closest) {
                        roomName = room.Name;
                        closest = tmp;
                    }
                }
            }

        }
        if(roomAvail)
        {
            JoinARoom(roomName);
        }
        else
        {
            CreateRoom();
        }
    }

    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void Connect()
    {
        Debug.Log("executing connect");
        if (PhotonNetwork.IsConnected)
        {
            // Join the lobby to do matchmaking 
            PhotonNetwork.JoinLobby(customLobby);
            //OnRoomListUpdate executes here
            //PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            Connect();
        }
    }

    //used for testing
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = (byte)2 });
        Connect();
    }


    //runs when a room is joined
    public override void OnJoinedRoom()
    {
        Debug.Log("Join Room called by PUN. Now this client is in a room." + roomName);
        //joinVoice();

        if (coach)
        {
            SceneManager.LoadScene("CommunicationCenter");
        }
        else
        {
            SceneManager.LoadScene("BlankCanvas");
        }
    }

    void joinVoice()
    {
        EnterRoomParams Roomoptions = new EnterRoomParams();
        Roomoptions.RoomName = roomName + "voice";
        voiceConnection.ConnectUsingSettings();
        voiceConnection.Client.OpJoinOrCreateRoom(Roomoptions);
        Debug.Log("join voice room");
        if (voiceConnection.PrimaryRecorder == null)
        {
            voiceConnection.PrimaryRecorder = this.gameObject.AddComponent<Recorder>();
        }
        voiceConnection.PrimaryRecorder.TransmitEnabled = true;
    }


    //used to join a room after one is picked
    void JoinARoom(string name)
    {
        Debug.Log("Trying to join room: " + name);
        roomName = name;
        PhotonNetwork.JoinRoom(name);
    }

    //create a room with the custom settings
    void CreateRoom()
    {
        coach = true;
        strOpts[0] = "LEVEL";
        int randomRoomName = UnityEngine.Random.Range(0, 10000);
        Debug.Log("Creating a room: Room" + randomRoomName.ToString());
        RoomOptions roomOpts = new RoomOptions();
        roomOpts.IsVisible = true;
        roomOpts.IsOpen = true;
        roomOpts.MaxPlayers = (byte)roomSize;
        roomOpts.CustomRoomPropertiesForLobby = strOpts;
        roomOpts.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { "LEVEL", LEVEL } };
        roomName = "Room" + randomRoomName;
        PhotonNetwork.CreateRoom(roomName, roomOpts);
    }

    //try again on the off chance that a name is repeated
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create room failed. Trying again.");
        CreateRoom();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
