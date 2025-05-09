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
        }
    }
    // called when "Random Person" button is clicked
    public void LogIn(string id, string pw)
    {
        StartCoroutine(LoginRequest(id, pw));
    }

    // called when "Add Person" button is clicked
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

    private IEnumerator LoginRequest(string id, string pw)
    {
        yield return StartCoroutine(IdCheckRequest(id));
        Debug.Log(idCheck);

        if (!idCheck)
        {
            Debug.LogWarning("존재하지 않는 아이디입니다.");
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
                Debug.LogError("Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("Protocol error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                // success! Let's parse the JSON data
                string json = webRequest.downloadHandler.text;
                //Debug.Log(json);
                if (json == null)
                {
                    Debug.Log("비밀번호가 틀립니다.");
                    break;
                }
                parseUserDataResult(json);
                Debug.Log("로그인 성공!");
                SceneManager.LoadScene("MainScene");
                break;

        }

    }
    // parsing JSON and showing it on the GUI
    private void parseUserDataResult(string json)
    {
        UserData u = JsonUtility.FromJson<UserData>(json);

        UserDataManager.udm.UserLogin(u.uuid, u.name);
    }
    private string GetPersonJson(string name, string id, string pw)
    {
        UserData u = new UserData();
        u.name = name;
        u.id = id;
        u.pw = pw;

        // convert to json and return
        return JsonUtility.ToJson(u);
    }

    private IEnumerator AddRequest(string name, string id, string pw)
    {
        yield return StartCoroutine(IdCheckRequest(id));
        Debug.Log(idCheck);

        if (idCheck)
        {
            Debug.LogWarning("이미 존재하는 아이디입니다.");
            yield break;
        }

        string info = GetPersonJson(name, id, pw);
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
                Debug.LogError("Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("Protocol error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.Success:
                // success! Let's parse the JSON data
                string json = webRequest.downloadHandler.text;
                //Debug.Log(json);
                if (json == null)
                {
                    Debug.Log("뭔가 문제가 발생했어요");
                    break;
                }
                Debug.Log("회원가입 성공!");
                SceneManager.LoadScene("Login");
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
                Debug.LogError("Error: " + webRequest.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("Protocol error: " + webRequest.error);
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
}
