using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly IPlayerController _playerController;
    protected readonly Animator _animator;

    protected static readonly int IdleHash = Animator.StringToHash("Idle");
    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected static readonly int BarrierChargeHash = Animator.StringToHash("BarrierCharge");
    protected static readonly int FirstAttackHash = Animator.StringToHash("ExplosionAttack");
    protected static readonly int SecondAttackHash = Animator.StringToHash("SecondAttack");


    protected const float CROSSFADE_DURATION = 0.4f;

    protected BaseState(IPlayerController player, Animator animator)
    {
        _playerController = player;
        _animator = animator;
    }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
        Debug.Log("Exit state");
    }
}
