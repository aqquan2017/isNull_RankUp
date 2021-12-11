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
        
        for(int i=0; i<GameController.Instance.leaderBoardData.Count; i++)
        {
            var newElement = Instantiate(prefabElement, parentElement);
            newElement.gameObject.SetActive(true);
            newElement.SetElement(GameController.Instance.leaderBoardData[i].name, GameController.Instance.leaderBoardData[i].point.ToString());
        }
    }

 
}
