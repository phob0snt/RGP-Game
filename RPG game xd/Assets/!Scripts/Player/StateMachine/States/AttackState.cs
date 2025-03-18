using UnityEngine;

public class AttackState : BasePlayerState
{
    public AttackState(PlayerController player, Animator animator) : base(player, animator) { }
    
    public override void Enter()
    {
        _playerController.Weapon.Enable();
        _playerController.Attack();
        _animator.CrossFade("MeleeAttack_OneHanded", 0.1f);
    }

    public override void Exit()
    {
        _playerController.Weapon.Disable();
    }
}
