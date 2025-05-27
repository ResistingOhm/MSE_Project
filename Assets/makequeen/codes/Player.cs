using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStatusUI statusUI;
    public GameoverManager gameoverManager;
    public int score; // 추가용

    public PlayerStat stat;
    public Vector2 inputVec;
    public float speed;
    
    public EnemyScanner enemyscanner;

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

        if (stat.IsDead()){
            Debug.Log("Game over");
            if (gameoverManager != null){
                gameoverManager.ShowGameOver(score);
            }
            else{
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
        if (statusUI != null){
            statusUI.UpdateLevel();
            statusUI.UpdateHPbar();
            statusUI.UpdateExpbar();
        }
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

}
