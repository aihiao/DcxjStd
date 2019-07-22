using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameMain : MonoBehaviour
{

	void Start ()
    {
        GetRuntimePlatform();
        GetBuildTarget();
	}

    void GetRuntimePlatform()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            LoggerManager.Instance.Error("GetRuntimePlatform " + RuntimePlatform.WindowsEditor);
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            LoggerManager.Instance.Error("GetRuntimePlatform " + RuntimePlatform.WindowsPlayer);
        }
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            LoggerManager.Instance.Error("GetRuntimePlatform " + RuntimePlatform.OSXEditor);
        }
        else if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            LoggerManager.Instance.Error("GetRuntimePlatform " + RuntimePlatform.OSXPlayer);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            LoggerManager.Instance.Error("GetRuntimePlatform " + RuntimePlatform.IPhonePlayer);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            LoggerManager.Instance.Error("GetRuntimePlatform " + RuntimePlatform.Android);
        }
        else
        {
            LoggerManager.Instance.Error("GetRuntimePlatform " + Application.platform);
        }
    }

    void GetBuildTarget()
    {
#if UNITY_EDITOR
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows)
        {
            LoggerManager.Instance.Error("GetBuildTarget " + BuildTarget.StandaloneWindows);
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64)
        {
            LoggerManager.Instance.Error("GetBuildTarget " + BuildTarget.StandaloneWindows64);
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneOSXIntel)
        {
            LoggerManager.Instance.Error("GetBuildTarget " + BuildTarget.StandaloneOSXIntel);
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneOSXIntel64)
        {
            LoggerManager.Instance.Error("GetBuildTarget " + BuildTarget.StandaloneOSXIntel64);
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneOSXUniversal)
        {
            LoggerManager.Instance.Error("GetBuildTarget " + BuildTarget.StandaloneOSXUniversal);
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
        {
            LoggerManager.Instance.Error("GetBuildTarget " + BuildTarget.iOS);
        }
        else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            LoggerManager.Instance.Error("GetBuildTarget " + BuildTarget.Android);
        }
        else
        {
            LoggerManager.Instance.Error("GetBuildTarget " + EditorUserBuildSettings.activeBuildTarget);
        }
#endif
    }

}
