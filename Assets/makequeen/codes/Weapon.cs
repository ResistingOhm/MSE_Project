using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    float timer;
    Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        Init();
    }
    void Update()
    {
        switch (id)
        {
            default:
                timer += Time.deltaTime;

                if (timer > speed) {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    public void Init()
    {
        switch (id) {
            default:
                speed = 1.8f;
                break;
        }
    }

    void Fire()
    {
        if (!player.enemyscanner.nearestTarget)
            return;

        Vector3 targetPos = player.enemyscanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        float totalDamage = damage + player.stat.attack;
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
