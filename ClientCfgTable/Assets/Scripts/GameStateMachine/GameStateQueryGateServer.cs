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

        FileStream fs = new FileStream("Assets/Configs/acupoint-2-Acupoint.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        List<Acupoint> list = ConfigDataBase.AcupointConfig.Acupoints;
        foreach (var item in list)
        {
            string log = string.Format(@"Id={0} Name={1} ChannelId={2} IconAssert={3} AtributeGroupId={4} CritActivePercent={5} NextAcupointId={6} Abandoned={7} Version={8}", 
                item.Id, item.Name, item.ChannelId, item.IconAssert, item.AtributeGroupId, item.CritActivePercent, item.NextAcupointId, item.Abandoned, item.Version);
            sw.WriteLine(log);
            //List<AttributeList> list2 = item.AttributeLists;
            //foreach (var item2 in list2)
            //{
            //    string log2 = string.Format(@"AttributeType={0} AttributeValue={1} AttributeBase={2} AttributeGrowth={3}",
            //        AttributeType.GetNameByType(item2.AttributeType), item2.AttributeValue, item2.AttributeBase, item2.AttributeGrowth);
            //    sw.WriteLine(log2);
            //}

            List<AcupointRequireItem> list3 = item.AcupointRequireItems;
            foreach (var item3 in list3)
            {
                string log3 = string.Format("ItemId={0} ItemCount={1}", item3.ItemId, item3.ItemCount);
                sw.WriteLine(log3);
            }

            List<int> list4 = item.RewardAttributes;
            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            foreach (var item4 in list4)
            {
                sb1.Append(item4).Append(" ");
            }
            sw.WriteLine(sb1.ToString());

            List<int> list5 = item.CritRewardAttributes;
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            foreach (var item5 in list5)
            {
                sb2.Append(item5).Append(" ");
            }
            sw.WriteLine(sb2.ToString());

            sw.WriteLine("");
        }
        fs.Flush();
        sw.Close();
        fs.Close();
    }

}
