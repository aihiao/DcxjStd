using System;
using LywGames.Messages;

/// <summary>
/// 链接Gs
/// </summary>
class PLoginGs
{
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
