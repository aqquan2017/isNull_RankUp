using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class LobbyPanel : BasePanel
{
    [SerializeField] Button hostBtn;
    [SerializeField] Button clientBtn;

    [SerializeField] InputField serverRoomID;
    [SerializeField] InputField clientName;
    [SerializeField] InputField clientRoomID;   

    private void Start()
    {
        hostBtn.onClick.AddListener(OnHostBtn);
        clientBtn.onClick.AddListener(OnClientBtn);
    }

    private void OnHostBtn()
    {
        if (string.IsNullOrEmpty(serverRoomID.text))
            return;

        PhotonNetwork.CreateRoom(serverRoomID.text);
    }

    private void OnClientBtn()
    {
        if (string.IsNullOrEmpty(clientRoomID.text) || string.IsNullOrEmpty(clientName.text))
            return;

        PhotonNetwork.JoinRoom(clientRoomID.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("ClientLobby");
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }
}
