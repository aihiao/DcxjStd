using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using LywGames.Network;
using LywGames.Messages;
using LywGames.Messages.Proto.Auth;
using LywGames.Messages.Proto.Game;

namespace LywGames.ClientHelper
{
    /// <summary>
    /// 网络层暴漏给业务层的类, 供业务层调用
    /// </summary>
    public class ClientHelper
    {
        public delegate void onASConnectFailed();
        public onASConnectFailed OnASConnectFailed
        {
            get;
            set;
        }

        private bool pollingMode;
        private bool userTypeMode;
        private ProtocolType type;

        private IConnection connection = null;

        private MessageDelegateInitializer msgDelegateInitializer;
        private MessageDelegateProcessor msgDelegateProcessor;
        public MessageDelegateProcessor MsgDelegateProcessor
        {
            get
            {
                return msgDelegateProcessor;
            }
        }

        private AbstractMessageInitializer messageInitializer;
        private AbstractNetworkInitializer networkInitializer;

        private IMessageHandler combatMsgHandler = null;
        private IMessageHandler combatInactiveHandler = null;

        public void Initialize(bool pollingMode, bool userTypeMode, ProtocolType type)
        {
            this.pollingMode = pollingMode;
            this.userTypeMode = userTypeMode;
            this.type = type;

            MySerializer.GetInstance().Initialize(userTypeMode);

            msgDelegateInitializer = new MessageDelegateInitializer();
            msgDelegateProcessor = new MessageDelegateProcessor(msgDelegateInitializer);

            messageInitializer = new ClientHelperMessageInitializer();
            networkInitializer = new ClientHelperNetworkInitializer(messageInitializer, msgDelegateProcessor, type);
        }

        public void SetProtocolType(ProtocolType type)
        {
            this.type = type;
        }

        public void Update()
        {
            if (connection != null)
            {
                connection.Update();
            }
        }

        public bool IsConnected()
        {
            return connection != null && connection.isConnected();
        }

        public long GetNetworkPing()
        {
            if (connection != null)
            {
                return connection.getNetworkPing();
            }

            return -1;
        }

        public long GetAliasedPing()
        {
            if (connection != null)
            {
                return connection.getAliasedPing();
            }

            return -1;
        }

        public bool GetSendStatics(out uint sendBytes, out uint sendNum, out long totalTime)
        {
            if (connection == null)
            {
                sendBytes = 0u;
                sendNum = 0u;
                totalTime = 0L;
            }
            else
            {
                return connection.getSendStatics(out sendBytes, out sendNum, out totalTime);
            }

            return false;
        }

        public bool GetRecvStatics(out uint recvBytes, out uint recvNum, out long totalTime)
        {
            if (connection == null)
            {
                recvBytes = 0u;
                recvNum = 0u;
                totalTime = 0L;
            }
            else
            {
                return connection.getRecvStatics(out recvBytes, out recvNum, out totalTime);
            }

            return false;
        }

        public void PauseStatics()
        {
            if (connection != null)
            {
                connection.pauseStatics();
            }
        }

        public void ResumeStatics()
        {
            if (connection != null)
            {
                connection.resumeStatics();
            }
        }

        public void Disconnect()
        {
            if (connection != null)
            {
                connection.Disconnect();
            }
            connection = null;
        }

        private bool ConnectAS(string asHost, int asPort, Message msg)
        {
            connection = NetworkManager.GetInstance().CreateConnection(type, 0);

            bool result;
            if (connection == null)
            {
                LoggerManager.Instance.Error("ConnectAS can't create connection. Connection is null.");
                result = false;
            }
            else
            {
                try
                {
                    LoggerManager.Instance.Info("connectAS host{0} port {1}", asHost, asPort);
                    
                    connection.SetNetworkInitializer(networkInitializer, ConnectionType.CONNECTION_AUTH);
                    connection.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), new IPEndPoint(NetUtil.GetIPV4Address(asHost), asPort));

                    ASConnectionHandler connectionActiveHandler = new ASConnectionHandler(msg, this);
                    messageInitializer.SetConnectionActiveHandler(connectionActiveHandler);

                    result = true;
                }
                catch (Exception ex)
                {
                    LoggerManager.Instance.Error(ex.ToString());
                    result = false;
                }
            }

            return result;
        }

        public bool LoginAS(string asHost, int asPort, int callback, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
        {
            bool result;
            try
            {
                result = ConnectAS(asHost, asPort, new CALoginAuthMessage()
                {
                    CallBackId = callback,
                    Protocol =
                    {
                        localLoginReq = new LoginReq.LocalLoginReq()
                        {
                            email = accountName,
                            password = password,
                            channelID = channelId,
                            randomSeed = randomSeed,
                            version = version,
                            deviceInfo = deviceInfo
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex.ToString());
                result = false;
            }

            return result;
        }

        public bool LoginAS(string asHost, int asPort, int callback, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo, string userId, string channelUserId, string channelCode, string productCode, string token)
        {
            bool result;
            try
            {
                result = ConnectAS(asHost, asPort, new CALoginAuthMessage
                {
                    CallBackId = callback,
                    Protocol =
                    {
                        platformLoginReq = new LoginReq.PlatformLoginReq()
                        {
                            email = accountName,
                            password = password,
                            channelID = channelId,
                            randomSeed = randomSeed,
                            version = version,
                            deviceInfo = deviceInfo,
                            userId = userId,
                            channelUserId = channelUserId,
                            channelCode = channelCode,
                            productCode = productCode,
                            token = token
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex.ToString());
                result = false;
            }

            return result;
        }

        public bool ActiveCodeAS(string asHost, int asPort, int callback, long accountId, string activeCode)
        {
            bool result;
            try
            {
                result = ConnectAS(asHost, asPort, new CAActiveCodeMessage
                {
                    CallBackId = callback,
                    Protocol =
                    {
                        accountId = accountId,
                        activeCode = activeCode
                    }
                });
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex.ToString());
                result = false;
            }

            return result;
        }

        public bool ChangePasswordAS(string asHost, int asPort, int callback, string accountName, string oldPassword, string newPassword)
        {
            bool result;
            try
            {
                result = this.ConnectAS(asHost, asPort, new CAChangePasswordMessage
                {
                    CallBackId = callback,
                    Protocol =
                    {
                        email = accountName,
                        newPassword = newPassword,
                        oldPassword = oldPassword
                    }
                });
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex.ToString(), new object[0]);
                result = false;
            }

            return result;
        }

        public bool BindAccountAS(string asHost, int asPort, int callback, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
        {
            bool result;
            try
            {
                result = ConnectAS(asHost, asPort, new CABindAccountMessage
                {
                    CallBackId = callback,
                    Protocol =
                    {
                        email = accountName,
                        password = password,
                        channelID = channelId,
                        randomSeed = randomSeed,
                        version = version,
                        deviceInfo = deviceInfo
                    }
                });
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex.ToString());
                result = false;
            }

            return result;
        }

        public bool CreateAccountAS(string asHost, int asPort, int callback, string accountName, string password, string randomSeed, int channelId, string version, DeviceInfoPro deviceInfo)
        {
            bool result;
            try
            {
                result = ConnectAS(asHost, asPort, new CACreateAccountMessage
                {
                    CallBackId = callback,
                    Protocol =
                    {
                        email = accountName,
                        password = password,
                        channelID = channelId,
                        randomSeed = randomSeed,
                        version = version,
                        deviceInfo = deviceInfo
                    }
                });
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error(ex.ToString());
                result = false;
            }

            return result;
        }

        public bool ConnectToBS(string bsHost, int bsPort, int timeout)
        {
            LoggerManager.Instance.Info("connecting battle server..... ip = {0}, port = {1}", new object[]
            {
                bsHost,
                bsPort
            });
            this.connection = NetworkManager.GetInstance().CreateConnection(this.type, timeout);
            bool result;
            try
            {
                this.connection.SetNetworkInitializer(this.networkInitializer, ConnectionType.CONNECTION_BATTLE);
                this.connection.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), new IPEndPoint(NetUtil.GetIPV4Address(bsHost), bsPort));
                if (this.combatMsgHandler != null)
                {
                    this.messageInitializer.SetConnectionActiveHandler(this.combatMsgHandler);
                    this.messageInitializer.SetConnectionInactiveHandler(this.combatInactiveHandler);
                }
                else
                {
                    LoggerManager.Instance.Warn("connect to bs but found combatMsgHandler is null need call SetCombatMsgHandler", new object[0]);
                }
            }
            catch (Exception ex)
            {
                LoggerManager.Instance.Error("ConnectToBS host:{0}, port:{1} exception {2}", new object[]
                {
                    bsHost,
                    bsPort,
                    ex.ToString()
                });
                result = false;
                return result;
            }
            result = true;
            return result;
        }

        public bool LoginGS(string gsHost, int gsPort, int callback, long accountID, int areadId, string token, List<TableVersion> everyTableVersions)
        {
            IPEndPoint remoteAddress = new IPEndPoint(NetUtil.GetIPV4Address(gsHost), gsPort);
            long num = 0L;
            long num2 = 0L;
            if (this.connection != null)
            {
                num = this.connection.SendAmount;
                num2 = this.connection.ReceiveAmount;
                LoggerManager.Instance.Info("LoginGs found lastConnection Amount send {0} receive {1} then call disconnect", new object[]
                {
                    num,
                    num2
                });
                this.connection.Disconnect();
                this.connection = null;
            }
            this.connection = NetworkManager.GetInstance().CreateConnection(this.type, 0);
            this.connection.SetNetworkInitializer(this.networkInitializer, ConnectionType.CONNECTION_GAME);
            this.connection.ConnectAsync(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0), remoteAddress);
            LoggerManager.Instance.Debug("LoginGS host {0} port {1} send {2} receive {3}", new object[]
            {
                gsHost,
                gsPort,
                num,
                num2
            });
            CGLoginGameMessage cG_LoginGameMessage = new CGLoginGameMessage();
            cG_LoginGameMessage.CallBackId = callback;
            cG_LoginGameMessage.Protocol.accountId = accountID;
            cG_LoginGameMessage.Protocol.areaId = areadId;
            cG_LoginGameMessage.Protocol.token = token;
            cG_LoginGameMessage.Protocol.sendProtocolAmount = num;
            cG_LoginGameMessage.Protocol.recvProtocolAmount = num2;
            cG_LoginGameMessage.Protocol.datas.AddRange(everyTableVersions);
            GSConnectionHandler connectionActiveHandler = new GSConnectionHandler(cG_LoginGameMessage);
            this.messageInitializer.SetConnectionActiveHandler(connectionActiveHandler);
            return true;
        }

        public bool LogiGSRes(out GCLoginGameMessage msg, out bool isNeedQueryData)
        {
            msg = null;
            isNeedQueryData = false;
            bool result;
            if (this.connection == null)
            {
                LoggerManager.Instance.Info("getLoginGsResult connection null", new object[0]);
                result = true;
            }
            else
            {
                if (this.connection.isConnectionNonStart())
                {
                    LoggerManager.Instance.Debug("getLoginGsResult but no login operation", new object[0]);
                    result = false;
                }
                else
                {
                    if (this.connection.isConnecting())
                    {
                        result = false;
                    }
                    else
                    {
                        if (this.connection.isConnected())
                        {
                            if (this.connection.LoginGameRes == null)
                            {
                                result = false;
                            }
                            else
                            {
                                msg = this.connection.LoginGameRes;
                                LoggerManager.Instance.Debug("get connection {0} callback {1}", new object[]
                                {
                                    this.connection.GetHashCode(),
                                    msg.CallBackId
                                });
                                isNeedQueryData = msg.Protocol.needQueryData;
                                if (isNeedQueryData)
                                {
                                    this.connection.clearProtocolAmount();
                                }
                                result = true;
                            }
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        public bool SendMessage(Message message)
        {
            bool result;
            if (connection != null)
            {
                result = connection.Send(message, 1);
            }
            else
            {
                LoggerManager.Instance.Info("_SendMessage {0}-{0:X} callback {1} found connection is null", message.ProtocolId, message.CallBackId);
                result = false;
            }

            return result;
        }

        public void SetCombatMsgHandler(IMessageHandler combatMsgHandler, IMessageHandler combatInactiveHandler)
        {
            this.combatMsgHandler = combatMsgHandler;
            this.combatInactiveHandler = combatInactiveHandler;

            messageInitializer.SetDefaultMsgHandler(combatMsgHandler);
        }

        public void AddCombatMsg(Type msgType)
        {
            messageInitializer.AddMessage(msgType);
        }

        public void RemoveCombatMsg(Type msgType)
        {
            messageInitializer.RemoveMessage(msgType);
        }

        public bool SendCombatMessage(byte[] data, int len)
        {
            bool result;
            if (connection != null)
            {
                CBMessageReqMessage cbMessageReqMessage = new CBMessageReqMessage();
                cbMessageReqMessage.CallBackId = -1;
                cbMessageReqMessage.Protocol.Buffer = data;
                result = connection.Send(cbMessageReqMessage, 1);
            }
            else
            {
                result = false;
            }

            return result;
        }

        public void SetOnCreateAccountRes(Action<ACCreateAccountMessage> onCreateAccountRes)
        {
            MessageDelegateNode node = new MessageDelegateNode();
            node.receiveAction = delegate(Message msg){
                onCreateAccountRes((ACCreateAccountMessage)msg);
            };
            node.isShortConnect = true;
            msgDelegateInitializer.AddMessageReceiveDelegate(typeof(ACCreateAccountMessage), node);
        }

        public void SetOnLoginASRes(Action<ACLoginAuthMessage> onLoginAuthRes)
        {
            MessageDelegateNode node = new MessageDelegateNode();
            node.receiveAction = delegate (Message msg)
            {
                onLoginAuthRes((ACLoginAuthMessage)msg);
            };
            node.isShortConnect = true;
            msgDelegateInitializer.AddMessageReceiveDelegate(typeof(ACLoginAuthMessage), node);
        }

        public void SetOnActiveCodeRes(Action<ACActiveCodeMessage> onActiveCodeRes)
        {
            MessageDelegateNode node = new MessageDelegateNode();
            node.receiveAction = delegate (Message msg)
            {
                onActiveCodeRes((ACActiveCodeMessage)msg);
            };
            node.isShortConnect = true;
            msgDelegateInitializer.AddMessageReceiveDelegate(typeof(ACActiveCodeMessage), node);
        }

     }
}