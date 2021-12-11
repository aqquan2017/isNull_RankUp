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
        SoundManager.Instance.Play(Sounds.UI_POPUP);

        if (string.IsNullOrEmpty(serverRoomID.text))
        {
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("Missing !!!" ,"Please input Room name !");
            return;
        }

        PhotonNetwork.NickName = "HOST";
        PhotonNetwork.CreateRoom(serverRoomID.text, 
            new Photon.Realtime.RoomOptions() { MaxPlayers = 10 });
    }

    private void OnClientBtn()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);

        if (string.IsNullOrEmpty(clientRoomID.text) || string.IsNullOrEmpty(clientName.text))
        {
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("Missing !!!", "Please input Room name and your name !");
            return;
        }

        PhotonNetwork.NickName = clientName.text;
        PhotonNetwork.JoinRoom(clientRoomID.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        UIManager.Instance.HidePanel(typeof(LobbyPanel));

        
        if (PhotonNetwork.IsMasterClient)
        {
            UIManager.Instance.ShowPanel(typeof(HostLobbyPanel));
            //PhotonNetwork.LoadLevel("HostLobby");

        }
        else
        {
            UIManager.Instance.ShowPanel(typeof(ClientLobbyPanel));
            //PhotonNetwork.LoadLevel("ClientLobby");
        }
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }
}
