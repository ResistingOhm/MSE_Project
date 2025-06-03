using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager apiManager;

    private string loginUrl = "http://localhost:4444/userlogin/login";
    private string addUrl = "http://localhost:4444/userlogin/add";
    private string idcheckUrl = "http://localhost:4444/userlogin/check/id";
    private string updateScoreUrl = "http://localhost:4444/userlogin/update/score";
    private string leaderBoardUrl = "http://localhost:4444/userlogin/leaderboard";
    private string likeScoreUrl = "http://localhost:4444/userlogin/like";

    private string randomjokeUrl = "https://official-joke-api.appspot.com/jokes/random/";

    //status
    private bool idCheck = false;

    void Awake()
    {
        if (apiManager != null && apiManager != this)
        {
            Destroy(this);
        }
        else
        {
            apiManager = GetComponent<NetworkManager>();
            DontDestroyOnLoad(this.gameObject); // Makes the gameobject survive across scene changes
        }
    }
    // called when "Login" button is clicked
    public void LogIn(string id, string pw)
    {
        StartCoroutine(LoginRequest(id, pw));
    }

    // called when "Register" button is clicked
    public void Register(string name, string id, string pw)
    {
        idCheck = false;
        StartCoroutine(AddRequest(name, id, pw));
    }

    public void UpdateScore(long id, long score, int enemynum, int gamelevel, PlayerStatData playerstat, int min, int sec)
    {
        StartCoroutine(UpdateScoreRequest(id, score, enemynum, gamelevel, playerstat, min, sec));
    }

    public void GetLeaderBoard()
    {
        StartCoroutine(LeaderBoardRequest());
    }

    public void LikeScore(long id)
    {
        StartCoroutine(LikeScoreRequest(id));
    }

    public void GetRandomJoke(int num)
    {
        StartCoroutine(RandomJokeRequest(num));
    }

    private IEnumerator LoginRequest(string id, string pw)
    {
        yield return StartCoroutine(IdCheckRequest(id));
        Debug.Log(idCheck);

        if (!idCheck)
        {
            PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.warning,"This ID does not exist. Please register first."
            );
            yield break;
        }

        UnityWebRequest webRequest = UnityWebRequest.Get(loginUrl+"?id="+id+"&password="+pw);
        
        // setting header
        webRequest.SetRequestHeader("Accept", "application/json");

        // executing the request
        yield return webRequest.SendWebRequest();

        // process the resuls
        switch(webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,"Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
               PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,"Protocol error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                // success! Let's parse the JSON data
                string json = webRequest.downloadHandler.text;
                //Debug.Log(json);
                if (json == null)
                {
                    PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,"Incorrect password");
                    break;
                }
                parseUserDataResult(json);
                
                SceneManager.LoadScene("MainScene");
                break;

        }

    }
    // parsing JSON and showing it on the GUI
    private void parseUserDataResult(string json)
    {
        ProducedUserData u = JsonUtility.FromJson<ProducedUserData>(json);

        UserDataManager.udm.UserLogin(u.uuid, u.name, u.highscore, u.scoreid);
    }

    private void parseScoreDataResult(string json)
    {
        ScoreDataList s = JsonUtility.FromJson<ScoreDataList>(json);

        LeaderBoardManager.instance.SetList(s);
    }
    private string GetUserDataJson(string name, string id, string pw)
    {
        RequestUserData u = new RequestUserData();
        u.name = name;
        u.id = id;
        u.pw = pw;

        // convert to json and return
        return JsonUtility.ToJson(u);
    }

    private string GetScoreDataJson(long id, long score, int enemynum, int gamelevel, PlayerStatData playerstat, int min, int sec)
    {
        RequestScoreData s = new RequestScoreData();
        s.id = id;
        s.score = score;
        s.enemynum = enemynum;
        s.gamelevel = gamelevel;
        s.playerstat = playerstat;
        s.min = min;
        s.sec = sec;

        return JsonUtility.ToJson(s);
    }

    private IEnumerator AddRequest(string name, string id, string pw)
    {
        yield return StartCoroutine(IdCheckRequest(id));
        Debug.Log(idCheck);

        if (idCheck)
        {
            PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.warning,"This ID is already taken. Please choose another one.");
            yield break;
        }

        string info = GetUserDataJson(name, id, pw);
        UnityWebRequest webRequest = UnityWebRequest.Post(addUrl, info, "application/json");

        // setting header
        webRequest.SetRequestHeader("Accept", "application/json");

        // executing the request
        yield return webRequest.SendWebRequest();

        // process the resuls
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,
                "Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,
                "Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                // success! Let's parse the JSON data
                string json = webRequest.downloadHandler.text;
                //Debug.Log(json);
                if (json == null)
                {
                    PopupWindow.instance.PopupWindowOpen(
                    PopupWindow.MsgType.error,
                    "Failed to register.");
                    break;
                }
                PopupWindow.instance.PopupCheckWindowOpen(
                () => {
                SceneManager.LoadScene("Login");},
                "MoveToLoginScene",
                PopupWindow.MsgType.notice,
                "Registration successful!");
                break;

        }
    }

    private IEnumerator UpdateScoreRequest(long id, long score, int enemynum, int gamelevel, PlayerStatData playerstat, int min, int sec)
    {

        string info = GetScoreDataJson(id, score, enemynum, gamelevel, playerstat, min, sec);
        UnityWebRequest webRequest = UnityWebRequest.Post(updateScoreUrl, info, "application/json");

        // setting header
        webRequest.SetRequestHeader("Accept", "application/json");

        // executing the request
        yield return webRequest.SendWebRequest();

        // process the resuls
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.ProtocolError:  
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Protocol Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.Success:
                // success! Let's parse the JSON data
                string json = webRequest.downloadHandler.text;
                //Debug.Log(json);
                UserDataManager.udm.SetHighscore(score);
                //Do Something after updating score
                SceneManager.LoadScene("MainScene");
                break;

        }
    }

    private IEnumerator IdCheckRequest(string id)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(idcheckUrl + "?id=" + id);

        // executing the request
        yield return webRequest.SendWebRequest();

        // process the resuls
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.ProtocolError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Protocol Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.Success:
                // success! Let's parse the JSON data
                string res = webRequest.downloadHandler.text;
                idCheck = Convert.ToBoolean(res);
                Debug.Log(idCheck);
                //parseUserDataResult(json);
                break;

        }
    }

    private IEnumerator LeaderBoardRequest()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(leaderBoardUrl);

        // setting header
        webRequest.SetRequestHeader("Accept", "application/json");

        // executing the request
        yield return webRequest.SendWebRequest();

        // process the resuls
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.ProtocolError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Protocol Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.Success:
                // success! Let's parse the JSON data
                string json = webRequest.downloadHandler.text;
                //Debug.Log(json);
                parseScoreDataResult(json);
                Debug.Log("LeaderBoard Done");
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.notice,("LeaderBoard Done"));
                break;

        }

    }

    private IEnumerator LikeScoreRequest(long id)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(likeScoreUrl + "?id=" + id);

        // setting header
        webRequest.SetRequestHeader("Accept", "application/json");

        // executing the request
        yield return webRequest.SendWebRequest();

        // process the resuls
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.ProtocolError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Protocol Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.Success:
                // success! Let's parse the JSON data
                string json = webRequest.downloadHandler.text;
                //Debug.Log(json);
                //Something about likecount
                Debug.Log("like Done");
                break;

        }

    }

    private IEnumerator RandomJokeRequest(int num)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(randomjokeUrl + num);

        // setting header
        webRequest.SetRequestHeader("Accept", "application/json");

        // executing the request
        yield return webRequest.SendWebRequest();

        // process the resuls
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.ProtocolError:
                PopupWindow.instance.PopupWindowOpen(
                PopupWindow.MsgType.error,("Protocol Error: " + webRequest.error));
                break;
            case UnityWebRequest.Result.Success:
                // success! Let's parse the JSON data
                string json = webRequest.downloadHandler.text;
                Debug.Log(json);
                parseRandomJokeResult(json);
                break;

        }

    }

    private void parseRandomJokeResult(string json)
    {
        json = "{\"randomjokes\":" + json + "}";
        RandomJokeListData j = JsonUtility.FromJson<RandomJokeListData>(json);

        foreach(RandomJokeData joke in j.randomjokes)
        {
            Debug.Log(joke.setup + " / " + joke.punchline);
            //Call some method to get result
        }
        
    }
}