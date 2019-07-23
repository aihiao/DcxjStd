using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using LywGames;

public class LoadedAssetBundle
{
    public AssetBundle assetBundle;
    public Object mainAssetObj;
    public int referencedCount;

    public LoadedAssetBundle(AssetBundle assetBundle, Object mainAssetObj = null)
    {
        this.assetBundle = assetBundle;
        this.mainAssetObj = mainAssetObj;
        referencedCount = 1;
    }
}

public class AssetBundlePathUtility
{
    public const string kAssetBundlesPath = "AssetBundles"; 
    
    public const string kManifestPath = "AssetBundleManifest";

    public static string GetCacheAssetpath(bool is4WWW = true, bool editor = false)
    {
#if UNITY_EDITOR
        if (editor)
        {
            return PathUtility.Combine(false, System.Environment.CurrentDirectory, kAssetBundlesPath, GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget), kManifestPath);
        }
        else
#endif
        {
            return FileManager.GetPersistentDataPath(kAssetBundlesPath, is4WWW);
        }
    }

#if UNITY_EDITOR
    public static string GetPlatformFolderForAssetBundles(BuildTarget target)
    {
        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "iOS";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                return "OSX";
            default:
                return null;
        }
    }
#endif

}

public class AssetBundleManager : AbsManager<AssetBundleManager>
{
    private static bool isInitialized = false;
    public static bool IsInitialized
    {
        get { return AssetBundleManager.isInitialized; }
        set { AssetBundleManager.isInitialized = value; }
    }

    private static AssetBundleManifest assetBundleManifest;
    public static AssetBundleManifest AssetBundleManifestObject
    {
        get { return assetBundleManifest; }
        set { assetBundleManifest = value; }
    }

    private static Dictionary<string, LoadedAssetBundle> loadedAssetBundleDic = new Dictionary<string, LoadedAssetBundle>();
    private static Dictionary<string, WWW> downloadingWWWDic = new Dictionary<string, WWW>();
    private static Dictionary<string, string> downloadingErrorDic = new Dictionary<string, string>();
    private static List<AssetBundleLoadOperation> inProgressOperationList = new List<AssetBundleLoadOperation>();
    private static Dictionary<string, string[]> dependencyDic = new Dictionary<string, string[]>();

    public IEnumerator InitializeAsync()
    {
        var request = InitializeAsync(AssetBundlePathUtility.kManifestPath);
        if (request != null)
        {
            yield return StartCoroutine(request);
        }

        isInitialized = true;
        yield return null;
    }

    private static AssetBundleLoadManifestOperation InitializeAsync(string manifestAssetBundleName)
    {
        LoadAssetBundle(manifestAssetBundleName, 0, true);

        var operation = new AssetBundleLoadManifestOperation(manifestAssetBundleName, manifestAssetBundleName, typeof(AssetBundleManifest));
        inProgressOperationList.Add(operation);
        return operation;
    }

    protected static string RemapVariantName(string assetBundleName)
    {
        return assetBundleName;
    }

    protected static bool LoadAssetBundleInternal(string assetBundleName, int dlcType, bool isLoadingAssetBundleManifest = false)
    {
        LoadedAssetBundle bundle = null;
        if (loadedAssetBundleDic.TryGetValue(assetBundleName, out bundle) && bundle != null)
        {
            bundle.referencedCount++;
            return true;
        }

        if (downloadingWWWDic.ContainsKey(assetBundleName))
        {
            return true;
        }

        if (isLoadingAssetBundleManifest)
        {
            if (!File.Exists(PathUtility.Combine(false, AssetBundlePathUtility.GetCacheAssetpath(false), assetBundleName)))
            {
                isInitialized = true;
                return false;
            }
        }

        string dlcUrl = PathUtility.Combine(false, AssetBundlePathUtility.GetCacheAssetpath(true), assetBundleName);
        WWW download = new WWW(dlcUrl);
        downloadingWWWDic.Add(assetBundleName, download);

        return false;
    }

    protected static void LoadDependencies(string assetBundleName)
    {
        if (assetBundleManifest == null)
        {
            LoggerManager.Instance.Error("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
            return;
        }

        string[] dependencies = assetBundleManifest.GetAllDependencies(assetBundleName);
        if (dependencies == null || dependencies.Length == 0)
        {
            return;
        }

        for (int i = 0; i < dependencies.Length; i++)
        {
            dependencies[i] = RemapVariantName(dependencies[i]);
        }

        dependencyDic.Add(assetBundleName, dependencies);
        foreach (var dependency in dependencies)
        {
            LoadAssetBundleInternal(dependency, 0, false);
        }
    }

    protected static void LoadAssetBundle(string assetBundleName, int dlcType, bool isLoadingAssetBundleManifest = false)
    {
        if (!isLoadingAssetBundleManifest)
        {
            assetBundleName = RemapVariantName(assetBundleName);
        }

        bool isAlreadyProcessed = LoadAssetBundleInternal(assetBundleName, dlcType, isLoadingAssetBundleManifest);
        if ((!isAlreadyProcessed) && (!isLoadingAssetBundleManifest))
        {
            LoadDependencies(assetBundleName);
        }
    }

    protected IEnumerator LoadLevelAsyncEnumeratored(string assetBundleName, string levelName, bool isAdditive)
    {
        Debug.Log("Start to load scene " + levelName + " at frame " + Time.frameCount);

        // Load level from assetBundle.
        AssetBundleLoadOperation request = LoadLevelAsync(assetBundleName, levelName, isAdditive);
        if (request == null)
            yield break;
        yield return StartCoroutine(request);

        // This log will only be output when loading level additively.
        Debug.Log("Finish loading scene " + levelName + " at frame " + Time.frameCount);
    }

    protected IEnumerator LoadAssetAsyncEnumeratored(string assetBundleName, string assetName, int dlc_type = 0)
    {
        Debug.Log("Start to load " + assetName + " at frame " + Time.frameCount);

        // Load asset from assetBundle.
        AssetBundleLoadAssetOperation request = LoadAssetAsync(assetBundleName, assetName, typeof(GameObject), dlc_type);
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
    }

    public static AssetBundleLoadAssetOperation LoadAssetAsync(string assetBundleName, string assetName, System.Type type, int dlcType)
    {
        LoadAssetBundle(assetBundleName, dlcType);
        AssetBundleLoadAssetOperation operation = new AssetBundleLoadAssetOperationFull(assetBundleName, assetName, type);

        inProgressOperationList.Add(operation);
        return operation;
    }

    public static AssetBundleLoadOperation LoadLevelAsync(string assetBundleName, string levelName, bool isAdditive, ILoadingProgress loadingProgress = null)
    {
        LoadAssetBundle(assetBundleName, 0);
        AssetBundleLoadOperation operation = new AssetBundleLoadLevelOperation(assetBundleName, levelName, isAdditive, ref loadingProgress);

        inProgressOperationList.Add(operation);
        return operation;
    }

    public static Object LoadAssetSync(string assetBundleName, string assetName = "")
    {
        if ((!assetBundleManifest) || (!Instance) || (!isInitialized))
        {
            return null;
        }

        LoadedAssetBundle bundle = null;
        if (loadedAssetBundleDic.TryGetValue(assetBundleName, out bundle) && (bundle != null))
        {
            bundle.referencedCount++;
            Object cacheObj = bundle.mainAssetObj;
            return cacheObj;
        }

        string[] dependencies = assetBundleManifest.GetAllDependencies(assetBundleName);
        for (int i = 0; i < dependencies.Length; i++)
        {
            dependencies[i] = RemapVariantName(dependencies[i]);
        }
        if (!dependencyDic.ContainsKey(assetBundleName))
        {
            dependencyDic.Add(assetBundleName, dependencies);
        }
        foreach (var dependency in dependencies)
        {
            LoadAssetSync(dependency);
        }

        string dlcFileUrl = PathUtility.Combine(false, AssetBundlePathUtility.GetCacheAssetpath(false), assetBundleName);
        if (!File.Exists(dlcFileUrl))
        {
            return null;
        }

        AssetBundle mainAb = AssetBundle.LoadFromFile(dlcFileUrl);
        if (mainAb == null)
        {
            return null;
        }

        Object[] loadedObjs = mainAb.LoadAllAssets();
        loadedAssetBundleDic.Add(assetBundleName, new LoadedAssetBundle(mainAb, loadedObjs.Length == 0 ? null : loadedObjs[0]));
        if (string.IsNullOrEmpty(assetName))
        {
            return loadedObjs.Length == 0 ? null : loadedObjs[0];
        }
        else
        {
            for (int i = 0; i < loadedObjs.Length; i++)
            {
                if (loadedObjs[i].name == assetName)
                {
                    return loadedObjs[i];
                }
            }

            return null;
        }
    }

    public static LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName, out string error)
    {
        if (downloadingErrorDic.TryGetValue(assetBundleName, out error))
        {
            return null;
        }

        LoadedAssetBundle bundle = null;
        if ((!loadedAssetBundleDic.TryGetValue(assetBundleName, out bundle)) && (bundle == null))
        {
            return null;
        }

        // 没有依赖
        string[] dependencies = null;
        if ((!dependencyDic.TryGetValue(assetBundleName, out dependencies)) && (dependencies == null))
        {
            return bundle;
        }
        // 确保所有依赖已经加载完毕后才能返回主assetbundle
        foreach (var dependency in dependencies)
        {
            LoadedAssetBundle dependentBundle = null;
            if ((!loadedAssetBundleDic.TryGetValue(dependency, out dependentBundle)) && (dependentBundle == null))
            {
                return null;
            }
        }

        return bundle;
    }

    private List<string> key2RemoveList = new List<string>();
    private void Update()
    {
        key2RemoveList.Clear();

        if (downloadingWWWDic.Count > 0)
        {
            foreach (var kv in downloadingWWWDic)
            {
                WWW download = kv.Value;
                if (download.error != null)
                {
                    if (!downloadingErrorDic.ContainsKey(kv.Key))
                    {
                        downloadingErrorDic.Add(kv.Key, download.error);
                    }
                    key2RemoveList.Add(kv.Key);
                    continue;
                }

                if (download.isDone)
                {
                    if (loadedAssetBundleDic.ContainsKey(kv.Key))
                    {
                        loadedAssetBundleDic.Remove(kv.Key);
                    }

                    loadedAssetBundleDic.Add(kv.Key, new LoadedAssetBundle(download.assetBundle));
                    key2RemoveList.Add(kv.Key);
                }
            }
        }

        if (key2RemoveList.Count > 0)
        {
            foreach (var key in key2RemoveList)
            {
                WWW download = downloadingWWWDic[key];
                downloadingWWWDic.Remove(key);
                download.Dispose();
            }
        }

        for (int i = 0; i < inProgressOperationList.Count;)
        {
            if (!inProgressOperationList[i].Update())
            {
                inProgressOperationList.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    public static void UnloadAll()
    {
        List<string> keyList = new List<string>();
        keyList.Clear();
        keyList.AddRange(loadedAssetBundleDic.Keys);

        foreach (var key in keyList)
        {
            UnloadAssetBundle(key);
        }
    }

    public static void UnloadAssetBundle(string assetBundleName)
    {
        UnloadAssetBundleInternal(assetBundleName);
        UnloadDependencies(assetBundleName);
    }

    protected static void UnloadAssetBundleInternal(string assetBundleName)
    {
        string error = string.Empty;
        LoadedAssetBundle bundle = GetLoadedAssetBundle(assetBundleName, out error);
        if (bundle == null)
        {
            return;
        }

        if (--bundle.referencedCount == 0)
        {
            bundle.assetBundle.Unload(false);
            loadedAssetBundleDic.Remove(assetBundleName);
        }
    }

    protected static void UnloadDependencies(string assetBundleName)
    {
        string[] dependencies = null;
        if (!dependencyDic.TryGetValue(assetBundleName, out dependencies))
        {
            return;
        }

        foreach (var dependency in dependencies)
        {
            UnloadAssetBundleInternal(dependency);
        }

        dependencyDic.Remove(assetBundleName);
    }

    public static bool IsDlcAssetbundleExist(string assetBundleName)
    {
        string dlcFileUrl = PathUtility.Combine(false, AssetBundlePathUtility.GetCacheAssetpath(false), assetBundleName);

        if (File.Exists(dlcFileUrl))
        {
            return true;
        }

        return false;
    }

}

public interface ILoadingProgress
{
    void SetLoadingProgress(float progress);
}

