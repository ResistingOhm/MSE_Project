using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float attck;
    [SerializeField] private float hp;
    [SerializeField] private float speed;
    public float Attck { get { return attck; } }
    public float Hp { get { return hp; } }
    public float Speed { get { return speed; } }
    
}
