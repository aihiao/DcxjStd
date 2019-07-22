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
    /// <summary>
    /// 1. 在Windows编辑器模式下, 切换到Web Player、PC, Mac & Linux Standalone或者Android平台, 输出的都是WindowsEditor
    /// 2. 在Mac编辑器模式下, 切换到PC, Mac & Linux Standalone或者ios平台, 输出的都是OSXEditor
    /// 3. 在Windows编辑器模式下, 切换到Web Player、PC, Mac平台, Build工程, 运行exe, 输出的都是WindowsPlayer
    /// 4. 在Mac编辑器模式下, 切换到Web Player、PC, Mac平台, Build工程, 运行app, 输出的都是OSXPlayer
    /// 5. 在Windows编辑器模式下, 切换到Android平台, Build的出apk, 真机运行, 输出的都是Android
    /// 6. 在Mac编辑器模式下, 切换到ios平台, Build的出xcode工程, 真机运行, 输出的都是IPhonePlayer
    /// </summary>
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

    /// <summary>
    /// 只有在编辑器模式下才可以使用, 构建exe、app、ipa或者apk均不能使用
    /// 1. 在Windows编辑器模式下, 切换到Web Player平台, 输出的都是WebPlayer
    /// 2. 在Windows编辑器模式下, 切换到PC, Mac & Linux Standalone平台, 输出的都是StandaloneWindows
    /// 3. 在Windows编辑器模式下, 切换到Android平台, 输出的都是Android
    /// 4. 在Mac编辑器模式下, 切换到PC, Mac & Linux Standalone平台, 输出的都是StandaloneOSXIntel
    /// 5. 在Mac编辑器模式下, 切换到ios平台, 输出的都是ios
    /// </summary>
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
