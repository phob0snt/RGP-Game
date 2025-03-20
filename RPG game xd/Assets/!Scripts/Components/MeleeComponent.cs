using System;
using System.Threading.Tasks;
using UnityEngine;

public class MeleeComponent : EntityComponent
{
    [SerializeField] private Sword _sword;
    private event Action<AttackStartedEvent> _attackStarted;
    private event Action<AttackEndedEvent> _attackEnded;

    private void Awake()
    {
        _attackStarted = (e) => _sword.gameObject.SetActive(true);
        _attackEnded = (e) => _sword.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        EventManager.AddListener(_attackStarted);
        EventManager.AddListener(_attackEnded);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(_attackStarted);
        EventManager.RemoveListener(_attackEnded);
    }

    public void MeleeAttack()
    {
        _sword.Attack();
    }
}
