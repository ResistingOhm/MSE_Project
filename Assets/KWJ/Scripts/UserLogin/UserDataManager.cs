using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager udm;

    private bool isLogin = false;
    private string uuid = string.Empty;
    private string username = string.Empty;
    private int highscore = 0;

    void Awake()
    {
        if (udm != null && udm != this)
        {
            Destroy(this);
        }
        else
        {
            udm = this;
            DontDestroyOnLoad(this.gameObject); // Makes the gameobject survive across scene changes
        }
    }
    
    public string GetUuid() { return this.uuid; }
    public string GetUserName() { return this.username;}
    public bool IsLogin() { return this.isLogin;}

    public void UserLogin(string uuid, string username, int score;)
    {
        isLogin = true;
        this.uuid = uuid;
        this.username = username;
        this.highscore = score;
    }

    public void UserLogout()
    {
        isLogin=false;
        this.uuid =string.Empty;
        this.username = string.Empty;
        this.highscore = 0;
    }

}
