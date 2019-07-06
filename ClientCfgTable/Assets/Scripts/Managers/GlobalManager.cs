using System;
using System.Collections.Generic;
using UnityEngine;
using LywGames;

public class GlobalManager : AutoCreateSingleton<GlobalManager>
{
    private GameObject rootGo;

    public void Initialize(GameObject go)
    {
        rootGo = go;
    }

    public static bool IsInstanceExist()
    {
        return Instance != null;
    }

    private List<BaseManager> managerList = new List<BaseManager>();
    private Dictionary<Type, BaseManager> type2ManagerDic = new Dictionary<Type, BaseManager>();

    public void Update()
    {
        foreach (var manager in managerList)
        {
            manager.OnUpdate();
        }
    }

#if UNITY_EDITOR
    public void OnGUI()
    {
        foreach (var manager in managerList)
        {
            manager.OnGUIUpdate();
        }
    }
#endif

    public BaseManager Add(Type type, params object[] parameters)
    {
        if (type.IsSubclassOf(typeof(BaseManager)))
        {
            if (type2ManagerDic.ContainsKey(type))
            {
                return type2ManagerDic[type];
            }

            BaseManager manager = rootGo.AddComponent(type) as BaseManager;
            if (manager != null)
            {
                managerList.Add(manager);
                type2ManagerDic.Add(type, manager);

                manager.SetRootObject(rootGo);
                manager.RunInitialize(parameters);

                return manager;
            }
        }

        return null;
    }

    public T Add<T>(params object[] parameters) where T : BaseManager
    {
        return Add(typeof(T), parameters) as T;
    }

    public T GetItem<T>() where T : BaseManager
    {
        Type t = typeof(T);
        // 没有包含T, 查找T的子类
        if (!type2ManagerDic.ContainsKey(t))
        {
            foreach (var manager in managerList)
            {
                if (manager.GetType().IsSubclassOf(t))
                {
                    return (T)(manager);
                }
            }

            return null;
        }

        return (T)(type2ManagerDic[t]);
    }

    public void DisposeAll(bool includePersistent)
    {
        foreach (var manager in managerList)
        {
            if (includePersistent)
            {
                manager.Dispose();
            }
            else
            {
                if (!manager.persistent)
                {
                    manager.Dispose();
                }
            }
        }
    }

    public bool Remove<T>() where T : BaseManager
    {
        Type t = typeof(T);
        // 没包含T, 包含了T的子类
        if (!type2ManagerDic.ContainsKey(t))
        {
            List<Type> removeList = new List<Type>();
            foreach (var kvp in type2ManagerDic)
            {
                if (kvp.Key.IsSubclassOf(t))
                {
                    removeList.Add(kvp.Key);
                }
            }

            foreach (var type in removeList)
            {
                DoRemove(type);
            }

            return removeList.Count != 0;
        }
        else
        {
            DoRemove(t);
            return true;
        }
    }

    public void RemoveAll(bool includePersistent)
    {
        if (includePersistent)
        {
            foreach (var type in type2ManagerDic.Keys)
            {
                DoRemove(type);
            }
        }
        else
        {
            List<Type> removeList = new List<Type>();
            foreach (var kvp in type2ManagerDic)
            {
                if (!kvp.Value.persistent)
                {
                    removeList.Add(kvp.Key);
                }
            }

            foreach (var type in removeList)
            {
                DoRemove(type);
            }
        }
    }

    private void DoRemove(Type type)
    {
        BaseManager manager = type2ManagerDic[type];

        manager.Dispose();
        managerList.Remove(manager);
        type2ManagerDic.Remove(type);

        Component component = rootGo.GetComponent(type);
        if (component != null)
        {
            GameObject.Destroy(component);
        }
    }

}