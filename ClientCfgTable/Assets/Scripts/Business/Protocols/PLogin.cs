using LywGames;
using LywGames.Messages;
using LywGames.Messages.Proto.Auth;

/*
 * 和登录相关的协议
 */

public class PSCreateAccount : BaseRequest
{
    private string asHostName; // 认证服务器地址
    private int asHostPort; // 认证服务器端口号
    private int channelId; // 渠道Id

    // 账号名
    private string accountName;
    public string AccountName
    {
        get { return accountName; }
        set { accountName = value; }
    }
    // 账号密码
    private string accountPassword;
    public string AccountPassword
    {
        get { return accountPassword; }
        set { accountPassword = value; }
    }

    private DeviceInfoPro deviceInfo; // 设备信息

    public PSCreateAccount(string asHostName, int asHostPort, string accountName, string accountPassword, int channelId, DeviceInfoPro deviceInfo)
    {
        this.asHostName = asHostName;
        this.asHostPort = asHostPort;
        this.accountName = accountName;
        this.accountPassword = accountPassword;
        this.channelId = channelId;
        this.deviceInfo = deviceInfo;
    }

    public override bool Execute(ServerBusiness bsn)
    {
        return bsn.CreateAccountAS(asHostName, asHostPort, CallBackId, accountName, accountPassword, "tttttttt", channelId, "1", deviceInfo);
    }
}

public class PRCreateAccount : AbsResponse<ACCreateAccountMessage>
{
    public override void Execute(BaseRequest request)
    {
        PSCreateAccount caReq = request as PSCreateAccount;
        GameStateMachineManager.Instance.GetCurrentState<GameStateLogin>().OnCreateAccountSuccess(caReq.AccountName, caReq.AccountPassword, false, caReq.AccountName.Length);
    }

    protected override void ErrorHandler(BaseRequest request, int errCode, string errorKey)
    {
        LoggerManager.Instance.Info("callback PRCreateAccount is back with error!");
        base.ErrorHandler(request, errCode, errorKey);
        GameStateMachineManager.Instance.GetCurrentState<GameStateLogin>().OnCreateAccountFalied(errCode, errorKey);
    }
}
 
public class PSLoginAS : BaseRequest
{
    private string asHost;
    private int asPort;
    private string accountName;
    private string password;
    private string randomSeed;
    private int channelId;
    private string version;
    private DeviceInfoPro deviceInfo;
    private string userId = string.Empty;
    private string channelUserId;
    private string channelCode;
    private string productCode;
    private string token;

    /// <summary>
    /// 本地登陆协议
    /// </summary>
    public PSLoginAS(string asHost, int asPort, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
    {
        this.asHost = asHost;
        this.asPort = asPort;
        this.accountName = accountName;
        this.password = password;
        this.randomSeed = randomSeed;
        this.channelId = channelId;
        this.version = version;
        this.deviceInfo = deviceInfo;
    }

    /// <summary>
    /// 平台登陆协议
    /// </summary>
    public PSLoginAS(string asHost, int asPort, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo, string userId, string channelUserId, string channelCode, string productCode, string token)
    {
        this.asHost = asHost;
        this.asPort = asPort;
        this.accountName = accountName;
        this.password = password;
        this.randomSeed = randomSeed;
        this.channelId = channelId;
        this.version = version;
        this.deviceInfo = deviceInfo;
        this.userId = userId;
        this.channelUserId = channelUserId;
        this.channelCode = channelCode;
        this.productCode = productCode;
        this.token = token;
    }

    public override bool Execute(ServerBusiness bsn)
    {
        if (LywConfig.GetPublisher() == ProductPublisher.Local)
        {
            // 本地登陆
            return bsn.LoginAS(asHost, asPort, CallBackId, accountName, password, randomSeed, channelId, version, deviceInfo);
        }
        else
        {
            // 平台登陆
            return bsn.LoginAS(asHost, asPort, CallBackId, accountName, password, randomSeed, channelId, version, deviceInfo, userId, channelUserId, channelCode, productCode, token);
        }
    }
}

public class PRLoginAS : AbsResponse<ACLoginAuthMessage>
{
    public PRLoginAS(ACLoginAuthMessage message)
    {
        // 将login的信息保存到loginInfo里, 包括token
        DataModelManager.Instance.LoginInfo.SetLoginAuthMessage(message);
    }

    public override void Execute(BaseRequest request)
    {
        if (DataModelManager.Instance.LoginInfo.IsShowActivityInterface == false)   //如果不需要显示输入激活码窗口
        {
            if (GameStateMachineManager.Instance.GetCurrentStateType() == GameStateBase.GameStateType.Login)
            {
                GameStateMachineManager.Instance.GetCurrentState<GameStateLogin>().OnLoginSuccess(this);
            }
        }
        else
        {
            UiManager.Instance.ShowByName(UiPrefabNames.UiPnlActivationCode);
        }
    }

    protected override void ErrorHandler(BaseRequest request, int errCode, string errMsg)
    {
        base.ErrorHandler(request, errCode, errMsg);
        GameStateMachineManager.Instance.GetCurrentState<GameStateLogin>().OnLoginFailed(errCode, errMsg);
    }
}

/// <summary>
/// 链接GS
/// </summary>
class PLoginGS : BaseRequest
{
    public delegate void OnIsConnectedSuccess(int chanelId);
    public delegate void OnIsConnectedFailed(int errorCode, string errorMessage);

    private OnIsConnectedSuccess onIsConnectedSuccessDel;
    public OnIsConnectedSuccess OnIsConnectedSuccessDel { get { return onIsConnectedSuccessDel; } }

    private OnIsConnectedFailed onIsConnectedFailedDel;
    public OnIsConnectedFailed OnIsConnectedFailedDel { get { return onIsConnectedFailedDel; } }

    private string gsHostName;
    private int gsHostPort;

    private int areaId;
    public int AreaId { get { return areaId; } }

    public LoginRes.AreaPro area;

    public PLoginGS(LoginRes.AreaPro area, OnIsConnectedSuccess connectedSuccessDel, OnIsConnectedFailed connectedFailedDel)
    {
        this.area = area;
        this.gsHostName = area.InterfaceServerIp;
        this.gsHostPort = area.InterfaceServerPort;
        onIsConnectedSuccessDel = connectedSuccessDel;
        onIsConnectedFailedDel = connectedFailedDel;

        NeedResponse = false; // 由网络层给回调，拿不到响应，所以不需要响应

        UiPnlTipIndicator.ShowIndicatorIfNot();
    }

    public override bool HasResponse
    {
        get { return false; }
    }

    public override int CallBackId
    {
        get
        {
            return 0; 
        }
    }

    public override bool Execute(ServerBusiness bsn)
    {
        PlayerSaveData.Destroy();   //登录成功需要使用新号的本地数据
        return bsn.LoginGS(gsHostName, gsHostPort, CallBackId, DataModelManager.Instance.LoginInfo.AccountId, area.AreaId, DataModelManager.Instance.LoginInfo.Token);
    }
}

/// <summary>
/// 请求玩家数据协议，对应网络连接状态为 NETSTATUS_QUERYDATA
/// </summary>
public class PQueryLoginGameData : AbsRequest<CGQueryLoginGameMessage>
{
    private long roleId = long.MaxValue;

    public PQueryLoginGameData(long roleId)
    {
        this.roleId = roleId;
    }
}
