using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameConnect : MonoBehaviour
{
    PhotonView view;
    void Start()
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

    
}
