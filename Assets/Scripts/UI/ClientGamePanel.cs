using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
[System.Serializable]
public enum Answer
{
    A = 1, B, C, D
}

public class ClientGamePanel : BasePanel
{
    public const byte InitListAnswer = 1;
    public int[] listAnswer;
    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        //int[] answers = (int[])PhotonNetwork.LocalPlayer.CustomProperties["ListAnswer"];
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["ListAnswer"]);
    }

    public void ChooseAnwser(int answer)
    {

    }
    public override void  OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public override void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    private void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == InitListAnswer)
        {
            listAnswer = (int[])photonEvent.CustomData;
        }
    }
}
