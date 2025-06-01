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

    public bool hasBerserkBoost = false;
    public bool hasSprintSurge = false;

    float berserkTimer = 0f;
    float sprintTimer = 0f;

    bool isBerserkActive = false;
    bool isSprintActive = false;

    float berserkDuration = 3f;
    float sprintDuration = 3f;

    float berserkCooldown = 10f;
    float sprintCooldown = 10f;

    float berserkBuffAmount = 10f;
    float sprintBuffAmount = 2f;

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

        HandleBerserkBoost();
        HandleSprintSurge();
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
        if (collision.CompareTag("Exp"))
        {
            float exp = collision.GetComponent<EXPController>().GetExpValue();
            stat.GainExp(exp);
            //Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                float damage = enemy.GetAttackPower();
                ReceiveDamage(damage);
            }
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

    void HandleBerserkBoost() //일정쿨마다 공격력 세짐
    {
        if (!hasBerserkBoost) return;

        berserkTimer += Time.deltaTime;

        if (!isBerserkActive && berserkTimer >= berserkCooldown)
        {
            isBerserkActive = true;
            berserkTimer = 0f;
            stat.attack += berserkBuffAmount;
            StartCoroutine(ResetBerserk());
        }
    }

    IEnumerator ResetBerserk()
    {
        yield return new WaitForSeconds(berserkDuration);
        stat.attack -= berserkBuffAmount;
        isBerserkActive = false;
    }

    void HandleSprintSurge() //일정 쿨마다 속도 빨라짐
    {
        if (!hasSprintSurge) return;

        sprintTimer += Time.deltaTime;

        if (!isSprintActive && sprintTimer >= sprintCooldown)
        {
            isSprintActive = true;
            sprintTimer = 0f;
            speed += sprintBuffAmount;
            StartCoroutine(ResetSprint());
        }
    }

    IEnumerator ResetSprint() 
    {
        yield return new WaitForSeconds(sprintDuration);
        speed -= sprintBuffAmount;
        isSprintActive = false;
    }

    public bool hasRoseThorn = false;
    public void LearnRoseThorn()
    {
        hasRoseThorn = true;
    }

    public bool hasSpinBlade = false;
    public void LearnSpinBlade()
    {
        hasSpinBlade = true;
    }


}

