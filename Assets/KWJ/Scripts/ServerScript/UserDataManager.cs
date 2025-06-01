using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager udm;

    [SerializeField]
    private bool isLogin = false;
    [SerializeField]
    private string uuid = string.Empty;
    [SerializeField]
    private string username = string.Empty;
    [SerializeField]
    private long highscore = -1;
    [SerializeField]
    private long scoreid = -1;

    private bool selectedLevel = true;

    void Awake()
    {
        if (udm != null && udm != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            udm = this;
            DontDestroyOnLoad(this.gameObject); // Makes the gameobject survive across scene changes
        }
    }
    
    public string GetUuid() { return this.uuid; }
    public string GetUserName() { return this.username;}
    public long GetHighscore() { return this.highscore;}
    public void SetHighscore(long score) { this.highscore = score;}
    public long GetScoreId() { return this.scoreid;}
    public bool IsLogin() { return this.isLogin;}
    public bool SelectedLevel() { return this.selectedLevel;}
    public void SelectedLevel(bool b) { this.selectedLevel = b; }

    public void UserLogin(string uuid, string username, long score, long scoreid)
    {
        isLogin = true;
        this.uuid = uuid;
        this.username = username;
        this.highscore = score;
        this.scoreid = scoreid;
    }

    public void UserLogout()
    {
        isLogin=false;
        this.uuid =string.Empty;
        this.username = string.Empty;
        this.highscore = -1;
        this.scoreid = -1;
    }

}
