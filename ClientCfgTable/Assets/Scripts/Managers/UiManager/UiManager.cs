using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1. 管理Ui创建销毁
/// 2. 对外接口, 打开关闭界面, 获取界面显、隐状态
/// </summary>
public class UiManager : AbsManager<UiManager>
{
    private List<Type> firstUiList; // 记录固定显示的界面, 不添加到显示列表
    private Dictionary<Type, BaseUi> showingUiDic = new Dictionary<Type, BaseUi>();
    private List<Type> showingList = new List<Type>();

    public void AddShowingDic(Type type, BaseUi ui)
    {
        if (firstUiList.Contains(type))
        {
            return;
        }
        if (showingUiDic.ContainsKey(type))
        {
            return;
        }
        showingList.Add(type);
        showingUiDic.Add(type, ui);
    }

    public void RemoveShowingDic(Type type)
    {
        if (showingList.Count == 0 || type == null || (!showingUiDic.ContainsKey(type)))
        {
            return;
        }
        BaseUi baseUi = GetCurShowingUi();
        if (baseUi != null)
        {
            baseUi.OnPopUi();
        }
        else
        {

        }
        showingList.Remove(type);
        showingUiDic.Remove(type);
    }

    public BaseUi GetCurShowingUi()
    {
        if (showingList.Count == 0)
        {
            return null;
        }
        var item = showingList[0];
        while(item != null)
        {
            if (showingUiDic[item].IsShowing)
            {
                return showingUiDic[item];
            }
            showingList.Remove(item);
            if (showingList.Count > 0)
            {
                item = showingList[0];
            }
            else
            {
                item = null;
            }
        }
        return null;
    }

    public void CloseAllShowingUi()
    {
        for (int i = 0; i < showingList.Count; i++)
        {
            if (showingUiDic.ContainsKey(showingList[i]))
            {
                if (showingUiDic[showingList[i]].IsShowing)
                {
                    showingUiDic[showingList[i]].Hide();
                }
            }
        }
        for (int i = 0; i < firstUiList.Count; i++)
        {
            Hide(firstUiList[i]);
        }
    }

    private UiShell uiShell = UiShell.Instance;
    public UiShell UiShell { get { return uiShell; } }

    public T SetIsShowing<T>(bool isShowing) where T : BaseUi
    {
        if (isShowing)
        {
            Hide<T>();
            return Show<T>();
        }
        return Hide<T>();
    }

    public BaseUi ShowByName(string typeName, params object[] dataList)
    {
        Type t = Type.GetType(typeName, false, true);
        // 检查cache
        BaseUi ui = UiCityPreloadManager.Instance.GetCacheUi(t);
        if (ui == null)
        {
            ui = UiRelations.Instance.GetUi(t);
        }
        else
        {
            ui.UiPanelName = typeName;
            initUi(ui);
        }

        // 若没命中cache,根据名字load对应的prefab
        if (ui == null)
        {
            ui = LoadUiPerfabByName(typeName);
            ui.UiPanelName = typeName;
            initUi(ui);
        }
        return Show(ui, ui.layer, dataList);
    }

    private BaseUi LoadUiPerfabByName(string typeName)
    {
        GameObject go = GameObjectUtility.CreateGameObject(typeName);
        go.SetActive(false);
        Type type = Type.GetType(typeName);
        Type t = Type.GetType(typeName, false, true);
        var cmp = go.AddComponent(t);

        var cmpUi = cmp as BaseUi;
        if (cmpUi != null)
        {
            cmpUi.BlandGameObjectLinkIfDidnt();
        }
        return cmpUi;
    }

    public T Show<T>(params object[] dataList) where T : BaseUi
    {
        BaseUi ui = GetUiCreateWhenNotFind(typeof(T));
        return (T)Show(typeof(T), ui.layer, dataList);
    }

    public BaseUi Show(Type type, params object[] dataList)
    {
        BaseUi ui = GetUiCreateWhenNotFind(type);
        return Show(type, ui.layer, dataList);
    }

    public BaseUi Show(string typeName, params object[] dataList)
    {
        Type t = Type.GetType(typeName, false, true);
        if (t != null)
        {
            return Show(t, dataList);
        }
        return null;
    }

    public T Hide<T>() where T : BaseUi
    {
        return (T)Hide(typeof(T));
    }

    public BaseUi Hide(string typeName)
    {
        Type t = Type.GetType(typeName, false, true);
        if (t != null)
        {
            return Hide(t);
        }
        return null;
    }

    public BaseUi Hide(Type type)
    {
        BaseUi ui = UiRelations.Instance.GetUi(type);
        if (ui != null)
        {
            ui.Hide();
        }
        return ui;
    }

    public T GetUi<T>(bool createIfNotFind = false) where T : BaseUi
    {
        if (createIfNotFind)
        {
            return (T)GetUiCreateWhenNotFind(typeof(T));
        }
        return (T)UiRelations.Instance.GetUi(typeof(T));
    }

    public BaseUi GetUiCreateWhenNotFind(Type type)
    {
        BaseUi ui = UiCityPreloadManager.Instance.GetCacheUi(type);
        if (ui != null)
        {
            return ui;
        }
        ui = UiRelations.Instance.GetUi(type);
        if (ui == null)
        {
            return Create(type);
        }
        return ui;
    }

    public BaseUi CreateUiNotShow(Type type)
    {
        GameObject go = GameObjectUtility.CreateGameObject(type.Name);
        go.SetActive(false);
        var cmp = go.AddComponent(type);
        var cmpUi = cmp as BaseUi;
        if (cmpUi != null)
        {
            cmpUi.BlandGameObjectLinkIfDidnt();
        }
        return cmpUi;
    }

    public bool GetIsShowing<T>()
    {
        return UiRelations.Instance.GetIsShowing(typeof(T));
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public BaseUi GetTopestShowingUi(Func<BaseUi, bool> addationCheckCondation = null)
    {
        return null;
    }

    public void DestroyAll(bool iscludeShowings = false)
    {

    }

    public void CloseAllUiByUiLayer(params UiLayer[] layers)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            CloseAllUiByUiLayer(layers[i]);
        }
    }

    public void CloseAllUiByUiLayer(UiLayer layer)
    {
        List<BaseUi> boBeHideUi = null;
        foreach (var ui in UiRelations.Instance.UiDic.Values)
        {
            if (ui.layer == layer && ui.IsShowing && ui.IsShowing && ui.IsVisible)
            {
                if (boBeHideUi == null)
                {
                    boBeHideUi = new List<BaseUi>();
                }
                boBeHideUi.Add(ui);
            }
        }

        if (boBeHideUi != null)
        {
            for (int i = 0; i < boBeHideUi.Count; i++)
            {
                boBeHideUi[i].Hide();
            }
        }
    }

    public void CloseAllUiByUiLayer(List<UiLayer> layers)
    {
        List<BaseUi> toBeHideUi = null;
        foreach (var ui in UiRelations.Instance.UiDic.Values)
        {
            if (layers.Contains(ui.layer) && ui.IsShowing)
            {
                if (toBeHideUi == null)
                {
                    toBeHideUi = new List<BaseUi>();
                }
                toBeHideUi.Add(ui);
            }
        }
        if (toBeHideUi != null)
        {
            for (int i = 0; i < toBeHideUi.Count; i++)
            {
                toBeHideUi[i].Hide();
            }
        }
    }

    public static bool IsPosInsideScreen(Vector3 screenPos)
    {
        int offset = 1;
        if (screenPos.x < -offset || screenPos.y < -offset || screenPos.x > (Screen.width + offset) || screenPos.y > (Screen.height + offset))
        {
            return false;
        }
        return true;
    }

    private void HideMutex(Type type, UiLayer layer)
    {

    }

    private BaseUi Create(Type type)
    {
        return null;
    }

    private BaseUi Show(Type type, UiLayer layer = UiLayer.Normal, params object[] dataList)
    {
        return null;
    }

    private BaseUi Show(BaseUi ui, UiLayer layer = UiLayer.Normal, params object[] dataList)
    {
        return null;
    }

    public override void Initialize(params object[] parameters)
    {
        base.Initialize(parameters);
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    private void initUi(BaseUi ui)
    {

    }

    private void OnDestroyUi(BaseUi ui)
    {

    }

}
