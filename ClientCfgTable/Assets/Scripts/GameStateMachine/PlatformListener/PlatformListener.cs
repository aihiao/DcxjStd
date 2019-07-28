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

}
