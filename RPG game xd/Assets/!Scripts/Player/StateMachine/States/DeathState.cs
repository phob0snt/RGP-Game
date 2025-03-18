using UnityEngine;

public class DeathState : BasePlayerState
{
    public DeathState(PlayerController player, Animator animator) : base(player, animator) { }
    
    public override void Enter()
    {
        _animator.SetTrigger("Death");
    }
}
