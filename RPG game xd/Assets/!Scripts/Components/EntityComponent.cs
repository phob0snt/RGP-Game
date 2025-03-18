using UnityEngine;

public class EntityComponent : MonoBehaviour, IEntityComponent
{
    public virtual void Initialize()
    {

    }
    
    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}
