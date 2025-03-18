using UnityEngine;

public class CastState : BasePlayerState
{
    public CastState(PlayerController player, Animator animator) : base(player, animator) { }
    
    public override void Enter()
    {
        _animator.SetTrigger("Cast");
    }
}