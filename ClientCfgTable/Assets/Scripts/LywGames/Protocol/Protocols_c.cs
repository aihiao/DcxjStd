using System;
using System.Collections.Generic;
using ProtoBuf;
using ProtoBuf.Meta;
using LywGames.Messages.Proto.Auth;
using LywGames.Messages.Proto.Game;

namespace LywGames.Protocol
{
    public class Protocols_c : TypeModel
    {
        // 这个key要从0开始
        private static readonly Dictionary<Type, int> type2KeyDic = new Dictionary<Type, int>(695);

        static Protocols_c()
        {
            type2KeyDic.Add(typeof(LoginReq), 0);
            type2KeyDic.Add(typeof(LoginReq.LocalLoginReq), 1);
            type2KeyDic.Add(typeof(LoginReq.PlatformLoginReq), 2);
            type2KeyDic.Add(typeof(DeviceInfoPro), 3);
            type2KeyDic.Add(typeof(LoginRes), 4);
            type2KeyDic.Add(typeof(LoginRes.ChannelMessage), 5);
            type2KeyDic.Add(typeof(LoginRes.AreaPro), 6);
            type2KeyDic.Add(typeof(CreateAccountReq), 7);
            type2KeyDic.Add(typeof(CreateAccountRes), 8);
            type2KeyDic.Add(typeof(BindAccountReq), 9);
            type2KeyDic.Add(typeof(BindAccountRes), 10);
            type2KeyDic.Add(typeof(ChangePasswordReq), 11);
            type2KeyDic.Add(typeof(ChangePasswordRes), 12);
            type2KeyDic.Add(typeof(ActiveCodeReq), 13);
            type2KeyDic.Add(typeof(ActiveCodeRes), 14);
            type2KeyDic.Add(typeof(LoginGameReq), 15);
            type2KeyDic.Add(typeof(LoginGameRes), 16);
            type2KeyDic.Add(typeof(QueryLoginGameDataREQ), 16);

        }

        private static void Write(ActiveCodeReq activeCodeReq, ProtoWriter protoWrite)
        {
            long accountId = activeCodeReq.AccountId;
            if (accountId != 0)
            {
                // Variant可变的
                ProtoWriter.WriteFieldHeader(1, WireType.Variant, protoWrite);
                ProtoWriter.WriteInt64(accountId, protoWrite);
            }

            string activeCode = activeCodeReq.ActiveCode;
            if (!string.IsNullOrEmpty(activeCode))
            {
                ProtoWriter.WriteFieldHeader(2, WireType.String, protoWrite);
                ProtoWriter.WriteString(activeCode, protoWrite);
            }
            ProtoWriter.AppendExtensionData(activeCodeReq, protoWrite);
        }

        private static ActiveCodeReq Read(ActiveCodeReq acReq, ProtoReader protoReader)
        {
            int fieldNum;
            while ((fieldNum = protoReader.ReadFieldHeader()) > 0)
            {
                if (fieldNum != 1)
                {
                    if (fieldNum != 2)
                    {
                        if (acReq == null)
                        {
                            ActiveCodeReq activeCodeReq = new ActiveCodeReq();
                            ProtoReader.NoteObject(activeCodeReq, protoReader);
                            acReq = activeCodeReq;
                        }

                        protoReader.AppendExtensionData(acReq);
                    }
                    else
                    {
                        if (acReq == null)
                        {
                            ActiveCodeReq activeCodeReq = new ActiveCodeReq();
                            ProtoReader.NoteObject(activeCodeReq, protoReader);
                            acReq = activeCodeReq;
                        }

                        string activeCode = protoReader.ReadString();
                        if (activeCode != null)
                        {
                            acReq.ActiveCode = activeCode;
                        }
                    }
                }
                else
                {
                    if (acReq == null)
                    {
                        ActiveCodeReq activeCodeReq = new ActiveCodeReq();
                        ProtoReader.NoteObject(activeCodeReq, protoReader);
                        acReq = activeCodeReq;
                    }

                    long accountId = protoReader.ReadInt64();
                    acReq.AccountId = accountId;
                }
            }

            if (acReq == null)
            {
                ActiveCodeReq activeCodeReq = new ActiveCodeReq();
                ProtoReader.NoteObject(activeCodeReq, protoReader);
                acReq = activeCodeReq;
            }

            return acReq;
        }

        protected override void Serialize(int key, object value, ProtoWriter dest)
        {
            switch (key)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    Write((ActiveCodeReq)value, dest);
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                default:
                    break;
            }
        }

        protected override object Deserialize(int key, object value, ProtoReader source)
        {
            switch (key)
            {
                case 0:
                    return null;
                case 1:
                    return null;
                case 2:
                    return null;
                case 3:
                    return null;
                case 4:
                    return null;
                case 5:
                    return null;
                case 6:
                    return null;
                case 7:
                    return null;
                case 8:
                    return null;
                case 9:
                    return null;
                case 10:
                    return null;
                case 11:
                    return null;
                case 12:
                    return null;
                case 13:
                    return Read((ActiveCodeReq)value, source);
                case 14:
                    return null;
                case 15:
                    return null;
                case 16:
                    return null;
                case 17:
                    return null;
            }

            return null;
        }

        protected override int GetKeyImpl(Type type)
        {
            int result;
            if (type2KeyDic.TryGetValue(type, out result))
            {
                return result;
            }

            return -1;
        }

    }
}
