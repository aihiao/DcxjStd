using System;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.Net.Sockets;
using LywGames.Messages;
using LywGames.ClientHelper;
using LywGames.Corgi.Protocol;

public partial class ServerBusiness
{
    protected ClientHelper protocol = new ClientHelper();
    protected string loginAccount;
    // 协议Id集合。int类型的是协议Id的值, string类型是协议Id的变量名称。
    protected Dictionary<int, string> protocolId2NameDic = new Dictionary<int, string>();

    public void Initialize()
    {
        InitProtocolIds();

        protocol.OnASConnectFailed = OnConnectASFailed;

#if UNITY_IPHONE
        protocol.Initialize(true, true, ProtocolType.Tcp);
#else
        protocol.Initialize(true, false, ProtocolType.Tcp);
#endif

        InitializeRegisters();
    }

    public void SetProtocolType(ProtocolType type)
    {
        protocol.SetProtocolType(type);
    }

    private void OnConnectASFailed()
    {
        ReConnectManager.Instance.ForceCutOffConnectShowReconnectDialog();
    }

    /// <summary>
    /// 读取所有的协议Id到protocolId2NameDic字典
    /// </summary>
    private void InitProtocolIds()
    {
        Type prtType = typeof(Protocols);
        FieldInfo[] fiArray = prtType.GetFields();
        for (int i = 0; i < fiArray.Length; i++)
        {
            FieldInfo fi = fiArray[i];
            if (!fi.IsStatic)
            {
                continue;
            }

            object obj = fi.GetValue(null);
            if (!(obj is int))
            {
                return;
            }

            protocolId2NameDic[(int)obj] = fi.Name;
        }
    }

    public void Dispose()
    {
        protocol.Disconnect();
        protocolId2NameDic.Clear();
    }

    public void Update()
    {
        protocol.Update();
    }
    
    public void DisconnectGS()
    {
        protocol.Disconnect();
    }

    public void ReceiveResponse(BaseResponse response)
    {
        response.ErrorKey = GetErrorKey(response.ResultCode);
        RequestManager.Instance.ReceiveResponse(response);
    }

    public bool SendMessage(Message message, int reSendCount = 0, bool checkLoseConnection = true)
    {
        bool result = protocol.SendMessage(message);
        if ((!result) && checkLoseConnection)
        {
            if (reSendCount >= 3)
            {
                ReConnectManager.Instance.HandleGSClosed(true);
                return false;
            }

            return ProcessOnLossGSConnectWhenSendMsg(ref message, reSendCount, result);
        }
        return result;
    }

    /// <summary>
    /// 处理当发送消息时, 游戏服务器断开连接的情况
    /// </summary>
    /// <param name="message"></param>
    /// <param name="reSendCount"></param>
    /// <param name="defaultResult"></param>
    /// <returns></returns>
    public bool ProcessOnLossGSConnectWhenSendMsg(ref Message message, int reSendCount, bool defaultResult)
    {
        PLoginGS loginGS = new PLoginGS(DataModelManager.Instance.AreaInfo, null, null);
        loginGS.Execute(this);

        bool isNeedQueryData = false;
        bool finished = false;

        int counter = 0;
        int sleepMS = 1; // 休息毫秒数
        GCLoginGameMessage loginMsg = null;
        while ((!finished) && sleepMS * counter < 2000)
        {
            RequestManager.Instance.Business.Update();
            finished = RequestManager.Instance.Business.OnLoginGSRes(out loginMsg, out isNeedQueryData);
            Thread.Sleep(sleepMS);
            counter++;
        }

        if (finished && loginMsg != null)
        {
            UiPnlTipIndicator.CloseIndicatorIfShowing();

            switch (loginMsg.ResultCode)
            {
                case Protocols.GameLoginSuccess:
                    {
                        if (isNeedQueryData)
                        {
                            ReConnectManager.Instance.HandleReQueryData();
                            return true;
                        }
                        else
                        {
                            if (message != null)
                            {
                                SendMessage(message, reSendCount + 1);
                                return true;
                            }
                        }
                    }
                    break;

                case Protocols.GameLoginSuccessRoleNotExist:
                case Protocols.GameLoginFail:
                case Protocols.GameLoginFromOthers:
                case Protocols.GameLoginFailNeedActiveCode:
                case Protocols.GameLoginTableVersionCheckSuccessUpdate:
                case Protocols.TableVersionCheckSuccessUpdate:
                default:
                    {
                        ReConnectManager.Instance.HandleGSClosed();
                        return true;
                    }
            }
        }
        else
        {
            ReConnectManager.Instance.HandleGSClosed();
            return false;
        }

        return defaultResult;
    }

    /// <summary>
    /// 通过错误码, 获取错误字符串Key
    /// </summary>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    public string GetErrorKey(int errorCode)
    {
        if (errorCode == 0)
        {
            return "normal success";
        }

        string errKey = string.Empty;
        if (protocolId2NameDic.ContainsKey(errorCode))
        {
            errKey = protocolId2NameDic[errorCode];
        }
        else
        {
            errKey = string.Format("UndefinedErrorCode_{0:X8}", errorCode);
        }

        return errKey;
    }

    public bool IsConnected()
    {
        return protocol.IsConnected();
    }

}

