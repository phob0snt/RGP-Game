using UnityEngine;

public class JumpState : BasePlayerState
{
    public JumpState(PlayerController player, Animator animator) : base(player, animator) { }
    
    public override void Update()
    {
        _playerController.Move();
    }
    public override void Enter()
    {
        _animator.CrossFade("Jump", 0.1f);
    }
}
