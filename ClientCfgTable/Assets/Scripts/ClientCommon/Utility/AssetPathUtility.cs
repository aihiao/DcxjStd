using System.Collections.Generic;
using LywGames;

namespace ClientCommon
{
	public static class AssetPathUtility
	{
		static Dictionary<int, string> assetPathDic = new Dictionary<int, string>();

		public static string GetTypePath(int assetType)
		{
			string assetPath = string.Empty;
			if (!assetPathDic.TryGetValue(assetType, out assetPath))
			{
				var assetPathCfg = ConfigDataBase.AssetPathConfig.Get(assetType);
                if (assetPathCfg == null)
                {
                    return assetPath;
                }
				assetPath = assetPathCfg.Path;
				assetPathDic.Add(assetType, assetPath);
			}

			return assetPath;
		}

		public static string GetAssetPath(int assetType, string assetName)
		{
			return PathUtility.Combine(GetTypePath(assetType), assetName);
		}

	}
}
