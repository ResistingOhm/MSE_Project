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
    [SerializeField] private EnemyData enemyData;
    private bool isAlive = true;

    private Vector3 dest; //destination for Horde, Wall
    private MoveType movetype = MoveType.FOLLOW;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        transform.Translate(dest * enemyData.Speed * LevelManager.LvManager.stageLv.Speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
//        if (collision.collider.tag == "Bullet")
//        {
//            Destroy(gameObject, 1);
//            isAlive = false;
//        }
    }
}
