using System;
using System.Collections.Generic;
using UnityEngine;
using LywGames;
using LywGames.Messages.Proto.Game;

public class TimeManager : AbsManager<TimeManager>
{
    /// <summary>
	/// 服务器返回的是自1970, 1, 1开始的毫秒数
	/// </summary>
	private long serverTime;
    private long timeZone;

    /// <summary>
    /// Time.realtimeSinceStartup不会计算后台时间,所以使用了TimeEx的方法
    /// </summary>	
    private long loginTimeSinceStartup = 0;

    /// <summary>
    /// 程序成功登陆后一共运行的时间（系统计时器）
    /// </summary>
    public long RealTimeSinceLogIn
    {
        get
        {
            return (long)(TimeEx.RealTimeSinceStartUp * 1000) - loginTimeSinceStartup;
        }
    }

    public event Action frameTick;

    /// <summary>
    /// 保存倒计时的链表
    /// </summary>
    public LinkedList<CountDown> cdList = new LinkedList<CountDown>();

    /// <summary>
    /// 时间到时触发的函数
    /// </summary>
    public delegate void TimeDelegate();

    private DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public void SetRealTimeBeforeLogIn(TimeInfo timeInfo)
    {
        Debug.Assert(timeInfo.millsecond != 0, "can't set client time!");

        serverTime = timeInfo.millsecond;
        timeZone = timeInfo.timezone;
        loginTimeSinceStartup = (long)(TimeEx.RealTimeSinceStartUp * 1000);

        LoggerManager.Instance.Info("sync time, serverTime: {0} , clientUnityTime : {1}", timeInfo.millsecond, Time.realtimeSinceStartup);
    }

    public long GetClientNowStringByTimeZone
    {
        get { return serverTime + timeZone + RealTimeSinceLogIn; }
    }

    /// <summary>
    /// 加上时区的时间，以前的RealTimeSinceLogIn Time.realtimeSinceStartup不会计算后台时间,所以使用了TimeEx的方法
    /// </summary>
    public DateTime NowDateTimeByTimeZone
    {
        get { return dt.AddMilliseconds(serverTime + timeZone + RealTimeSinceLogIn); }
    }

    public DateTime GetClientNowDateTime()
    {
        return dt.AddMilliseconds(serverTime + RealTimeSinceLogIn);
    }

    public long GetClientNowLongTime()
    {
        return (serverTime + RealTimeSinceLogIn);
    }

    public string GetClientNowStringTime()
    {
        return dt.AddMilliseconds(serverTime + RealTimeSinceLogIn).ToLocalTime().ToString();
    }

    /// <summary>
    /// 服务器给出某个endTime，客户端算出具体时间
    /// </summary>
    public DateTime GetDateTime(long endTime)
    {
        return dt.AddMilliseconds(endTime);
    }

    public void AddCountDown(int totalTime, params TimeDelegate[] funcs)
    {
        CountDown cd = new CountDown(totalTime, funcs);
        cd.listNode = this.cdList.AddLast(cd);
        frameTick += cd.Calculate;
    }

    public void AddCountDown(CountDown cd)
    {
        cd.listNode = this.cdList.AddLast(cd);
        frameTick += cd.Calculate;
    }

    public override void OnUpdate()
    {
        if (frameTick != null)
        {
            frameTick();
        }
    }

    /// <summary>
    /// 更换账号时，清除旧的倒计时
    /// </summary>
    public void Clear()
    {
        while (this.cdList.Count > 0)
        {
            CountDown cd = this.cdList.Last.Value;
            cd.Stop();
        }
        this.cdList.Clear();
    }

}

/// <summary>
/// 倒计时
/// </summary>
public class CountDown
{
    /// <summary>
    /// 倒计时开始时的时间点(单位秒)
    /// </summary>
    private int startTimePoint;

    /// <summary>
    /// 总时间(单位秒)
    /// </summary>
    private int totalTime;

    private TimeManager.TimeDelegate OnTimeout;

    /// <summary>
    /// 该项在链表中的位置信息，删除性能更好
    /// </summary>
    public LinkedListNode<CountDown> listNode;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="totalTime">总时间</param>
    /// <param name="funcDels">时间到时触发的方法，可以多个</param>
    public CountDown(int totalTime, params TimeManager.TimeDelegate[] funcDels)
    {
        this.totalTime = totalTime;
        foreach (TimeManager.TimeDelegate func in funcDels)
        {
            this.OnTimeout += func;
        }
        this.startTimePoint = (int)Time.realtimeSinceStartup;

    }

    public void Stop()
    {
        if (this.OnTimeout != null)
        {
            TimeManager.Instance.cdList.Remove(this.listNode);
            TimeManager.Instance.frameTick -= Calculate;
            this.OnTimeout = null;
        }
    }

    public void Calculate()
    {
        if (this.totalTime < (Time.realtimeSinceStartup - this.startTimePoint))
        {
            if (this.OnTimeout != null)
                this.OnTimeout();
            this.Stop();

        }

    }
}