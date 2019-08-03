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

    public bool GetLoginGsResult(out GCLoginGameMessage loginRes, out bool isNeedQueryData)
    {
        return protocol.GetLogiGsResult(out loginRes, out isNeedQueryData);
    }

}
