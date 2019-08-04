using UnityEngine;
using System.Collections;

public class UiPnlTipIndicator : BaseUi
{
    public static void CloseIndicatorIfShowing()
    {
        if (UiManager.Instance.GetIsShowing<UiPnlTipIndicator>())
        {
            UiManager.Instance.Hide<UiPnlTipIndicator>();
        }
    }

    public static void ShowIndicatorIfNot()
    {
        if (!UiManager.Instance.GetIsShowing<UiPnlTipIndicator>())
        {
            UiManager.Instance.ShowByName(UiPrefabNames.UiPnlTipIndicator);
        }
    }

}
