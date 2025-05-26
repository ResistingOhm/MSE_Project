using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class RandomBox : MonoBehaviour
{
    private bool used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used || !collision.CompareTag("Player"))
            return;

        used = true;
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            StartCoroutine(ActivateBox(player));
        }
    }

    IEnumerator ActivateBox(Player player)
    {
        string joke = "No joke today";

        UnityWebRequest www = UnityWebRequest.Get("https://official-joke-api.appspot.com/jokes/random/1");
        yield return www.SendWebRequest();

        if (!www.isNetworkError && !www.isHttpError)
        {
            string result = www.downloadHandler.text;
            joke = ParseJokeFromJSON(result);
        }

        int outcome = GetBoxResult(player.stat.luck); // 0: 꽝 1: 하급포션 2: 상급포셩

        switch (outcome)
        {
            case 0:
                Debug.Log($"Joke: {joke}\n 꽝! 아무것도 얻지 못했습니다.");
                break;
            case 1:
                player.stat.Heal(15);
                Debug.Log($"Joke: {joke}\n하급 포션! 체력 15 회복!");
                break;
            case 2:
                player.stat.Heal(30);
                Debug.Log($"Joke: {joke}\n상급 포션! 체력 30 회복!");
                break;
        }

        gameObject.SetActive(false);
    }

    int GetBoxResult(float luck)
    {
        float rand = Random.value;

        float badChance = Mathf.Clamp01(0.6f - luck * 0.01f);
        float midChance = Mathf.Clamp01(0.3f + luck * 0.007f);
        float goodChance = 1f - badChance - midChance;

        if (rand < badChance)
            return 0; //꽝
        else if (rand < badChance + midChance)
            return 1; // 하급
        else
            return 2; // 상급
    }

    string ParseJokeFromJSON(string json)
    {
        try
        {
            var jokeData = JsonUtility.FromJson<JokeWrapper>($"{{\"jokes\":{json}}}");
            return jokeData.jokes[0].setup + " " + jokeData.jokes[0].punchline;
        }
        catch
        {
            return "Parsing error.";
        }
    }

    [System.Serializable]
    public class Joke
    {
        public string setup;
        public string punchline;
    }

    [System.Serializable]
    public class JokeWrapper
    {
        public Joke[] jokes;
    }
}
