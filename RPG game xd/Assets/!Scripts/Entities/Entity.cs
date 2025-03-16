using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Entity<T> : MonoBehaviour, IEntity<T> where T : EntityConfig
{
    protected List<IEntityComponent> _components = new();

    public virtual void Initialize(T config)
    {
        _components = GetComponentsInChildren<IEntityComponent>().ToList();
    }
}
