using System;
using System.Linq;
using UnityEngine;

public class MeleeComponent : EntityComponent
{
    [SerializeField] private Sword _sword;
    [SerializeField] private BoxCollider _attackZone;
    private event Action<AttackStartedEvent> _attackStarted;
    private event Action<AttackEndedEvent> _attackEnded;

    private void Awake()
    {
        _attackStarted = (e) =>
        {
            MeleeAttack();
            _sword.gameObject.SetActive(true);
        };
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
        Attack(_sword.Damage);
    }

    private void Attack(int damage)
    {
        Health health = null;
        Physics.OverlapBox(_attackZone.bounds.center, _attackZone.bounds.extents, Quaternion.identity)
                .FirstOrDefault(c => c.TryGetComponent(out health) && health.gameObject != gameObject);
        Debug.Log(health.gameObject);
        Debug.Log(gameObject);
        if (health.gameObject == gameObject) return;
        health?.TakeDamage(damage);
    }

    private void OnDrawGizmos() 
    {
        if (_attackZone == null) return;
        
        // Сохраняем текущий цвет Gizmos
        Color originalColor = Gizmos.color;
        
        // Устанавливаем цвет для нашей зоны атаки
        Gizmos.color = Color.red;
        
        // Рисуем проволочный куб в позиции зоны атаки
        Gizmos.DrawWireCube(
            _attackZone.bounds.center, 
            _attackZone.bounds.size
        );
        
        // Восстанавливаем исходный цвет
        Gizmos.color = originalColor;
    }
}
