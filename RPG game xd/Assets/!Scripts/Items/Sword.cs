using UnityEngine;

public class Sword : BaseItem, IWeapon
{
    public void Attack()
    {
        Debug.Log("Sword Attack");
    }
}