using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject spawnPointsParent;
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = spawnPointsParent.GetComponentsInChildren<Transform>().Where(t=>t != spawnPointsParent.transform).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            SpawnEnemies(MoveType.WALL_W);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnEnemies(MoveType.WALL_L);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnEnemies(MoveType.HORDE);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            SpawnBoss();
        }
        
    }

    public void SpawnEnemies(MoveType mt, int spawnNum = -1)
    {
        if (mt == MoveType.WALL_L)
        {
            int ri = Random.Range(0, 2);
            float pos_x = spawnPoints[ri].position.x;

            for (int i = -60; i < 65; i+=3)
            {
                Enemy spawnedEnemies = chooseEnemy(new Vector3(pos_x, i, 0));

                spawnedEnemies.setMoveType(mt);
                spawnedEnemies.setDest(new Vector3(LevelManager.LvManager.GetPlayerPos().x, i, 0));
            }

            return;
        }

        if (mt == MoveType.WALL_W)
        {
            int ri = Random.Range(0, 2);
            float pos_y = spawnPoints[ri].position.y;

            for (int i = -45; i < 40; i += 3)
            {
                Enemy spawnedEnemies = chooseEnemy(new Vector3(i, pos_y, 0));

                spawnedEnemies.setMoveType(mt);
                spawnedEnemies.setDest(new Vector3(i, LevelManager.LvManager.GetPlayerPos().y,0));
            }

            return;
        }

        int res = GetSpawnPointRanodm();

        int sn = spawnNum;
        if(spawnNum == -1) sn = Random.Range(1, 4);

        //Debug.Log(res);

        for(int i = 0; i < sn; i++)
        {
            Enemy spawnedEnemies = chooseEnemy(spawnPoints[res].transform.position);

            spawnedEnemies.setMoveType(mt);
            spawnedEnemies.setDest(LevelManager.LvManager.GetPlayerPos());
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
        int res = GetSpawnPointRanodm();
        Boss boss = ObjectPoolManager.pm.SpawnFromPool("BSS", spawnPoints[res].transform.position, Quaternion.identity).GetComponent<Boss>();
        boss.setMoveType(MoveType.FOLLOW);
        boss.setDest(LevelManager.LvManager.GetPlayerPos());
        if(LevelManager.LvManager.BossSpawnNum()==3) boss.SetFInalBoss();
    }

    private int GetSpawnPointRanodm()
    {
        int res;
        int randomPos = Random.Range(0, spawnPoints.Length);
        if (spawnPoints[randomPos].GetComponent<SpawnPoints>().GetAble())
        {
            res = randomPos;
        } else
        {
            res = randomPos + 1;
            if (res > spawnPoints.Length - 1) res -= spawnPoints.Length;
        }
        return res;
    }
}
