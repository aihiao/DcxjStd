using System;
using System.Collections.Generic;
using UnityEngine;
using LywGames;

public enum UiEvent : int
{
    OnShow,
    OnHide
}

public abstract class BaseUi : BaseMonoBehaviour
{
    public Action<BaseUi> OnShow;
    public Action<BaseUi> OnHide;
    public Action<BaseUi> OnBeDestroy;
    public Action<BaseUi> OnInitialize;

    [HideInInspector]
    [SerializeField]
    public List<UIToggle> selectedToggles;

    public bool model = false;
    public bool autoClickHide = false;
    public bool destroyOnClose = false;

    [HideInInspector]
    public UIPanel modelBackground;

    protected bool isMainUI = false;

    [HideInInspector]
    public bool ignoreDestory = false;

    private bool markedNotDestroyWhenDestroyAll = false;
    public bool MarkedNotDestroyWhenDestroyAll
    {
        get { return markedNotDestroyWhenDestroyAll; }
        set { markedNotDestroyWhenDestroyAll = value; }
    }

    protected Dictionary<string, UILabel> descUiLabels = new Dictionary<string, UILabel>();
    protected Dictionary<string, UITexture> descUiIcons = new Dictionary<string, UITexture>();

    private bool hasInitedGameObjectLink = false;

    private bool isRedDotShowingDirty = false;
    private int checkRedDotShowingCounter = 0;

    private string uiPanelName;
    public string UiPanelName
    {
        get { return uiPanelName; }
        set { uiPanelName = value; }
    }

    public virtual void InitializeGameObjectLink()
    {

    }

    public virtual void InitializeCommonGameObjectLink()
    {

    }

    public virtual void OnReturn(GameObject obj)
    {

    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Destroy()
    {
        base.Destroy();
    }

    public virtual void OnPopUi()
    {

    }

    public virtual void Hide()
    {

    }

    public virtual bool IsCanStartGuide()
    {
        return true;
    }

    public virtual void Guide(int guideType, Action<Transform> foundCallBack)
    {

    }

    public virtual void OnBack()
    {

    }

    public void BlandGameObjectLinkIfDidnt()
    {

    }

    void SetLogicUiLayerByObjectLayer()
    {

    }

    public virtual void Show(params object[] dataList)
    {

    }

    protected virtual void CheckGuide()
    {

    }

    protected virtual void ChangeVisible()
    {

    }

    protected void AddToBackList()
    {

    }

    public virtual void MenuNavigation(List<string> args)
    {

    }

    public virtual void RefreshData(object data)
    {

    }

    private Dictionary<UiEvent, List<Action<UiEvent, BaseUi>>> listeners = new Dictionary<UiEvent, List<Action<UiEvent, BaseUi>>>();

    public void AddListener(UiEvent type, Action<UiEvent, BaseUi> callBack)
    {

    }

    public void RemoveListener(UiEvent type, Action<UiEvent, BaseUi> callBack)
    {

    }

    protected void ExecuteEvent(UiEvent type)
    {

    }

    public UiLayer layer = UiLayer.Normal;
    private bool isVisible;
    public bool IsVisible { get { return isVisible; } }

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

    public virtual void Overlaid()
    {

    }

    public virtual void RemoveOverlay()
    {

    }

    protected void ModifyLabel(string key, params string[] text)
    {

    }

    protected void ActiveLabel(string key, bool isActive)
    {

    }

    protected void ModifyIcon(string key, string iconAsset)
    {

    }

    private void DataEventAllPanelRedDotRefreshEvent()
    {

    }

    protected virtual void UpdateRedDotShowingInPanel()
    {

    }

    protected void CheckToUpdateRedDotShowing()
    {

    }

    private void LateUpdate()
    {
        
    }

}

public class MenuOnBackManager
{
    public static Stack<BaseUi> menuList = new Stack<BaseUi>();

    public static void AddMenu<T>(T menu) where T : BaseUi
    {
        if (menu != null)
        {
            menuList.Push(menu);
        }
    }

    public static BaseUi GetCurrentUi()
    {
        return menuList.Peek();
    }
}