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

    public const byte InitListAnswer = 1;
    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        List<QuestionData> questionDatas = new List<QuestionData>();
        questionDatas.Add(new QuestionData(Answer.A));
        questionDatas.Add(new QuestionData(Answer.B));
        SendListAnswer(questionDatas);
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
}
