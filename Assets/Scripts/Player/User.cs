using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ROLE { NONE ,HOST , MEMBER}
[System.Serializable]
public class UserData
{
    public string userName;
    public ROLE userRole;

}
public class User : MonoBehaviour
{
    public UserData userData;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public bool CheckRole(ROLE roleCheck)
    {
        return userData.userRole == roleCheck ? true : false;
    }
}
