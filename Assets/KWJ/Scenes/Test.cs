using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // This Script is only for testing RandomJokeAPI. Nothing happen if we delete this file. (Only Some test scenes go wrong)
    void Start()
    {
        NetworkManager.apiManager.GetRandomJoke(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
