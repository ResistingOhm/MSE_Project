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
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        int randomint = Random.Range(0, 5);
        Debug.Log(randomint);

        ObjectPoolManager.pm.SpawnFromPool("FGN", spawnPoints[randomint].position, Quaternion.identity);
        ObjectPoolManager.pm.SpawnFromPool("FBN", spawnPoints[randomint].position, Quaternion.identity);
        ObjectPoolManager.pm.SpawnFromPool("CCN", spawnPoints[randomint].position, Quaternion.identity);
    }

    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }
}
