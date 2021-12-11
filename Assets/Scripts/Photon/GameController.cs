using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : BaseManager<GameController>
{
    public List<QuestionData> questionDatas = new List<QuestionData>();
    public List<PlayerData> leaderBoardData = new List<PlayerData>();
    public override void Init()
    {

    }
}
