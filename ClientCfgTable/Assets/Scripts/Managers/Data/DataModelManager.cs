using System;
using System.Collections.Generic;
using System.Linq;
using LywGames.Messages.Proto.Auth;

public class DataModelManager : AbsManager<DataModelManager>
{
    /// <summary>
	/// 登录
	/// </summary>
	private LoginInfo loginInfo = null;
    public LoginInfo LoginInfo
    {
        get
        {
            if (loginInfo == null)
            {
                loginInfo = new LoginInfo();
                loginInfo.Initialize(); //在这里初始化一下就好了，省的在调用的时候先初始化
            }

            return loginInfo;
        }
    }

    private LoginRes.AreaPro areaInfo;
    public LoginRes.AreaPro AreaInfo
    {
        get { return areaInfo; }
        set { areaInfo = value; }
    }

    //多人副本
    private TeamDungeonInfo teamDungeonInfo;
    public TeamDungeonInfo TeamDungeonInfo
    {
        get
        {
            if (teamDungeonInfo == null)
            {
                teamDungeonInfo = new TeamDungeonInfo();
            }
            return teamDungeonInfo;
        }
    }

    private UiMenuNavgationInfo menuNavgation;
    public UiMenuNavgationInfo UIMenuNavgation
    {
        get
        {
            if (menuNavgation == null)
            {
                menuNavgation = new UiMenuNavgationInfo();
            }
            return menuNavgation;
        }
    }

    private long roleId = long.MaxValue;
    public long RoleId
    {
        get { return roleId; }
        set { roleId = value; }
    }
}
