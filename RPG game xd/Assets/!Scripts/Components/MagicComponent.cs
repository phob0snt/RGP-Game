using System.Threading.Tasks;
using UnityEngine;

public class MagicComponent : EntityComponent
{
    public async Task CastSpell()
    {
        Debug.Log("Magic spell");
        await Task.Delay(1000);
        Debug.Log("Magic spell done");
    }
}