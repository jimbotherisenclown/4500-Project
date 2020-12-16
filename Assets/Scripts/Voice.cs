// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConnectAndJoin.cs" company="Exit Games GmbH">
//   Part of: Photon Voice Utilities for Unity - Copyright (C) 2018 Exit Games GmbH
// </copyright>
// <summary>
// Simple component to call voiceConnection.ConnectUsingSettings() and get into a Voice room easily.
// </summary>
// <remarks>
// Requires a VoiceConnection component attached to the same GameObject.
// </remarks>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;

namespace Photon.Voice.Unity.UtilityScripts
{
    [RequireComponent(typeof(VoiceConnection))]
    public class Voice : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks
    {
        private VoiceConnection voiceConnection;

        public bool RandomRoom = true;
        public int VoiceStart = 3;
        public int VoiceEnd = 4;

        [SerializeField]
        private bool autoConnect = true;

        [SerializeField]
        private bool publishUserId = false;

        public bool voiceEn = false;
        

        public string RoomName;

        private readonly EnterRoomParams enterRoomParams = new EnterRoomParams
        {
            RoomOptions = new RoomOptions()
        };

        public bool IsConnected { get { return this.voiceConnection.Client.IsConnected; } }

        private void Awake()
        {
            this.voiceConnection = this.GetComponent<VoiceConnection>();
            GameObject go = GameObject.Find("NetworkController");
            NetworkController cs = go.GetComponent<NetworkController>();
            RoomName = NetworkController.roomName;
            this.voiceConnection.PrimaryRecorder.TransmitEnabled = voiceEn;
        }

        private void OnEnable()
        {
            this.voiceConnection.Client.AddCallbackTarget(this);
            if (this.autoConnect)
            {
                this.ConnectNow();
            }
        }

        private void OnDisable()
        {
            this.voiceConnection.Client.RemoveCallbackTarget(this);
        }

        public void ConnectNow()
        {
            this.voiceConnection.ConnectUsingSettings();
        }

        #region MatchmakingCallbacks

        public void OnCreatedRoom()
        {

        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogErrorFormat("OnCreateRoomFailed errorCode={0} errorMessage={1}", returnCode, message);
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {

        }

        public void OnJoinedRoom()
        {
            if (this.voiceConnection.PrimaryRecorder == null)
            {
                this.voiceConnection.PrimaryRecorder = this.gameObject.AddComponent<Recorder>();
            }
            Debug.Log(RoomName);
            Debug.Log(NetworkController.LEVEL);
        }

        public void toggle()
        {
            Debug.Log("Voice Toggle");
            if (voiceEn)
            {
                voiceEn = false;
            }
            else
            {
                voiceEn = true;
            }
            this.voiceConnection.PrimaryRecorder.TransmitEnabled = voiceEn;
        }
        public void toggle(bool b)
        {
            Debug.Log("Voice Toggle");
            voiceEn = b;
            this.voiceConnection.PrimaryRecorder.TransmitEnabled = b;
        }

        void Update()
        {

            if((GameController.currentPhase == VoiceStart) && !voiceEn)
            {
                toggle(true);
            }
            if((GameController.currentPhase == VoiceEnd) && voiceEn)
            {
                toggle(false);
            }
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogErrorFormat("OnJoinRandomFailed errorCode={0} errorMessage={1}", returnCode, message);
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogErrorFormat("OnJoinRoomFailed roomName={0} errorCode={1} errorMessage={2}", this.RoomName, returnCode, message);
        }

        public void OnLeftRoom()
        {

        }

        #endregion

        #region ConnectionCallbacks

        public void OnConnected()
        {

        }

        public void OnConnectedToMaster()
        {
            this.enterRoomParams.RoomOptions.PublishUserId = this.publishUserId;
            if (this.RandomRoom)
            {
                this.enterRoomParams.RoomName = null;
                this.voiceConnection.Client.OpJoinRandomOrCreateRoom(new OpJoinRandomRoomParams(), this.enterRoomParams);
            }
            else
            {
                this.enterRoomParams.RoomName = this.RoomName;
                this.voiceConnection.Client.OpJoinOrCreateRoom(this.enterRoomParams);
            }
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            if (cause == DisconnectCause.None || cause == DisconnectCause.DisconnectByClientLogic)
            {
                return;
            }
            Debug.LogErrorFormat("OnDisconnected cause={0}", cause);
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {

        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {

        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {

        }

        #endregion
    }
}