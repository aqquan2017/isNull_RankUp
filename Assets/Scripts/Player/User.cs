using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ROLE { NONE ,HOST , MEMBER}
[System.Serializable]
public class UserData
{
    public string userName;
    public ROLE userRole;

    public UserData()
    {
        userName = "";
        userRole = ROLE.NONE;
    }
}
public class User : MonoBehaviour
{
    public UserData userData = new UserData(); 
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public bool CheckRole(ROLE roleCheck)
    {
        return userData.userRole == roleCheck ? true : false;
    }

    public void SetData(string name, ROLE role)
    {
        userData.userName = name;
        userData.userRole = role;
    }
}
