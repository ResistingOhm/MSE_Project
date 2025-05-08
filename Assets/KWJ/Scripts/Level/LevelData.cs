using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Scriptable Object/Level Data", order = int.MaxValue)]
public class LevelData : ScriptableObject
{
    [SerializeField] private float attack;
    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private bool isCouncilSpawn;
    [SerializeField] private int exp;
    public float Attack { get { return attack; } }
    public float Hp { get { return hp; } }
    public float Speed { get { return speed; } }
    public bool IsCouncilSpawn { get { return isCouncilSpawn; } }
    public int EXP { get { return exp; } }

}
