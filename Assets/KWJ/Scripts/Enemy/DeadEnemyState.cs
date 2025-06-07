using UnityEngine;

public class DeadEnemyState : IEnemyState
{
    private Enemy enemy;
    private float deadTime = 0f;

    public DeadEnemyState(Enemy e)
    {
        this.enemy = e;
    }

    public void Enter()
    {
        enemy.PlayDeadAudio();
        //Debug.Log("DeadAudioPlay");
        enemy.SetDeadAnimation();
        enemy.EnemyHpGone();
    }

    public void Update()
    {
        deadTime += Time.deltaTime;

        if (deadTime > 1f)
        {
            enemy.enemyDeadEvent();
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {

    }
}
