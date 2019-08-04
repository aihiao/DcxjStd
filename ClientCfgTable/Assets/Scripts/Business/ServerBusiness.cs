using System;
using System.Collections.Generic;
using LywGames.Messages;
using LywGames.Messages.Proto.Auth;
using LywGames.Messages.Proto.Game;

public partial class ServerBusiness
{
    public void InitializeRegisters()
    {

    }

    public void PrintBusinessLog(string log)
    {
        LoggerManager.Instance.Info(log);
    }

    /**
	 * 登陆游戏服务器
	 * b)	调用LoginGS，有三个关键输入参数都要输入
		i.	Token
		ii.	KOD文件的版本信息列表（包括运营配置）
		iii.	是登录还是断线重连
	 */
    public bool LoginGS(string hostname, int port, int callback, long accountID, int areadId, string token)
    {
        PrintBusinessLog("[ServerBusiness] LoginGS" + " IP: " + hostname + "  port: " + port.ToString());
        List<TableVersion> versions = GameUtility.GetConfigVersions();
        bool result = protocol.LoginGS(hostname, port, callback, accountID, areadId, token, versions);
        return result;
    }

    public bool GetLoginGsResult(out GCLoginGameMessage loginRes, out bool isNeedQueryData)
    {
        return protocol.GetLogiGsResult(out loginRes, out isNeedQueryData);
    }

}
