using System;
using System.Collections.Generic;
using LywGames.Messages;
using LywGames.Messages.Proto.Auth;
using LywGames.Messages.Proto.Game;

/// <summary>
/// 服务器业务处理, 这个分类主要处理: 调用网络层暴露给业务层的请求接口和给网络层设置请求的回调函数
/// </summary>
public partial class ServerBusiness
{
    public void InitializeRegisters()
    {
        protocol.SetOnCreateAccountRes(OnCreateAccountRes);
        protocol.SetOnLoginASRes(OnLoginASRes);
        protocol.SetOnActiveCodeRes(OnActiveCodeRes);
    }

    public void PrintBusinessLog(string log, params object[] obj)
    {
        LoggerManager.Instance.Info(log, obj);
    }

    // 认证服务器上创建账号
    public bool CreateAccountAS(string asHost, int asPort, int callback, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
    {
        PrintBusinessLog("[ServerBusiness] CreateAccountAS");
        return protocol.CreateAccountAS(asHost, asPort, callback, accountName, password, randomSeed, channelId, version, deviceInfo);
    }

    // 创建账号响应
    private void OnCreateAccountRes(ACCreateAccountMessage message)
    {
        PrintBusinessLog("[ServerBusiness] OnCreateAccountRes " + GetErrorKey(message.ResultCode) + " 0x" + Convert.ToString(message.ResultCode, 16));
        ReceiveResponse(new PRCreateAccount().InitMessage(message));
    }

    // 认证服务器上绑定账号
    public bool BindAccountAS(string authServerHostName, int port, string email, string password, int callback, string version, int chanelId, DeviceInfoPro deviceInfo)
    {
        PrintBusinessLog("[Serverbusiness] BindAccountAS");
        return protocol.BindAccountAS(authServerHostName, port, callback, email, password, UnityEngine.Random.Range(1, 1000000).ToString(), chanelId, version, deviceInfo);
    }

    // 登录认证服务器
    public bool LoginAS(string asHost, int asPort, int callback, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
    {
        PrintBusinessLog("[ServerBusiness] LoginAS {0}:{1}", asHost, asPort);
        return protocol.LoginAS(asHost, asPort, callback, accountName, password, randomSeed, channelId, version, deviceInfo);
    }

    public bool LoginAS(string asHost, int asPort, int callback, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo, string userId, string channelUserId, string channelCode, string productCode, string token)
    {
        PrintBusinessLog("[ServerBusiness] LoginAS {0}:{1}", asHost, asPort);
        return protocol.LoginAS(asHost, asPort, callback, accountName, password, randomSeed, channelId, version, deviceInfo, userId, channelUserId, channelCode, productCode, token);
    }

    // 登录认证服务器响应
    private void OnLoginASRes(ACLoginAuthMessage message)
    {
        PrintBusinessLog("[OnLoginAuthRes] OnLoginASRes " + GetErrorKey(message.ResultCode) + " 0x" + Convert.ToString(message.ResultCode, 16));
        ReceiveResponse(new PRLoginAS(message).InitMessage(message));
    }

    // 激活码请求
    public bool ActiveCodeAS(string asHost, int asPort, int callback, long accountId, string activeCode)
    {
        PrintBusinessLog("[Serverbusiness] ActiveCodeAS");
        return protocol.ActiveCodeAS(asHost, asPort, callback, accountId, activeCode);
    }

    // 激活码响应
    public void OnActiveCodeRes(ACActiveCodeMessage message)
    {
        PrintBusinessLog("[Serverbusiness] OnActiveCodeRes");
        ReceiveResponse(new ACActiveCodeRes().InitMessage(message));
    }

    // 登陆游戏服务器
    public bool LoginGS(string hostname, int port, int callback, long accountID, int areadId, string token)
    {
        PrintBusinessLog("[ServerBusiness] LoginGS" + " IP: " + hostname + "  port: " + port.ToString());
        List<TableVersion> versions = GameUtility.GetConfigVersions();
        bool result = protocol.LoginGS(hostname, port, callback, accountID, areadId, token, versions);
        return result;
    }

    // 登录游戏服务器响应
    public bool OnLoginGSRes(out GCLoginGameMessage loginRes, out bool isNeedQueryData)
    {
        PrintBusinessLog("[Serverbusiness] OnLoginGSRes");
        return protocol.LogiGSRes(out loginRes, out isNeedQueryData);
    }

}
