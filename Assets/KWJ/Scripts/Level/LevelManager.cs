using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager LvManager;

    public LevelData stageLv;

    public float EnemySpawnTime = 5f;
    public float BossSpawnTime = 300f;
    public TextMeshProUGUI timerText;

    private bool isGamePlaying = false;
    private float currentTimer = 0f;
    private float enemySpawnTimer = 0f;
    private float bossSpawnTimer = 0f;

    private int bossSpawnNum = 0;


    void Awake()
    {
        if (LvManager == null) LvManager = GetComponent<LevelManager>();
    }
    void Start()
    {
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

            int min = (int)Mathf.Floor(currentTimer / 60);
            int sec = (int)Mathf.Floor(currentTimer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", min, sec);

            if (enemySpawnTimer > EnemySpawnTime)
            {
                enemySpawnTimer = 0f;
                int i = Random.Range(0, 4);
                EnemySpawnManager.esm.SpawnEnemies((MoveType)i);
            }
            if (bossSpawnTimer > BossSpawnTime && bossSpawnNum < 3)
            {
                bossSpawnTimer = 0f;
                EnemySpawnManager.esm.SpawnBoss();
                bossSpawnNum++;
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

}
