using UnityEngine;

public class CrouchState : BasePlayerState
{
    //private readonly int _startCrouchHash = Animator.StringToHash("StartCrouch"); 
    public CrouchState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void Enter()
    {
        _playerController.EnableCrouch();
        Debug.Log("EnterCrouch");
    }

    public override void Update()
    {
        _playerController.Move();
    }

    public override void Exit()
    {
        _playerController.DisableCrouch();
        Debug.Log("ExitCrouch");
    }
}
