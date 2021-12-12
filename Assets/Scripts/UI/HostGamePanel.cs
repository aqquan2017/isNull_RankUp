using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
public class HostGamePanel : BasePanel
{
    [SerializeField] Text timeTxt;  
    [SerializeField] Text questionTxt;

    [SerializeField] Text answerA;
    [SerializeField] Text answerB;
    [SerializeField] Text answerC;
    [SerializeField] Text answerD;  

    private int curQuest = 0;
    private float time = 0;
    private bool isWait = false;

    public const byte InitListAnswer = 1;
    public const byte SendCurQuest = 2;
    public const byte CheckAnwser = 3;
    public const byte AddToLeaderBoard = 4;

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        SendListAnswer(GameController.Instance.questionDatas);

        SendCurrentQuest(curQuest);
        ShowQuestion(GameController.Instance.questionDatas[curQuest]);
    }

    private void ShowQuestion(QuestionData questionData)
    {
        timeTxt.text = questionData.time.ToString();
        questionTxt.text = questionData.questionTxt;
        answerA.text = questionData.answerA;
        answerB.text = questionData.answerB;
        answerC.text = questionData.answerC;
        answerD.text = questionData.answerD;

        time = questionData.time;

        isWait = true;

    }

    private void Update()
    {
        if (!isWait)
           return;

        if(time > 0)
        {
            time -= Time.deltaTime;
            int timeInt = (int)time;
            timeTxt.text = timeInt.ToString();
        }
        else
        {
            curQuest++;
            //new question, send result for all client
            if (curQuest >= GameController.Instance.questionDatas.Count)
            {
                //end game, show leaderboard
                isWait = false;

                TimerManager.Instance.AddTimer(3, () =>
                {
                    UIManager.Instance.HideAllPanel();
                    UIManager.Instance.ShowPanel(typeof(LeaderBoardPanel));
                });

                PhotonNetwork.RaiseEvent(CheckAnwser, 1, RaiseEventOptions.Default, SendOptions.SendReliable);
            }
            else
            {
                isWait = false;

                PhotonNetwork.RaiseEvent(CheckAnwser, 1, RaiseEventOptions.Default, SendOptions.SendReliable);

                TimerManager.Instance.AddTimer(3f, () =>
                {
                    SendCurrentQuest(curQuest);
                    ShowQuestion(GameController.Instance.questionDatas[curQuest]);
                });
            }
            

        }
    }
    public void ChooseAnswer(int question)
    {

    }

    public void SendListAnswer(List<QuestionData> listQuestionData)
    {
        int[] listAnswer = new int[listQuestionData.Count];
        for(int i = 0; i < listAnswer.Length; i++)
        {
            listAnswer[i] = (int)listQuestionData[i].rightAnswer;
        }
        PhotonNetwork.RaiseEvent(InitListAnswer, listAnswer, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void SendCurrentQuest(int cur)
    {
        
        PhotonNetwork.RaiseEvent(SendCurQuest, cur, RaiseEventOptions.Default, SendOptions.SendReliable);
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
        if (eventCode == AddToLeaderBoard)
        {
            object[] data = (object[])photonEvent.CustomData;

            GameController.Instance.leaderBoardData.Add(new PlayerData()
            {
                name = data[0].ToString(),
                point = (int)data[1]
            }); 
        }
    }
}
