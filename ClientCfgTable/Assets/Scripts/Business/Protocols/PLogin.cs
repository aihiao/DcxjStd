using System;
using LywGames.Messages;
using LywGames.Messages.Proto.Auth;

/*
 * 和登录相关的协议
 */

/// <summary>
/// 链接Gs
/// </summary>
class PLoginGs : BaseRequest
{
    public delegate void OnIsConnectedSuccess(int chanelId);
    public delegate void OnIsConnectedFailed(int errorCode, string errorMessage);

    private OnIsConnectedSuccess onIsConnectedSuccessDel;
    public OnIsConnectedSuccess OnIsConnectedSuccessDel { get { return onIsConnectedSuccessDel; } }

    private OnIsConnectedFailed onIsConnectedFailedDel;
    public OnIsConnectedFailed OnIsConnectedFailedDel { get { return onIsConnectedFailedDel; } }

    private string hostname;
    private int port;

    private int areaID;
    public int AreaID { get { return this.areaID; } }

    public LoginRes.AreaPro area;

    public PLoginGs(LoginRes.AreaPro area, OnIsConnectedSuccess connectedSuccessDel, OnIsConnectedFailed connectedFailedDel)
    {
        this.area = area;
        this.hostname = area.interfaceServerIP;
        this.port = area.interfaceServerPort;
        onIsConnectedSuccessDel = connectedSuccessDel;
        onIsConnectedFailedDel = connectedFailedDel;

        NeedResponse = false; // 由网络层给回调，拿不到响应，所以不需要响应

        UiPnlTipIndicator.ShowIndicatorIfNot();
    }

    public override bool HasResponse
    {
        get { return false; }
    }

    public override int ID
    {
        get
        {
            return 0; 
        }
    }

    public override bool Execute(ServerBusiness bsn)
    {
        PlayerSaveData.Destroy();   //登录成功需要使用新号的本地数据
        return bsn.LoginGS(hostname, port, ID, DataModelManager.Instance.LoginInfo.AccountId, area.areaID, DataModelManager.Instance.LoginInfo.Token);
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
