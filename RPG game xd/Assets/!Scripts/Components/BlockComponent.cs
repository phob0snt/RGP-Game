using System;
using UnityEngine;

public class BlockComponent : EntityComponent
{
    [SerializeField] private Shield _shield;
    private event Action<BlockStartedEvent> _blockStarted;
    private event Action<BlockEndedEvent> _blockEnded;

    private void Awake()
    {
        _blockStarted = (e) => _shield.gameObject.SetActive(true);
        _blockEnded = (e) => _shield.gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        EventManager.AddListener(_blockStarted);
        EventManager.AddListener(_blockEnded);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(_blockStarted);
        EventManager.RemoveListener(_blockEnded);
    }

    public void ShieldBlock()
    {
        Debug.Log("Block");
    }
}