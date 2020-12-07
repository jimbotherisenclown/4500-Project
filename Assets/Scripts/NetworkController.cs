using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static string roomName;
    public int roomSize = 2;
    public bool roomAvail = false;
    
    public string LEVEL;
    string[] strOpts = new string[1];
    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);



    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //connects to Photon master servers
        if(StaticPlayerData.Level.ToString() == null){
            LEVEL = "50";
        }
        else
        {
            LEVEL = StaticPlayerData.Level.ToString();
        }
}

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        //Connect();
    }

    //connect to lobby when start is pressed
    public void StartButton()
    {
        Connect();
    }

    //this is where matchmaking would be done if PUN was well enough documented
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("updating room list");
        foreach(RoomInfo room in roomList)
        {
            Debug.Log(room.ToStringFull()); //this is for testing if I had a way to test
            if(room.IsOpen && room.IsVisible)
            {
                roomAvail = true;
                break;
                //if((room.LEVEL-user.Level) <=5 and (room.LEVEL-user.Level) >=-5
                //then roomName = room.name
                //else int closest = 1000 int tmp = (room.LEVEL-user.Level) string  
                //if tmp < 0 then tmp * -1
                //if tmp < closest roomName = room.name closest = tmp
            }

        }
        //if roomAvail join roomName
        //else CreateRoom()
        CreateRoom();
    }



    public void Connect()
    {
        Debug.Log("executing connect");
        if (PhotonNetwork.IsConnected)
        {
            // Join the lobby to do matchmaking 
            PhotonNetwork.JoinLobby(customLobby);
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
        strOpts[0] = LEVEL;
        int randomRoomName = Random.Range(0, 10000);
        Debug.Log("Creating a room: Room" + randomRoomName.ToString());
        RoomOptions roomOpts = new RoomOptions();
        roomOpts.IsVisible = true;
        roomOpts.IsOpen = true;
        roomOpts.MaxPlayers = (byte)roomSize;
        roomOpts.CustomRoomPropertiesForLobby = strOpts;
        roomOpts.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { LEVEL, 1 } };
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
