using UnityEngine;

public class EntityComponent : MonoBehaviour, IEntityComponent
{
    public void Initialize()
    {

    }
    
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
