using System.Threading.Tasks;
using UnityEngine;

public class EnemyFactory : IEnemyFactory, IAsyncInitializable
{
    private readonly IAssetLoader _loader;
    private Enemy _meleeEnemy;
    private EnemyConfig _config;

    public EnemyFactory(IAssetLoader loader)
    {
        _loader = loader;
    }

    public async Task InitializeAsync()
    {
        var prefab = await _loader.LoadAssetAsync<GameObject>("Enemy");
        _config = await _loader.LoadAssetAsync<EnemyConfig>("EnemyConfig");
        _meleeEnemy = prefab.GetComponent<Enemy>();
    }
    public Enemy CreateEnemy()
    {
        Enemy enemy = Object.Instantiate(_meleeEnemy);
        enemy.Initialize(_config);
        return enemy;
    }
}
