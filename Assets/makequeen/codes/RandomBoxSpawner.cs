using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBoxSpawner : MonoBehaviour
{
    public GameObject randomBoxPrefab;

    private float spawnInterval;

    void Start()
    {
        if (LevelManager.LvManager.stageLv.name.Contains("Level1"))
        {
            spawnInterval = 20f;
        }
        else if (LevelManager.LvManager.stageLv.name.Contains("Level2"))
        {
            spawnInterval = 40f;
        }
        else
        {
            spawnInterval = 30f;
        }

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector3 basePos = LevelManager.LvManager.GetPlayerPos();
            Vector3 offset = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);
            Vector3 spawnPos = basePos + offset;

            Instantiate(randomBoxPrefab, spawnPos, Quaternion.identity);
        }
    }
}
