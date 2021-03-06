using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostQuestionPanel : BasePanel
{
    [SerializeField] InputField timeInput;
    [SerializeField] InputField questionInput;
    [SerializeField] InputField answerAInput;
    [SerializeField] InputField answerBInput;
    [SerializeField] InputField answerCInput;
    [SerializeField] InputField answerDInput;
    [SerializeField] Toggle answerA;
    [SerializeField] Toggle answerB;
    [SerializeField] Toggle answerC;
    [SerializeField] Toggle answerD;
    [SerializeField] Button backBtn;
    [SerializeField] Button nextQuestBtn;


    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        answerA.onValueChanged.AddListener((x) => ChangeTooggleAnswer(answerA, x));
        answerB.onValueChanged.AddListener((x) => ChangeTooggleAnswer(answerB, x));
        answerC.onValueChanged.AddListener((x) => ChangeTooggleAnswer(answerC, x));
        answerD.onValueChanged.AddListener((x) => ChangeTooggleAnswer(answerD, x));

        nextQuestBtn.onClick.AddListener(OnNextQuestion);
        backBtn.onClick.AddListener(OnBackBtn);
    }

    private void OnNextQuestion()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);

        //check exception
        if (string.IsNullOrEmpty(timeInput.text))
        {
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("Time Input !", "Please input Time for question!");
            return;

        }

        if (string.IsNullOrEmpty(questionInput.text)
            || string.IsNullOrEmpty(answerAInput.text)
            || string.IsNullOrEmpty(answerBInput.text)
            || string.IsNullOrEmpty(answerCInput.text)
            || string.IsNullOrEmpty(answerDInput.text))
        {
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("Missing Input Question!", "Please type answer and question!");
            return;
        }

        if (!answerA.isOn
            && !answerB.isOn
            && !answerC.isOn
            && !answerD.isOn)
        {
            UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
            UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("Missing Right Question!", "Please choose correct answer!");
            return;

        }

       

        QuestionData questionData = new QuestionData
        {
            time = float.Parse(timeInput.text),
            answerA = answerAInput.text,
            answerB = answerBInput.text,
            answerC = answerCInput.text,
            answerD = answerDInput.text,
            questionTxt = questionInput.text,
            rightAnswer = answerA.isOn ? Answer.A
                        : answerB.isOn ? Answer.B
                        : answerC.isOn ? Answer.C
                        : Answer.D
        };

        GameController.Instance.questionDatas.Add(questionData);

        //reset UI
        timeInput.text = "";
        questionInput.text = "";
        answerAInput.text = "";
        answerBInput.text = "";
        answerCInput.text = "";
        answerDInput.text = "";
        answerA.isOn = false;
        answerB.isOn = false;
        answerC.isOn = false;
        answerD.isOn = false;

        UIManager.Instance.ShowPanelWithDG(typeof(TextPopupPanel));
        UIManager.Instance.GetPanel<TextPopupPanel>().SetInfo("Create Success", "Your question have been saved!");
    }

    private void OnBackBtn()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        UIManager.Instance.HideAllPanel();
        UIManager.Instance.ShowPanel(typeof(HostLobbyPanel));
    }

    private void ChangeTooggleAnswer(Toggle toggle, bool state)
    {
        if (state)
        {
            answerA.isOn = answerA == toggle;
            answerB.isOn = answerB == toggle;
            answerC.isOn = answerC == toggle;
            answerD.isOn = answerD == toggle;
        }
    }
}
