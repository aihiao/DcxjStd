﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ui深度控制
/// </summary>
public class UiAutoDepth
{
    /// <summary>
    /// 封装层级的一些层次数据
    /// </summary>
    class LayerData
    {
        public int index; // layer的下标
        public int startDepth; // layer的起始深度
        public int endDepth; // layer的结束深度
        public int space; // 同一层2个同级Panel深度间隔
    }

    private const int DefaultPanelsInnerDepthSpace = 1; // Panel内部Panel深度间隔
    private const int DefaultPanelDepthSpace = 100; // 同一层2个同级Panel深度间隔
    private const int DefaultLayerDepth = 1000; // 不同层级深度间隔

    private UiLayer[] layers; // 所有层的容器
    private Dictionary<UiLayer, LayerData> layerDataDic; // 每一层对应的层次数据的集合
    private Dictionary<UiLayer, List<UIPanel>> uiDic; // 每一层对应的Panel的集合
    private Dictionary<UIPanel, UiLayer> layerDic; // Panel对应层的集合

    public UiAutoDepth(params UiLayer[] layers)
    {
        this.layers = layers;
        layerDataDic = new Dictionary<UiLayer, LayerData>();
        uiDic = new Dictionary<UiLayer, List<UIPanel>>();
        layerDic = new Dictionary<UIPanel, UiLayer>();

        InitLayerData();
    }

    public UiLayer[] GetLayers()
    {
        return layers;
    }

    protected void InitLayerData()
    {
        if (layers != null)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                LayerData layerData = new LayerData();
                layerData.index = i;
                layerData.startDepth = i * DefaultLayerDepth + 1;
                layerData.endDepth = (i + 1) * DefaultLayerDepth;
                layerData.space = DefaultPanelDepthSpace;
                layerDataDic[layers[i]] = layerData;
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

    /// <summary>
    /// 添加一个显示的ui
    /// </summary>
    /// <param name="ui"></param>
    /// <param name="layer"></param>
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

            LayerData layerData = layerDataDic[layer];
            if (uiDic[layer].Count > 1) // 一层2个Ui的深度初始化
            {
                ui.depth = uiDic[layer][uiDic[layer].Count - 2].depth + layerData.space;
                if (ui.depth >= layerData.endDepth)
                {
                    AutoResetDepth(layer);
                }
            }
            else
            {
                ui.depth = layerData.startDepth; // 一层1个Ui的深度初始化
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
                    baseUi.Overlaid(); // 被覆盖的Ui进行覆盖操作
                }
            }
        }
    }

    #region 对一个Panel内部Panel进行深度排序
    private int depthCache = 0;
    protected void ResetPanelsDepthInPanel(Transform ts, int depth)
    {
        if (ts != null && ts.gameObject.activeSelf)
        {
            depthCache = depth;
            SetPanelDepth(ts, depth);
        }
    }

    private void SetPanelDepth(Transform target, int depth)
    {
        UIPanel panel = target.GetComponent<UIPanel>();
        if (panel != null)
        {
            panel.depth = depthCache + DefaultPanelsInnerDepthSpace;
            depthCache += DefaultPanelsInnerDepthSpace;
        }
        for (int i = 0; i < target.childCount; i++)
        {
            SetPanelDepth(target.GetChild(i), depthCache);
        }
    }
    #endregion

    #region 对同一层级内的所有Panel进行深度排序
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
                    start += layerDataDic[layer].space;
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
        if (layerDataDic.ContainsKey(layer))
        {
            if (layerDataDic[layer].space <= 1)
            {
                LoggerManager.Instance.Error("UiAutoDepth's UILayer {0} space is out of range.", layer);
            }
            else
            {
                layerDataDic[layer].space /= 10;
            }
        }
    }
    #endregion

    #region 对Panel的modelBackground进行深度排序
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
    #endregion

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
        InitLayerData();
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

}
