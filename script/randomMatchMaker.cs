using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class randomMatchMaker : MonoBehaviourPunCallbacks
{
    public GameObject PhotonObject;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;
        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(
            PhotonObject.name,
            new Vector3(0f, 1f, 0f),
            Quaternion.identity,
            0
        );
        GameObject gc = GameObject.FindGameObjectWithTag("GameController");
        GameObject.FindGameObjectWithTag("SubCamera").SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>().gameObject.SetActive(true);
        gc.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<Camera>();
    }
}