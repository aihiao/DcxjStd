using UnityEngine;

public abstract class BaseManager : MonoBehaviour
{
    protected GameObject rootGo;

    public void SetRootObject(GameObject go)
    {
        rootGo = go;
    }

    // 是否是常驻
    public bool persistent { get; set; }

    protected bool initialized;

    public void RunInitialize(params object[] parameters)
    {
        if (!initialized)
        {
            Initialize(parameters);
            initialized = true;
        }
    }

    public virtual void Initialize(params object[] parameters) { }

    public virtual void OnUpdate() { }

#if UNITY_EDITOR
    public virtual void OnGUIUpdate() { }
#endif

    public virtual void Dispose() { }
}

public abstract class AbsManager<T> : BaseManager where T : BaseManager
{
    public static T Instance
    {
        get { return GlobalManager.Instance.GetItem<T>(); }
    }
}
