using UnityEngine;

public class RunState : BasePlayerState
{
    public RunState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void Update()
    {
        _playerController.Move();
    }

    public override void Enter()
    {
        
    }
}
