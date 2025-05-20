using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    FOLLOW=0,
    HORDE=1,
    WALL_W=2,
    WALL_L=3,
}

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected GameObject expPrefab;
    protected bool isAlive = true;
    private float deadTime = 0f;
    [SerializeField]
    private float currentHp = 0f;
    [SerializeField]
    private bool isPoison = false;
    private float poisonTime = 0f;
    private float poisonDamageTime = 0.5f;
    private float currentPoisonTime = 0f;

    protected Vector3 dest; //destination for Horde, Wall
    protected MoveType movetype = MoveType.FOLLOW;
    protected Rigidbody2D rb;
    protected Collider2D c;

    public void setDest(Vector3 v)
    {
        dest = (v - transform.position).normalized;
    }

    public void setMoveType(MoveType mt)
    {
        movetype = mt;
    }

    public float GetAttackPower()
    {
        return enemyData.Attack * LevelManager.LvManager.stageLv.Attack;
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PoisonDamage(3f);
        }

        if (isAlive)
        {
            if (currentHp < 0f)
            {
                isAlive = false;
                c.enabled = false;
                rb.velocity = Vector2.zero;
                int j = enemyData.Exp * LevelManager.LvManager.stageLv.EXP;
                for (int i = 0; i < j; i++)
                {
                    float x = Random.Range(-0.5f, 0.5f);
                    float y = Random.Range(-0.5f, 0.5f);
                    ObjectPoolManager.pm.SpawnFromPool("EXP", transform.position + new Vector3(x, y, 0), Quaternion.identity);
                }
                //Add Score Event here ex) LevelManager.LvManager.ScoreUp(int);
                LevelManager.LvManager.AddScore(j*10);
            }

            if (isPoison)
            {
                currentPoisonTime += Time.deltaTime;
                if (currentPoisonTime > poisonDamageTime)
                {
                    currentHp -= 0.5f;
                    poisonTime -= poisonDamageTime;
                    currentPoisonTime = 0f;
                }
                if (currentPoisonTime > poisonTime)
                {
                    isPoison = false;
                }
            }

            Vector3 offset = transform.position - LevelManager.LvManager.GetPlayerPos();
            float d = offset.magnitude;
            if (d > 50f) { gameObject.SetActive(false); }

            return;
        }

        deadTime += Time.deltaTime;

        if (deadTime > 2f)
        {
            enemyDeadEvent();
        }
    }

    virtual protected void enemyDeadEvent()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

        switch (movetype)
        {
            case MoveType.FOLLOW:
                setDest(LevelManager.LvManager.GetPlayerPos());
                break;
            case MoveType.HORDE:
                break;
            case MoveType.WALL_L:
                break;
            case MoveType.WALL_W:
                break;
            default:
                break;
        }

        //transform.Translate(dest * enemyData.Speed * LevelManager.LvManager.stageLv.Speed * Time.deltaTime);
        rb.velocity = dest * enemyData.Speed * LevelManager.LvManager.stageLv.Speed;
    }
    void OnEnable()
    {
        isAlive = true;
        c.enabled = true;
        currentHp = enemyData.Hp * LevelManager.LvManager.stageLv.Hp;
    }

    private void OnDisable()
    {
        isAlive = false;
        rb.velocity = Vector2.zero;
        currentHp = 0;
    }

    public void PoisonDamage(float duration)
    {
        currentPoisonTime = 0f;
        poisonTime = duration;
        isPoison = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            currentHp -= collision.gameObject.GetComponent<Bullet>().damage;
        }
    }
}
