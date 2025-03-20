using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class AttackState : BasePlayerState
{
    public AttackState(PlayerController player, Animator animator) : base(player, animator)
    {
    }
    
    public override void Enter()
    {
        EventManager.Broadcast(Events.AttackStartedEvent);
        _animator.CrossFade("MeleeAttack_OneHanded", 0.1f);
    }
}
