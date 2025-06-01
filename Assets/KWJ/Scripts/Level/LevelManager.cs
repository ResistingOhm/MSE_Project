using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LvManager;

    [SerializeField]
    private EnemySpawnManager esm;

    public Player player;

    public LevelData stageLv;

    public LevelData levelOne;
    public LevelData levelTwo;

    public float EnemySpawnTime = 3f;
    public float BossSpawnTime = 300f;
    public TextMeshProUGUI timerText;

    private bool isGamePlaying = false;
    private float currentTimer = 0f;
    private float enemySpawnTimer = 0f;
    private float bossSpawnTimer = 0f;

    private int bossSpawnNum = 0;

    private long score = 0;
    private int enemynum = 0;
    private int gamelevel = 1;

    private int min = 0;
    private int sec = 0;

    void Awake()
    {
        if (LvManager != null && LvManager != this)
        {
            Destroy(this);
        }
        else
        {
            LvManager = GetComponent<LevelManager>();
        }
    }
    void Start()
    {
        if (UserDataManager.udm.SelectedLevel()) { stageLv = levelOne; gamelevel = 1; SoundManager.soundManager.SetLevelOneBGM(); }
        else { stageLv = levelTwo; gamelevel = 2; SoundManager.soundManager.SetLevelTwoBGM(); }
        onStartGame();
    }
    // Update is called once per frame
    void Update()
    {
        if (isGamePlaying)
        {
            currentTimer += Time.deltaTime;
            enemySpawnTimer += Time.deltaTime;
            bossSpawnTimer += Time.deltaTime;

            min = (int)Mathf.Floor(currentTimer / 60);
            sec = (int)Mathf.Floor(currentTimer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);

            if (enemySpawnTimer > EnemySpawnTime)
            {
                enemySpawnTimer = 0f;
                int i = Random.Range(0, 4);
                esm.SpawnEnemies((MoveType)i);
            }
            if (bossSpawnNum < 3 && bossSpawnTimer > BossSpawnTime)
            {
                bossSpawnTimer = 0f;
                ++bossSpawnNum;
                esm.SpawnBoss();
            }
            if (currentTimer > 90000)
            {
                isGamePlaying = false;
                timerText.text = string.Format("{0:00}", 0);
            }
        }

    }

    public void onStartGame()
    {
        currentTimer = 0;
        isGamePlaying = true;
    }

    public void onGameEnd()
    {
        isGamePlaying = false;
        //Show UI
        SoundManager.soundManager.SetTitleBGM();
        if (score > UserDataManager.udm.GetHighscore())
        {
            NetworkManager.apiManager.UpdateScore(UserDataManager.udm.GetScoreId(),score, enemynum, gamelevel, new PlayerStatData(player.stat), min, sec);
        }
    }
    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }

    public int BossSpawnNum()
    {
        return bossSpawnNum;
    }

    public void AddScore(int i)
    {
        score += i;
    }

}
