using UnityEngine;

public class MagicState : BasePlayerState
{
    public MagicState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void Enter()
    {
        _playerController.Spell.Enable();
        _playerController.Magic();
        _animator.CrossFade("SpellCast", 0.1f);
    }

    public override void Exit()
    {
        _playerController.Spell.Disable();
    }
}