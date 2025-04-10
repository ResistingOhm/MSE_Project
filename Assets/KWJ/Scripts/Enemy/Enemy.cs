using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 playerPos = EnemySpawnManager.esm.GetPlayerPos();
            Vector3 dir = (playerPos - transform.position).normalized;
            transform.Translate(dir * enemyData.Speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
//        if (collision.collider.tag == "Bullet")
//        {
//            Destroy(gameObject, 1);
//            isAlive = false;
//        }
    }
}
