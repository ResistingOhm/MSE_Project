using UnityEngine;
using System.Collections.Generic;
using static ObjectPoolManager;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager pm;

    [SerializeField]
    private ObjectPool fanGirl;
    [SerializeField]
    private ObjectPool fanBoy;
    [SerializeField]
    private ObjectPool council;
    [SerializeField]
    private ObjectPool boss;


    public Dictionary<string, ObjectPool> poolDictionary;

    private void Awake()
    {
        if (pm==null) pm = GetComponent<ObjectPoolManager>();
        poolDictionary = new Dictionary<string, ObjectPool>
        {
            { "FGN", fanGirl },
            { "FBN", fanBoy },
            { "CCN", council },
            { "BSS", boss },
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
