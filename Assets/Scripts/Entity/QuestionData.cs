using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string answerA;
    public string answerB;
    public string answerC;
    public string answerD;

    public string questionTxt;
    public Answer rightAnswer;
    public float time;

    public QuestionData(Answer rightAnswer)
    {
        this.rightAnswer = rightAnswer;
    }

    public QuestionData()
    {

    }
}
