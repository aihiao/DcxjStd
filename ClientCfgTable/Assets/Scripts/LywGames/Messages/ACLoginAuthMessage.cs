﻿using LywGames.Messages.Proto.Auth;

namespace LywGames.Messages
{
    [Message(ProtocolId = 65539, IsRequest = false)]
    public class ACLoginAuthMessage : ProtocolMessage<LoginRes>
    {
        public override int Result
        {
            get
            {
                return protocol.result;
            }
        }
    }
}