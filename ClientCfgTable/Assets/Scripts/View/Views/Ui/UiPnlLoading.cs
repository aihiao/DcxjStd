using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using ClientCommon;

public class UiPnlLoading : BaseUi
{
    private Transform progressBar;
    private UITexture bgTexture;
    private UISlider slider;
    private TweenAlpha tweenAlpha;

    private UILabel desLabel;
    private UILabel bottomDesLabel;

    public LoadingProgress loadingProgress = new LoadingProgress();

    public override void InitializeCommonGameObjectLink()
    {
        base.InitializeCommonGameObjectLink();
    }

    /// <summary>
    /// 改变进度
    /// </summary>
    /// <param name="value"></param>
    public void SetSliderValue(float value)
    {
        this.slider.value = value;
    }

}

public class LoadingProgress
{

    public float CurrentAsyncLoadingProgress
    {
        get
        {
            float originalValue = 0f;
            return originalValue;
        }
    }

}

