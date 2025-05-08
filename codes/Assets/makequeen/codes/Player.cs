using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat stat;
    public Vector2 inputVec;
    public float speed;
    public EnemyScanner enemyscanner;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        enemyscanner = GetComponent<EnemyScanner>();
        stat = GetComponent<PlayerStat>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed *Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void LateUpdate() 
    {
        if (inputVec.x != 0) {
            spriter.flipX = inputVec.x < 0;
        }
    }

    public void ReceiveDamage(float dmg)
    {
        stat.TakeDamage(dmg);
        if (stat.IsDead())
        {
            Debug.Log("플레이어 사망");
            // 사망 
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EXP"))
        {
            float exp = collision.GetComponent<EXPController>().GetExpValue();
            stat.GainExp(exp);
            Destroy(collision.gameObject);
        }
    }

    public void OnLevelUp()
    {
        Debug.Log("레벨업! 올릴 스탯을 선택하세요. (1: HP, 2: 공격력, 3: 방어력, 4: 행운, 5: 이동속도)");
        StartCoroutine(WaitForStatInput());
    }

    IEnumerator WaitForStatInput()
    {
        bool selected = false;

        while (!selected)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                stat.maxHP += 10;
                stat.currentHP = stat.maxHP;
                selected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                stat.attack += 10;
                selected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                stat.defense += 10;
                selected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                stat.luck += 10;
                selected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                stat.moveSpeed += 1;
                selected = true;
            }

            yield return null;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                float damage = enemy.GetAttackPower();
                stat.TakeDamage(damage);
            }
        }
    }*/


}
