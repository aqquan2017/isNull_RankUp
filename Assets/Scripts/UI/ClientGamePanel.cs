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
    public const byte SendCurQuest = 2;
    public const byte CheckAnwser = 3;

    private int curQuest;
    private int PlayerPoint;
    private Answer curAnswer;
    public int[] listAnswer;

    [SerializeField] GameObject resultPanel;
    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        
    }

    public void ChooseAnwser(int answer)
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        curAnswer = (Answer)answer;
        Debug.Log(curAnswer.ToString());
    }

    private void CheckAnswer()
    {
        if(curAnswer == (Answer)listAnswer[curQuest])
        {
            PlayerPoint++;
            
        }
        else
        {
            
        }
        if(curQuest>=(listAnswer.Length-1))
            SendPointToHost();
        
    }
 
    public void SendPointToHost()
    {
        object[] data = new object[] { PhotonNetwork.LocalPlayer.NickName, PlayerPoint };
        PhotonNetwork.RaiseEvent(4, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }
    public override void OnEnable()
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
        else if(eventCode == SendCurQuest)
        {
            curQuest = (int) photonEvent.CustomData;
        }
        else if (eventCode == CheckAnwser)
        {
            CheckAnswer();
        }
    }
}
