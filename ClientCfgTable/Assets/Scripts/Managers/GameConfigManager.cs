using System.Collections.Generic;

public class GameConfigManager : AbsManager<GameConfigManager>
{
    // cheat level name
    private static string debugDungeonName = "";
    public static string DebugDungeonName
    {
        get { return debugDungeonName; }
        set { debugDungeonName = value; }
    }

    // cheat level id
    private static int debugDungeonId = -1;
    public static int DebugDungeonId
    {
        get { return debugDungeonId; }
        set { debugDungeonId = value; }
    }

    private static bool isDebugingUiPnl;
    public static bool IsDebugingUiPnl
    {
        get { return isDebugingUiPnl; }
        set { isDebugingUiPnl = value; }
    }

    private static string debugingUiPnlName;
    public static string DebugingUiPnlName
    {
        get { return debugingUiPnlName; }
        set { debugingUiPnlName = value; }
    }

    private static bool debugForceAllowEnterDungeon = false;
    public static bool DebugForceAllowEnterDungeon
    {
        get { return debugForceAllowEnterDungeon; }
        set { debugForceAllowEnterDungeon = value; }
    }

    // end of cheat
    private static bool usingAssetToLoadResource = !true;
    public static bool UsingAssetToLoadResource
    {
        get { return usingAssetToLoadResource; }
        set { usingAssetToLoadResource = value; }
    }

    // 优化想干选项
    private static bool optAllowShadow = true;
    public static bool OptAllowShadow
    {
        get { return optAllowShadow; }
        set { optAllowShadow = value; }
    }

    private static bool optAllowNpcShadow = true;
    public static bool OptAllowNpcShadow
    {
        get { return optAllowNpcShadow && OptAllowShadow; }
        set { optAllowNpcShadow = value; }
    }

    private static bool optAllowOtherPlayerShadow = true;
    public static bool OptAllowOtherPlayerShadow
    {
        get { return optAllowOtherPlayerShadow && OptAllowShadow; }
        set { optAllowOtherPlayerShadow = value; }
    }

    private static bool debugShowMainCityDebugGui = false;
    public static bool DebugShowMainCityDebugGui
    {
        get { return debugShowMainCityDebugGui; }
        set { debugShowMainCityDebugGui = value; }
    }

    private static bool debugEnableGuide = false;
    public static bool DebugEnableGuide
    {
        get { return debugEnableGuide; }
        set { debugEnableGuide = value; }
    }

    private static int gloableFxPropertyLevel = -1;
    public static int GloableFxPropertyLevel
    {
        get
        {
            if (!PlayerSaveData.CanUseLocalData())
            {
                return 7;
            }
            if (gloableFxPropertyLevel == -1)
            {
                if (!int.TryParse(PlayerSaveData.Instance.GetValueInDic(PlayerSaveData.SaveDataKeys.FxPropertyLevel), out gloableFxPropertyLevel))
                {
                    gloableFxPropertyLevel = 7;

                }
            }

            return gloableFxPropertyLevel;
        }

        set
        {
            gloableFxPropertyLevel = value;
            if (PlayerSaveData.CanUseLocalData())
            {
                PlayerSaveData.Instance.AddToDic(PlayerSaveData.SaveDataKeys.FxPropertyLevel, gloableFxPropertyLevel.ToString());
            }
        }
    }

    //界面选项索引和特效等级的映射  因为值比较特殊  这样存下
    public static Dictionary<int, int> fxIndexAndLevelDic = new Dictionary<int, int>() { { 1, 1 }, { 2, 4 }, { 3, 7 } };
    public static Dictionary<int, int> fxLevelAndIndexDic = new Dictionary<int, int>() { { 1, 0 }, { 4, 1 }, { 7, 2 } };
}
