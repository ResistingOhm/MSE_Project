using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{
    private float chargeCoolDown = 5f;
    private float currentTime = 0f;

    private bool isFinalBoss = false;
    void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentState != null) { currentState.FixedUpdate(); }

        if (currentTime > chargeCoolDown && currentTime < chargeCoolDown + 1f)
        {
            movetype = MoveType.HORDE;
            chargeSpeed = 0f;
        }

        if (currentTime > chargeCoolDown + 1f && currentTime < chargeCoolDown + 2f)
        {
            chargeSpeed = 5f;
        }

        if (currentTime > chargeCoolDown + 2f)
        {
            chargeSpeed = 1f;
            currentTime = 0f;
            movetype = MoveType.FOLLOW;
        }
    }
    override public void enemyDeadEvent()
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
