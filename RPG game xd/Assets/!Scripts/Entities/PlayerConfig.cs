using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "EntityConfigs/PlayerConfig")]
public class PlayerConfig : EntityConfig
{
    [field: SerializeField] public int HP {get; private set;}
    [field: SerializeField] public int SwordDamage {get; private set;}
    [field: SerializeField] public int MagicDamage {get; private set;}
}
