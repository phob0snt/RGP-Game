using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{ 
    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }
}