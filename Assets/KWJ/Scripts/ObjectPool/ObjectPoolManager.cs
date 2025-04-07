using UnityEngine;
using System.Collections.Generic;
using static ObjectPoolManager;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager pm;

    [SerializeField]
    private ObjectPool fanGirlNormal;
    [SerializeField]
    private ObjectPool fanBoyNormal;
    [SerializeField]
    private ObjectPool councilNormal;
    [SerializeField]
    private ObjectPool fanGirlHorde;
    [SerializeField]
    private ObjectPool fanBoyHorde;
    [SerializeField]
    private ObjectPool councilHorde;
    [SerializeField]
    private ObjectPool fanGirlWall;
    [SerializeField]
    private ObjectPool fanBoyWall;
    [SerializeField]
    private ObjectPool councilWall;


    public Dictionary<string, ObjectPool> poolDictionary;

    private void Awake()
    {
        if (pm==null) pm = GetComponent<ObjectPoolManager>();
        poolDictionary = new Dictionary<string, ObjectPool>
        {
            { "FGN", fanGirlNormal },
            { "FBN", fanBoyNormal },
            { "CCN", councilNormal },
            { "FGH", fanGirlHorde },
            { "FBH", fanBoyHorde },
            { "CCH", councilHorde },
            { "FGW", fanGirlWall },
            { "FBW", fanBoyWall },
            { "CCW", councilWall },
        };
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].SpawnObject();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        return objectToSpawn;
    }

    public GameObject[] SpawnsFromPool(string tag, Vector3 position, Quaternion rotation, int num)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject[] objectToSpawn = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            objectToSpawn[i] = poolDictionary[tag].SpawnObject();
            objectToSpawn[i].SetActive(true);
            objectToSpawn[i].transform.position = position;
            objectToSpawn[i].transform.rotation = rotation;
        }

        return objectToSpawn;
    }


}
