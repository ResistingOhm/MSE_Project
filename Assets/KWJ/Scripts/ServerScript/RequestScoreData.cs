using System;
using UnityEngine;

[Serializable]
public class RequestScoreData
{
    public long id;
    public long score;
    public int enemynum;
    public int gamelevel;
    public PlayerStatData playerstat;
    public int min;
    public int sec;
}
