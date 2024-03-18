using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy", order = 0)]
public class Enemy : ScriptableObject
{
    public int Id;
    public int Score;
    public int MaxHealth;
    public int Damage;
    public int Speed;
    public int StoppingDistance;
    public float AttackCooldown;
}