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

        FileStream fs = new FileStream("Assets/Configs/LywTest.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        List<Dattribute> list = ConfigDataBase.DattributeConfig.Dattributes;
        //foreach (var item in list)
        //{
        //    string log = string.Format(@"DattributeId={0} Abandoned={1} Version={2}", item.DattributeId, item.Abandoned, item.Version);
        //    sw.WriteLine(log);
        //    List<AttributeList> list2 = item.AttributeLists;
        //    foreach (var item2 in list2)
        //    {
        //        string log2 = string.Format(@"AttributeType={0} AttributeValue={1} AttributeBase={2} AttributeGrowth={3}", 
        //            AttributeType.GetNameByType(item2.AttributeType), item2.AttributeValue, item2.AttributeBase, item2.AttributeGrowth);
        //        sw.WriteLine(log2);
        //    }

        //    List<AttackTalk> list3 = item.AttackTalks;
        //    foreach (var item3 in list3)
        //    {
        //        string log3 = string.Format("Content={0} Rate={1} Time={2}", item3.Content, item3.Rate, item3.Time);
        //        sw.WriteLine(log3);
        //    }

        //    List<int> list4 = item.LevelRanges;
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    foreach (var item4 in list4)
        //    {
        //        sb.Append(item4).Append(" ");
        //    }
        //    sw.WriteLine(sb.ToString());
        //    sw.WriteLine("");
        //}
        fs.Flush();
        sw.Close();
        fs.Close();
    }

}
