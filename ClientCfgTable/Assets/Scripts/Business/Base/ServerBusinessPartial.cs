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
    protected Dictionary<int, string> prtEnum = new Dictionary<int, string>();

    public void Initialize()
    {
        InitPrtEnum();

        protocol.OnAsConnectFailed = OnConnectAsFailed;

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

    private void OnConnectAsFailed()
    {
        ReConnectManager.Instance.ForceCutOffConnectShowReconnectDialog();
    }

    private void InitPrtEnum()
    {
        Type prtType = typeof(Protocols);
        FieldInfo[] fis = prtType.GetFields();
        for (int i = 0; i < fis.Length; i++)
        {
            FieldInfo fi = fis[i];
            if (!fi.IsStatic)
            {
                continue;
            }

            object obj = fi.GetValue(null);
            if (!(obj is int))
            {
                return;
            }

            prtEnum[(int)obj] = fi.Name;
        }
    }

    public void Dispose()
    {
        protocol.Disconnect();
        prtEnum.Clear();
    }

    public void Update()
    {
        protocol.Update();
    }
    
    public void DisconnectGs()
    {
        protocol.Disconnect();
    }

    public void ReceiveResponse(BaseResponse response)
    {
        response.errorContent = GetErrorContent(response.result);
        RequestManager.Instance.ReceiveResponse(response);
    }

    public bool SendMessage(Message message, int reSendCount = 0, bool checkLoseConnection = true)
    {
        bool result = protocol.SendMessage(message);
        if ((!result) && checkLoseConnection)
        {
            if (reSendCount >= 3)
            {
                ReConnectManager.Instance.HandleGsClosed(true);
                return false;
            }

            return ProcessOnLossGsConnectWhenSendMsg(ref message, reSendCount, result);
        }
        return result;
    }

    public bool ProcessOnLossGsConnectWhenSendMsg(ref Message message, int reSendCount, bool defaultResult)
    {
        PLoginGs loginGs = new PLoginGs(DataModelManager.Instance.AreaInfo, null, null);
        loginGs.Execute(this);

        bool isNeedQueryData = false;
        bool finished = false;

        int counter = 0;
        int sleepMs = 1;
        GCLoginGameMessage loginMsg = null;
        while ((!finished) && sleepMs * counter < 2000)
        {
            RequestManager.Instance.Business.Update();
            finished = RequestManager.Instance.Business.GetLoginGsResult(out loginMsg, out isNeedQueryData);
            Thread.Sleep(sleepMs);
            counter++;
        }

        if (finished && loginMsg != null)
        {
            UiPnlTipIndicator.CloseIndicatorIfShowing();

            switch (loginMsg.Result)
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
                        ReConnectManager.Instance.HandleGsClosed();
                        return true;
                    }
            }
        }
        else
        {
            ReConnectManager.Instance.HandleGsClosed();
            return false;
        }

        return defaultResult;
    }

    public string GetErrorContent(int errorCode)
    {
        if (errorCode == 0)
        {
            return "normal success";
        }

        string errKey = string.Empty;
        if (prtEnum.ContainsKey(errorCode))
        {
            errKey = prtEnum[errorCode];
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

