using System.IO;
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

        FileStream fs = new FileStream("D:/GitHub/Configs/Monster.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        List<Monster> list = ConfigDataBase.MonsterConfig.Monsters;
        foreach (var item in list)
        {
            string log = string.Format(@"Id={0} Name={1} ObjectTemplateId={2} FactionType={3} HeadIcon={4} ModelId={5} BodyPartId={6} HeadPartId={7} LeftWpnId={8} RightWpnId={9} BreatheTime={10} 
                Scale={11} Level={12} EliteLevel={13} PushableLevel={14} RewardId={15} AiDataGroupId={16} ViewRange={17} CraftType={18} ShouldAdaptaByPlayerLevel={19} PatrolRange={20} 
                PreferCraftType={21} DisappearAfterDead={22}", 
                item.Id, item.Name, item.ObjectTemplateId, item.FactionType, item.HeadIcon, item.ModelId, item.BodyPartId, item.HeadPartId, item.LeftWpnId, item.RightWpnId, item.BreatheTime, item.Scale,
                item.Level, item.EliteLevel, item.PushableLevel, item.RewardId, item.AiDataGroupId, item.ViewRange, item.CraftType, item.ShouldAdaptaByPlayerLevel, item.PatrolRange, item.PreferCraftType,
                item.DisappearAfterDead);
            sw.WriteLine(log);
            List<MonsterList> list2 = item.MonsterLists;
            foreach (var item2 in list2)
            {
                string log2 = string.Format("MonsterType={0} MonsterValue={1}", MonsterType.GetNameByType(item2.MonsterType), item2.MonsterValue);
                sw.WriteLine(log2);
            }
           
            List<AttackTalk> list3 = item.AttackTalks;
            foreach (var item3 in list3)
            {
                string log3 = string.Format("Content={0} Rate={1} Time={2}", item3.Content, item3.Rate, item3.Time);
                sw.WriteLine(log3);
            }

            List<int> list4 = item.Abilitiess;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item4 in list4)
            {
                sb.Append(item4).Append(" ");
            }
            sw.WriteLine(sb.ToString());
            sw.WriteLine("");
        }
        fs.Flush();
        sw.Close();
        fs.Close();
    }

}
