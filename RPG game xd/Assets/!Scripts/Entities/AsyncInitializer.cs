using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class AsyncInitializer : IInitializable
{
    private readonly IEnumerable<IAsyncInitializable> _systems;

    public AsyncInitializer(IEnumerable<IAsyncInitializable> systems)
    {
        _systems = systems;
    }

    public void Initialize()
    {
        _ = RunAsyncInitialization();
    }

    private async Task RunAsyncInitialization()
    {
        try
        {
            await Task.WhenAll(_systems.Select(x => x.InitializeAsync()));
            Debug.Log("All async systems initialized");
            EventManager.Broadcast(Events.SystemsInitializedEvent);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Async initialization failed: {ex.Message}");
        }
    }

}