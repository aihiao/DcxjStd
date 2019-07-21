using UnityEngine;
using LywGames;

/// <summary>
/// 发布到平台类型类
/// </summary>
public class PlatformType
{
    public const int UnKnow = 0;
    public const int IPhone = 1; // 苹果官方
    public const int IPhoneJailbreak = 2; // 苹果越狱
    public const int Android = 3;
}

public interface IPlatformListener
{
    /// <summary>
    /// 初始化成功
    /// </summary>
    void OnIntiailized();

    /// <summary>
    /// 调用平台登录, 但是平台不提供登录支持
    /// </summary>
    void OnNoPlatformLoginProvide();

    /// <summary>
    /// 平台登录结果
    /// </summary>	
    void OnLoginResult(bool success, string userId, string username, string channelUserId, string token, string productCode, string channelLabel, string channelId, int kodChannelId);

    /// <summary>
    /// 登出成功
    /// </summary>
    void OnLogout();

}

public class Platform : MonoBehaviour
{
    private IPlatformListener platformListener = null;

    private static Platform instance;
    public static Platform Instance { get { return instance; } }

    /// <summary>
    /// 创建发布者平台实例, 并调用发布者平台的初始化方法
    /// </summary>
    /// <param name="gameObject"></param>
    public static void CreateOnGameObject(GameObject gameObject, IPlatformListener platformListener)
    {
        switch (LywConfig.GetPublisher())
        {
            case ProductPublisher.Local: instance = gameObject.AddComponent<Platform>(); break;
            case ProductPublisher.LywGames: instance = gameObject.AddComponent<LywPlatform>(); break;
            default: LoggerManager.Instance.Error("Invalid publisher type when create platform.Invalid Platform type is " + LywConfig.GetPublisher()); break;
        }

        // 创建平台即初始化平台
        if (instance != null)
        {
            instance.platformListener = platformListener;
            instance.Initialize();
        }
    }

    /// <summary>
	/// 初始化平台
	/// 注意: 整个生命周期应该只调用一次
	/// </summary>
	public virtual void Initialize()
    {
        
    }

    /// <summary>
	/// 退出游戏
	/// </summary>
	public virtual void Exit(string title, string text, string ok, string cancel)
    {
        LywConfig.Exit(title, text, ok, cancel);
    }


}
