using System;
using System.Buffers;
using UnityEngine;

[Serializable]
public class LeaderBoardScoreData
{
    public long id;
    public long score;
    public int enemynum;
    public int gamelevel;
    public PlayerStatData playerstat;
    public int min;
    public int sec;
    public int likenum;
    public DataOwner owner;

}

[Serializable]
public class DataOwner
{
    public string uuid;
    public string name;
}
