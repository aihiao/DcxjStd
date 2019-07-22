using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class AssetBundleLoadOperation : IEnumerator
{
    public object Current { get { return null; } }

    public bool MoveNext()
    {
        return !IsDone();
    }

    public void Reset() { }

    public abstract bool Update();

    public abstract bool IsDone();

}

public class AssetBundleLoadLevelSimulationOperation : AssetBundleLoadOperation
{
    public AssetBundleLoadLevelSimulationOperation()
    {

    }

    public override bool IsDone()
    {
        return false;
    }

    public override bool Update()
    {
        return false;
    }
}

public class AssetBundleLoadLevelOperation : AssetBundleLoadOperation
{
    protected string assetBundleName;
    protected string levelName;
    protected bool isAdditive;
    protected string downloadingError;
    protected AsyncOperation request;
    ILoadingProgress loadingProgress;

    public AssetBundleLoadLevelOperation(string assetBundleName, string levelName, bool isAdditive, ref ILoadingProgress loadingProgress)
    {
        this.assetBundleName = assetBundleName;
        this.levelName = levelName;
        this.isAdditive = isAdditive;
        this.loadingProgress = loadingProgress;
    }

    public override bool IsDone()
    {
        if (request != null)
        {
            return false;
        }

        LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out downloadingError);
        if (bundle != null)
        {
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool Update()
    {
        if (request == null && downloadingError != null)
        {
            LoggerManager.Instance.Error(downloadingError);
            return true;
        }

        if (request != null && loadingProgress != null)
        {
            loadingProgress.SetLoadingProgress(request.progress);
        }

        return request != null && request.isDone;
    }
}

public abstract class AssetBundleLoadAssetOperation : AssetBundleLoadOperation
{
    public abstract T GetAsset<T>() where T : Object;
}

public class AssetBundleLoadAssetOperationSimulation : AssetBundleLoadAssetOperation
{
    Object simulatedObject;

    public AssetBundleLoadAssetOperationSimulation(Object simulatedObject)
    {
        this.simulatedObject = simulatedObject;
    }

    public override T GetAsset<T>()
    {
        return simulatedObject as T;
    }

    public override bool Update()
    {
        return false;
    }

    public override bool IsDone()
    {
        return true;
    }
}

public class AssetBundleLoadAssetOperationFull : AssetBundleLoadAssetOperation
{
    protected string assetBundleName;
    protected string assetName;
    protected string downloadingError;
    protected System.Type type;
    protected AssetBundleRequest request;

    public AssetBundleLoadAssetOperationFull(string assetBundleName, string assetName, System.Type type)
    {
        this.assetBundleName = assetBundleName;
        this.assetName = assetName;
        this.type = type;
    }

    public override T GetAsset<T>()
    {
        if (request != null && request.isDone)
        {
            return request.asset as T;
        }
        else
        {
            return null;
        }
    }

    public override bool Update()
    {
        if (request != null)
        {
            return false;
        }

        LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out downloadingError);
        if (bundle != null)
        {
            if (string.IsNullOrEmpty(assetName))
            {
                request = bundle.assetBundle.LoadAllAssetsAsync();
            }
            else
            {
                request = bundle.assetBundle.LoadAssetAsync(assetName, type);
            }

            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool IsDone()
    {
        if (request == null && downloadingError != null)
        {
            LoggerManager.Instance.Error(downloadingError);
            return true;
        }

        return request != null && request.isDone;
    }
}

public class AssetBundleLoadManifestOperation : AssetBundleLoadAssetOperationFull
{
    public AssetBundleLoadManifestOperation(string assetBundleName, string assetName, System.Type type) : base(assetBundleName, assetName, type){}

    public override bool Update()
    {
        base.Update();

        if (request != null && request.isDone)
        {
            AssetBundleManager.AssetBundleManifestObject = GetAsset<AssetBundleManifest>();
            LoggerManager.Instance.Warn("----------load AssetBundleManager.AssetBundleManifestObject start");
            LoggerManager.Instance.Warn(AssetBundleManager.AssetBundleManifestObject == null ? "null" : AssetBundleManager.AssetBundleManifestObject.name);
            LoggerManager.Instance.Warn("----------load AssetBundleManager.AssetBundleManifestObject end");
            return false;
        }
        else
        {
            return true;
        }
    }
}
