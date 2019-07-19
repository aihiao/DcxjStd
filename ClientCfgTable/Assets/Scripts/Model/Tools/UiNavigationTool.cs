using System;
using System.Collections.Generic;
using ClientCommon;

/// <summary>
/// 界面跳转工具
/// </summary>
public class UiNavigationTool
{
    public static void Navigation2Ui(string navId)
    {
        Navigation2Ui(int.Parse(navId));
    }

    /// <summary>
	/// 如果只获取到Id, 比如任务中的跳转, 不需要显示跳转的详细信息的
	/// </summary>
    public static BaseUi Navigation2Ui(int navId)
    {
        if (navId == IdSeg.InvalidId)
        {
            LoggerManager.Instance.Info("无效的跳转!!!");
            return null;
        }

        MenuNavigation nav = ConfigDataBase.MenuNavigationConfig.Get(navId);
        if (AssertHelper.Check(nav != null, "没有找到跳转数据, 没有找到跳转数据 Id=" + navId))
        {
            return Navigation2Ui(nav);
        }

        return null;
    } 

    public static void NPCNavigation2Ui(string navId)
    {

    }

    private static void Walk2NPC(int serviceId)
    {

    }

    /// <summary>
	/// 获取数据跳转
	/// </summary>
    public static void Navigation2Ui(GainData gain)
    {
        switch (gain.GainType)
        {
            case MenuGainType.Function:
                break;

            case MenuGainType.DungeonPoint: // 跳转到关卡
                break;

            case MenuGainType.DungeonChapater: // 跳转到剧情副本章节
                break;

            case MenuGainType.SubFunction:
                break;

            default:
                break;
        }
    }

    private List<string> GetMenuArgs(string gainParam)
    {
        List<string> argList = new List<string>();
        if (gainParam.Contains("#"))
        {
            string[] menus = gainParam.Split('#');
            for (int i = 0; i < menus.Length; i++)
            {
                argList.Add(menus[i]);
            }
        }
        else
        {
            argList.Add(gainParam);
        }
        return argList;
    }

    private static BaseUi Navigation2Ui(MenuNavigation nav)
    {
        return null;
    }

    public static bool IsUnlockPanel(int navId, bool showTip = false)
    {
        MenuNavigation nav = ConfigDataBase.MenuNavigationConfig.Get(navId);
        if (AssertHelper.Check(nav != null, "没有找到跳转数据, 没有找到跳转数据 Id=" + navId))
        {
            return IsUnlockPanel(nav, showTip);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
	/// 获取某个界面是否解锁
	/// </summary>
    public static bool IsUnlockPanel(MenuNavigation nav, bool showTip = false)
    {
        bool isParentUnlock = true;
        if (nav.ParentMenuId != -1 && (!MenuParentId.IsMenuIgnore(nav.ParentMenuId))) // 有父菜单
        {
            isParentUnlock = IsUnlockPanel(nav.ParentMenuId);
        }
        bool isUnlocked = false;
        switch (nav.MenuUnlockType)
        {
            case MenuUnlockType.DungeonUnlock:
                break;
            case MenuUnlockType.Dungeon:
            case MenuUnlockType.VIPLevel:
            case MenuUnlockType.Level:
                isUnlocked = DataModelManager.Instance.UIMenuNavgation.IsPanelUnlocked(nav.UiRegisterName);
                break;
            default:
                isUnlocked = DataModelManager.Instance.UIMenuNavgation.IsPanelUnlocked(nav.UiRegisterName);
                break;
        }

        if (isParentUnlock && isUnlocked)
        {
            return true;
        }

        if (showTip)
        {
            ShowUnlockCondition(nav.MenuUnlockType, nav.MenuUnlockPara);
        }

        return false;
    }

    /// <summary>
	/// 外部调用显示
	/// </summary>
    public static void ShowUnOpenError(int menuId)
    {
        MenuNavigation navdata = ConfigDataBase.MenuNavigationConfig.Get(menuId);
        if (AssertHelper.Check(navdata != null, "没有找到跳转数据，没有找到跳转数据 Id=" + menuId))
        {
            ShowUnlockCondition(navdata.MenuUnlockType, navdata.MenuUnlockPara);
        }
    }

    /// <summary>
	/// 弹窗显示解锁需要的条件
	/// </summary>
    private static void ShowUnlockCondition(int unlockType, int arg)
    {
        string code = "{0}";
        string formatArg = arg.ToString();
        switch (unlockType)
        {
            case MenuUnlockType.VIPLevel:
                code = "VIPUnlockText";
                break;
            case MenuUnlockType.Level:
                code = "LevelUnlockText";
                break;
            case MenuUnlockType.Dungeon:
            case MenuUnlockType.DungeonUnlock:
                code = "DungeonUnlockText";
                Dungeon dung = ConfigDataBase.DungeonConfig.Get(arg);
                if (dung != null)
                {
                    formatArg = dung.DungeonName;
                }
                break;
            default:
                code = "FunctionNotOpen";
                break;
        }

        ShowErrorTip(formatArg, code);
    }

    private static void ShowErrorTip(string formatArg, string code)
    {
        if (!string.IsNullOrEmpty(code))
        {
            string errormsg = "";
            Text content = ConfigDataBase.TextConfig.Get(code);
            if (string.IsNullOrEmpty(content.Content))
            {
                content.Content = code;
            }
            errormsg = string.Format(content.Content, formatArg);
            AlertMessageManager.Instance.ShowPop(errormsg);
        }
    }

    /// <summary>
	/// 跳转到目标界面
	/// </summary>
	/// <param name="uiRegisterName"></param>
	/// <param name="openArgs"></param>
	/// <param name="navArgs"></param>
    public static BaseUi Navigation2Ui(string uiRegisterName, List<string> openArgs, List<string> navArgs)
    {
        if (string.IsNullOrEmpty(uiRegisterName))
        {
            LoggerManager.Instance.Error("data error uiRegisterName is null");
            return null;
        }

        Type t = Type.GetType(uiRegisterName, false, true);
#if UNITY_EDITOR
        if (t != null)
        {
            if (t.ToString() != uiRegisterName) // 防止大小写问题
            {
                LoggerManager.Instance.Info("try to get type for NavigationToUI, type : {0}",  uiRegisterName);
                AssertHelper.Check(false, "can't get type of " + uiRegisterName);
                return null;
            }
        }
        else
        {
            AssertHelper.AssertFalse(false, "数据配置错误", "跳转界面名=" + uiRegisterName + "不存在");
        }
#endif
        BaseUi ui = UiRelations.Instance.GetUi(t);
        if (ui == null || (!ui.IsShowing))
        {
            // 多人副本需要做更多的判断
            if (uiRegisterName.Equals(UiPrefabNames.UiPnlTeamDungeon))
            {
                if (DataModelManager.Instance.TeamDungeonInfo.IsInTeam)
                {
                    UiManager.Instance.ShowByName(UiPrefabNames.UiPnlTeamDungeonTeam);
                }
                else
                {
                    UiManager.Instance.ShowByName(UiPrefabNames.UiPnlTeamDungeon);
                }
            }
            else
            {
                ui = UiManager.Instance.ShowByName(uiRegisterName, openArgs.ToArray());
            }
        }

        if (ui != null && ui.IsShowing && navArgs != null && navArgs.Count > 0)
        {
            ui.MenuNavigation(navArgs);
        }

        return ui;
    }

    /// <summary>
    ///  获取导航数据
    /// </summary>
    /// <param name="nav"></param>
    /// <param name="args">跳转参数</param>
    /// <param name="menuOpendData">需要被打开的面板</param>
    private static void GetNavArgs(MenuNavigation nav, Stack<string> args, out MenuNavigation menuOpenData)
    {
        if (nav.ParentMenuId != -1 && (!MenuParentId.IsMenuIgnore(nav.ParentMenuId)))
        {
            MenuNavigation parentMenu = ConfigDataBase.MenuNavigationConfig.Get(nav.ParentMenuId);
            if (AssertHelper.Check(parentMenu != null, "没有找到跳转数据,没有找到跳转数据 Id=" + parentMenu.Id))
            {
                if (parentMenu.ParentMenuId != -1)
                {
                    if (!string.IsNullOrEmpty(nav.MenuNavParam))
                    {
                        args.Push(nav.MenuNavParam);
                    }
                    GetNavArgs(parentMenu, args, out menuOpenData);

                    return;
                }
            }
        }

        menuOpenData = nav;
    }

}
