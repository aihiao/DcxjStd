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

    private static Dictionary<string, LoadedAssetBundle> loadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();
    private static Dictionary<string, WWW> downloadingWWWs = new Dictionary<string, WWW>();
    private static Dictionary<string, string> downloadingErrors = new Dictionary<string, string>();
    private static List<AssetBundleLoadOperation> inProgressOperations = new List<AssetBundleLoadOperation>();
    private static Dictionary<string, string[]> dependencies = new Dictionary<string, string[]>();

    public static Object LoadAssetSync(string assetBundleName, string assetName = "")
    {
        return null;
    }

    public static LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName, out string error)
    {
        error = string.Empty;
        return null;
    }

    public static void UnloadAssetBundle(string assetBundleName)
    {

    }

}

public interface ILoadingProgress
{
    void SetLoadingProgress(float progress);
}

