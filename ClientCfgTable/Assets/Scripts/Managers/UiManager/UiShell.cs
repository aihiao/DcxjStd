using System.Collections.Generic;
using UnityEngine;
using LywGames;

/// <summary>
/// UiManager的辅助类, 创建UIRoot和UICamera, 显示和关闭窗口的通用操作
/// </summary>
public class UiShell : AutoCreateSingleton<UiShell>
{
    private GameObject uiRootGo;

    private Dictionary<UiLayer, GameObject> layerDic;
    private Dictionary<string, Camera> layerCameraDic;

    private UiAutoDepth autoDepth;

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

    private void InitLayers()
    {
        layerDic = new Dictionary<UiLayer, GameObject>();
        layerCameraDic = new Dictionary<string, Camera>();
        List<UiLayer> layers = new List<UiLayer>();
        for (UiLayer i = UiLayer.TopMost; i >= UiLayer.BottomMost; i--)
        {
            layers.Add(i);
            CreateNewLayer(i, uiRootGo, (int)i * 1000);
        }
        autoDepth = new UiAutoDepth(layers.ToArray());
    }

    private void CreateNewLayer(UiLayer layer, GameObject goParent, int depthStart)
    {
        GameObject go = GameObjectUtility.CreateGameObject();
        go.name = layer.ToString();
        go.AddComponent<UIPanel>().depth = depthStart;
        GameObjectUtility.AddUiChild(goParent, go);
        go.layer = LayerMask.NameToLayer(layer.ToString());
        layerDic[layer] = go;
        CreateLayerCamera(layer, LayerMask.NameToLayer(layer.ToString()), go);
    }

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

    private void addClickEvent(BaseUi ui)
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

    private void addBackground(BaseUi ui)
    {
        if (ui.model && ui.modelBackground == null)
        {
            GameObject go = GameObjectUtility.CreateGameObject("UiPnlModelBackground");
            go.SetActive(false);
            UiPnlModelBackground modelBackground = go.GetComponent<UiPnlModelBackground>();

            UiUtility.SetParent(modelBackground.gameObject, ui.gameObject);
            modelBackground.transform.SetAsFirstSibling();
            modelBackground.gameObject.SetActive(true);
            ui.modelBackground = modelBackground.GetComponent<UIPanel>();
        }
    }

    private UIPanel GetPanel(BaseUi ui)
    {
        return autoDepth == null ? null : autoDepth.GetUiPanel(ui);
    }

    public void OnUiDestroy(BaseUi ui)
    {
        if (ui != null)
        {
            autoDepth.Remove(GetPanel(ui));
        }
    }

    public void OnShow(BaseUi ui)
    {
        if (!GetLayerCamera(ui.layer.ToString()).transform.parent.gameObject.activeSelf)
        {
            GetLayerCamera(ui.layer.ToString()).transform.parent.gameObject.SetActive(true);
        }
        ui.gameObject.SetActive(true);
        addBackground(ui);
        addClickEvent(ui);
        if (layerDic.ContainsKey(ui.layer))
        {
            GameObjectUtility.AddUiChild(layerDic[ui.layer], ui.gameObject);
        }
        autoDepth.Add(GetPanel(ui), ui.layer);
    }

    public void OnHide(BaseUi ui)
    {
        autoDepth.Remove(GetPanel(ui));
        if (ui.destroyOnClose)
        {
            ui.Destroy();
        }
        else
        {
            ui.gameObject.SetActive(false);
        }
    }

    public GameObject GetLayerGameObject(UiLayer uiLayer)
    {
        return layerDic.ContainsKey(uiLayer) ? layerDic[uiLayer].gameObject : null;
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