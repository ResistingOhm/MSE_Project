using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    FOLLOW,
    HORDE,
    WALL_W,
    WALL_L,
}

public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    protected bool isAlive = true;
    private float deadTime = 0f;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            isAlive = false;
        }
    }
}
