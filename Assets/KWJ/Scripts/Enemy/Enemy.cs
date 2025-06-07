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
    [SerializeField]
    private float currentHp = 0f;
    [SerializeField]
    private bool isPoison = false;
    private float poisonTime = 0f;
    private float poisonDamageTime = 0.5f;
    private float currentPoisonTime = 0f;

    protected Vector3 dest; //destination for Horde, Wall
    protected MoveType movetype = MoveType.FOLLOW;
    protected float chargeSpeed = 1f;

    protected Rigidbody2D rb;
    protected Collider2D c;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audio;

    [SerializeField]
    private AudioClip deadAudio;

    protected IEnemyState currentState;

    public IEnemyState idleState;
    public IEnemyState moveState;
    public IEnemyState deadState;
    public void setDest(Vector3 v)
    {
        dest = (v - transform.position).normalized;
    }

    public void setMoveType(MoveType mt)
    {
        movetype = mt;
    }

    public MoveType GetMoveType()
    {
        return this.movetype;
    }

    public float getCurrentHp()
    {
        return currentHp;
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
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        animator.SetBool("isMoving", false);
        animator.SetBool("isDead", false);

        idleState = new IdleEnemyState(this);
        moveState = new MovingEnemyState(this);
        deadState = new DeadEnemyState(this);

        currentState = idleState;

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null) { currentState.Update(); }

        if (currentState != deadState)
        {
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
            if (d > 200f) { gameObject.SetActive(false); }
        }
    }

    public void EnemyHpGone()
    {
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
        LevelManager.LvManager.AddScore(j * 10);
    }

    virtual public void enemyDeadEvent()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState != null) { currentState.FixedUpdate(); }
    }
    void OnEnable()
    {
        currentState = idleState;
        c.enabled = true;
        currentHp = enemyData.Hp * LevelManager.LvManager.stageLv.Hp;
    }

    private void OnDisable()
    {
        currentState = null;
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

    public void SetIdleAnimation()
    {
        animator.SetBool("isMoving", false);
    }
    public void SetMoveAnimation()
    {
        animator.SetBool("isMoving", true);
    }

    public void SetDeadAnimation()
    {
        animator.SetBool("isDead", true);
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0;
    }

    public bool IsHeadingDirectionPositive()
    {
        return rb.velocity.x > 0;
    }

    public void FlipSprite(bool b)
    {
        spriteRenderer.flipX = b;
    }

    public void SetState(IEnemyState state)
    {
        currentState.Exit();
        this.currentState = state;
        state.Enter();
    }

    public void SetVelocityWithDirection()
    {
        rb.velocity = dest * enemyData.Speed * LevelManager.LvManager.stageLv.Speed * chargeSpeed;
    }

    public void PlayDeadAudio()
    {
        audio.PlayOneShot(deadAudio);
    }
}
