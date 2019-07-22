using System;
using System.Collections.Generic;
using UnityEngine;
using ClientCommon;
using LywGames;

public class ResourceManager : AbsManager<ResourceManager>
{
    private Dictionary<string, UnityEngine.Object> cacheDic = new Dictionary<string, UnityEngine.Object>();

    private const string AssetBundleIdPrefix = "assets/resources/";

    public static string GetLevelBundleNameByLevelName(string levelName)
    {
        return "assets/workassets/levels/game/" + levelName.ToLower() + "/" + levelName.ToLower() + ".assetbundle";
    }

    public void Clear()
    {
        cacheDic.Clear();
    }

    public void Unload(bool unloadAllLoadedobjects = false)
    {
        Clear();
    }

    protected UnityEngine.Object GetInstantiate(UnityEngine.Object ob)
    {
        if (ob != null)
        {
            return UnityEngine.Object.Instantiate(ob);
        }
        else
        {
            return null;
        }
    }

    protected UnityEngine.Object GetObjectByAssetBundle(int assetType, string assetId, bool isAutoUnload = true)
    {
        string assetTypePath = AssetPathUtility.GetTypePath(assetType);

        assetTypePath = AssetBundleIdPrefix + assetTypePath;

        string assetBundleName = PathUtility.Combine(true, assetTypePath, assetId);
        assetBundleName = assetBundleName + ".assetbundle";

        UnityEngine.Object obj = AssetBundleManager.LoadAssetSync(assetBundleName, assetId);

        AssetBundleManager.UnloadAssetBundle(assetBundleName);

        return obj;
    }

    public UnityEngine.Object LoadAsset(int assetType, string assetId, bool isCache = false, bool detiledLog = false)
    {
        if (detiledLog)
        {
            LoggerManager.Instance.Info("log reload " + assetType + ", " + assetId);
        }

        assetId = assetId.Trim();
        var assetPath = AssetPathUtility.GetAssetPath(assetType, assetId);

        if (detiledLog)
        {
            LoggerManager.Instance.Info("log reload: " + assetPath);
        }

        UnityEngine.Object ob = null;

        bool matchDic = false;
        if (cacheDic.ContainsKey(assetPath))
        {
            ob = cacheDic[assetPath];

            if (detiledLog)
            {
                LoggerManager.Instance.Info("log reload:  ob is null ? " + ((ob == null) ? "true" : "false"));
            }
            if (ob != null)
            {
                matchDic = true;
                return ob;
            }
            else
            {
                cacheDic.Remove(assetPath);
            }
        }

        if (!matchDic)
        {
            if (assetType != AssetType.Shader)
                ob = GetObjectByAssetBundle(assetType, assetId);

            if (ob == null)
            {
                ob = Resources.Load(assetPath);


                if (detiledLog)
                {
                    LoggerManager.Instance.Info(" after reload:  ob is null ? " + ((ob == null || ob.IsNull()) ? "true" : "false"));
                }
            }

            if (ob != null)
            {
                if (isCache)
                {
                    cacheDic[assetPath] = ob;
                }
            }
        }

        return ob;
    }

    public T LoadAsset<T>(int assetType, string filePath, bool isCache) where T : UnityEngine.Object
    {
        return LoadAsset(assetType, filePath, isCache) as T;
    }

    /// <summary>
	/// 根据类型和类型下的相对路径获取
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="assetType">从ClientCommon.AssetType获取</param>
	/// <param name="filePath">先对路径参见AssetPath数据表</param>
	/// <returns></returns>
	public T LoadAsset<T>(int assetType, string filePath) where T : UnityEngine.Object
    {
        return LoadAsset<T>(assetType, filePath, false);
    }

    public T InstantiateAsset<T>(int assetType, string filePath, bool cache = false) where T : UnityEngine.Object
    {
        return GetInstantiate(LoadAsset<T>(assetType, filePath, cache)) as T;
    }
}
