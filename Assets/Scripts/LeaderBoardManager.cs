using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderBoardManager : MonoBehaviour
{
    public static LeaderBoardManager instance;
    /*
    [System.Serializable]
    public class EntryData
    {
        public string name;
        public int level;
        public int score;
    }
    */
    public Transform entryContainer;
    //public Transform entryTemplate;  
    public GameObject entryPrefab;

    //private List<Transform> entryTransformList = new();
    private ScoreDataList scoreDataList;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        /*
        List<EntryData> dataList = new List<EntryData>()
        {
            new EntryData() { name = "Alice", level = 1, score = 300 },
            new EntryData() { name = "Bob", level = 2, score = 450 },
            new EntryData() { name = "Charlie", level = 2, score = 410 }//예비 데이터들임
        };
        dataList.Sort((a, b) => b.score.CompareTo(a.score));
        Refresh(dataList);
        */

        NetworkManager.apiManager.GetLeaderBoard();
    }

    private void RefreshLeaderBoard()
    {
        foreach (Transform entry in entryContainer)
        {
            if (entry != entryContainer) Destroy(entry.gameObject);
        }

        for (int i=0; i<scoreDataList.scores.Count; i++)
        {
            LeaderBoardScoreData score = scoreDataList.scores[i];

            ScoreEntry info = Instantiate(entryPrefab).GetComponent<ScoreEntry>();
            info.transform.SetParent(entryContainer);
            info.SetEntry(score,i+1);
        }

    }
    /*
    public void Refresh(List<EntryData> dataList)
    {
        foreach (Transform entry in entryTransformList)
        {
            Destroy(entry.gameObject);
        }
        entryTransformList.Clear();

        for (int i = 0; i < dataList.Count; i++)
        {
            EntryData data = dataList[i];

            Transform entry = Instantiate(entryTemplate, entryContainer);
            entry.gameObject.SetActive(true);

            entry.Find("Pos").GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            entry.Find("Name").GetComponent<TextMeshProUGUI>().text = data.name;
            entry.Find("Level").GetComponent<TextMeshProUGUI>().text = "Level " + data.level;
            entry.Find("Score").GetComponent<TextMeshProUGUI>().text = data.score.ToString();
            entry.Find("GoodButton").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                Debug.Log($"❤️ {data.name}");
            });

            entryTransformList.Add(entry);
        }
    }
    */

    public void SetList(ScoreDataList sdl)
    {
        this.scoreDataList = sdl;
        RefreshLeaderBoard();
    }

    public void OnBackButtonClicked(){
        SceneManager.LoadScene("Title");
    }
}
