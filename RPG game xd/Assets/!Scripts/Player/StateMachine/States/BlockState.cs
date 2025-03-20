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
        EventManager.Broadcast(Events.BlockStartedEvent);
        _animator.CrossFade("BlockingLoop", 0.1f);
    }

    public override void Exit()
    {
        EventManager.Broadcast(Events.BlockEndedEvent);
    }
}
