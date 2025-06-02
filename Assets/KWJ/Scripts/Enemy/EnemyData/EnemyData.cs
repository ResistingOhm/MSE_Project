using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float attack;
    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private int exp;
    public float Attack { get { return attack; } }
    public float Hp { get { return hp; } }
    public float Speed { get { return speed; } }
    public int Exp { get { return exp; } }

}
