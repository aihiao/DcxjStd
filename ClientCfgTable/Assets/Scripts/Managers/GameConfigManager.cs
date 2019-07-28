using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameConfigManager : AbsManager<GameConfigManager>
{
    // cheat level name
    static string debugDungeonName = "";
    public static string DebugDungeonName
    {
        get { return GameConfigManager.debugDungeonName; }
        set { GameConfigManager.debugDungeonName = value; }
    }

    // cheat level id
    static int debugDungeonId = -1;
    public static int DebugDungeonId
    {
        get { return GameConfigManager.debugDungeonId; }
        set { GameConfigManager.debugDungeonId = value; }
    }



    static bool isDebugingUiPnl;

    public static bool IsDebugingUiPnl
    {
        get { return isDebugingUiPnl; }
        set { isDebugingUiPnl = value; }
    }
    static string debugingUiPnlName;

    public static string DebugingUiPnlName
    {
        get { return debugingUiPnlName; }
        set { debugingUiPnlName = value; }
    }

    static bool debugForceAllowEnterDungeon = false;
    public static bool DebugForceAllowEnterDungeon
    {
        get { return debugForceAllowEnterDungeon; }
        set { debugForceAllowEnterDungeon = value; }
    }

    // end of cheat

    static bool usingAssetToLoadResource = !true;

    public static bool UsingAssetToLoadResource
    {
        get { return GameConfigManager.usingAssetToLoadResource; }
        set { GameConfigManager.usingAssetToLoadResource = value; }
    }

    // 优化想干选项
    static bool optAllowShadow = true;

    public static bool OptAllowShadow
    {
        get { return GameConfigManager.optAllowShadow; }
        set { GameConfigManager.optAllowShadow = value; }
    }

    static bool optAllowNpcShadow = true;

    public static bool OptAllowNpcShadow
    {
        get { return GameConfigManager.optAllowNpcShadow && OptAllowShadow; }
        set { GameConfigManager.optAllowNpcShadow = value; }
    }

    static bool optAllowOtherPlayerShadow = true;

    public static bool OptAllowOtherPlayerShadow
    {
        get { return GameConfigManager.optAllowOtherPlayerShadow && OptAllowShadow; }
        set { GameConfigManager.optAllowOtherPlayerShadow = value; }
    }



    private static bool debugShowMainCityDebugGui = false;

    public static bool DebugShowMainCityDebugGui
    {
        get { return GameConfigManager.debugShowMainCityDebugGui; }
        set { GameConfigManager.debugShowMainCityDebugGui = value; }
    }
    private static bool debugEnableGuide = false;

    public static bool DebugEnableGuide
    {
        get { return GameConfigManager.debugEnableGuide; }
        set { GameConfigManager.debugEnableGuide = value; }
    }


    private static int gloableFxPropertyLevel = -1;
    public static int GloableFxPropertyLevel
    {
        get
        {
            if (!PlayerSaveData.CanUseLocalData())
                return 7;
            if (gloableFxPropertyLevel == -1)
            {
                if (!int.TryParse(PlayerSaveData.Instance.GetValueInDic(PlayerSaveData.SaveDataKeys.FxPropertyLevel), out gloableFxPropertyLevel))
                    gloableFxPropertyLevel = 7;
            }
            return gloableFxPropertyLevel;
        }
        set
        {
            gloableFxPropertyLevel = value;
            if (PlayerSaveData.CanUseLocalData())
                PlayerSaveData.Instance.AddToDic(PlayerSaveData.SaveDataKeys.FxPropertyLevel, gloableFxPropertyLevel.ToString());
        }
    }

    //界面选项索引和特效等级的映射  因为值比较特殊  这样存下
    public static Dictionary<int, int> FxIndexAndLevelDic = new Dictionary<int, int>() { { 1, 1 }, { 2, 4 }, { 3, 7 } };
    public static Dictionary<int, int> FxLevelAndIndexDic = new Dictionary<int, int>() { { 1, 0 }, { 4, 1 }, { 7, 2 } };
}
