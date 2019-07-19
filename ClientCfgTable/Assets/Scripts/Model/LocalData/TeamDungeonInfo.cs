using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TeamDungeonInfo
{
    /// <summary>
	/// 队伍人数
	/// </summary
	public const int GroupCount = 3;

    /// <summary>
    /// 是否在组队中
    /// </summary>
    private bool isInTeam = false;
    public bool IsInTeam
    {
        get { return isInTeam; }
    }

    /// <summary>
	/// 当前剩余奖励次数
	/// </summary>
	private int remainRewardCount = 0;
    public int RemainRewardCount
    {
        set { remainRewardCount = value; }
        get { return remainRewardCount; }
    }

    /// <summary>
    /// 已经购买奖励次数
    /// </summary>
    private int alreadyBuyCount = 0;
    public int AlreadyBuyCount
    {
        set { alreadyBuyCount = value; }
        get { return alreadyBuyCount; }
    }

    /// <summary>
    /// 剩余购买奖励次数
    /// </summary>
    private int remainBuyCount = 0;
    public int RemainBuyCount
    {
        set { remainBuyCount = value; }
        get { return remainBuyCount; }
    }

}