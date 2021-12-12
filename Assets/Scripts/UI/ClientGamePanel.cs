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
    [SerializeField] Button answerA;
    [SerializeField] Button answerB;
    [SerializeField] Button answerC;
    [SerializeField] Button answerD;

    public const byte InitListAnswer = 1;
    public const byte SendCurQuest = 2;
    public const byte CheckAnwser = 3;
    public const byte AddToLeaderBoard = 4;


    private int curQuest;
    private int PlayerPoint;
    private Answer curAnswer;
    public int[] listAnswer;

    private ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        answerA.onClick.AddListener(() => ChooseAnswer(answerA.GetComponent<Image>()));
        answerB.onClick.AddListener(() => ChooseAnswer(answerB.GetComponent<Image>()));
        answerC.onClick.AddListener(() => ChooseAnswer(answerC.GetComponent<Image>()));
        answerD.onClick.AddListener(() => ChooseAnswer(answerD.GetComponent<Image>()));
    }

    private void ChooseAnswer(Image image)
    {
        answerA.GetComponent<Image>().color = image == answerA.GetComponent<Image>() ? Color.black : Color.white;
        answerB.GetComponent<Image>().color = image == answerB.GetComponent<Image>() ? Color.black : Color.white;
        answerC.GetComponent<Image>().color = image == answerC.GetComponent<Image>() ? Color.black : Color.white;
        answerD.GetComponent<Image>().color = image == answerD.GetComponent<Image>() ? Color.black : Color.white;
    }

    public void ChooseAnwser(int answer)
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        curAnswer = (Answer)answer;
    }

    private void CheckAnswer()
    {
        if(curAnswer == (Answer)listAnswer[curQuest])
        {
            //show panel correct
            PlayerPoint += Random.Range(800,1000);

            SoundManager.Instance.Play(Sounds.WIN_LV);
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("Your answer is correct!", "Your point : " + PlayerPoint);
        }
        else
        {
            //show panel Wrong
            SoundManager.Instance.Play(Sounds.LOSE_LV);
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("Your answer is wrong!", "Your point : " + PlayerPoint);
        }

        if (curQuest >= (listAnswer.Length - 1))
        {
            SendPointToHost();
            UIManager.Instance.HideAllPanel();
            UIManager.Instance.ShowPanel(typeof(LeaderBoardPanel));
        }
        
    }
 
    public void SendPointToHost()
    {
        object[] data = new object[] { PhotonNetwork.LocalPlayer.NickName, PlayerPoint };
        PhotonNetwork.RaiseEvent(AddToLeaderBoard, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
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
