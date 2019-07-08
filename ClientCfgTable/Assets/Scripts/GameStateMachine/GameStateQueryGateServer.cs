using System;
using System.Collections.Generic;
using UnityEngine;
using ClientCommon;

public class GameStateQueryGateServer : GameStateBase
{
    public override bool IsGamingState
    {
        get
        {
            return false;
        }
    }

    public override GameStateType StateType
    {
        get
        {
            return GameStateType.QueryGateServer;
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

}
