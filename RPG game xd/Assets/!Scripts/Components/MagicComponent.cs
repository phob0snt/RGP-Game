using System;
using UnityEngine;

public class MagicComponent : EntityComponent
{
    [SerializeField] private MagicSpell _spell;
    [SerializeField] private Transform _castRoot;

    private event Action<MagicStartedEvent> _magicStarted;
    private event Action<MagicEndedEvent> _magicEnded;

    private void Awake()
    {
        _magicStarted = (e) => _spell.StartCast();
        _magicEnded = (e) => _spell.StopCast();
    }
    
    private void OnEnable()
    {
        EventManager.AddListener(_magicStarted);
        EventManager.AddListener(_magicEnded);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(_magicStarted);
        EventManager.RemoveListener(_magicEnded);
    }
    public void CastSpell()
    {
        _spell.Cast(_castRoot);
    }
}