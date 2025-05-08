using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStat : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP = 100f;
    public float attack = 10f;
    public float defense = 5f;  // ����
    public float luck = 0f;     // ���
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
        float finalDmg = Mathf.Max(dmg - defense, 1); // �ּ� 1 �������� ����
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
        maxExp *= 1.2f; // ���� �������� �ʿ��� ����ġ ����
        Debug.Log("������! ���� ����: " + level);
        // ���Ȱ�ȭui�ʿ�...
        GameManager.instance.player.OnLevelUp(); 
    }


}

