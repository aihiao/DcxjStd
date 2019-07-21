using System;
using System.Collections.Generic;
using UnityEngine;
using ClientCommon;

/// <summary>
/// 1. 管理Ui创建销毁
/// 2. 对外接口, 打开关闭界面, 获取界面显、隐状态
/// </summary>
public class UiManager : AbsManager<UiManager>
{
    private UiShell uiShell = UiShell.Instance; // UiManager的辅助类
    public UiShell UiShell { get { return uiShell; } }

    private List<Type> firstUiList; // 记录固定显示的界面, 不添加到显示列表

    #region 所有显示的ui
    // 显示ui的容器
    private Dictionary<Type, BaseUi> showingUiDic = new Dictionary<Type, BaseUi>();
    private List<Type> showingList = new List<Type>();
  
    /// <summary>
    /// 添加显示ui, 从BaseUi的显示方法里调用过来
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ui"></param>
    public void AddShowingDic(Type type, BaseUi ui)
    {
        if (firstUiList.Contains(type) || showingUiDic.ContainsKey(type))
        {
            return;
        }
        showingList.Add(type);
        showingUiDic.Add(type, ui);
    }

    /// <summary>
    /// 移除显示ui, 从BaseUi的隐藏方法调用过来
    /// </summary>
    /// <param name="type"></param>
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
            // 界面没打开时不能触发。这地方关联有点多, 有空要优化下。
            if (GetUi<UiPnlMainCityMenu>() != null)
            {
                GetUi<UiPnlMainCityMenu>().OnPopUi();
            }
        }
        showingList.Remove(type);
        showingUiDic.Remove(type);
    }

    /// <summary>
    /// 获取当前显示的ui
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 关闭所有显示的ui
    /// </summary>
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
    #endregion 所有显示的ui

    /// <summary>
    /// 通过名字显示ui
    /// </summary>
    /// <param name="typeName"></param>
    /// <param name="dataList"></param>
    /// <returns></returns>
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
            // 在UiCityPreloadManager预加载的ui需要进行这一步
            ui.UiPanelName = typeName;
            InitUi(ui);
        }

        // 若没有cache, 根据名字load对应的prefab
        if (ui == null)
        {
            ui = LoadUiPrefabByName(typeName);
            ui.UiPanelName = typeName;
            InitUi(ui);
        }

        return Show(ui, ui.layer, dataList);
    }

    /// <summary>
    /// 通过名字加载ui prefab
    /// </summary>
    /// <param name="typeName"></param>
    /// <returns></returns>
    private BaseUi LoadUiPrefabByName(string typeName)
    {
        GameObject go = GameObjectUtility.CreateGameObject(typeName);
        go.SetActive(false);
        Type t = Type.GetType(typeName, false, true);
        var cmp = go.AddComponent(t);

        var cmpUi = cmp as BaseUi;
        if (cmpUi != null)
        {
            cmpUi.BlandGameObjectLinkIfDidnt();
        }
        return cmpUi;
    }

    /// <summary>
    /// ui prefab被创建后, 初始化
    /// </summary>
    /// <param name="ui"></param>
    private void InitUi(BaseUi ui)
    {
        if (ui != null)
        {
            UiRelations.Instance.AddUi(ui);
            ui.OnShow = uiShell.OnShow;
            ui.OnHide = uiShell.OnHide;
            ui.OnDestroyUi = OnDestroyUi;
            ui.RunInitialize();
        }
    }

    /// <summary>
    /// ui显示方法, 调用BaseUi的显示方法
    /// </summary>
    /// <param name="ui"></param>
    /// <param name="layer"></param>
    /// <param name="dataList"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 这个创建ui, prefab本身已经绑定了脚本, 但是也没有调用BaseUi的BlandGameObjectLinkIfDidnt方法啊???
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
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
            InitUi(ui);
        }
        return ui;
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

    /// <summary>
    /// 显示Ui, 以及关联Ui
    /// </summary>
    /// <param name="type"></param>
    /// <param name="layer"></param>
    /// <param name="dataList"></param>
    /// <returns></returns>
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

    public T SetIsShowing<T>(bool isShowing) where T : BaseUi
    {
        if (isShowing)
        {
            Hide<T>();
            return Show<T>();
        }
        return Hide<T>();
    }

    /// <summary>
    /// 创建ui, 虽然没有调用InitUi方法, 但是在Show的时候会调用
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
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

    public T GetUi<T>(bool createIfNotFind = false) where T : BaseUi
    {
        if (createIfNotFind)
        {
            return (T)GetUiCreateWhenNotFind(typeof(T));
        }
        return (T)UiRelations.Instance.GetUi(typeof(T));
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

    public bool GetIsShowing<T>()
    {
        return UiRelations.Instance.GetIsShowing(typeof(T));
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

    private void HideMutex(Type type, UiLayer layer)
    {

    }

    /// <summary>
    /// 关闭所有显示的ui
    /// </summary>
    /// <param name="layers"></param>
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

    /// <summary>
    /// 销毁所有ui
    /// </summary>
    /// <param name="iscludeShowings"></param>
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

    public static bool IsPosInsideScreen(Vector3 screenPos)
    {
        int offset = 1;
        if (screenPos.x < -offset || screenPos.y < -offset || screenPos.x > (Screen.width + offset) || screenPos.y > (Screen.height + offset))
        {
            return false;
        }
        return true;
    }

    #region 框架方法
    public override void Initialize(params object[] parameters)
    {
        uiShell.Initialize();
        firstUiList = new List<Type>
        {
            typeof(UiPnlMainCityMenu),
            typeof(UiPnlRoleInfo),
            typeof(UiPnlCentralCityMessage),
            typeof(UiPnlDialogSystem)
        };
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Application.platform == RuntimePlatform.Android && Input.GetKeyUp(KeyCode.Escape))
        {
            bool guideShowing = (UiManager.Instance != null && UiManager.Instance.GetUi<UiPnlGuide>() != null && UiManager.Instance.GetUi<UiPnlGuide>().IsShowing);
            if (!guideShowing)
            {
                var emulator = UiRelations.Instance.UiDic.GetEnumerator();
                var topMostUi = GetTopestShowingUi((BaseUi ui) =>
                {
                    var uiCfg = ConfigDataBase.UiPannelConfig.Get(ui.UiPanelName);
                    if (uiCfg != null && uiCfg.CanCloseByBackKey)
                    {
                        return true;
                    }
                    return false;
                });

                if (topMostUi != null)
                {
                    LoggerManager.Instance.Info("On escape, close : " + topMostUi.UiPanelName);
                    topMostUi.OnReturn(topMostUi.gameObject);
                }
                else
                {
                    Platform.Instance.Exit(GameUtility.GetUiString("UIPnlMessage"), GameUtility.GetUiString("UIPnlMessage_Exit"), GameUtility.GetUiString("UIPnlMessage_Ok"), GameUtility.GetUiString("UIPnlMessage_Cancel"));
                }
            }
        }
    }

    public override void Dispose()
    {
        UiModelTool.DeleteAllModel(); // 销毁所有界面模型
        DestroyAll(true);
    }
    #endregion 框架方法

    /// <summary>
    /// 销毁ui, 被BaseUi反向调用
    /// </summary>
    /// <param name="ui"></param>
    public void OnDestroyUi(BaseUi ui)
    {
        if (ui != null)
        {
            UiRelations.Instance.RemoveUi(ui);
            uiShell.OnDestroyUi(ui);
        }
    }

}
