using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager esm;
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (esm == null) esm = GetComponent<EnemySpawnManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ObjectPoolManager.pm.SpawnFromPool("FGN", new Vector3(10, 10, 0), Quaternion.identity);
            ObjectPoolManager.pm.SpawnFromPool("FBN", new Vector3(-10, 10, 0), Quaternion.identity);
            ObjectPoolManager.pm.SpawnFromPool("CCN", new Vector3(0, 15, 0), Quaternion.identity);
        }
    }

    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }
}
