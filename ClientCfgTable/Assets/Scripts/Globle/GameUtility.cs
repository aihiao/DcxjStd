using UnityEngine;
using ClientCommon;

public static class GameUtility
{
    public static string GetUiString(string key)
    {
        var value = ConfigDataBase.TextConfig.Get(key);
        if (value != null)
        {
            return value.Content;
        }
        return "Can't find string for key:" + key;
    }
}
