using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.Init();
        TimerManager.Instance.Init();
        UIManager.Instance.Init();
        SceneController.Instance.Init();
    }

    
}
