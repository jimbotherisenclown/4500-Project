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
    public string roomName = NetworkController.roomName;
    public InputField input;
    public bool voiceEn = false;

    Dictionary<string, string[]> censor = new Dictionary<string, string[]>
    {
        {"apple", new string[] {"apple", "fruit", "stem", "worm", "core", "keep the doctor away"}},
        {"banana", new string[] {"banana", "fruit", "monkey", "peel"}},
        {"bandAid", new string[] {"band-aid", "bandage", "tape", "adhesive"}},
        {"basketball", new string[] {"basketball", "hoops", "b-ball", "sport", "round ball", "ball"}},
        {"bee", new string[] {"bee", "yellow jackets", "honey", "buzz", "stinger", "stripes", "hive"}},
        {"beehive", new string[] {"beehive", "Beyonce", "nest", "honey", "buzzing"}},
        {"bucket", new string[] {"bucket", "pail", "handle", "pitcher", "vessel"}},
        {"butterfly", new string[] {"butterfly", "caterpillar", "monarch", "wings"}},
        {"candle", new string[] {"candle", "wax", "wick", "lamp", "light", "beacon", "lantern", "candelabra"}},
        {"carrot", new string[] {"carrot", "bunny", "bunnnies", "veggie", "vegetable" }},
        {"christmasTree", new string[] {"christmas tree", "tree", "x-mas"}},
        {"clock", new string[] {"clock", "timepiece", "timekeeper", "timer", "chronograph", "dial", "numbers", "o'clock"}},
        {"coffeeCup", new string[] {"coffee cup", "java", "mug", "steam", "saucer"}},
        {"fence", new string[] {"fence", "barrier", "wall", "partition", "enclosure", "border"}},
        {"fish ", new string[] {"fish", "angle", "bob", "cast", "chum", "trawl"}},
        {"flag", new string[] {"flag", "banner", "symbol", "pennant", "streamer", "country"}},
        {"flashlight", new string[] {"flashlight", "electric lantern", "flash lamp", "spotlight", "streamlight"}},
        {"flower", new string[] {"flower", "spring", "bloom", "blossom"}},
        {"ghost", new string[] {"ghost", "ghoul", "wraith", "halloween", "Casper"}},
        {"house", new string[] {"home", "homestead", "habitation", "residence", "abode", "house", "domicile", "door"}},
        {"lamp", new string[] {"lamp", "light", "beacon", "lantern", "candelabra"}},
        {"lightBulb", new string[] {"light bulb", "lighting", "electric", "lamp"}},
        {"lemon", new string[] {"lemon", "lemonade"}},
        {"milkCarton", new string[] {"milk carton", "milk", "vitamin D", "calcium", "missing"}},
        {"mountain", new string[] {"mountain", "peak", "mountaintop", "alp", "elevation", "summit", "range", "ridge"}},
        {"mountains", new string[] {"mountain", "peak", "mountaintop", "alp", "elevation", "summit", "range", "ridge"}},
        {"ornament", new string[] {"ornament", "christmas", "decoration", "trinket"}},
        {"pants", new string[] {"pants", "trousers", "slacks", "jeans", "clothing", "clothes"}},
        {"peeledBanana", new string[] {"peeled banana", "banana", "fruit", "monkey", "peel"}},
        {"paintBrush", new string[] {"paint brush", "paintbrush", "paint", "brush"}},
        {"pencil", new string[] {"pencil", "write", "note", "scribble", "take down", "inscribe"}},
        {"pizza", new string[] {"pizza", "pie", "slice", "pizzeria", "italian food"}},
        {"pointyFlower", new string[] {"flower", "pointy flower", "bloom", "blossom"}},
        {"popcorn", new string[] {"popcorn", "movies", "butter", "corn"}},
        {"pumpkin", new string[] {"pumpkin", "october", "jack-o-lantern", "halloween", "pie"}},
        {"road", new string[] {"road", "boulevard", "course", "highway", "roadway", "pavement", "drive", "lane", "route"}},
        {"shirt", new string[] {"shirt", "blouse", "jersey", "pullover", "tunic", "pullover", "clothing"}},
        {"shoe", new string[] {"shoe", "sneaker", "boot", "feet", "loafer", "slipper", "moccasin"}},
        {"smileyFace", new string[] {"smiley face", "emoji", "smiling", "grin", "smiley"}},
        {"snowman", new string[] {"snowman", "jack frost", "Olaf", "christmas", "snow"}},
        {"spoon", new string[] {"spoon", "scoop", "lade", "ladle", "utensil"}},
        {"stopSign", new string[] {"stop sign", "red octogan", "traffic", "signal"}},
        {"strawberry", new string[] {"strawberry", "seeds", "fruit"}},
        {"sun", new string[] {"sun", "sunshine", "sunlight", "daylight", "light", "warmth", "beams", "rays"}},
        {"tomato", new string[] {"tomato", "vegetable", "fruit"}},
        {"trafficLight", new string[] {"traffic light", "traffic", "red light", "stoplight", "traffic control", "yellow light", "amber light", "caution light", "go light", "green light"}},
        {"tree", new string[] {"tree", "forest", "sapling", "shrub", "timber", "wood", "woods", "stump"}},
        { "vase", new string[] {"vase", "decor", "pottery", "flower holder","urn"}},
        {"watch", new string[] {"watch", "timepiece", "timekeeper", "timer", "chronograph"}},
        {"water", new string[] {"water", "h2o", "glass", "cup", "aqua"}},
        {"watermelon", new string[] {"watermelon", "fruit", "seed"}},
        {"wave", new string[] {"wave", "water", "ripple", "ocean", "tide"}},
        {"window", new string[] {"window", "casment", "opening", "aperture", "bow", "bay"} }
    };





    // Start is called before the first frame update
    void Start() {
        if (NetworkController.username == null)
        {
            NetworkController.username = "user" + Environment.TickCount%99;
        }
        drawingTips.text = "Waiting on drawing tips!";
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "us";
        this.chatClient.Connect("7f873d11-eec7-4421-a8dd-311e26a71171", "1", new AuthenticationValues(NetworkController.username));
        Debug.Log("created chat client");
        if (NetworkController.roomName == null)
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
        if ((GameController.currentPhase == 2) && !voiceEn && NetworkController.coach)
        {
            Debug.Log("voice connected");
            voiceEn = true;
        }
        if ((GameController.currentPhase == 4) && voiceEn && NetworkController.coach)
        {
            Debug.Log("voice disconnected");
            voiceEn = false;
        }
    }

    public void setDrawingTips(string tips) {
        drawingTips.text = tips;
    }

    public void submit()
    {
        if (censor.ContainsKey(ImageGenerator.image[ImageGenerator.num].name))
        {
            send(input.text, censor[ImageGenerator.image[ImageGenerator.num].name]);
        }
        else
        {
            send(input.text);
        }
        
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
        if (!NetworkController.coach)
        {
            this.chatClient.PublishMessage(roomName, "connected.");
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
            //this.chatClient.PublishMessage(channel, "connected.");
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
