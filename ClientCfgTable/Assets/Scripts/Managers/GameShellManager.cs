using UnityEngine;
using ClientCommon;

/// <summary>
/// 和系统运行相关的事件
/// </summary>
public class GameShellManager : AbsManager<GameShellManager>
{
    // 内存函数
    private void OnReceiveMemoryWarning(string message)
    {
        Debug.LogError("OnReceiveMemoryWarning");
        FreeMemory();
    }

    /// <summary>
    /// 深度释放内存，只应该在两种情况调用, 1切关时，2收到OnReceiveMemoryWarning时
    /// </summary>
    public void FreeMemory(bool isMemoryWarning = false)
    {

    }

    /// <summary>
	/// 处理中断返回
	/// </summary>
    private void OnApplicationFocus(bool focus)
    {
        
    }

    /// <summary>
	/// 处理中断
	/// </summary>
    private void OnApplicationPause(bool pause)
    {
        
    }

    /// <summary>
	/// 应用退出
	/// </summary>
    private void OnApplicationQuit()
    {
        ConfigDataBase.Instance.DbAccessorFactory.ReleaseAll();
    }

}
