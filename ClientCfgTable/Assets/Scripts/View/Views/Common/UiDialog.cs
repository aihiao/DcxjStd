using UnityEngine;
using System;

public class UiDialogBtn
{
    public const uint Ok = 1;
    public const uint Yes = 2;
    public const uint No = 4;
    public const uint Cancel = 8;
    public const uint Close = 16;
    public const uint None = 0;
}

public class UiDialog : BaseUi
{
    public UiDialog Set(string contentText, uint btnFlag = UiDialogBtn.None, Action<AlertBtnType, object> callBack = null, bool destroyOnClick = true)
    {
        return null;
    }

    public UiDialog SetData(object obj)
    {
        return this;
    }

}
