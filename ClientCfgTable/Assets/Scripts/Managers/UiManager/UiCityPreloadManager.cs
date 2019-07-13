using System;
using System.Collections;
using System.Collections.Generic;

public class UiCityPreloadManager : AbsManager<UiCityPreloadManager>
{
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

        }
    }

    [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
    public IEnumerator StartPreloadAsync()
    {
        yield return null;
    }

    private bool CheckFuncUnlock(string name)
    {
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
