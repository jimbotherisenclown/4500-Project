/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using UnityEngine.UI;
using System.Globalization;
using System.Runtime.InteropServices;
using System;
using agora_utilities;

public class ScreenShare : MonoBehaviour
{
    Texture2D mTexture;
    Rect mRect;
    private string appId = "c391473e2bff4f92b41cb1ce291ee8de";
    public static string channelName = NetworkController.roomName;
    public IRtcEngine mRtcEngine;
    //int i = 100;
    bool running = true;
    int timestamp = 0;

    void Start()
    {
        if ((channelName == null) || (channelName == ""))
        {
            channelName = "Guild";
        }
        Debug.Log("initializeEngine");

        if (mRtcEngine != null)
        {
            Debug.Log("Engine exists. Please unload it first!");
            return;
        }
        // init engine
        mRtcEngine = IRtcEngine.GetEngine(appId);
        // enable log
        mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR | LOG_FILTER.CRITICAL);
        // set callbacks (optional)
        //mRtcEngine.SetParameters("{\"rtc.log_filter\": 65535}");
        mRtcEngine.OnJoinChannelSuccess = onJoinChannelSuccess;
        //Configure the external video source
        mRtcEngine.SetExternalVideoSource(true, false);
        mRtcEngine.SetChannelProfile(CHANNEL_PROFILE.CHANNEL_PROFILE_LIVE_BROADCASTING);
        mRtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
        mRtcEngine.SetVideoEncoderConfiguration(new VideoEncoderConfiguration()
        {
            bitrate = 1130,
            frameRate = FRAME_RATE.FRAME_RATE_FPS_15,
            dimensions = new VideoDimensions() { width = Screen.width, height = Screen.height },

            // Note if your remote user video surface to set to flip Horizontal, then we should flip it before sending
            mirrorMode = VIDEO_MIRROR_MODE_TYPE.VIDEO_MIRROR_MODE_ENABLED
        });
        // Start video mode
        mRtcEngine.EnableVideo();
        // allow camera output callback
        mRtcEngine.EnableVideoObserver();
        // join channel
        mRtcEngine.JoinChannel(channelName, null, 0);
        //Create a rectangle width and height of the screen
        mRect = new Rect(80, 100, Screen.width - 140, Screen.height - 130);
        //Create a texture the size of the rectangle you just created
        mTexture = new Texture2D((int)mRect.width, (int)mRect.height, TextureFormat.RGBA32, false);
        Debug.Log("ScreenShare Activated. Room: " + channelName);
    }
    void Update()
    {
        //Start the screenshare Coroutine
        StartCoroutine(shareScreen());
    }
    //Screen Share
    IEnumerator shareScreen()
    {
        while (running)
        {
            yield return new WaitForEndOfFrame();
            //Read the Pixels inside the Rectangle
            mTexture.ReadPixels(mRect, 0, 0);
            //Apply the Pixels read from the rectangle to the texture
            mTexture.Apply();
            // Get the Raw Texture data from the the from the texture and apply it to an array of bytes
            byte[] bytes = mTexture.GetRawTextureData();
            // int size = Marshal.SizeOf(bytes[0]) * bytes.Length;
            // Check to see if there is an engine instance already created
            //if the engine is present
            if (mRtcEngine != null)
            {
                //Create a new external video frame
                ExternalVideoFrame externalVideoFrame = new ExternalVideoFrame();
                //Set the buffer type of the video frame
                externalVideoFrame.type = ExternalVideoFrame.VIDEO_BUFFER_TYPE.VIDEO_BUFFER_RAW_DATA;
                // Set the video pixel format
                //externalVideoFrame.format = ExternalVideoFrame.VIDEO_PIXEL_FORMAT.VIDEO_PIXEL_BGRA;  // V.2.9.x
                externalVideoFrame.format = ExternalVideoFrame.VIDEO_PIXEL_FORMAT.VIDEO_PIXEL_RGBA;  // V.3.x.x
                //apply raw data you are pulling from the rectangle you created earlier to the video frame
                externalVideoFrame.buffer = bytes;
                //Set the width of the video frame (in pixels)
                externalVideoFrame.stride = (int)mRect.width;
                //Set the height of the video frame
                externalVideoFrame.height = (int)mRect.height;
                //Remove pixels from the sides of the frame
                externalVideoFrame.cropLeft = 10;
                externalVideoFrame.cropTop = 10;
                externalVideoFrame.cropRight = 10;
                externalVideoFrame.cropBottom = 10;
                //Rotate the video frame (0, 90, 180, or 270)
                externalVideoFrame.rotation = 180;
                externalVideoFrame.timestamp = timestamp++;
                //Push the external video frame with the frame we just created
                mRtcEngine.PushVideoFrame(externalVideoFrame);
                if (timestamp % 100 == 0)
                {
                    Debug.Log("Pushed frame = " + timestamp);
                }

            }
        }
    }

    private void onJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);
        GameObject textVersionGameObject = GameObject.Find("VersionText");
        textVersionGameObject.GetComponent<Text>().text = "SDK Version : " + getSdkVersion();
    }

    public string getSdkVersion()
    {
        string ver = IRtcEngine.GetSdkVersion();
        return ver;
    }

}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using UnityEngine.UI;
using System.Globalization;
using System.Runtime.InteropServices;
using System;
public class ScreenShare : MonoBehaviour
{
    Texture2D mTexture;
    Rect mRect;
    [SerializeField]
    private string appId = "c391473e2bff4f92b41cb1ce291ee8de";
    [SerializeField]
    private string channelName = "agora";
    public IRtcEngine mRtcEngine;
    int i = 100;
    void Start()
    {
        
        //Configure the external video source
        mRtcEngine.SetExternalVideoSource(true, false);
        
        //Create a rectangle width and height of the screen
        mRect = new Rect(0, 0, Screen.width-1, Screen.height-1);
        //Create a texture the size of the rectangle you just created
        mTexture = new Texture2D((int)mRect.width, (int)mRect.height, TextureFormat.BGRA32, false);
    }
    void Update()
    {
        //Start the screenshare Coroutine
        StartCoroutine(shareScreen());
    }

    public void loadEngine()
    {
        if(mRtcEngine != null)
        {
            Debug.Log("engine already exists.");
            return;
        }
        Debug.Log("ScreenShare Activated");
        mRtcEngine = IRtcEngine.getEngine(appId);
        // enable log
        mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG);
    }

    public void joinChannel(string channelName)
    {
        Debug.Log("joining channel: " + channelName);
        if (mRtcEngine == null)
        {
            Debug.Log("engine needs to be started first.");
            loadEngine();
        }
        //callbacks
        mRtcEngine.OnJoinChannelSuccess = onJoinChannelSuccess;

        // Start video mode
        mRtcEngine.EnableVideo();
        // allow camera output callback
        mRtcEngine.EnableVideoObserver();
        // join channel
        mRtcEngine.JoinChannel(channelName, null, 0);
    }

    private void onJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("successfully joined channel");
    }

    public void leaveChannel()
    {
        Debug.Log("leaving channel");
        if (mRtcEngine == null)
        {
            Debug.Log("engine needs to be started first.");
            loadEngine();
        }
        mRtcEngine.LeaveChannel();
        mRtcEngine.DisableVideoObserver();
    }

    public void unloadEngine()
    {
        Debug.Log("unloading agora engine.");
        if (mRtcEngine != null)
        {   
            IRtcEngine.Destroy();
            mRtcEngine = null;
        }
        

    }


    //Screen Share
    IEnumerator shareScreen()
    {
        yield return new WaitForEndOfFrame();
        //Read the Pixels inside the Rectangle
        mTexture.ReadPixels(mRect, 0, 0);
        //Apply the Pixels read from the rectangle to the texture
        mTexture.Apply();
        // Get the Raw Texture data from the the from the texture and apply it to an array of bytes
        byte[] bytes = mTexture.GetRawTextureData();
        // Make enough space for the bytes array
        int size = Marshal.SizeOf(bytes[0]) * bytes.Length;
        // Check to see if there is an engine instance already created
        IRtcEngine rtc = IRtcEngine.QueryEngine();
        //if the engine is present
        if (rtc != null)
        {
            //Create a new external video frame
            ExternalVideoFrame externalVideoFrame = new ExternalVideoFrame();
            //Set the buffer type of the video frame
            externalVideoFrame.type = ExternalVideoFrame.VIDEO_BUFFER_TYPE.VIDEO_BUFFER_RAW_DATA;
            // Set the video pixel format
            externalVideoFrame.format = ExternalVideoFrame.VIDEO_PIXEL_FORMAT.VIDEO_PIXEL_BGRA;
            //apply raw data you are pulling from the rectangle you created earlier to the video frame
            externalVideoFrame.buffer = bytes;
            //Set the width of the video frame (in pixels)
            externalVideoFrame.stride = (int)mRect.width;
            //Set the height of the video frame
            externalVideoFrame.height = (int)mRect.height;
            //Remove pixels from the sides of the frame
            externalVideoFrame.cropLeft = 10;
            externalVideoFrame.cropTop = 10;
            externalVideoFrame.cropRight = 10;
            externalVideoFrame.cropBottom = 10;
            //Rotate the video frame (0, 90, 180, or 270)
            externalVideoFrame.rotation = 180;
            // increment i with the video timestamp
            externalVideoFrame.timestamp = i++;
            //Push the external video frame with the frame we just created
            int a = rtc.PushVideoFrame(externalVideoFrame);
            Debug.Log(" pushVideoFrame =       " + a);
        }
    }
}