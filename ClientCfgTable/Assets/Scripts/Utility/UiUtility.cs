using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UiUtility
{
    public static T AddIfMissing<T>(GameObject go) where T : Component
    {
        T component = (T)go.GetComponent(typeof(T));
        if (component == null)
        {
            return (T)go.AddComponent(typeof(T));
        }
        return component;
    }

    public static void SetParent(GameObject child, GameObject parent)
    {
        if (child != null)
        {
            SetParent(child.transform, parent.transform);
        }
    }

    public static void SetParent(Transform child, Transform parent, bool resetPosition = true)
    {
        if (child != null)
        {
            Vector3 localScale = child.localScale;
            Vector3 localPosition = child.localPosition;
            child.SetParent(parent);
            child.localPosition = resetPosition ? Vector3.zero : localPosition;
            child.localScale = localScale;
        }
    }

    public static BaseUi LoadUiPerfab(Type type, string resourceName)
    {
        GameObject go = GameObjectUtility.CreateGameObject(resourceName);
        go.SetActive(false);
        return go.GetComponent(type) as BaseUi;
    }

}