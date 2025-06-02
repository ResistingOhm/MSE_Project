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
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void LogIn(string id, string pw)
    {
        StartCoroutine(LoginRequest(id, pw));
    }

    public void Register(string name, string id, string pw)
    {
        idCheck = false;
        StartCoroutine(AddRequest(name, id, pw));
    }

    public bool IdCheck(string id)
    {
        StartCoroutine(IdCheckRequest(id));
        return idCheck;
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
            PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.warning, "This ID does not exist. Please register first.");
            yield break;
        }

        UnityWebRequest webRequest = UnityWebRequest.Get(loginUrl + "?id=" + id + "&password=" + pw);
        webRequest.SetRequestHeader("Accept", "application/json");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.error, "Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.error, "Protocol error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                string json = webRequest.downloadHandler.text;
                if (json == null)
                {
                    PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.error, "Incorrect password");
                    break;
                }
                parseUserDataResult(json);
                PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.notice, "Login successful");
                SceneManager.LoadScene("MainScene");
                break;
        }
    }

    private void parseUserDataResult(string json)
    {
        ProducedUserData u = JsonUtility.FromJson<ProducedUserData>(json);
        UserDataManager.udm.UserLogin(u.uuid, u.name, u.highscore, u.scoreid);
    }

    private string GetUserDataJson(string name, string id, string pw)
    {
        RequestUserData u = new RequestUserData { name = name, id = id, pw = pw };
        return JsonUtility.ToJson(u);
    }

    private IEnumerator AddRequest(string name, string id, string pw)
    {
        yield return StartCoroutine(IdCheckRequest(id));
        Debug.Log(idCheck);

        if (idCheck)
        {
            PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.warning, "This ID is already taken. Please choose another one.");
            yield break;
        }

        string info = GetUserDataJson(name, id, pw);
        UnityWebRequest webRequest = UnityWebRequest.Post(addUrl, info, "application/json");
        webRequest.SetRequestHeader("Accept", "application/json");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.error, "Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.error, "Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                string json = webRequest.downloadHandler.text;
                if (json == null)
                {
                    PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.error, "Failed to register.");
                    break;
                }
                PopupWindow.instance.PopupWindowOpen(PopupWindow.MsgType.notice, "Registration successful!");
                SceneManager.LoadScene("Login");
                break;
        }
    }

    private IEnumerator IdCheckRequest(string id)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(idcheckUrl + "?id=" + id);
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("ID Check Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                string res = webRequest.downloadHandler.text;
                idCheck = Convert.ToBoolean(res);
                break;
        }
    }

    private IEnumerator UpdateScoreRequest(long id, long score, int enemynum, int gamelevel, PlayerStatData playerstat, int min, int sec)
    {
        string info = GetScoreDataJson(id, score, enemynum, gamelevel, playerstat, min, sec);
        UnityWebRequest webRequest = UnityWebRequest.Post(updateScoreUrl, info, "application/json");
        webRequest.SetRequestHeader("Accept", "application/json");
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                UserDataManager.udm.SetHighscore(score);
                SceneManager.LoadScene("MainScene");
                break;
            default:
                Debug.LogError("UpdateScore Error: " + webRequest.error);
                break;
        }
    }

    private string GetScoreDataJson(long id, long score, int enemynum, int gamelevel, PlayerStatData playerstat, int min, int sec)
    {
        RequestScoreData s = new RequestScoreData
        {
            id = id,
            score = score,
            enemynum = enemynum,
            gamelevel = gamelevel,
            playerstat = playerstat,
            min = min,
            sec = sec
        };
        return JsonUtility.ToJson(s);
    }

    private IEnumerator LeaderBoardRequest()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(leaderBoardUrl);
        webRequest.SetRequestHeader("Accept", "application/json");
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string json = webRequest.downloadHandler.text;
            ScoreDataList s = JsonUtility.FromJson<ScoreDataList>("{\"scores\":" + json + "}");
            LeaderBoardManager.instance.SetList(s);
        }
        else
        {
            Debug.LogError("Leaderboard Error: " + webRequest.error);
        }
    }

    private IEnumerator LikeScoreRequest(long id)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(likeScoreUrl + "?id=" + id);
        webRequest.SetRequestHeader("Accept", "application/json");
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Like Error: " + webRequest.error);
        }
    }

    private IEnumerator RandomJokeRequest(int num)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(randomjokeUrl + num);
        webRequest.SetRequestHeader("Accept", "application/json");
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string json = webRequest.downloadHandler.text;
            RandomJokeListData j = JsonUtility.FromJson<RandomJokeListData>("{\"randomjokes\":" + json + "}");
            foreach (RandomJokeData joke in j.randomjokes)
            {
                Debug.Log(joke.setup + " / " + joke.punchline);
            }
        }
        else
        {
            Debug.LogError("Joke Error: " + webRequest.error);
        }
    }
}
