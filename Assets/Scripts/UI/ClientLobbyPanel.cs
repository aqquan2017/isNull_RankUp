using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ClientLobbyPanel : BasePanel
{
    [SerializeField] Text roomName;
    [SerializeField] Transform clientNameParent;
    [SerializeField] Text clientNamePrefab;

    private void Start()
    {
        roomName.text = "Room name : " + PhotonNetwork.CurrentRoom.Name;

        UpdatePlayerList();
    }

    void UpdatePlayerList()
    {
        foreach (Transform obj in clientNameParent)
        {
            Destroy(obj.gameObject);
        }

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            var newElement = Instantiate(clientNamePrefab, clientNameParent);
            newElement.gameObject.SetActive(true);
            newElement.text = PhotonNetwork.PlayerList[i].NickName;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("ON CLIENT JOIN ROOM : ALL " + PhotonNetwork.PlayerList.Length);
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("ON CLIENT LEFT ROOM : ALL " + PhotonNetwork.PlayerList.Length);
        UpdatePlayerList();
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }
}
