using UnityEngine;
using LywGames;

public class PlatformListener : AutoCreateSingleton<PlatformListener>, IPlatformListener
{
    public void OnIntiailized()
    {
        
    }

    public void OnLoginResult(bool success, string userId, string username, string channelUserId, string token, string productCode, string channelLabel, string channelId, int kodChannelId)
    {
        
    }

    public void OnLogout()
    {
        
    }

    public void OnNoPlatformLoginProvide()
    {
        
    }

    public void ProcessLogout()
    {

    }

    #region gameplay需要的外部接口
    bool isPlatformInited = false;

    bool isPlatformLoginProvide = true;
    public bool IsPlatformLoginProvide { get { return isPlatformLoginProvide; } }

    public bool IsWaitingForPlatformInitialize()
    {
        return !isPlatformInited;
    }

    // 显示游戏的登录界面, 支持自定义的登录界面
    public void ShowLoginUIModule()
    {
        UiManager.Instance.ShowByName(UiPrefabNames.UiPnlLogin);
    }

    public void HideLoginUIModule()
    {
        UiManager.Instance.Hide<UiPnlLogin>();
    }
    #endregion
}
