using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameConnect : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.HideAllPanel();
        SoundManager.Instance.StopAll(true);
        SoundManager.Instance.PlayLoop(Sounds.LevelBGM, true);

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
