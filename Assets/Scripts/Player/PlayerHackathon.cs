using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int  point;

}
public class PlayerHackathon : Singleton<PlayerHackathon>
{
    public PlayerData playerData;

    public void SetPlayerName(string name)
    {
        playerData.name = name;
        
    }
    public void AddPoint(int point)
    {
        playerData.point += point;
    }
    
}
