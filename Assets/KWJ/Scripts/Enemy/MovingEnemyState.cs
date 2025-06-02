
using UnityEngine;

public class MovingEnemyState : IEnemyState
{
    private Enemy enemy;

    public MovingEnemyState(Enemy e)
    {
        this.enemy = e;
    }

    public void Enter()
    {
        enemy.SetMoveAnimation();
    }

    public void Update()
    {
        if (enemy.getCurrentHp() < 0)
        {
            enemy.SetState(enemy.deadState);
            return;
        }

        if (!enemy.IsMoving())
        {
            enemy.SetState(enemy.idleState);
            return;
        }

        bool dir = enemy.IsHeadingDirectionPositive();
        if (dir) { enemy.FlipSprite(false); }
        if (!dir) { enemy.FlipSprite(true); }
    }

    public void FixedUpdate()
    {
        switch (enemy.GetMoveType())
        {
            case MoveType.FOLLOW:
                enemy.setDest(LevelManager.LvManager.GetPlayerPos());
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

        enemy.SetVelocityWithDirection();
    }

    public void Exit()
    {

    }
}
