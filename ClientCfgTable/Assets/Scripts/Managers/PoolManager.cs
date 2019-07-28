using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 总manager，暴漏给外边接口，内部包含按资源类型区分的TypeManager
/// </summary>
public class PoolManager : AbsManager<PoolManager>, SceneManager.ISceneManagerListener
{
    private bool isInEditorMode;

    public bool IsInEditorMode
    {
        get { return isInEditorMode; }
        set { isInEditorMode = value; }
    }

    Dictionary<string, TypedPool> m_typedPools = new Dictionary<string, TypedPool>();

    private TypedPool getTypedPool(string poolId)
    {
        TypedPool typedPool = null;
        m_typedPools.TryGetValue(poolId, out typedPool);

        if (typedPool == null)
        {
            typedPool = new TypedPool(this.gameObject, poolId);
            m_typedPools.Add(poolId, typedPool);
        }
        return typedPool;
    }

    /// <summary>
    /// 注意，通过这个方法Spawm之后，pool会自动更改他的名字
    /// 注意，如果需要reset，那么在逻辑层需要自己reset
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="assetType">用于指定这个prefab按哪种类型，在哪个路径下加载</param>
    /// <param name="poolId">用于指定这个prefab通过哪个pool生成, 如果使用poolId，那么Spawm和Store时的传入必须相同</param>
    /// <returns></returns>

    public GameObject Spawm(string prefabName, int assetType, string poolId = "unknown")
    {
        TypedPool objPool = getTypedPool(poolId);

        return objPool.Spawm<GameObject>(prefabName, assetType);
    }


    public void PreCache(int count, string prefabName, int assetType, string poolId = "unknown")
    {
        List<GameObject> objs = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Spawm(prefabName, assetType, poolId);
            if (obj)
                objs.Add(obj);
        }
        foreach (var obj in objs)
            Store(obj, poolId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="poolId">如果使用poolId，那么Spawm和Store时的传入必须相同, 推荐使用poolId,这样可以更好的区分各个池</param>
    public void Store(GameObject obj, string poolId = "unknown")
    {
        if (IsInEditorMode)
            return;
        TypedPool objPool = getTypedPool(poolId);

        objPool.Store(obj);
    }

    public void Clear()
    {
        foreach (var p in m_typedPools)
        {
            p.Value.ClearAll();
        }
    }

    public void OnSceneChanged(SceneManager manager, string oldSceneName, string currentSceneName)
    {
        Clear();
    }

    public void OnSceneWillChange(SceneManager manager, string currentSceneName, string newSceneName)
    {
        Clear();
    }
}


/// <summary>
/// 管理一类资源， 按string id区分，比如 "1" - Objects/Character ， "5" - Objects/UI/Common_NGUI
/// </summary>
public class TypedPool
{
    // 根节点，每个根节点下存放一类（poolTypeId区分) 的GameobjectPool， 如果不想管这个，可以在Spawm时不传poolId
    GameObject root;
    string poolTypeId; // 作为根节点的名字
    private Dictionary<string, GameobjectPool> m_objectDic = new Dictionary<string, GameobjectPool>();
    public TypedPool(GameObject fatherRoot, string poolId)
    {
        this.poolTypeId = poolId;
        root = new GameObject(poolTypeId);
        root.transform.parent = fatherRoot.transform;
    }

    private GameobjectPool getObjPool(string prefabName)
    {
        GameobjectPool objPool = null;
        m_objectDic.TryGetValue(prefabName, out objPool);

        if (objPool == null)
        {
            objPool = new GameobjectPool(root, prefabName);
            m_objectDic.Add(prefabName, objPool);
        }
        return objPool;
    }
    public T Spawm<T>(string prefabName, int assetType) where T : UnityEngine.Object
    {
        GameobjectPool objPool = getObjPool(prefabName);

        return objPool.Spawm<T>(prefabName, assetType) as T;
    }


    public void Store(GameObject obj)
    {
        GameobjectPool objPool = getObjPool(obj.name);

        objPool.Store(obj);
    }

    public void ClearAll()
    {
        foreach (var objPoolPair in m_objectDic)
        {
            objPoolPair.Value.ClearAll();
            GameObject.Destroy(objPoolPair.Value.Root);
        }
        m_objectDic.Clear();
    }
}

/// <summary>
/// 管理某个特定的prefab的pool， 按名字区分
/// </summary>
public class GameobjectPool
{
    GameObject root;

    public GameObject Root
    {
        get { return root; }
    }
    private Stack<GameObject> m_objectStack = new Stack<GameObject>();
    private Stack<Transform> m_objectParentStack = new Stack<Transform>();

    const int max_size = 24;

    public GameobjectPool(GameObject fatherRoot, string prefabName)
    {
        root = new GameObject(prefabName);
        root.transform.parent = fatherRoot.transform;
        root.name = prefabName;
    }


    public T Spawm<T>(string prefabName, int assetType) where T : Object
    {
        GameObject tmp = null;

        while (m_objectStack.Count > 0)
        {
            tmp = m_objectStack.Pop();
            var tmpParent = m_objectParentStack.Pop();
            if (tmp != null)
            {
                tmp.transform.parent = tmpParent;
                tmp.SetActive(true);
                return tmp as T;
            }
        }


        tmp = ResourceManager.Instance.InstantiateAsset<GameObject>(assetType, prefabName, true);

        AssertHelper.Check(tmp != null, "can't load: " + prefabName);
        if (tmp == null)
            return null;

        tmp.gameObject.name = prefabName;

        return tmp as T;
    }


    public void Store(GameObject obj)
    {
        if (m_objectStack.Count >= max_size)
        {
            Object.Destroy(obj);
        }
        else
        {
            obj.SetActive(false);
            m_objectStack.Push(obj);
            m_objectParentStack.Push(obj.transform.parent);
            obj.transform.parent = root.transform;
        }
    }

    // 似乎不需要，父节点删除就会引起它删除了
    public void ClearAll()
    {
        m_objectStack.Clear();
        m_objectParentStack.Clear();
        foreach (var obj in m_objectStack)
        {
            GameObject.Destroy(obj);
        }
    }

}