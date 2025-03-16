using UnityEngine;

public class Health : EntityComponent
{
    private int _value;

    public void Initialize(int value)
    {
        _value = value; 
    }
    
    public void TakeDamage(int damage)
    {
        _value -= damage;
        if (_value <= 0)
        {
            Die();
            _value = 0;
        }
    }

    private void Die()
    {
        Debug.Log("I'm dead!");
    }
}
