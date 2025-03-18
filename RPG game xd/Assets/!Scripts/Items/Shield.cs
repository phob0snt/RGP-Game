using UnityEngine;

public class Shield : BaseItem, IShield
{
    public void Block()
    {
        Debug.Log("Block");
    }
}