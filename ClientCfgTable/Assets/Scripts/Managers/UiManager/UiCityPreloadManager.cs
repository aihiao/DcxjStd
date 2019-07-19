using System;
using System.Collections;
using System.Collections.Generic;
using ClientCommon;

public class UiCityPreloadManager : AbsManager<UiCityPreloadManager>
{
    // 预加载状态
    protected enum PreloadState
    {
        Init, // 初始化
        WaitLoad, // 等待加载
        Loading, // 加载中
        Done // 加载完成
    }

    protected class PreloadData
    {
        public Type uiType;
        public BaseUi baseUi;
        public PreloadState loadState;

        public PreloadData(Type type)
        {
            uiType = type;
            loadState = PreloadState.Init;
        }
    }

    protected List<PreloadData> cityPreloadList = new List<PreloadData>(); // 需要预加载的界面
    public override void Initialize(params object[] parameters)
    {
        base.Initialize(parameters);
        cityPreloadList = new List<PreloadData>
        {
            new PreloadData(typeof(UiPnlTipIndicator)),
            new PreloadData(typeof(UiPnlTextTip)),
            new PreloadData(typeof(UiPnlBag)),
            new PreloadData(typeof(UiPnlShop)),
            new PreloadData(typeof(UiPnlFaction)),
            new PreloadData(typeof(UiPnlMeridian))
        };
    }

    protected int curLoadIndex; // 当前加载索引
    protected const float loadRate = 0.5f;
    protected void SetLoadState()
    {
    }
    protected bool CheckLoadOver()
    {
        return (curLoadIndex == (cityPreloadList.Count - 1) && cityPreloadList[curLoadIndex].loadState == PreloadState.Done);
    }

    public void StartPreLoad()
    {
        curLoadIndex = 0;
        for (int i = 0; i < cityPreloadList.Count; i++)
        {
            var list = ConfigDataBase.MenuNavigationConfig.MenuNavigations;
            for (int j = 0; j < list.Count; j++)
            {
                if (cityPreloadList[i].uiType.Name.Equals(list[j].UiRegisterName))
                {
                    if (UiNavigationTool.IsUnlockPanel(list[j].Id))
                    {
                        PreloadData data = cityPreloadList[i];
                        data.baseUi = UiManager.Instance.CreateUiNotShow(data.uiType);
                    }
                }
            }
        }
    }

    [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
    public IEnumerator StartPreloadAsync()
    {
        curLoadIndex = 0;
        for (int i = 0; i < cityPreloadList.Count; i++)
        {
            if (CheckFuncUnlock(cityPreloadList[i].uiType.Name))
            {
                PreloadData data = cityPreloadList[i];
                data.baseUi = UiManager.Instance.CreateUiNotShow(data.uiType);
            }
            yield return null;
        }

        yield return null;
    }

    private bool CheckFuncUnlock(string name)
    {
        var list = ConfigDataBase.MenuNavigationConfig.MenuNavigations;
        for (int i = 0; i < list.Count; i++)
        {
            if (name.Equals(list[i].UiRegisterName))
            {
                if (UiNavigationTool.IsUnlockPanel(list[i].Id))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public BaseUi GetCacheUi(Type type)
    {
        for (int i = 0; i < cityPreloadList.Count; i++)
        {
            if (type == cityPreloadList[i].uiType)
            {
                return cityPreloadList[i].baseUi;
            }
        }
        return null;
    }

    public bool NeedCache(BaseUi ui)
    {
        for (int i = 0; i < cityPreloadList.Count; i++)
        {
            if (ui == cityPreloadList[i].baseUi)
            {
                return true;
            }
        }

        return false;
    }

    [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
    private void Preload()
    {
        PreloadData data = cityPreloadList[curLoadIndex];
        UiRelationData relation = UiRelations.Instance.GetUiRelationData(data.uiType);
        UiUtility.LoadUiPerfab(data.uiType, relation.resourceName);
        data.loadState = PreloadState.Done;
        if (!CheckLoadOver())
        {
            curLoadIndex++;
            Invoke("Preload", loadRate);
        }
    }

}
