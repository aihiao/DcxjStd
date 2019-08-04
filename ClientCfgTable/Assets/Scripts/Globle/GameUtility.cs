using System.Collections.Generic;
using ClientCommon;
using LywGames.Messages.Proto.Game;

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

    public static List<TableVersion> GetConfigVersions()
    {
        List<TableVersion> versions = new List<TableVersion>();

        foreach (var version in ConfigDataBase.VersionConfig.Versions)
        {
            TableVersion ver = new TableVersion();
            ver.tableName = version.TableName;
            ver.versionId = version.VersionText;

            versions.Add(ver);
        }

        foreach (var gmVersion in ConfigDataBase.GmVersionConfig.GmVersions)
        {
            TableVersion ver = new TableVersion();
            ver.tableName = gmVersion.TableName;
            ver.versionId = gmVersion.VersionText;

            versions.Add(ver);
        }

        return versions;
    }

}
