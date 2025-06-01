
public class IdleEnemyState : IEnemyState
{
    private Enemy enemy;

    public IdleEnemyState(Enemy e)
    {
        this.enemy = e;
    }

    public void Enter()
    {
        enemy.SetIdleAnimation();
    }

    public void Update()
    {
        if (enemy.getCurrentHp() < 0)
        {
            enemy.SetState(enemy.deadState);
            return;
        }

        if (enemy.IsMoving())
        {
            enemy.SetState(enemy.moveState);
            return;
        }
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
