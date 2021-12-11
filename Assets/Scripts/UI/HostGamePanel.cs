using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HostGamePanel : BasePanel
{
    [SerializeField] Text timeTxt;  
    [SerializeField] Text questionTxt;
    


    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChooseAnswer(int question)
    {

    }
}
