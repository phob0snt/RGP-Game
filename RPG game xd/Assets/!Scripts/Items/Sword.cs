using UnityEngine;

public class Sword : BaseItem, IWeapon
{
    [field: SerializeField] public int Damage { get; private set; } = 10;
    public void Attack()
    {
        Debug.Log("Sword Attack");
    }
}