using UnityEngine;

public class IdleState : BasePlayerState
{
    public IdleState(PlayerController player, Animator animator) : base(player, animator) { }
    
    public override void Enter()
    {
        _animator.CrossFade("Idle 0", 0.1f);
    }
}
