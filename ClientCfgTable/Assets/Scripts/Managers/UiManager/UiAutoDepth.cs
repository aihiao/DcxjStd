using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ui深度控制
/// </summary>
public class UiAutoDepth
{
    class LayerData
    {
        public int index; // layer的下标
        public int startDepth; // layer的起始深度
        public int endDepth; // layer的结束深度
        public int space;
    }

    private const int DefaultLayerInnerDepthSpace = 1;
    private const int DefaultDepthSpace = 100;
    private const int DefaultLayerDepth = 1000;

    private Dictionary<UiLayer, LayerData> depthSpaceDic;
    private int depthMax;

    private Dictionary<UiLayer, List<UIPanel>> uiDic;
    private Dictionary<UIPanel, UiLayer> layerDic;
    private UiLayer[] layers;

    private List<UIPanel> cachePanels; // 这个成员缓存显示的面板，用做实现overlay和remover overlay

    public UiAutoDepth(params UiLayer[] layers)
    {
        cachePanels = new List<UIPanel>();
        uiDic = new Dictionary<UiLayer, List<UIPanel>>();
        layerDic = new Dictionary<UIPanel, UiLayer>();
        depthSpaceDic = new Dictionary<UiLayer, LayerData>();
        depthMax = layers.Length * DefaultLayerDepth;
        this.layers = layers;

        InitDepth();
    }

    public UiLayer[] GetLayers()
    {
        return layers;
    }

    protected void InitDepth()
    {
        if (layers != null)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                LayerData layerData = new LayerData();
                layerData.index = i;
                layerData.startDepth = i * DefaultLayerDepth + 1;
                layerData.endDepth = (i + 1) * DefaultLayerDepth;
                layerData.space = DefaultDepthSpace;
                depthSpaceDic[layers[i]] = layerData;
                if (!uiDic.ContainsKey(layers[i]))
                {
                    uiDic[layers[i]] = new List<UIPanel>();
                }
            }
        }
    }

    private int GetLayerIndex(UiLayer layer)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i] == layer)
            {
                return i;
            }
        }
        return 0;
    }

    public void Add(UIPanel ui, UiLayer layer)
    {
        if (ui != null && uiDic.ContainsKey(layer) && (!uiDic[layer].Contains(ui)))
        {
            uiDic[layer].Add(ui);
            if (layerDic.ContainsKey(ui))
            {
                LoggerManager.Instance.Warn("Layer dictionary contained ui nameed {0}", ui.name);
            }
            else
            {
                layerDic.Add(ui, layer);
            }
            ui.transform.SetAsLastSibling();

            LayerData layerData = depthSpaceDic[layer];
            if (uiDic[layer].Count > 1)
            {
                ui.depth = uiDic[layer][uiDic[layer].Count - 2].depth + layerData.space;
                if (ui.depth >= layerData.endDepth)
                {
                    AutoResetDepth(layer);
                }
            }
            else
            {
                ui.depth = layerData.startDepth;
            }

            ResetPanelsDepthInPanel(ui.transform, ui.depth);
            ResetUiModelBackgroundDepth(ui, ui.depth);

            List<UIPanel> panelListInLayer = uiDic[layer];
            int count = panelListInLayer.Count;
            if (count >= 2)
            {
                BaseUi baseUi = GetBaseUiByPanel(panelListInLayer[count - 2]);
                if (baseUi != null)
                {
                    baseUi.Overlaid();
                }
            }
        }
    }

    private int SortPanel(UIPanel p1, UIPanel p2)
    {
        return p1.depth - p2.depth;
    }

    protected void ResetPanelsDepthInPanel(Transform ts, int depth)
    {
        if (ts != null && ts.gameObject.activeSelf)
        {
            depthCache = depth;
            SetPanelDepth(ts, depth);
        }
    }

    private int depthCache = 0;
    private void SetPanelDepth(Transform target, int depth)
    {
        UIPanel panel = target.GetComponent<UIPanel>();
        if (panel != null)
        {
            panel.depth = depthCache + DefaultLayerInnerDepthSpace;
            depthCache += DefaultLayerInnerDepthSpace;
        }
        for (int i = 0; i < target.childCount; i++)
        {
            SetPanelDepth(target.GetChild(i), depthCache);
        }
    }

    public void SortShowingUi(List<BaseUi> uiList)
    {
        var sortingUis = new List<BaseUi>();
        for (int i = 0; i < layers.Length; i++)
        {
            UiLayer layer = layers[i];
            sortingUis.Clear();
            for (int j = 0; j < uiList.Count; j++)
            {
                BaseUi ui = uiList[i];
                if (ui.layer == layer)
                {
                    sortingUis.Add(ui);
                }
            }
            SortLayerUi(sortingUis);
        }
    }

    private void SortLayerUi(List<BaseUi> uiList)
    {
        for (int i = uiList.Count; i > 1; i--)
        {
            for (int j = 0; j < i - 1; j++)
            {
                UIPanel ui1 = GetUiPanel(uiList[j]);
                UIPanel ui2 = GetUiPanel(uiList[j + 1]);

                if (ui1.depth > ui2.depth)
                {
                    int tempDepth = ui1.depth;

                    ui1.depth = ui2.depth;
                    ResetUiModelBackgroundDepth(uiList[j], ui1.depth);

                    ui2.depth = tempDepth;
                    ResetUiModelBackgroundDepth(uiList[j + 1], ui2.depth);

                    int index = ui1.gameObject.transform.GetSiblingIndex();
                    ui1.gameObject.transform.SetSiblingIndex(ui2.gameObject.transform.GetSiblingIndex());
                    ui2.gameObject.transform.SetSiblingIndex(index);
                }
            }
        }
    }

    public void ResetUiModelBackgroundDepth(UIPanel ui, int uiDepth)
    {
        ResetUiModelBackgroundDepth(ui.GetComponent<BaseUi>(), uiDepth);
    }

    public void ResetUiModelBackgroundDepth(BaseUi ui, int uiDepth)
    {
        if (ui != null && ui.modelBackground != null)
        {
            ui.modelBackground.depth = uiDepth - 1;
        }
    }

    public UIPanel GetUiPanel(GameObject ui)
    {
        if (ui != null)
        {
            return ui.GetComponent<UIPanel>();
        }
        return null;
    }

    public UIPanel GetUiPanel(BaseUi ui)
    {
        if (ui != null)
        {
            return ui.GetComponent<UIPanel>();
        }
        return null;
    }

    public BaseUi GetBaseUiByPanel(UIPanel panel)
    {
        if (panel != null)
        {
            return panel.GetComponent<BaseUi>();
        }
        return null;
    }

    public void Remove(UIPanel ui)
    {
        if (ui != null && layerDic.ContainsKey(ui))
        {
            UiLayer layer = layerDic[ui];
            List<UIPanel> panelList = uiDic[layer];
            int count = panelList.Count;
            if (count >= 2)
            {
                BaseUi baseUi = GetBaseUiByPanel(panelList[count - 2]);
                if (baseUi != null)
                {
                    baseUi.RemoveOverlay();
                }
            }
            uiDic[layer].Remove(ui);
            layerDic.Remove(ui);
        }
    }

    public void Clear(UiLayer layer = UiLayer.All)
    {
        if (layerDic != null)
        {
            layerDic.Clear();
        }
        InitDepth();
        if (layer == UiLayer.All)
        {
            foreach (var item in uiDic)
            {
                item.Value.Clear();
            }
        }
        else
        {
            if (uiDic.ContainsKey(layer))
            {
                uiDic[layer].Clear();
            }
        }
    }

    private void AutoResetDepth(UiLayer layer = UiLayer.All)
    {
        if (layer == UiLayer.All)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                ResetSingleDepth(layers[i]);
            }
        }
        else
        {
            ResetSingleDepth(layer);
        }
    }

    private void ResetSingleDepth(UiLayer layer)
    {
        if (uiDic.ContainsKey(layer))
        {
            List<UIPanel> list = uiDic[layer];
            if (list != null && list.Count > 0)
            {
                int start = (int)layer * DefaultLayerDepth + 1;
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].depth = start;
                    ResetPanelsDepthInPanel(list[i].transform, list[i].depth);
                    ResetUiModelBackgroundDepth(list[i], list[i].depth);
                    start += depthSpaceDic[layer].space;
                }
            }
        }
    }

    /// <summary>
    /// 每次重置把深度距离缩小10倍
    /// </summary>
    /// <param name="layer"></param>
    private void ResetSpace(UiLayer layer)
    {
        if (depthSpaceDic.ContainsKey(layer))
        {
            if (depthSpaceDic[layer].space <= 1)
            {
                LoggerManager.Instance.Error("UiAutoDepth's UILayer {0} space is out of range.", string.Empty, null, layer.ToString());
            }
            else
            {
                depthSpaceDic[layer].space /= 10;
            }
        }
    }

}
