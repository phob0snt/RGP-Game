using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity<EnemyConfig>, IEnemy
{
    [SerializeField] private NavMeshAgent _agent;
    private Transform _target;
    private Health _health;

    public override void Initialize(EnemyConfig config)
    {
        base.Initialize(config);
        _health = _components.OfType<Health>().FirstOrDefault();
        _health.Initialize(config.HP);
    }
    
    public async void SetTarget(Transform target)
    {
        while (!_agent.isOnNavMesh)
        {
            Debug.Log("))))");
            await Task.Delay(100);
        }
        _target = target;
        //_agent.SetDestination(target.position);
    }

    private void Update()
    {
        if (_target != null)
            _agent.SetDestination(_target.position);
    }

    public void SetPosition(Vector3 position)
    {
        _agent.Warp(position);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

}
