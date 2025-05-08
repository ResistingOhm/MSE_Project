using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStat : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP = 100f;
    public float attack = 10f;
    public float defense = 5f;  // 방어력
    public float luck = 0f;     // 행운
    public float moveSpeed = 5f;

    public int level = 1;
    public float currentExp = 0f;
    public float maxExp = 10f;

    public void ResetHP()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(float dmg)
    {
        float finalDmg = Mathf.Max(dmg - defense, 1); // 최소 1 데미지는 받음
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
        maxExp *= 1.2f; // 점점 레벨업에 필요한 경험치 증가
        Debug.Log("레벨업! 현재 레벨: " + level);
        // 스탯강화ui필요...
        GameManager.instance.player.OnLevelUp(); 
    }


}

