using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    void Start()
    {
        TimerManager.Instance.Init();
        UIManager.Instance.Init();
        SceneController.Instance.Init();
        GameController.Instance.Init();
        SoundManager.Instance.Init();

        SoundManager.Instance.PlayLoop(Sounds.BGM, true);
    }

    
}
