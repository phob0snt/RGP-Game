using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyHandler : IEnemyHandler, IAsyncInitializable
{
    private readonly IAssetLoader _loader;
    private readonly List<Enemy> _enemies = new();
    public EnemyHandler(IAssetLoader loader)
    {
        _loader = loader;
    }

    public async Task InitializeAsync()
    {
        var enemy = await _loader.LoadAssetAsync<GameObject>("Enemy");
        _enemies.Add(enemy.GetComponent<Enemy>());
    }
}