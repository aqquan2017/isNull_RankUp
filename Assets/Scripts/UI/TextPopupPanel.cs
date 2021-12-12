using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextPopupPanel : BasePanel, IPointerDownHandler
{
    [SerializeField] private Text header;
    [SerializeField] private Text des;

    public event System.Action afterClose;

    public void SetInfo(string head, string desTxt, System.Action closeAct = null)
    {
        header.text = head;
        des.text = desTxt;
        afterClose = closeAct;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClosePanel();
    }

    public void ClosePanel()
    {
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        afterClose?.Invoke();
        HideWithDG();
    }

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }
}
