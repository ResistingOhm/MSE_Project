using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EXPController : MonoBehaviour
{
    private bool isDetected = false;

    private float value = 1f;
    private float speed = 5f;
    private Vector3 playerPos;

    public float GetExpValue()
    {
        return value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (isDetected)
        {
            playerPos = EnemySpawnManager.esm.GetPlayerPos();

            Vector3 dir = (playerPos - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
            speed += 60.0f * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void Detected()
    {
        isDetected = true;
    }

}
