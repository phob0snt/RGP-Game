using UnityEngine;

public class MagicSpell : BaseItem, IWeapon
{
    public void Attack()
    {
        Debug.Log("Magic Attack");
    }
}