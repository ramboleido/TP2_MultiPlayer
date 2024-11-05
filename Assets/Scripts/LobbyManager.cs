using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    private string gameVersion = "1.0.0";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        Conectar();
    }

    private void Conectar()
    {
        if (PhotonNetwork.IsConnected) 
        {
            PhotonNetwork.JoinRandomRoom();

        }
        else 
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado ao Photon Master Server");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Falha ao entrar em sala aleatória, criando nova sala");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou na sala com sucesso");
        Debug.Log("Id: " + PhotonNetwork.CurrentRoom.Name);

        if(PhotonNetwork.CurrentRoom.PlayerCount == 1) 
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }
}
