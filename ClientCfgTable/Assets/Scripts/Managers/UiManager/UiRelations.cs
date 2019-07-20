using System;
using System.Collections.Generic;
using LywGames;

/// <summary>
/// Ui配置数据
/// </summary>
public class UiRelationData
{
    public Type type;
    public string resourceName;
    public bool hideOtherModules;
    public bool isPopup;
    public Type[] linkedTypes;
    public Type[] ignoreMutexTypes;
}

/// <summary>
/// Ui配置数据容器, 存储了所有Ui实例, 通过单例操作, 没必要在UiManager里转一次
/// Relation关系的意思
/// </summary>
public class UiRelations : AutoCreateSingleton<UiRelations>
{
    // 保存当前已显示或已隐藏, 被创建过没有销毁的ui, 以prefab的名字作为key
    private Dictionary<string, BaseUi> uiDic = new Dictionary<string, BaseUi>();
    public Dictionary<string, BaseUi> UiDic { get { return uiDic; } }

    // 所有ui实例容器
    private List<UiRelationData> relationList = new List<UiRelationData>();
    private Dictionary<Type, UiRelationData> relationDic = new Dictionary<Type, UiRelationData>();
    /// <summary>
    /// 注册所有的ui实例
    /// </summary>
    /// <param name="type"></param>
    /// <param name="resourceName"></param>
    /// <param name="linkedTypes"></param>
    /// <param name="hideOtherModules"></param>
    /// <param name="igoreMutexTypes"></param>
    /// <returns></returns>
    public bool Register(Type type, string resourceName, Type[] linkedTypes, bool hideOtherModules, Type[] igoreMutexTypes)
    {
        if (relationDic.ContainsKey(type))
        {
            return false;
        }

        UiRelationData relation = new UiRelationData();
        relation.type = type;
        relation.resourceName = resourceName;
        relation.linkedTypes = linkedTypes;
        relation.ignoreMutexTypes = igoreMutexTypes;
        relation.hideOtherModules = hideOtherModules;

        relationList.Add(relation);
        relationDic.Add(type, relation);

        return true;
    }

    public List<UiRelationData> GetRelationList()
    {
        return relationList;
    }

    public UiRelationData GetUiRelationData(Type type)
    {
        return relationDic.ContainsKey(type) ? relationDic[type] : null;
    }

    public bool GetIsShowing(string prefabName)
    {
        BaseUi ui = null;
        if (uiDic.TryGetValue(prefabName, out ui))
        {
            return ui.IsShowing;
        }
        return false;
    }

    public bool GetIsShowing(Type t)
    {
        return GetIsShowing(t.ToString());
    }

    public BaseUi GetUi(Type type)
    {
        return GetUi(type.ToString());
    }

    public BaseUi GetUi(string prefabName)
    {
        return uiDic.ContainsKey(prefabName) ? uiDic[prefabName] : null;
    }

    /// <summary>
    /// 添加已创建的ui
    /// </summary>
    /// <param name="ui"></param>
    /// <returns></returns>
    public bool AddUi(BaseUi ui)
    {
        if (ui != null)
        {
            string key = ui.GetType().ToString();
            if (!uiDic.ContainsKey(key))
            {
                uiDic.Add(key, ui);
                return true;
            }
        }
        return false;
    }

    public bool RemoveUi(BaseUi ui)
    {
        return ui == null ? false : uiDic.Remove(ui.GetType().ToString());
    }

    #region 获取关联Ui
    public List<Type> GetLinkedList(Type type, List<Type> list = null)
    {
        UiRelationData relation = GetUiRelationData(type);
        if (relation != null)
        {
            List<Type> relationList = null;
            if (list == null)
            {
                relationList = new List<Type>();
            }
            else
            {
                relationList = list;
            }

            if (relation.linkedTypes != null)
            {
                GetLinkedUis(type, relationList);
            }
            else
            {
                relationList.Add(type);
            }

            return relationList;
        }
        return null;
    }

    private void GetLinkedUis(Type type, List<Type> linkedUis)
    {
        if (linkedUis != null && (!linkedUis.Contains(type)))
        {
            linkedUis.Add(type);
        }

        UiRelationData relation = GetUiRelationData(type);
        if (relation != null && relation.linkedTypes != null)
        {
            for (int i = 0; i < relation.linkedTypes.Length; i++)
            {
                GetLinkedUis(relation.linkedTypes[i], linkedUis);
            }
        }
    }
    #endregion 获取关联Ui

}
