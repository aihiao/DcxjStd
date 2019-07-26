using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 切换关卡只有两种形式
/// ChangeScene: 异步
/// ChangeSceneSync: 同步
/// </summary>
public class SceneManager : AbsManager<SceneManager>
{
    public interface ISceneManagerListener
    {
        void OnSceneWillChange(SceneManager manager, string currentScene, string newScene);
        void OnSceneChanged(SceneManager manager, string oldScene, string currentScene);
    }

    private string startScene = "";
    public string StartScene { get { return startScene; } }

    public string CurrentScene { get { return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name; } }

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
       
        startScene = CurrentScene;
    }

    public bool IsSceneLoaded(string sceneName)
    {
        return CurrentScene.Equals(GetSceneName(sceneName), StringComparison.InvariantCultureIgnoreCase);
    }


    /// <summary>
    /// 切换关卡前的判断，检查切换是否合法
    /// </summary>
    private bool CheckSceneChanging(string sceneName, bool forceLoad)
    {
        if (sceneName == startScene)
        {
            LoggerManager.Instance.Error("Can not reload start scene.");
            return false;
        }

        if (forceLoad == false && sceneName.Equals(CurrentScene))
            return false;

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
            return;

        // Notice listener scene will change
        for (int i = 0; i < sceneManagerListeners.Count; ++i)
            sceneManagerListeners[i].OnSceneWillChange(this, CurrentScene, sceneName);

        // Load level
        LoggerManager.Instance.Info("LoadLevel : " + sceneName);
        string oldScene = CurrentScene;
        // Application.LoadLevel(sceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        OnChangeSceneCompleted(oldScene);

        selfLoadingProgress = 1f;
    }

    // 切换到空场景，用于释放内存
    public void ChangeToEmptyLevelForMemoryRelease()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Application.loadedLevelName == "empty01" ? "empty02" : "empty01", UnityEngine.SceneManagement.LoadSceneMode.Single);
        //Application.LoadLevel(Application.loadedLevelName == "empty01" ? "empty02" : "empty01");
    }

    public Coroutine ChangeSceneAsync(string sceneName, bool forceLoad = true)
    {
        //Application.LoadLevel(Application.loadedLevelName == "empty01" ? "empty02" : "empty01");

        selfLoadingProgress = 0f;
        StopCoroutine("DoChangeSceneAsync");
        return StartCoroutine("DoChangeSceneAsync", new object[] { sceneName, forceLoad });
    }

    public System.Collections.IEnumerator ChangeSceneAsyncByAssetBundle(string assetbundleName, string sceneName, ILoadingProgress loadingBar, bool forceLoad = true)
    {
        //Application.LoadLevel(Application.loadedLevelName == "empty01" ? "empty02" : "empty01");
        //yield return null;

        Debug.LogWarning("-----sync load " + assetbundleName);
        selfLoadingProgress = 0f;
        //ProcessBeforeChangeScene(sceneName, forceLoad);
        sceneName = GetSceneName(sceneName);

        // Previous action maybe stop, Wait for previous loading finished
        while (Application.isLoadingLevel)
        {
            Debug.LogWarning("Waiting for previous loading finished");
            yield return null;
        }

        if (CheckSceneChanging(sceneName, forceLoad) == false)
            yield break;

        // Notice listener scene will change
        for (int i = 0; i < sceneManagerListeners.Count; ++i)
            sceneManagerListeners[i].OnSceneWillChange(this, CurrentScene, sceneName);

        yield return null;
        yield return null;

        yield return StartCoroutine(AssetBundleManager.LoadLevelAsync(assetbundleName, sceneName, false, loadingBar));


        string oldScene = CurrentScene;
        OnChangeSceneCompleted(oldScene);

        AssetBundleManager.UnloadAssetBundle(assetbundleName);


        yield return null;
        yield return null;
    }

    [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
    private System.Collections.IEnumerator DoChangeSceneAsync(object[] param)
    {
        string sceneName = param[0] as string;
        bool forceLoad = (bool)param[1];
        //yield return StartCoroutine(ProcessBeforeChangeScene(sceneName, forceLoad));
        sceneName = GetSceneName(sceneName);

        // Previous action maybe stop, Wait for previous loading finished
        while (Application.isLoadingLevel)
        {
            Debug.LogWarning("Waiting for previous loading finished");
            yield return null;
        }

        if (CheckSceneChanging(sceneName, forceLoad) == false)
            yield break;

        // Notice listener scene will change
        for (int i = 0; i < sceneManagerListeners.Count; ++i)
            sceneManagerListeners[i].OnSceneWillChange(this, CurrentScene, sceneName);
        selfLoadingProgress = 0.05f;
        yield return null;


        // AsyncOperation operation = Application.LoadLevelAsync(sceneName);
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        curentAysncOperation = operation;
        selfLoadingProgress = 0.1f;
        yield return operation;
        while (!operation.isDone)
        {
            yield return null;
        }

        // Load unity level and save the destination scene.
        string oldScene = CurrentScene;
        OnChangeSceneCompleted(oldScene);
        yield return null;
        selfLoadingProgress = 1f;
        yield return null;
    }

    private void OnChangeSceneCompleted(string oldScene)
    {
        // Notice listener scene changed
        for (int i = 0; i < sceneManagerListeners.Count; ++i)
            sceneManagerListeners[i].OnSceneChanged(this, CurrentScene, oldScene);

        // Player BGM
        //string currentMusic = ClientServerCommon.ConfigDatabase.DefaultCfg.SceneConfig.GetBgMusicBySceneName(CurrentScene);
        //if (!AudioManager.Instance.IsMusicPlaying(currentMusic))
        //{
        //    AudioManager.Instance.StopMusic();
        //    AudioManager.Instance.PlayMusic(currentMusic, true);
        //}
        selfLoadingProgress = 1f;
        curentAysncOperation = null;
    }

}
