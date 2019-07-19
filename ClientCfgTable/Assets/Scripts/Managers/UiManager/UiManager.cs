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
        if (firstUiList.Contains(type) || showingUiDic.ContainsKey(type))
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
            InitUi(ui);
        }

        // 若没命中cache,根据名字load对应的prefab
        if (ui == null)
        {
            ui = LoadUiPerfabByName(typeName);
            ui.UiPanelName = typeName;
            InitUi(ui);
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
        LoggerManager.Instance.Error("UiManager Show method cannot get type: {0} from typeName.", typeName);
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
        LoggerManager.Instance.Error("UiManager Hide method cannot get type: {0} from typeName.", typeName);
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
        BaseUi currentSelected = null;
        int currentDepth = 0;

        var enumerator = UiRelations.Instance.UiDic.GetEnumerator();
        do
        {
            var tmpUi = enumerator.Current.Value;
            if (tmpUi != null)
            {
                // 过滤不显示的ui
                if (!tmpUi.IsShowing)
                {
                    continue;
                }

                bool muchToper = false;
                if (currentSelected == null)
                {
                    muchToper = true;
                }
                else
                {
                    if (tmpUi.layer > currentSelected.layer)
                    {
                        muchToper = true;
                    }
                    else if (tmpUi.layer == currentSelected.layer)
                    {
                        var tmpPanel = tmpUi.GetComponent<UIPanel>();
                        if (tmpPanel != null)
                        {
                            muchToper = tmpPanel.depth > currentDepth;
                        }
                    }
                    else
                    {
                        muchToper = false;
                    }
                }

                // 自定义的额外过滤条件
                if (muchToper)
                {
                    if (addationCheckCondation != null && (!addationCheckCondation(tmpUi)))
                    {
                        muchToper = false;
                    }
                }

                if (muchToper)
                {
                    currentSelected = tmpUi;
                    var currentSelectedPanel = currentSelected.GetComponent<UIPanel>();
                    if (currentSelectedPanel != null)
                    {
                        currentDepth = currentSelectedPanel.depth;
                    }
                }
            }
        } while (enumerator.MoveNext());

        return currentSelected;
    }

    public void DestroyAll(bool iscludeShowings = false)
    {
        List<BaseUi> toBeRemoveUis = new List<BaseUi>();
        List<string> removeds = new List<string>();
        foreach (var uiPair in UiRelations.Instance.UiDic)
        {
            if (uiPair.Value.ignoreDestory)
            {
                continue;
            }
            if (uiPair.Value.MarkedNotDestroyWhenDestroyAll)
            {
                uiPair.Value.MarkedNotDestroyWhenDestroyAll = false;
            }
            else
            {
                toBeRemoveUis.Add(uiPair.Value);
                removeds.Add(uiPair.Key);
            }
        }

        for (int i = 0; i < removeds.Count; i++)
        {
            if (UiRelations.Instance.UiDic.ContainsKey(removeds[i]))
            {
                UiRelations.Instance.UiDic.Remove(removeds[i]);
            }
        }

        foreach (var baseUi in toBeRemoveUis)
        {
            if (baseUi != null)
            {
                if (baseUi.gameObject.activeSelf)
                {
                    baseUi.Hide();
                }
                baseUi.Destroy();
            }
        }
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
            if (ui.layer == layer && ui.IsShowing && ui.IsVisible)
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
                // 有的界面关闭时会关闭多个, 保存后一起关
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
        UiRelationData relation = UiRelations.Instance.GetUiRelationData(type);
        if (relation == null)
        {
            LoggerManager.Instance.Error("Create ui type: {0} cannot find.", type);
            return null;
        }
        BaseUi ui = UiUtility.LoadUiPerfab(relation.type, relation.resourceName);
        if (ui != null)
        {
            ui.UiPanelName = relation.resourceName;
        }
        InitUi(ui);
        return ui;
    }

    private BaseUi Show(Type type, UiLayer layer = UiLayer.Normal, params object[] dataList)
    {
        HideMutex(type, layer);

        List<Type> linkedList = UiRelations.Instance.GetLinkedList(type);
        if (linkedList == null || linkedList.Count <= 0)
        {
            return null;
        }
        BaseUi mainUi = null;
        Type tempType = linkedList[0];
        linkedList.RemoveAt(0);
        linkedList.Add(tempType);
        for (int i = 0; i < linkedList.Count; i++)
        {
            BaseUi ui = GetUiCreateWhenNotFind(linkedList[i]);
            if (ui != null)
            {
                if (ui.GetType() == type)
                {
                    if (layer != UiLayer.Normal)
                    {
                        ui.layer = layer;
                    }
                    mainUi = ui;
                }
                ui.Show(dataList);
            }
        }
        return mainUi;
    }

    private BaseUi Show(BaseUi ui, UiLayer layer = UiLayer.Normal, params object[] dataList)
    {
        HideMutex(ui.GetType(), layer);
        if (ui != null)
        {
            if (layer != UiLayer.Normal)
            {
                ui.layer = layer;
            }
            ui.Show(dataList);
        }
        return ui;
    }

    public override void Initialize(params object[] parameters)
    {
        uiShell.Initialize();
        firstUiList = new List<Type>
        {

        };
    }

    public override void Dispose()
    {
        DestroyAll(true);
    }

    private void InitUi(BaseUi ui)
    {
        if (ui != null)
        {
            UiRelations.Instance.AddUi(ui);
            ui.OnShow = uiShell.OnShow;
            ui.OnHide = uiShell.OnHide;
            ui.OnBeDestroy = OnDestroyUi;
            ui.RunInitialize();
        }
    }

    private void OnDestroyUi(BaseUi ui)
    {
        if (ui != null)
        {
            UiRelations.Instance.RemoveUi(ui);
            uiShell.OnUiDestroy(ui);
        }
    }

}
