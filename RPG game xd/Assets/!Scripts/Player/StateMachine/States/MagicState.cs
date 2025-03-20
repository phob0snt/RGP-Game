using UnityEngine;

public class MagicState : BasePlayerState
{
    public MagicState(PlayerController player, Animator animator) : base(player, animator) { }

    public override void Enter()
    {
        EventManager.Broadcast(Events.MagicStartedEvent);
        _animator.CrossFade("SpellCast", 0.1f);
    }
}