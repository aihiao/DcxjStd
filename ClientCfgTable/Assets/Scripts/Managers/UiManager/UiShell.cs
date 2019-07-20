using System.Collections.Generic;
using UnityEngine;
using LywGames;

/// <summary>
/// UiManager的辅助类, 创建UIRoot和UICamera, 显示和关闭窗口的通用操作
/// </summary>
public class UiShell : AutoCreateSingleton<UiShell>
{
    private GameObject uiRootGo; // uiRoot游戏物体

    private Dictionary<UiLayer, GameObject> layerGoDic; // 每一层的游戏物体集合, 层对应游戏物体
    private Dictionary<string, Camera> layerCameraDic; // 每一层的摄像机集合, 层名称对应摄像机

    private UiAutoDepth autoDepth; // Ui深度控制

    public void Initialize()
    {
        LoggerManager.Instance.Info("Dpi:{0} Width:{1} Height:{2}", Screen.dpi, Screen.width, Screen.height);

        UIRoot uiRoot;
        var go = InitUiRoot(out uiRoot);
        go.AddComponent<Rigidbody>().useGravity = false;
        Object.DontDestroyOnLoad(uiRoot.gameObject);

        InitLayers();
        UIRoot.list.Add(uiRoot);
    }

    /// <summary>
    /// 初始化UiRoot
    /// </summary>
    /// <param name="uiRoot"></param>
    /// <returns></returns>
    private GameObject InitUiRoot(out UIRoot uiRoot)
    {
        GameObject go = GameObjectUtility.CreateGameObject();
        go.name = Defines.UIRoot;
        uiRootGo = go;
        uiRoot = go.AddComponent<UIRoot>();
        uiRoot.scalingStyle = UIRoot.Scaling.ConstrainedOnMobiles;
        uiRoot.fitHeight = uiRoot.fitWidth = true;
        uiRoot.manualWidth = Defines.ScreenWidth;
        uiRoot.manualHeight = Defines.ScreenHeight;
        uiRoot.gameObject.layer = LayerMask.NameToLayer("UI");
        return go;
    }

    /// <summary>
    /// 初始化所有层
    /// </summary>
    private void InitLayers()
    {
        layerGoDic = new Dictionary<UiLayer, GameObject>();
        layerCameraDic = new Dictionary<string, Camera>();
        List<UiLayer> layers = new List<UiLayer>();
        for (UiLayer i = UiLayer.TopMost; i >= UiLayer.BottomMost; i--)
        {
            layers.Add(i);
            CreateNewLayer(i, uiRootGo, (int)i * 1000);
        }
        autoDepth = new UiAutoDepth(layers.ToArray());
    }

    /// <summary>
    /// 创建层
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="goParent"></param>
    /// <param name="depthStart"></param>
    private void CreateNewLayer(UiLayer layer, GameObject goParent, int depthStart)
    {
        GameObject go = GameObjectUtility.CreateGameObject();
        go.name = layer.ToString();
        go.AddComponent<UIPanel>().depth = depthStart;
        GameObjectUtility.AddUiChild(goParent, go);
        go.layer = LayerMask.NameToLayer(layer.ToString());
        layerGoDic[layer] = go;
        CreateLayerCamera(layer, LayerMask.NameToLayer(layer.ToString()), go);
    }

    /// <summary>
    ///  创建每一层的摄像机
    /// </summary>
    /// <param name="layer"></param>
    /// <param name="layerMask"></param>
    /// <param name="cameraParent"></param>
    private void CreateLayerCamera(UiLayer layer, int layerMask, GameObject cameraParent)
    {
        GameObject go = GameObjectUtility.CreateGameObject();
        go.name = layer + "Camera";
        Camera camera = go.AddComponent<Camera>();
        UICamera uiCamera = go.AddComponent<UICamera>();
        uiCamera.eventReceiverMask = (1 << layerMask);
        uiCamera.allowMultiTouch = false;
        camera.clearFlags = CameraClearFlags.Depth;
        camera.cullingMask = (1 << layerMask);
        camera.orthographic = true;
        camera.orthographicSize = 1f;
        camera.nearClipPlane = -10f;
        camera.farClipPlane = 10f;
        camera.depth = (int)layer;
        GameObjectUtility.AddUiChild(cameraParent, go);
        camera.transform.localPosition = new Vector3(0, 0, -10f);
        layerCameraDic.Add(layer.ToString(), camera);
    }

    /// <summary>
    /// 给Ui添加UiPnlModelBackground
    /// </summary>
    /// <param name="ui"></param>
    private void AddBackground(BaseUi ui)
    {
        if (ui.model && ui.modelBackground == null)
        {
            GameObject go = GameObjectUtility.CreateGameObject(UiPrefabNames.UiPnlModelBackground);
            go.SetActive(false);
            UiPnlModelBackground modelBackground = go.GetComponent<UiPnlModelBackground>();

            UiUtility.SetParent(modelBackground.gameObject, ui.gameObject);
            modelBackground.transform.SetAsFirstSibling();
            modelBackground.gameObject.SetActive(true);
            ui.modelBackground = modelBackground.GetComponent<UIPanel>();
        }
    }

    /// <summary>
    /// 给UiPnlModelBackground添加点击事件
    /// </summary>
    /// <param name="ui"></param>
    private void AddClickEvent(BaseUi ui)
    {
        if (ui.model && ui.autoClickHide)
        {
            UiLayer layer = ui.layer;
            UiPnlModelBackground back = ui.modelBackground.GetComponent<UiPnlModelBackground>();
            BoxCollider collider = UiUtility.AddIfMissing<BoxCollider>(back.spriteBack.gameObject);
            collider.isTrigger = true;
            UIEventListener.Get(back.spriteBack.gameObject).onClick = go => ui.Hide();
            UiModelTool.ChangeTransformLayer(ui.modelBackground.gameObject, layer.ToString());
        }
    }

    /// <summary>
    /// ui的显示事件, 通过BaseUi的显示方法调用过来的
    /// </summary>
    /// <param name="ui"></param>
    public void OnShow(BaseUi ui)
    {
        if (!GetLayerCamera(ui.layer.ToString()).transform.parent.gameObject.activeSelf)
        {
            GetLayerCamera(ui.layer.ToString()).transform.parent.gameObject.SetActive(true);
        }
        ui.gameObject.SetActive(true);
        AddBackground(ui);
        AddClickEvent(ui);
        if (layerGoDic.ContainsKey(ui.layer))
        {
            GameObjectUtility.AddUiChild(layerGoDic[ui.layer], ui.gameObject);
        }
        autoDepth.Add(autoDepth.GetUiPanel(ui), ui.layer);
    }

    /// <summary>
    /// ui的隐藏事件, 通过BaseUi的隐藏方法调用过来的
    /// </summary>
    /// <param name="ui"></param>
    public void OnHide(BaseUi ui)
    {
        autoDepth.Remove(autoDepth.GetUiPanel(ui));
        if (ui.destroyOnClose)
        {
            ui.Destroy();
        }
        else
        {
            ui.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ui的销毁事件, 通过UiManager调用过来的
    /// </summary>
    /// <param name="ui"></param>
    public void OnDestroyUi(BaseUi ui)
    {
        if (ui != null)
        {
            autoDepth.Remove(autoDepth.GetUiPanel(ui));
        }
    }

    public GameObject GetLayerGameObject(UiLayer uiLayer)
    {
        return layerGoDic.ContainsKey(uiLayer) ? layerGoDic[uiLayer].gameObject : null;
    }

    public Camera GetLayerCamera(string uiLayer)
    {
        return layerCameraDic.ContainsKey(uiLayer) ? layerCameraDic[uiLayer] : null;
    }

    /// <summary>
    /// 如果结点下只有一个对象 表示只有相机 没有界面在显示 可以关闭提升效率
    /// </summary>
    public void CloseUnuseCamera()
    {
        foreach (var camera in layerCameraDic.Values)
        {
            if (camera.transform.parent.childCount == 1)
            {
                camera.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// UiModel很特殊 一定要摄像机都激活才能正常显示 所以进场景前需要激活全部
    /// </summary>
    public void ActiveAllCamera()
    {
        foreach (var camera in layerCameraDic.Values)
        {
            camera.transform.parent.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 设置camera是否接收事件
    /// </summary>
    /// <param name="enable"></param>
    public void ActiveCameraEvent(bool enable)
    {
        foreach (var camera in layerCameraDic.Values)
        {
            camera.GetComponent<UICamera>().enabled = enable;
        }
    }

}