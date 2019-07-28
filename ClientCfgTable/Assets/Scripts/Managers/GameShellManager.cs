using System;
using System.Reflection;
using UnityEngine;
using ClientCommon;

/// <summary>
/// 和系统运行相关的事件
/// </summary>
public class GameShellManager : AbsManager<GameShellManager>
{
    // 内存函数
    [Obfuscation(Exclude = true, Feature = "renaming")]
    private void OnReceiveMemoryWarning(string message)
    {
        LoggerManager.Instance.Error("OnReceiveMemoryWarning");
        FreeMemory();
    }

    /// <summary>
    /// 深度释放内存，只应该在两种情况调用, 1切关时，2收到OnReceiveMemoryWarning时
    /// </summary>
    public void FreeMemory(bool isMemoryWarning = false)
    {
        if (UiManager.Instance != null)
        {
            UiManager.Instance.DestroyAll();
        }
        // 销毁所有界面模型
        UiModelTool.DeleteAllModel();

        // 2 管理层，释放缓存的数据
        if (PoolManager.Instance != null)
        {
            PoolManager.Instance.Clear();  // manager本身在切关时已经释放一次了，不过无所谓,统一在这里写一次清晰一点
        }

        if (!isMemoryWarning)
        {
            if (ResourceManager.Instance != null)
            {
                ResourceManager.Instance.Clear();
            }
        }

        GC.Collect();   
    }

    /// <summary>
	/// 处理中断返回
	/// </summary>
    private void OnApplicationFocus(bool focus)
    {
        if (GlobalManager.IsInstanceExist())
        {
            if (ReConnectManager.Instance != null)
            {
                ReConnectManager.Instance.CheckWhetherNetworkBroken();
            }
        }
    }

    /// <summary>
	/// 处理中断
	/// </summary>
    private void OnApplicationPause(bool pause)
    {
        LoggerManager.Instance.Info("OnApplicationPause {0}", pause);
        ProcessOnApplicationPause();
    }

    private void ProcessOnApplicationPause()
    {
        Debug.LogWarning("OnApplication pause or quite, saving");

        if (DataModelManager.Instance != null && DataModelManager.Instance.LoginInfo != null && DataModelManager.Instance.LoginInfo.SelectedArea != null) //避免刚进游戏就退出导致保存数据报错
            PlayerSaveData.Instance.SaveData(); //退出前保存各系统的小红点数据

        Debug.LogWarning("OnApplication pause or quite, save finished");

        if (RequestManager.Instance != null)
        {
            // Force process event
            RequestManager.Instance.FlushAllRequest();
        }
    }

    /// <summary>
	/// 应用退出
	/// </summary>
    private void OnApplicationQuit()
    {
        try
        {
            ConfigDataBase.Instance.ReleaseAll(true);
            LoggerManager.Instance.Warn("OnApplicationQuit, saving");
        }
        catch (Exception e)
        {
            LoggerManager.Instance.Error(e.Message);
        }

        // save
        ProcessOnApplicationPause();

        if (RequestManager.Instance != null)
        {
            // Force process event
            RequestManager.Instance.FlushAllRequest();
            // Dispose request manager.
            RequestManager.Instance.Dispose();
        }

        // Dispose all system modules.
        if (GlobalManager.Instance != null)
        {
            GlobalManager.Instance.DisposeAll(true);
        }
    }

    private bool brokenDlgShown = false;
    public void OnRequestManagerBroken(string brokenMessage)
    {
        LoggerManager.Instance.Info("OnRequestManagerBroken {0}", brokenMessage);

        // 防止重复显示断线框
        if (UiManager.Instance.GetIsShowing<UiPnlReconnectMessage>() && brokenDlgShown)
        {
            return;
        }

        brokenDlgShown = true;

        ReConnectManager.Instance.ForceCutOffConnectShowReconnectDialog();
    }

}
