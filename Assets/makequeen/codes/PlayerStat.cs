using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStat : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP = 100f;
    public float attack = 10f;
    public float defense = 5f; 
    public float luck = 0f;    
    public float moveSpeed = 5f;

    public int level = 1;
    public float currentExp = 0f;
    public float maxExp = 10f;

    public PlayerStatusUI statusUI;

    void Start()
    {
        statusUI = GetComponent<PlayerStatusUI>();
    }

    public void ResetHP()
    {
        currentHP = maxHP;
        if (statusUI != null)
            statusUI.UpdateHPbar();
    }

    public void TakeDamage(float dmg)
    {
        float finalDmg = Mathf.Max(dmg - defense, 1); // 최소 1은 데미지를 받게 함
        currentHP -= finalDmg;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public void GainExp(float exp)
    {
        currentExp += exp;
        while (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        maxExp *= 1.2f;

        float hpIncrease = 20f;
        maxHP += hpIncrease;
        currentHP = maxHP;

        if (statusUI != null)
        {
            statusUI.UpdateHPbar();
            statusUI.UpdateLevel();
        }

        Debug.Log("level up ! : " + level);

        LevelManager.LvManager.player.OnLevelUp(); 
    }

    public void Heal(float amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }
}
