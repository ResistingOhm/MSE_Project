using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStatusUI statusUI;
    public GameoverManager gameoverManager;
    public GameObject levelUpPopup;
    public int score; // 추가용

    public PlayerStat stat;
    public Vector2 inputVec;
    public float speed;
    
    public EnemyScanner enemyscanner;

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

    Rigidbody2D rigid;
    SpriteRenderer spriter;

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
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
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
                gameoverManager.ShowGameOver(score);
            }
            else
            {
                Debug.LogError("❌ gameoverManager 연결 불가");
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
        if (levelUpPopup != null)
                levelUpPopup.SetActive(false);
    }

    void HandleBerserkBoost()
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

    void HandleSprintSurge()
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
