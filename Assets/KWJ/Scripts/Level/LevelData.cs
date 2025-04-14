using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Scriptable Object/Level Data", order = int.MaxValue)]
public class LevelData : ScriptableObject
{
    [SerializeField] private float attck;
    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private bool isCouncilSpawn;
    public float Attck { get { return attck; } }
    public float Hp { get { return hp; } }
    public float Speed { get { return speed; } }
    public bool IsCouncilSpawn { get { return isCouncilSpawn; } }
    
}
