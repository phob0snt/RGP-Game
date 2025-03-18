using System.Threading.Tasks;
using UnityEngine;

public class MeleeComponent : EntityComponent
{
    public async Task Attack()
    {
        Debug.Log("Melee attack");
        await Task.Delay(800);
        Debug.Log("Melee attack done");
    }
}
