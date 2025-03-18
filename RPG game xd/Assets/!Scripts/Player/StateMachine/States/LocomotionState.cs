using UnityEngine;

public class LocomotionState : BasePlayerState
{
    public LocomotionState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void Update()
    {
        _playerController.Move();
    }

    public override void Enter()
    {
        _animator.CrossFade("Sprint", 0.1f);
    }
}
