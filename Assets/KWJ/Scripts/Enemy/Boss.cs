using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{
    private float chargeCoolDown = 5f;
    private float currentTime = 0f;
    private float chargeSpeed = 1f;

    private bool isFinalBoss = false;
    void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if (!isAlive)
        {
            return;
        }

        if (currentTime > chargeCoolDown && currentTime < chargeCoolDown + 2f)
        {
            movetype = MoveType.HORDE;
            chargeSpeed = 0f;
        }

        if (currentTime > chargeCoolDown + 2f && currentTime < chargeCoolDown + 3f)
        {
            chargeSpeed = 3f;
        }

        if (currentTime > chargeCoolDown + 3f)
        {
            chargeSpeed = 1f;
            currentTime = 0f;
            movetype = MoveType.FOLLOW;
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
        rb.velocity = dest * enemyData.Speed * LevelManager.LvManager.stageLv.Speed * chargeSpeed;
    }
    override protected void enemyDeadEvent()
    {
        gameObject.SetActive(false);
        if (isFinalBoss)
        {
            //Here for game clear code
            LevelManager.LvManager.onGameEnd();

        }
    }

    public void SetFInalBoss() { this.isFinalBoss = true; }
}
