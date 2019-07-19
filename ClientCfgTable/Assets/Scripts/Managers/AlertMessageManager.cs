using System;
using ClientCommon;

/// <summary>
/// 按钮类型
/// </summary>
public enum AlertBtnType
{
    Ok,
    Cancel,
    Yes,
    No
}

/// <summary>
/// 提示消息管理器, 所有提示都走这里
/// </summary>
public class AlertMessageManager : AbsManager<AlertMessageManager>
{
    private UiPnlTextTip textTip;
    public UiPnlTextTip TextTip
    {
        get
        {
            if (textTip == null)
            {
                textTip = (UiPnlTextTip)UiManager.Instance.ShowByName(UiPrefabNames.UiPnlTextTip);
            }
            return textTip;
        }
    }

    /// <summary>
	/// 根据配置Id显示消息, 提示服务器的错误码, 使用AlertMessage表
	/// </summary>
    public void Show(int id, Action<AlertBtnType, object> callBack = null, object data = null)
    {
        AlertMessage alertMessage = ConfigDataBase.AlertMessageConfig.Get(id);
        if (alertMessage != null)
        {
            if (alertMessage.Type == AlertMessageType.Unknown)
            {
                alertMessage.Type = AlertMessageType.Popup;
                alertMessage.Content = "Error: Type undefined for id =>" + id;
            }

            switch (alertMessage.Type)
            {
                case AlertMessageType.Popup:
                case AlertMessageType.Unknown:
                    ShowPop(alertMessage.Description);
                    break;
            }
        }
        else
        {
            ShowPop("Error: No Alert text message for id =>" + id);
        }
    }

    /// <summary>
	/// 根据配置Id显示消息, 客户端本地使用, 使用textConfig表
	/// </summary>
    public void Show(string id, Action<AlertBtnType, object> callBack = null, object data = null)
    {
        Text text = ConfigDataBase.TextConfig.Get(id);
        if (text != null)
        {
            ShowPop(text.Content);
        }
        else
        {
            ShowPop("Error: No Alert text message for id =>" + id);
        }
    }

    /// <summary>
	/// 显示弹出框
	/// </summary>
    public UiDialog ShowAlert(string content, uint flag, Action<AlertBtnType, object> callBack = null, object data = null)
    {
        UiManager.Instance.ShowByName(UiPrefabNames.UiPnlMessage);
        UiPnlMessage alert = UiManager.Instance.GetUi<UiPnlMessage>();
        if (alert != null)
        {
            alert.Set(content, flag, callBack).SetData(data);
        }
        return alert;
    }

    /// <summary>
	/// 显示漂浮框
	/// </summary>
    public void ShowPop(string message)
    {
        UiPnlTextTip textTipPnl = textTip;
        if (!textTipPnl.IsShowing)
        {
            textTipPnl.Show();
        }
        textTipPnl.AddContent(message);
    }
}

