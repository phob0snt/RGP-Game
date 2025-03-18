using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyHandler : IEnemyHandler, IInitializable
{
    private readonly List<IEnemy> _enemies = new();
    private IEnemyFactory _factory;

    public EnemyHandler(IEnemyFactory factory)
    {
        _factory = factory;
    }

    public async void Initialize()
    {
        await Task.Delay(5000);
        CreateEnemy();
    }

    public async void CreateEnemy()
    {
        IEnemy enemy = _factory.CreateEnemy();
        _enemies.Add(enemy);
        Vector3 randPos = new Vector3(Random.Range(-30, 30), 20, Random.Range(-30, 30)) + Player.Transform.position;
        NavMeshHit hit;
        while (!NavMesh.SamplePosition(randPos, out hit, 10, NavMesh.AllAreas))
        {
            Debug.Log("UNLUCK");
            randPos = new Vector3(Random.Range(-30, 30), 0, Random.Range(-30, 30)) + Player.Transform.position;
        }
        enemy.SetPosition(hit.position);
        await Task.Delay(1000);
        enemy.SetTarget(Player.Transform);
    }
}
