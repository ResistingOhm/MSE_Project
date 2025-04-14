using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnEnemies(MoveType.FOLLOW);
        }
    }

    public void SpawnEnemies(MoveType mt)
    {
        int randomPos = Random.Range(0, 5);
        int randomNum = Random.Range(1, 4);

        Debug.Log(randomPos);

        for(int i = 0; i < randomNum; i++)
        {
            Enemy spawnedEnemies;
            int temp = LevelManager.LvManager.stageLv.IsCouncilSpawn ? 3 : 2;
            int randomint = Random.Range(0, temp);
            spawnedEnemies = ObjectPoolManager.pm.SpawnFromPool("FGN", spawnPoints[randomPos].position, Quaternion.identity).GetComponent<Enemy>();
            if (randomint == 1) spawnedEnemies = ObjectPoolManager.pm.SpawnFromPool("FBN", spawnPoints[randomPos].position, Quaternion.identity).GetComponent<Enemy>();
            if (randomint == 2) spawnedEnemies = ObjectPoolManager.pm.SpawnFromPool("CCN", spawnPoints[randomPos].position, Quaternion.identity).GetComponent<Enemy>();

            spawnedEnemies.setMoveType(mt);
            spawnedEnemies.setDest(GetPlayerPos());
        }
    }

    public void SpawnBoss()
    {

    }

    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }
}
