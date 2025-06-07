using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStatusUI statusUI;
    public GameoverManager gameoverManager;
    public GameObject levelUpPopup;

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
        statusUI = GetComponent<PlayerStatusUI>();
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
        statusUI?.UpdateHPbar();

        if (stat.IsDead())
        {
            Debug.Log("Game over");
            if (gameoverManager != null)
            {
                gameoverManager.ShowGameOver();
            }
            else
            {
                Debug.LogError("❌ gameoverManager is not connect");
            }
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
        Debug.Log("(level up ! choose one. (1: HP, 2: attack, 3: defense, 4: luck, 5: moveSpeed)");
        if (statusUI != null)
        {
            statusUI.UpdateLevel();
            statusUI.UpdateHPbar();
            statusUI.UpdateExpbar();
        }

        if (levelUpPopup != null)
            levelUpPopup.SetActive(true);

        Time.timeScale = 0f;

        StartCoroutine(WaitForStatInput());
    }
    public void IncreaseStat(int index)
    {
        switch (index)
        {
            case 1:
                stat.maxHP += 10;
                stat.currentHP = stat.maxHP;
                break;
            case 2:
                stat.attack += 10;
                break;
            case 3:
                stat.defense += 10;
                break;
            case 4:
                stat.luck += 10;
                break;
            case 5:
                stat.moveSpeed += 1;
                break;
        }

        statusUI?.UpdateLevel();
        statusUI?.UpdateHPbar();
        statusUI?.UpdateExpbar();

        if (levelUpPopup != null)
            levelUpPopup.SetActive(false);

        Time.timeScale = 1f;
    }
    IEnumerator WaitForStatInput()
    {
        bool selected = false;

        while (!selected)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                IncreaseStat(1);
                selected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                IncreaseStat(2);
                selected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                IncreaseStat(3);
                selected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                IncreaseStat(4);
                selected = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                IncreaseStat(5);
                selected = true;
            }

            yield return null;
        }
    }


    void HandleBerserkBoost() //�����𸶴� ���ݷ� ����
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

    void HandleSprintSurge() //���� �𸶴� �ӵ� ������
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


/*
 Boss.cs�� �߰��ؾ���!...
 public enum BossRewardType
{
    None,
    RoseThorn,
    BerserkBoost,
    SprintSurge
}

public BossRewardType rewardType = BossRewardType.None;

override protected void enemyDeadEvent()
{
    gameObject.SetActive(false);

    if (GameManager.instance != null && GameManager.instance.player != null)
    {
        switch (rewardType)
        {
            case BossRewardType.RoseThorn:
                GameManager.instance.player.LearnRoseThorn();
                break;
            case BossRewardType.BerserkBoost:
                GameManager.instance.player.LearnBerserkBoost();
                break;
            case BossRewardType.SprintSurge:
                GameManager.instance.player.LearnSprintSurge();
                break;
        }
    }
 
 */