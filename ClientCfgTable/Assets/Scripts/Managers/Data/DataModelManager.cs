using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DataModelManager : AbsManager<DataModelManager>
{

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
