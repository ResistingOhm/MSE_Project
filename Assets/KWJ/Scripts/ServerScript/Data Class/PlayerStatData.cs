using System;
using UnityEngine;

[Serializable]
public class PlayerStatData
{
    public float hp;
    public float atk;
    public float def;
    public float lck;
    public float spd;
    public int lvl;

    public PlayerStatData(PlayerStat player)
    {
        this.hp = player.maxHP;
        this.atk = player.attack;
        this.def = player.defense;
        this.lck = player.luck;
        this.spd = player.moveSpeed;
        this.lvl = player.level;
    }
}
