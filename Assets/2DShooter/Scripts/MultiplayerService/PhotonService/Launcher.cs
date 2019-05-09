using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace MultiplayerSystem
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField][Tooltip("Max number of player")]
        byte maxPlayers = 4;

        MultiplayerService multiplayerService;

        public void ConnectPhoton(MultiplayerService multiplayerService)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            this.multiplayerService = multiplayerService;
            PhotonNetwork.ConnectUsingSettings(); 
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRandomRoom();
            Debug.Log("[Launcher] JoinRoom");
        }

        public void LogIn(string playerName)
        {
            PhotonNetwork.NickName = playerName;
            PhotonNetwork.JoinLobby(TypedLobby.Default);
            Debug.Log("[Launcher] LogIn");
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("[Launcher]Connected to the Master");
            multiplayerService.ConnectedToMasterEvent();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("[Launcher]Disconnected Reason:" + cause);
            multiplayerService.DisconnectedFromServerEvent();
        }

        public override void OnJoinedLobby()
        {
            string playerName = PhotonNetwork.NickName;
            multiplayerService.JoinedToLobbyEvent(playerName);
        }

        public override void OnLeftLobby()
        {

        }

        public override void OnJoinedRoom()
        {
            multiplayerService.JoinedRoomEvent();
            Debug.Log("[Launcher] JoinRoom Photon");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("[Launcher]Failed To Join Room");
            PhotonNetwork.CreateRoom("Room" + PhotonNetwork.CountOfRooms, 
                                    new RoomOptions { MaxPlayers = maxPlayers });
        }

        public override void OnCreatedRoom()
        {

            Debug.Log("[Launcher]Created Room");
        }

        public override void OnLeftRoom()
        {

        }



    }
}