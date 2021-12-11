using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HostLobbyPanel : BasePanel
{
    [SerializeField] Button startGame; 
    [SerializeField] Button createQuest;
    [SerializeField] Text roomName;
    [SerializeField] Transform clientNameParent;
    [SerializeField] Text clientNamePrefab;

    //public GameObject playerPrefab;
    //private GameObject curPlayer;
    //private PhotonView view;

    private void Start()
    {
        startGame.onClick.AddListener(OnStartGame);
        createQuest.onClick.AddListener(OnCreateQuest);

        roomName.text = "Room name : " + PhotonNetwork.CurrentRoom.Name;

        UpdatePlayerList();

        //curPlayer = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        //view = curPlayer.GetComponent<PhotonView>();
    }

    void UpdatePlayerList()
    {
        foreach(Transform obj in clientNameParent)
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

    private void OnStartGame()
    {
        //load for server
        //UIManager.Instance.HideAllPanelWithDG();
        //UIManager.Instance.ShowPanel(typeof(HostGamePanel));

        //load for client

        //view.RPC("SwitchGamePanel", RpcTarget.All);
        PhotonNetwork.LoadLevel("Game");
    }

    [PunRPC]
    public void SwitchGamePanel()
    {
        UIManager.Instance.HideAllPanel();
        if (PhotonNetwork.IsMasterClient)
        {
            UIManager.Instance.ShowPanel(typeof(HostGamePanel));
        }
        else
        {
            UIManager.Instance.ShowPanel(typeof(ClientGamePanel));
        }
    }

    private void OnCreateQuest()
    {

    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }
}
