using System;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景管理器
/// 切换关卡只有两种形式: ChangeScene(异步); ChangeSceneSync(同步)
/// </summary>
public class SceneManager : AbsManager<SceneManager>
{
    public interface ISceneManagerListener
    {
        void OnSceneWillChange(SceneManager manager, string currentSceneName, string newSceneName);
        void OnSceneChanged(SceneManager manager, string oldSceneName, string currentSceneName);
    }

    private string startSceneName = "";
    public string StartSceneName { get { return startSceneName; } }

    public string CurrentSceneName { get { return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; } }

    public List<ISceneManagerListener> sceneManagerListeners = new List<ISceneManagerListener>();

    private static float selfLoadingProgress = 0f;
    private static AsyncOperation curentAysncOperation = null;
    public static AsyncOperation CurentAysncOperation
    {
        get { return curentAysncOperation; }
    }

    public static float CurrentAsyncLoadingProgress
    {
        get
        {
            if (selfLoadingProgress >= 0.8f)
            {
                return selfLoadingProgress;
            }
            else if (curentAysncOperation != null)
            {
                return 0.2f + (curentAysncOperation.progress) * (0.8f - 0.2f);
            }
            else if (selfLoadingProgress <= 0.2f)
            {
                return selfLoadingProgress;
            }
            else
            {
                return selfLoadingProgress;
            }
        }
    }

    public static void ResetSelfLoadingProgress()
    {
        selfLoadingProgress = 0f;
    }

    public override void Initialize(params object[] parameters)
    {
        base.Initialize(parameters);

        // Save start scene and current scene
        startSceneName = CurrentSceneName;
    }

    public bool IsSceneLoaded(string sceneName)
    {
        return CurrentSceneName.Equals(GetSceneName(sceneName), StringComparison.InvariantCultureIgnoreCase);
    }


    /// <summary>
    /// 切换关卡前的判断, 检查切换是否合法
    /// </summary>
    private bool CheckSceneChanging(string sceneName, bool forceLoad)
    {
        if (sceneName == startSceneName)
        {
            LoggerManager.Instance.Error("Can not reload start scene.");
            return false;
        }

        if (forceLoad == false && sceneName.Equals(CurrentSceneName))
        {
            return false;
        }

        return true;
    }

    public void AddSceneManagerListener(ISceneManagerListener listener)
    {
        if (sceneManagerListeners.Contains(listener))
        {
            return;
        }

        sceneManagerListeners.Add(listener);
    }

    public void RemoveSceneManagerListener(ISceneManagerListener listener)
    {
        sceneManagerListeners.Remove(listener);
    }

    private static string GetSceneName(string sceneName)
    {
        return Path.GetFileNameWithoutExtension(sceneName);
    }

    public void ChangeSceneSync(string sceneName, bool forceLoad = true)
    {
        sceneName = GetSceneName(sceneName);

        if (CheckSceneChanging(sceneName, forceLoad) == false)
        {
            return;
        }

        // Notice listener scene will change
        for (int i = 0; i < sceneManagerListeners.Count; ++i)
        {
            sceneManagerListeners[i].OnSceneWillChange(this, CurrentSceneName, sceneName);
        }

        // Load level
        LoggerManager.Instance.Info("LoadLevel : " + sceneName);

        string oldSceneName = CurrentSceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        OnChangeSceneCompleted(oldSceneName);
    }

    /// <summary>
    /// 切换到空场景, 用于释放内存
    /// </summary>
    public void ChangeToEmptyLevelForMemoryRelease()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Application.loadedLevelName == "empty01" ? "empty02" : "empty01", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    [Obfuscation(Exclude = true, Feature = "renaming")]
    private IEnumerator DoChangeSceneAsync(object[] param)
    {
        string sceneName = param[0] as string;
        bool forceLoad = (bool)param[1];

        sceneName = GetSceneName(sceneName);

        // Previous action maybe stop, Wait for previous loading finished
        while (Application.isLoadingLevel)
        {
            LoggerManager.Instance.Warn("Waiting for previous loading finished");
            yield return null;
        }

        if (CheckSceneChanging(sceneName, forceLoad) == false)
        {
            yield break;
        }

        // Notice listener scene will change
        for (int i = 0; i < sceneManagerListeners.Count; ++i)
        {
            sceneManagerListeners[i].OnSceneWillChange(this, CurrentSceneName, sceneName);
        }
        selfLoadingProgress = 0.05f;
        yield return null;

        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        curentAysncOperation = operation;
        selfLoadingProgress = 0.1f;
        yield return operation;
        while (!operation.isDone)
        {
            yield return null;
        }

        // Load unity level and save the destination scene.
        string oldSceneName = CurrentSceneName;
        OnChangeSceneCompleted(oldSceneName);

        yield return null;
        yield return null;
    }

    public Coroutine ChangeSceneAsync(string sceneName, bool forceLoad = true)
    {
        selfLoadingProgress = 0f;
        StopCoroutine("DoChangeSceneAsync");
        return StartCoroutine("DoChangeSceneAsync", new object[] { sceneName, forceLoad });
    }

    public IEnumerator ChangeSceneAsyncByAssetBundle(string assetbundleName, string sceneName, ILoadingProgress loadingBar, bool forceLoad = true)
    {
        LoggerManager.Instance.Warn("-----sync load " + assetbundleName);
        selfLoadingProgress = 0f;
        sceneName = GetSceneName(sceneName);

        // Previous action maybe stop, Wait for previous loading finished
        while (Application.isLoadingLevel)
        {
            LoggerManager.Instance.Warn("Waiting for previous loading finished");
            yield return null;
        }

        if (CheckSceneChanging(sceneName, forceLoad) == false)
        {
            yield break;
        }

        // Notice listener scene will change
        for (int i = 0; i < sceneManagerListeners.Count; ++i)
        {
            sceneManagerListeners[i].OnSceneWillChange(this, CurrentSceneName, sceneName);
        }

        yield return null;
        yield return null;

        yield return StartCoroutine(AssetBundleManager.LoadLevelAsync(assetbundleName, sceneName, false, loadingBar));

        string oldSceneName = CurrentSceneName;
        OnChangeSceneCompleted(oldSceneName);

        AssetBundleManager.UnloadAssetBundle(assetbundleName);

        yield return null;
        yield return null;
    }

    private void OnChangeSceneCompleted(string oldSceneName)
    {
        // Notice listener scene changed
        for (int i = 0; i < sceneManagerListeners.Count; ++i)
        {
            sceneManagerListeners[i].OnSceneChanged(this, CurrentSceneName, oldSceneName);
        }
        selfLoadingProgress = 1f;
        curentAysncOperation = null;
    }

}
