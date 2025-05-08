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
    private float currentHp = 0f;

    protected Vector3 dest; //destination for Horde, Wall
    protected MoveType movetype = MoveType.FOLLOW;
    protected Rigidbody2D rb;

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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (currentHp < 0f)
            {
                isAlive = false;
                Collider2D c = GetComponent<Collider2D>();
                c.enabled = false;
                rb.velocity = Vector2.zero;
                int j = enemyData.Exp * LevelManager.LvManager.stageLv.EXP;
                for (int i = 0; i < j; i++)
                {
                    float x = Random.Range(-0.5f, 0.5f);
                    float y = Random.Range(-0.5f, 0.5f);
                    ObjectPoolManager.pm.SpawnFromPool("EXP", transform.position + new Vector3(x, y, 0), Quaternion.identity);
                }
            }
            return;
        }

        deadTime += Time.deltaTime;

        if (deadTime > 2f)
        {
            gameObject.SetActive(false);
        }
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
                setDest(EnemySpawnManager.esm.GetPlayerPos());
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
        currentHp = enemyData.Hp * LevelManager.LvManager.stageLv.Hp;
    }

    private void OnDisable()
    {
        isAlive = false;
        currentHp = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            currentHp -= 1f;
        }
    }
}
