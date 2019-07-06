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

        AlertMessage altMsg = ConfigDataBase.AlertMessageConfig.Get(1179655);
        Debug.LogError(string.Format("Id = {0} Description = {1} Code = {2} Version = {3} Abandoned = {4}", altMsg.Id, altMsg.Description, altMsg.Code, altMsg.Version, altMsg.Abandoned));

        Debug.LogError(string.Format("AttributeValueAP = {0} AttributeValueASP = {1} AttributeValueCDP = {2}", ConstValue.AttributeValueAP, ConstValue.AttributeValueASP, ConstValue.AttributeValueCDP));

        Ability ability = ConfigDataBase.AbilityConfig.Abilitys[0];
        Debug.LogError(string.Format("Id = {0} AbilityType = {1} SkillName = {2} TargetType = {3} ExpirationTime = {4}", ability.Id, ability.AbilityType, ability.SkillName, ability.TargetType, ability.ExpirationTime));
    }

}
