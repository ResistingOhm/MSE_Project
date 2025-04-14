using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager esm;

    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject spawnPointsParent;

    // Start is called before the first frame update
    void Start()
    {
        if (esm == null) esm = GetComponent<EnemySpawnManager>();

        spawnPoints = spawnPointsParent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnEnemies(MoveType.WALL_W);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnEnemies(MoveType.WALL_L);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnEnemies(MoveType.HORDE);
        }
    }

    public void SpawnEnemies(MoveType mt)
    {
        if (mt == MoveType.WALL_L)
        {
            int ri = Random.Range(2, 4);
            float pos_x = spawnPoints[ri].position.x;

            for (int i = -12; i < 13; i+=2)
            {
                Enemy spawnedEnemies = chooseEnemy(new Vector3(pos_x, i, 0));

                spawnedEnemies.setMoveType(mt);
                spawnedEnemies.setDest(new Vector3(GetPlayerPos().x, i, 0));
            }

            return;
        }

        if (mt == MoveType.WALL_W)
        {
            int ri = Random.Range(2, 4);
            float pos_y = spawnPoints[ri].position.y;

            for (int i = -12; i < 13; i += 2)
            {
                Enemy spawnedEnemies = chooseEnemy(new Vector3(i, pos_y, 0));

                spawnedEnemies.setMoveType(mt);
                spawnedEnemies.setDest(new Vector3(i, GetPlayerPos().y,0));
            }

            return;
        }

        int randomPos = Random.Range(0, 5);
        int randomNum = Random.Range(1, 4);

        Debug.Log(randomPos);

        for(int i = 0; i < randomNum; i++)
        {
            Enemy spawnedEnemies = chooseEnemy(spawnPoints[i].transform.position);

            spawnedEnemies.setMoveType(mt);
            spawnedEnemies.setDest(GetPlayerPos());
        }
    }

    private Enemy chooseEnemy(Vector3 v)
    {
        
        int temp = LevelManager.LvManager.stageLv.IsCouncilSpawn ? 3 : 2;
        int randomint = Random.Range(0, temp);
        string tag = "FGN";
        if (randomint == 0) tag = "FGN";
        if (randomint == 1) tag = "FBN";
        if (randomint == 2) tag = "CCN";
        Enemy enemy = ObjectPoolManager.pm.SpawnFromPool(tag, v, Quaternion.identity).GetComponent<Enemy>();
        return enemy;
    }

    public void SpawnBoss()
    {

    }

    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }
}
