using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardElement : MonoBehaviour
{
    [SerializeField] Text nameTxt;
    [SerializeField] Text scoreTxt;

    public void SetElement(string name, string score)
    {
        nameTxt.text = name;
        scoreTxt.text = score;
    }
}
