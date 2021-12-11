using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LeaderBoardPanel : BasePanel
{
    [SerializeField] Transform parentElement;
    [SerializeField] LeaderBoardElement prefabElement;


    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        for(int i=0; i<PhotonNetwork.PlayerList.Length; i++)
        {
            var newElement = Instantiate(prefabElement, parentElement);
            newElement.gameObject.SetActive(true);
            newElement.SetElement(PhotonNetwork.PlayerList[i].NickName, "0");
        }
    }

 
}
