using System.Threading.Tasks;
using UnityEngine;

public class MagicComponent : EntityComponent
{
    public async Task CastSpell(MagicSpell _spell, Transform caster)
    {
        Debug.Log("Magic spell");
        await Task.Delay(1000);
        _spell.Cast(caster);
        Debug.Log("Magic spell done");
    }
}