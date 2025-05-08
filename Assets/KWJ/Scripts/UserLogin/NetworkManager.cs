using System.Collections;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    private string loginUrl = "http://localhost:4444/userlogin/login";
    private string addUrl = "http://localhost:4444/userlogin/add";

    public TMP_InputField nameInput;
    public TMP_InputField ageInput;
    public TMP_InputField hobbiesInput;
    public TMP_InputField majorInput;

    // called when "Random Person" button is clicked
    public void GetRandomPerson()
    {
        StartCoroutine(LoginRequest());
    }

    // called when "Add Person" button is clicked
    public void AddPerson()
    {
        StartCoroutine(AddRequest());
    }

    private IEnumerator LoginRequest()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(loginUrl);
        
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
                parseResult(json);
                break;

        }
    }
    // parsing JSON and showing it on the GUI
    private void parseResult(string json)
    {
        Person p = JsonUtility.FromJson<Person>(json);

        nameInput.text = p.name;
        ageInput.text = ""+p.age;
        hobbiesInput.text = p.hobbies;
        majorInput.text = p.major;


    }
    
    // read data from the input fields, and create a json object based on them.
    private string GetPersonJson()
    {
        Person p = new Person();
        p.name = nameInput.text;
        p.age = int.Parse(ageInput.text);
        p.hobbies = hobbiesInput.text;
        p.major = majorInput.text;

        // convert to json and return
        return JsonUtility.ToJson(p);
    }

    private IEnumerator AddRequest()
    {
        string json = GetPersonJson();

        UnityWebRequest webRequest = UnityWebRequest.Post(addUrl, json, "application/json");


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

                Debug.Log("Adding result: " + webRequest.downloadHandler.text);
                break;

        }
    }
}
