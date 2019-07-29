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
    // TODO : 上次注释我说了YES和OK是同一样的意义, 如果对我写的注释有不同意见, 要跟我商量.
    public UIButton YESBtn;
    public static string yesLabel = "YES";

    public UIButton OKBtn;
    public static string okLabel = "OK";

    public UIButton NOBtn;
    public static string noLabel = "NO";

    public UIButton CANCELBtn;
    public static string cancelLabel = "CANCEL";

    public UIButton CLOSEBtn;
    public static string closeLabel = "CLOSE";

    public UISprite innerPanel;
    public UILabel title;
    public UILabel content;
    private object _data;

    public UiDialog Set(string contentText, uint btnFlag = UiDialogBtn.None, Action<AlertBtnType, object> callBack = null, bool destroyOnClick = true)
    {
        return null;
    }

    public UiDialog SetData(object obj)
    {
        return this;
    }

}
