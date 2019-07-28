using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData : SaveData
{
    private static PlayerSaveData _saveData;
    public static PlayerSaveData Instance
    {
        get { return _saveData ?? (_saveData = new PlayerSaveData()); }
    }

    public static void Destroy()
    {
        _saveData = null;
    }
    PlayerSaveData()
    {
        TimeManager.Instance.AddCountDown(saveRate, SaveData);
        GetXmlData();
    }

    #region 需要保存的数据 数据需要实现ToString方法  xml要按字符串存取

    private Dictionary<string, string> dataDic = new Dictionary<string, string>();  //玩家数据 直接保存一个字典里
    private Dictionary<string, string> delaySaveDic = new Dictionary<string, string>(); //需要延迟存的数据 临时变量 最后都通过dataDic存
    private CountDown curDelayCount;    //保存延迟存储的计时器 以最后的为准

    private Dictionary<string, Dictionary<string, Dictionary<string, string>>> funcListDic = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();//各功能的数据  小红点需要隔几分钟存一次 数据都需要保存起来 key是文件名

    #endregion 需要保存的数据 数据需要实现ToString方法  xml要按字符串存取

    private const int saveRate = 300;	//自动保存的时间间隔 单位！秒

    public class SaveDataKeys   //保存所有的key  方便查看
    {
        public const string MusicKey = "MusicKey";
        public const string SoundKey = "SoundKey";  //音乐和音效开关放在PlayerSettings里，这个是环境的设置，不属于功能里的设置
        public const string CameraDistance = "CameraDistance";
        public const string CameraDistanceBattle = "CameraDistanceBattle";
        public const string EffectLevel = "bloomEffectLevel";   //0-表示关闭 
        public const string ZoneUnlockIdWhenLastViewWorldMap = "ZoneUnlockIdWhenLastViewWorldMap"; // 上次查看大地图时解锁的章节号
        public const string MaxShowInScreenPlayerNum = "MaxShowInScreenPlayerNum";
        public const string AutoUseSkill = "autoUseSkillWhenTeamAutoFight";
        public const string FxPropertyLevel = "FxPropertyLevel";
        public const string AutoUseReforce = "AutoUseReforce";
        public const string AutoUseNoReduce = "AutoUseNoReduce";
        public const string LastWorldFame = "LastWorldFame";//保存上一次我的江湖名声，用于称号界面特效的播放
        public const string FollowInCombat = "FollowInCombat";//战斗中是否跟随
        public const string ArenaSyncTeamTip = "ArenaSyncTeamTip";//镜像竞技场同步阵容提示窗口是否弹出
        public const string PvPArenaShowRedDot = "PvPArenaShowRedDot";//控制老1v1的红点显示，每次登陆最多显示一次
        public const string LastShopRefreshSnPrefex = "LastShopRefreshSnPrefex_"; // 每个商店
        public const string OutofControlShowHpName = "OutofControlShowHpName";//战斗中非控英雄是否显示血条名字
        public const string ArenaRedDot = "ArenaRedDot";//擂台红点提示
    }

    //在登录账号之前不能用本地数据   数据是按账号 角色和分区存的  调用太早数据不全
    public static bool CanUseLocalData()
    {
        if (!GlobalManager.IsInstanceExist())
            return false;
        if (GameStateMachineManager.Instance == null)
            return false;
        return GameStateMachineManager.Instance.GetCurrentStateType() != GameStateBase.GameStateType.Initializing &&
               GameStateMachineManager.Instance.GetCurrentStateType() != GameStateBase.GameStateType.Login;
    }

    //设置xml格式 需要手动添加
    protected override Dictionary<string, string> getXmlPairs()
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        return dic;
    }

    protected override void setXmlData()
    {
        data = new Dictionary<string, Dictionary<string, string>>
        {
            //名字应该是数据对应的功能名  值可以是多个key value
            //{"list0",getXmlPairs()},
            {"dicData",dataDic}
        };
    }

    //本来是数据在一个文件里  数据量小 可以直接读取出来  后来改成分文件，一次读取数据就多了，改成只有玩家数据这样读取
    protected override void analysisXml()
    {
        if (!data.ContainsKey("dicData"))
        {
            setDefaultData();
            return;
        }
        Dictionary<string, string> valuePairs = data["dicData"];
        if (valuePairs != null)
        {
            dataDic = valuePairs;
        }
    }

    //TODO 在声明时设置更方便  这需要删掉
    protected override void setDefaultData() { }

    //延迟保存 会把列表中的数据都保存 因为数据量不会大，也不会经常触发，所以这样简单处理 精确处理逻辑太多 反而效率不高
    private void delaySave()
    {
        foreach (var item in delaySaveDic)
        {
            if (dataDic.ContainsKey(item.Key))
                dataDic[item.Key] = item.Value;
            else
                dataDic.Add(item.Key, item.Value);
        }
        delaySaveDic.Clear();
        curDelayCount = null;
        Save();
    }

    private bool checkFuncDicKey(string fileName, string nodeName, string key)
    {
        //文件夹不存在  就不用再判断了
        if (!Directory.Exists(folderPath))
        {
            return false;
        }
        if (!funcListDic.ContainsKey(fileName))
        {
            if (CheckFileExit(fileName))
                funcListDic.Add(fileName, ReadFile(fileName));
            else
                return false;
        }
        if (!funcListDic[fileName].ContainsKey(nodeName))
            return false;
        return funcListDic[fileName][nodeName].ContainsKey(key);
    }

    #region 存取字典数据接口
    public void AddToDic(string key, string value)
    {
        if (dataDic.ContainsKey(key))
            dataDic[key] = value;
        else
        {
            dataDic.Add(key, value);
        }
        Save();
    }

    public void DelayAddToDic(string key, string value, int time)   //延迟保存 单位秒
    {
        if (time.IsNull())
        {
            AddToDic(key, value);
            return;
        }
        if (!delaySaveDic.ContainsKey(key))
        {
            delaySaveDic.Add(key, value);
            if (curDelayCount != null)
                curDelayCount.Stop();
            curDelayCount = new CountDown(time, delaySave);
            TimeManager.Instance.AddCountDown(curDelayCount);
        }
        else
            delaySaveDic[key] = value;
    }

    public string GetValueInDic(string key)
    {
        if (dataDic.ContainsKey(key))
            return dataDic[key];
        return "";
    }

    public void RemoveInDic(params string[] keys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (dataDic.ContainsKey(keys[i]))
                dataDic.Remove(keys[i]);
            else
                Debug.LogError("remove xml data error-- given key not exist-----" + keys);
        }
        Save();
    }

    //退出时保存延迟存储的数据
    public void SaveData()
    {
        delaySave();
    }
    #endregion 存取字典数据接口

}
