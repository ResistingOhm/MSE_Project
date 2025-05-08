using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private string loginUrl = "http://localhost:4444/userlogin/login";
    private string addUrl = "http://localhost:4444/userlogin/add";

    public TMP_InputField sNameInput;
    public TMP_InputField sIdInput;
    public TMP_InputField sPwInput;
    public TMP_InputField sPwCheckInput;

    public TMP_InputField loginIdInput;
    public TMP_InputField loginPwInput;

    // called when "Random Person" button is clicked
    public void LogIn()
    {
        if (UserDataManager.udm.IsLogin())
        {
            Debug.Log("Already Login");
            return;
        }
        string id = loginIdInput.text;
        string pw = loginPwInput.text;
        StartCoroutine(LoginRequest(id, pw));
    }

    // called when "Add Person" button is clicked
    public void SignIn()
    {
        string name = sNameInput.text;
        string id = sIdInput.text;
        if (!sPwInput.text.Equals(sPwCheckInput.text))
        {
            Debug.Log("Pw Not Correct");
            return;
        }
        string pw = sIdInput.text;
        StartCoroutine(AddRequest(name, id, pw));
    }

    private IEnumerator LoginRequest(string id, string pw)
    {
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
                Debug.Log(json);
                parseUserDataResult(json);
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
                Debug.Log(json);
                break;

        }
    }
}
