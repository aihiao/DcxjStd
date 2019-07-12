using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    protected bool init;

    public bool RunInitialize()
    {
        if (!init)
        {
            Initialize();
            init = !init;
        }
        return init;
    }

    public virtual void Initialize() { }

    public virtual void Dispose() { }

    public virtual void Destroy()
    {
        init = false;
    }

}