using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LeaderBoardPanel : BasePanel
{
    [SerializeField] Transform parentElement;
    [SerializeField] LeaderBoardElement prefabElement;
    [SerializeField] Button playAgain;

    public override void OverrideText()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        for(int i=0; i<GameController.Instance.leaderBoardData.Count; i++)
        {
            var newElement = Instantiate(prefabElement, parentElement);
            newElement.gameObject.SetActive(true);
            newElement.SetElement(GameController.Instance.leaderBoardData[i].name, GameController.Instance.leaderBoardData[i].point.ToString());
        }

        playAgain.onClick.AddListener(OnPlayAgain);
    }

    private void OnPlayAgain() {
        UIManager.Instance.HideAllPanel();
        SoundManager.Instance.Play(Sounds.UI_POPUP);
        FadeInFadeOut.Instance.Fade(1, () => {
            SceneManager.LoadScene("Lobby");
            UIManager.Instance.ShowPanel(typeof(LobbyPanel));
            
            PhotonNetwork.LeaveRoom();
        }, 1);
    }


 
}
