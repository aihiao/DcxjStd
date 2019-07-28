using LywGames;
using LywGames.Messages;

public class ReConnectManager : AbsManager<ReConnectManager>
{
    private int deviceConnectedInternetStatus = 0; // 设备网络状态: 0 未联网; 1 wifi联网; 2 移动网络

    /// <summary>
    /// 当发包时网络链接中断, 调用这个接口弹出确认板
    /// </summary>
    /// <param name="enablePlayerReconnectGs"></param>
    public void HandleGsClosed(bool enablePlayerReconnectGs = false)
    {
        RequestManager.Instance.DiscardAllRequests();
        UiManager.Instance.ShowByName(UiPrefabNames.UiPnlReconnectMessage, int.MaxValue, enablePlayerReconnectGs);
    }

    public void HandleReQueryData()
    {
        UiManager.Instance.ShowByName(UiPrefabNames.UiPnlMessage);
        UiManager.Instance.GetUi<UiPnlMessage>().Set(GameUtility.GetUiString("ReConnectManager_NeedQueryData"), UiDialogBtn.Ok, callBack: (button, data) =>
        {
            RequestManager.Instance.DiscardAllRequests();
            RequestManager.Instance.SendRequest(new PQueryLoginGameData(DataModelManager.Instance.RoleId));
        });
    }

    /// <summary>
	/// 当发现网络连接关闭时, 或超时玩家主动点重连后
	/// </summary>
	public void HandleNetStateNotOk()
    {
        RequestManager.Instance.DiscardAllRequests();
        PlatformListener.Instance.ProcessLogout();
    }

    public void BreakConnectGsCauseServerError(int errorId)
    {
        LoggerManager.Instance.Error("exception in gs, error id: {0}", errorId);

        UiManager.Instance.ShowByName(UiPrefabNames.UiPnlReconnectMessage, errorId);

        RequestManager.Instance.Business.DisconnectGs();
        RequestManager.Instance.DiscardRequest(typeof(PLoginGs));
    }

    public void ForceCutOffConnectShowReconnectDialog()
    {
        HandleGsClosed();
    }

    public void CheckWhetherNetworkBroken()
    {
        if (!GlobalManager.IsInstanceExist())
        {
            return;
        }

        if (GameStateMachineManager.Instance != null && GameStateMachineManager.Instance.GetCurrentState() != null)
        {
            if (GameStateMachineManager.Instance.GetCurrentState().IsGamingState && (!GameStateMachineManager.Instance.GetCurrentState().IsLoading()))
            {
                LoggerManager.Instance.Info("CheckWhetherNetworkBroken");
                if (RequestManager.Instance.Business.IsConnected())
                {
                    LoggerManager.Instance.Info("networkd broken!");

                    Message message = null;
                    // 同步做登陆
                    RequestManager.Instance.Business.ProcessOnLossGsConnectWhenSendMsg(ref message, 0, true);
                }
                else
                {
                    LoggerManager.Instance.Info("networkd is not broken, ignore");
                }
            }
        }
    }

    long lastCheckTimer = 0;
    public override void OnUpdate()
    {
        if (TimeManager.Instance != null)
        {
            if (TimeManager.Instance.RealTimeSinceLogIn - lastCheckTimer > 5000)
            {
                if (deviceConnectedInternetStatus == 0) // 没赋值过 取网络类型
                {
                    deviceConnectedInternetStatus = LywConfig.GetDeviceConnectedInternetStatus();
                }
                lastCheckTimer = TimeManager.Instance.RealTimeSinceLogIn;

                bool netChanged = false; // 标记网络类型是否有改变
                if (deviceConnectedInternetStatus != LywConfig.GetDeviceConnectedInternetStatus())
                {
                    netChanged = true;
                    deviceConnectedInternetStatus = LywConfig.GetDeviceConnectedInternetStatus();
                }

                if (netChanged)
                {
                    CheckWhetherNetworkBroken();
                }
            }
        }
    }

}
