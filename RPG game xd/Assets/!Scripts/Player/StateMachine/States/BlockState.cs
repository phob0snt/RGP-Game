using UnityEngine;

public class BlockState : BasePlayerState
{
    public BlockState(PlayerController player, Animator animator) : base(player, animator) { }
    
    public override void Update()
    {
        _playerController.Move(0.1f);
    }
    public override void Enter()
    {
        _playerController.Shield.Enable();
        _playerController.Shield.Block();
         _animator.CrossFade("BlockingLoop", 0.1f);
    }

    public override void Exit()
    {
        _playerController.Shield.Disable();
    }
}
