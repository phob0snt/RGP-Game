using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "EntityConfigs/EnemyConfig")]
public class EnemyConfig : EntityConfig
{
    [field: SerializeField] public int HP {get; private set;}
    [field: SerializeField] public int SwordDamage {get; private set;}
}
