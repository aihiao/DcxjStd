using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MenuParentId
{
    public const int NPCMenu = -2; // 父菜单Id=-2就是Npc中的功能按钮, 解锁的时候飞向NPC按钮
    public const int CommonMenu = -3; // 父菜单Id=-3就是普通功能按钮, 解锁的时候飞向自己的位置, 并显示出来

    public static bool IsMenuIgnore(int id)
    {
        return id == NPCMenu || id == CommonMenu;
    }
}

public class UiMenuNavgationInfo
{

}
