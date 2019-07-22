using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiModelTool
{
    public static void DeleteAllModel()
    {

    }

    public static void ChangeTransformLayer(GameObject go, string uiLayer)
    {
        if (go == null)
        {
            return;
        }
        int layer = LayerMask.NameToLayer(uiLayer);
        go.layer = layer;
        foreach (Transform tr in go.transform)
        {
            ChangeTransformLayer(tr.gameObject, uiLayer);
        }
    }
}
