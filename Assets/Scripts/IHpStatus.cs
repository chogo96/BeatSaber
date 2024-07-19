using UnityEngine;

public interface IHpStatus 
{
    float PlayerHp  { get; set; }
    float PlayerHpMax { get; }
    float PlayerHpMin { get; }
    float EnemyHp { get; set; }
    float EnemyHpMax { get; }
    float EnemyHpMin { get; }
}
