using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Transform planeTr;         
    [SerializeField] private GameObject tilePrefab;     
    [SerializeField] private Sprite[] mapSp;            

    [SerializeField] private int posX = 15, posY = 10;  

    public void CreateRandomMap()
    {
        if (planeTr.childCount > 0) return;

        for (int x = -posX; x < posX; x++)
        {
            for (int y = -posY; y < posY; y++)
            {
                GameObject tile = Instantiate(tilePrefab);
                tile.transform.localPosition = new Vector3(x, y, 0f);
                tile.transform.SetParent(planeTr);

                int rand = Random.Range(0, 10);
                if (rand < 1)
                {
                    tile.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mapSp.[Random.Range(0,mapSp.Length)];
                }
            }
        }
    }
}
