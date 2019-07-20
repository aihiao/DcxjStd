using System;
using System.Collections.Generic;
using UnityEngine;
using ClientCommon;

public enum UiEvent : int
{
    OnShow,
    OnHide
}

public abstract class BaseUi : BaseMonoBehaviour
{
    public Action<BaseUi> OnInitialize; // ui的初始化事件
    public Action<BaseUi> OnShow; // ui的显示事件, 这里在BaseUi里反向调用UiManager辅助类UiShell的显示方法
    public Action<BaseUi> OnHide; // ui的隐藏事件, 这里在BaseUi里反向调用UiManager辅助类UiShell的隐藏方法
    public Action<BaseUi> OnDestroyUi; // ui的销毁事件, 这里在BaseUi里反向调用UiManager类的销毁方法

    [HideInInspector]
    [SerializeField]
    public List<UIToggle> selectedToggles;

    public bool model = false;
    public bool autoClickHide = false;
    public bool destroyOnClose = false; // 关闭时是否销毁, 默认没销毁

    [HideInInspector]
    public UIPanel modelBackground; // UIPnlModelBackground

    protected bool isMainUI = false;

    [HideInInspector]
    public bool ignoreDestory = false;

    // 在下次UiManager  Destroy all的时候是否销毁, 目前只用于使得loading bar不被自动销毁
    private bool markedNotDestroyWhenDestroyAll = false;
    public bool MarkedNotDestroyWhenDestroyAll
    {
        get { return markedNotDestroyWhenDestroyAll; }
        set { markedNotDestroyWhenDestroyAll = value; }
    }

    protected Dictionary<string, UILabel> descUiLabels = new Dictionary<string, UILabel>();
    protected Dictionary<string, UITexture> descUiIcons = new Dictionary<string, UITexture>();

    private bool hasInitedGameObjectLink = false;
   
    // 界面中显示的小红点是否需要刷新
    private bool isRedDotShowingDirty = false;
    private int checkRedDotShowingCounter = 0;
  
    // 运行时的界面名, 和界面的.cs类型一致
    private string uiPanelName;
    public string UiPanelName
    {
        get { return uiPanelName; }
        set { uiPanelName = value; }
    }

    /// <summary>
	/// 在show之前调用, pnl里有需要查找到某些东西, 绑定关联关系的操作需要首先在这个函数里实现, 以供给Show方法使用
	/// </summary>
    public virtual void InitializeGameObjectLink()
    {

    }

    /// <summary>
	/// 一些通用的初始化
	/// </summary>
    public virtual void InitializeCommonGameObjectLink()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "GameHelpPannelEnterButton")
            {
                var parentTrans = transform.GetChild(i);
                if (parentTrans.childCount == 0)
                {
                    var helpBtnExe = ResourceManager.Instance.InstantiateAsset<GameObject>(AssetType.UI, "GameHelpPannelEnterButtonExec", true);
                    helpBtnExe.transform.parent = parentTrans;
                    helpBtnExe.transform.localPosition = Vector3.one;
                    helpBtnExe.transform.localRotation = Quaternion.identity;
                    helpBtnExe.layer = parentTrans.gameObject.layer;

                    var helpBtn = helpBtnExe.GetComponent<UIButton>();
                    if (helpBtn != null)
                    {
                        helpBtn.onClick.Clear();
                        helpBtn.events.onClick = go =>
                        {
                            UiManager.Instance.ShowByName(UiPrefabNames.UiPnlHelp, UiPanelName);
                        };
                    }
                }
            }
        }
    }

    public virtual void OnReturn(GameObject obj)
    {
        Hide();
    }

    public override void Initialize()
    {
        base.Initialize();
        if (OnInitialize != null)
        {
            OnInitialize(this);
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        if (OnDestroyUi != null)
        {
            OnDestroyUi(this);
        }
        Destroy(gameObject);
    }

    // 关闭一个界面后发消息 执行一些特殊操作
    public virtual void OnPopUi()
    {

    }

    [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
    public virtual void Hide()
    {
        if (isShowing)
        {
            ExecuteEvent(UiEvent.OnHide);
            if (OnHide != null)
            {
                OnHide(this);
            }
            UiManager.Instance.RemoveShowingDic(GetType());

            if (MenuOnBackManager.menuList.Count > 0 && isMainUI)
            {
                MenuOnBackManager.menuList.Pop(); 

                if (MenuOnBackManager.menuList.Peek() != null) // 有可能已将销毁了
                {
                    MenuOnBackManager.menuList.Peek().OnBack();
                }
            }
        }

        isShowing = false;
        isVisible = false;
    }

    /// <summary>
	/// 是否该界面允许指引, 如果某些事儿需要做完之后才允许指引
	/// </summary>
	/// <returns></returns>
    public virtual bool IsCanStartGuide()
    {
        return true;
    }

    /// <summary>
	/// 指引目标
	/// </summary>
	/// <param name="guideType">指引类型ClientCommon.GuideTargetType</param>
	/// <param name="child">指引参数，child.TargetParam</param>
	/// <returns></returns>
    public virtual void Guide(int guideType, GuideChild child, Action<Transform> foundCallBack)
    {
        
    }

    /// <summary>
	/// 从别的界面返回
	/// </summary>
    public virtual void OnBack()
    {

    }

    public void BlandGameObjectLinkIfDidnt()
    {
        if (!hasInitedGameObjectLink)
        {
            SetLogicUiLayerByObjectLayer();
            InitializeGameObjectLink();
            InitializeCommonGameObjectLink();
            LoadAudioRes(transform);
            hasInitedGameObjectLink = true;
        }
    }

    void SetLogicUiLayerByObjectLayer()
    {
        int layer = gameObject.layer;
        UiLayer logicLayer = UiLayer.Normal;

        if (layer == LayerMask.NameToLayer(UiLayer.TopMost.ToString()))
        {
            logicLayer = UiLayer.TopMost;
        }
        else if (layer == LayerMask.NameToLayer(UiLayer.Top.ToString()))
        {
            logicLayer = UiLayer.Top;
        }
        else if (layer == LayerMask.NameToLayer(UiLayer.ShelterModel.ToString()))
        {
            logicLayer = UiLayer.ShelterModel;
        }
        else if (layer == LayerMask.NameToLayer(UiLayer.UiModel.ToString()))
        {
            logicLayer = UiLayer.UiModel;
        }
        else if (layer == LayerMask.NameToLayer(UiLayer.Normal.ToString()))
        {
            logicLayer = UiLayer.Normal;
        }
        else if (layer == LayerMask.NameToLayer(UiLayer.Bottom.ToString()))
        {
            logicLayer = UiLayer.Bottom;
        }
        else if (layer == LayerMask.NameToLayer(UiLayer.BottomMost.ToString()))
        {
            logicLayer = UiLayer.BottomMost;
        }

        this.layer = logicLayer;
    }

    static void LoadAudioRes(Transform trans)
    {
       
    }

    public virtual void Show(params object[] dataList)
    {
        BlandGameObjectLinkIfDidnt();

        if (!isShowing)
        {
            ExecuteEvent(UiEvent.OnShow);
            if (OnShow != null)
            {
                OnShow(this);
            }
            UiManager.Instance.AddShowingDic(GetType(), this);

            if (selectedToggles != null)
            {
                for (int i = 0; i < selectedToggles.Count; i++)
                {
                    if (selectedToggles[i] != null)
                    {
                        selectedToggles[i].value = true;
                    }
                }
            }
        }

        int layer = gameObject.layer;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.layer != layer)
            {
                transform.GetChild(i).gameObject.layer = layer;
            }
        }
        gameObject.SetActive(false);
        isVisible = false;
        isShowing = true;
        ChangeVisible();
    }

    /// <summary>
	/// 检测新手指引
	/// </summary>
    protected virtual void CheckGuide()
    {

    }

    /// <summary>
	/// 界面显示出来
	/// </summary>
    protected virtual void ChangeVisible()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        isVisible = true;
        CheckGuide();
    }

    /// <summary>
	/// 主城检查解锁, 获得经验之后, 开了这个菜单, 关了之后, 有可能会触发解锁等事件
	/// </summary> 
    protected void AddToBackList()
    {
        MenuOnBackManager.AddMenu(this);
        isMainUI = true;
        if (isVisible)
        {
            CheckGuide();
        }
    }

    /// <summary>
	/// 菜单跳转
	/// </summary>
    public virtual void MenuNavigation(List<string> args)
    {

    }

    public virtual void RefreshData(object data)
    {

    }

    private Dictionary<UiEvent, List<Action<UiEvent, BaseUi>>> listeners = new Dictionary<UiEvent, List<Action<UiEvent, BaseUi>>>();

    public void AddListener(UiEvent type, Action<UiEvent, BaseUi> callBack)
    {
        if (!listeners.ContainsKey(type))
        {
            listeners.Add(type, new List<Action<UiEvent, BaseUi>>());
        }
        if (!listeners[type].Contains(callBack))
        {
            listeners[type].Add(callBack);
        }
    }

    public void RemoveListener(UiEvent type, Action<UiEvent, BaseUi> callBack)
    {
        if (listeners.ContainsKey(type))
        {
            listeners[type].Remove(callBack);
        }
    }

    protected void ExecuteEvent(UiEvent type)
    {
        if (listeners.ContainsKey(type))
        {
            for (int i = 0; i < listeners[type].Count; i++)
            {
                listeners[type][i](type, this);
            }
        }
    }

    public UiLayer layer = UiLayer.Normal;
    private bool isVisible; // 是否处于激活状态
    public bool IsVisible { get { return isVisible; } }

    // 是否界面被UiManager Show出来, 如果true, 在UiManager, UiRelation中可以getUI得到
    private bool isShowing = false;
    public bool IsShowing
    {
        get
        {
            if (this != null && gameObject != null)
            {
                return isShowing && transform.parent != null;
            }
            return false;
        }
    }

    /// <summary>
    /// Ui被覆盖时, 覆盖操作
    /// </summary>
    public virtual void Overlaid()
    {

    }

    /// <summary>
    /// Ui被移除覆盖时, 移除覆盖操作
    /// </summary>
    public virtual void RemoveOverlay()
    {

    }

    protected void ModifyLabel(string key, params string[] text)
    {
        UILabel label;
        if(descUiLabels.TryGetValue(key, out label))
        {
            
        }
    }

    protected void ActiveLabel(string key, bool isActive)
    {
        UILabel label;
        if (descUiLabels.TryGetValue(key, out label))
        {
            label.gameObject.SetActive(isActive);
        }
    }

    protected void ModifyIcon(string key, string iconAsset)
    {
        if (!descUiIcons.ContainsKey(key))
        {
            return;
        }
        
    }

    #region 小红点相关
    /// <summary>
    /// 收到事件后, 设置为dirty
    /// </summary>
    private void DataEventAllPanelRedDotRefreshEvent()
    {
        isRedDotShowingDirty = true;
    }

    /// <summary>
	/// 每个pnl要重载这个函数来实际刷新小红点
	/// </summary>
    protected virtual void UpdateRedDotShowingInPanel()
    {

    }

    /// <summary>
	/// 检查界面中的小红点是否需要刷新(是否dirty)
	/// </summary>
    protected void CheckToUpdateRedDotShowing()
    {
        // 每10帧刷新一次小红点
        checkRedDotShowingCounter++;
        if (checkRedDotShowingCounter >= 10)
        {
            if (isRedDotShowingDirty)
            {
                UpdateRedDotShowingInPanel();
            }
            isRedDotShowingDirty = false;
            checkRedDotShowingCounter = 0;
        }
    }

    private void LateUpdate()
    {
        CheckToUpdateRedDotShowing();
    }
    #endregion

}

public class MenuOnBackManager
{
    public static Stack<BaseUi> menuList = new Stack<BaseUi>();

    public static void AddMenu<T>(T menu) where T : BaseUi
    {
        if (menu != null)
        {
            menuList.Push(menu); // 将对象插入 Stack 的顶部。
        }
    }

    public static BaseUi GetCurrentUi()
    {
        return menuList.Peek(); // 返回位于 Stack 顶部的对象但不将其移除。
    }
}