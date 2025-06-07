using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float roseThornTimer = 0f;
    public float roseThornInterval = 5f;

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
            case 1: // Rose Thorn µ¶µ©
                if (!player.hasRoseThorn)
                    return;

                roseThornTimer += Time.deltaTime;
                if (roseThornTimer >= roseThornInterval)
                {
                    roseThornTimer = 0f;
                    CastRoseThorn();
                }
                break;

            case 2:
                if (!player.hasSpinBlade) 
                    return;

                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

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
            case 2: //È¸Àü¹«±â
                speed = 150;
                Batch();
                break;

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

        Transform bullet = ObjectPoolManager.pm.SpawnFromPool("BLT", transform.position, Quaternion.identity).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        float totalDamage = damage + player.stat.attack;
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }

    void CastRoseThorn()
    {
        GameObject aoeObj = ObjectPoolManager.pm.SpawnFromPool("RTH", transform.position, Quaternion.identity);
        if (aoeObj == null) return;

        aoeObj.transform.position = transform.position;
        RoseThorn aoe = aoeObj.GetComponent<RoseThorn>();
        if (aoe != null)
        {
            aoe.damage = damage + player.stat.attack;
        }
    }

    void Batch() {
        for (int index = 0; index < count; index++) {
            GameObject bulletObj = ObjectPoolManager.pm.SpawnFromPool("BLT", transform.position, Quaternion.identity);
            if (bulletObj == null) continue;

            Transform bullet = bulletObj.transform;
            bullet.parent = transform;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);

        }
    }
}
