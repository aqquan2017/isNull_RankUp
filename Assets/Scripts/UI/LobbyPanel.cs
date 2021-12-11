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

    public GameObject playerPrefab; 
    private GameObject curPlayer;
    private User userData;

    private void Start()
    {
        hostBtn.onClick.AddListener(OnHostBtn);
        clientBtn.onClick.AddListener(OnClientBtn);

        curPlayer = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        userData = curPlayer.GetComponent<User>();
    }

    private void OnHostBtn()
    {

        if (string.IsNullOrEmpty(serverRoomID.text))
            return;

        userData.SetData("HOST", ROLE.HOST);
        PhotonNetwork.NickName = "HOST";
        PhotonNetwork.CreateRoom(serverRoomID.text);
    }

    private void OnClientBtn()
    {

        if (string.IsNullOrEmpty(clientRoomID.text) || string.IsNullOrEmpty(clientName.text))
            return;

        userData.SetData(clientName.text, ROLE.MEMBER);
        PhotonNetwork.NickName = clientName.text;
        PhotonNetwork.JoinRoom(clientRoomID.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UIManager.Instance.HidePanel(typeof(LobbyPanel));

        
        if (userData.CheckRole(ROLE.HOST))
        {
            PhotonNetwork.LoadLevel("HostLobby");
            UIManager.Instance.ShowPanel(typeof(HostLobbyPanel));

        }
        if (userData.CheckRole(ROLE.MEMBER))
        {
            UIManager.Instance.ShowPanel(typeof(ClientLobbyPanel));
            PhotonNetwork.LoadLevel("ClientLobby");
        }
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }
}
