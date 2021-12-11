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
        curQuest++;
    }

    private void Update()
    {
        if (!isWait)
           return;

        if(time > 0)
        {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString();
        }
        else
        {

            //new question, send result for all client
            if(curQuest >= GameController.Instance.questionDatas.Count)
            {
                //end game, show leaderboard
                Debug.Log("END GAME");
                isWait = false;

                TimerManager.Instance.AddTimer(4f, () =>
                {
                   UIManager.Instance.HideAllPanel();
                   UIManager.Instance.ShowPanel(typeof(LeaderBoardPanel));
                });
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

    IEnumerator OnEndGame()
    {
        PhotonNetwork.RaiseEvent(CheckAnwser, 1, RaiseEventOptions.Default, SendOptions.SendReliable);
        yield return new WaitForSeconds(3);
        SendCurrentQuest(curQuest);
        ShowQuestion(GameController.Instance.questionDatas[curQuest]);
        yield return new WaitForSeconds(3);
        UIManager.Instance.HideAllPanel();
        UIManager.Instance.ShowPanel(typeof(LeaderBoardPanel));
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
}
