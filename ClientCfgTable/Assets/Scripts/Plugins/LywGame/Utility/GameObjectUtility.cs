using UnityEngine;
using ClientCommon;

public class GameObjectUtility
{
    public static GameObject CreateGameObject(string resName = null)
    {
        var go = !string.IsNullOrEmpty(resName) ? ResourceManager.Instance.InstantiateAsset<GameObject>(AssetType.UI, resName, true) : new GameObject();
        if (go != null)
        {
            Transform trans = go.transform;
            if (trans != null)
            {
                trans.localPosition = Vector3.zero;
                trans.localRotation = Quaternion.identity;
                trans.localScale = Vector3.one;
            }
        }
        return go;
    } 

    public static void AddUiChild(GameObject parent, GameObject child)
    {
        if (parent != null && child != null)
        {
            RectTransform rt = child.transform as RectTransform;
            if (rt)
            {
                rt.SetParent(parent.transform);
                rt.anchorMax = Vector2.one;
                rt.anchorMin = Vector2.zero;
                rt.sizeDelta = Vector2.zero;
                rt.anchoredPosition = Vector2.zero;
            }
            else
            {
                Transform tr = child.transform;
                Vector3 position = tr.position;
                Vector3 localScale = tr.localScale;
                tr.SetParent(parent.transform);
                tr.position = Vector3.zero; // position
                tr.localScale = localScale;
            }
        }
    }

    public static void AddUiChildKeepOrignalRectTransform(GameObject parent, GameObject child)
    {
        if (parent != null && child != null)
        {
            RectTransform rt = child.transform as RectTransform;
            Vector2 anchorMax = rt.anchorMax;
            Vector2 anchorMin = rt.anchorMin;
            Vector2 sizeDelta = rt.sizeDelta;
            Vector2 anchoredPosition = rt.anchoredPosition;

            if (rt)
            {
                rt.SetParent(parent.transform);
                rt.anchorMax = anchorMax;
                rt.anchorMin = anchorMin;
                rt.sizeDelta = sizeDelta;
                rt.anchoredPosition = anchoredPosition;
            }
        }
    }

}
