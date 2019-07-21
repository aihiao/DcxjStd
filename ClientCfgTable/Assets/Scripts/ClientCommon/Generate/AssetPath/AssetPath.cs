using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("program_asset_path", "AssetPath", "", "")]
	sealed public class AssetPath : AutoCreateConfigElem
	{
		private int _asset_type = 0;
		[DbColumn(true, "asset_type")]
		public int AssetType { get { return _asset_type; } set { _asset_type = value; } }

		private string _path = "";
		[DbColumn(false, "path")]
		public string Path { get { return _path; } set { _path = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

	}

	public class AssetPathConfig : Configuration
	{
		private List<AssetPath> _asset_paths = null;
		private Dictionary<int, AssetPath> _asset_pathMap = new Dictionary<int, AssetPath>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_asset_paths = DbClassLoader.Instance.QueryAllData<AssetPath>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _asset_path in _asset_paths)
			{
				if (_asset_pathMap.ContainsKey(_asset_path.AssetType) == false)
					_asset_pathMap.Add(_asset_path.AssetType, _asset_path);
				else
					_asset_pathMap[_asset_path.AssetType] = _asset_path;

				if (_refMap.ContainsKey(_asset_path.AssetType) == false)
					_refMap.Add(_asset_path.AssetType, DateTime.Now.Ticks);
				else
					_refMap[_asset_path.AssetType] = DateTime.Now.Ticks;
			}
		}

		public List<AssetPath> AssetPaths
		{
			get
			{
				if (_asset_paths == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _asset_paths;
			}
		}

		public AssetPath Get(int asset_type)
		{
			if(asset_type <= 0)
				return null;
			AssetPath asset_path = null;
			if (_asset_pathMap.TryGetValue(asset_type, out asset_path))
			{
				_refMap[asset_path.AssetType] = GetCurrentTimeTick();
				ReleaseData(false);
				return asset_path;
			}

			asset_path = DbClassLoader.Instance.QueryData<AssetPath>(ConfigDataBase.Instance.DbAccessorFactory, asset_type);
			if (asset_path == null)
			{
#if UNITY_EDITOR
				LoggerManager.Instance.Warn("Invalid `asset_type` value in table `asset_path` : " + asset_type);
#endif
				return null;
			}

			_asset_pathMap.Add(asset_type, asset_path);
			if (_refMap.ContainsKey(asset_path.AssetType) == false)
				_refMap.Add(asset_path.AssetType, GetCurrentTimeTick());

			ReleaseData(false);
			return asset_path;
		}

		public override void ReleaseData(bool isForce)
		{
			long nowtime = GetCurrentTimeTick();
			if (!isForce && nowtime - lastCheckReleaseTime < CheckReleaseTime)
				return;
			lastCheckReleaseTime = nowtime;


			var keys = new List<int>(_refMap.Keys);
			for (int index = 0; index < keys.Count; index++)
			{
				var key = keys[index];
				if (isForce || nowtime - _refMap[key] > MaxStayTime)
				{
					_asset_pathMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _asset_pathMap.Count <= 0)
				_asset_paths = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, AssetPath asset_path)
		{
			AssetPaths.RemoveAll(n => n.AssetType == key);
			if (_asset_pathMap.ContainsKey(key))
			{
				_asset_pathMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (asset_path != null)
			{
				AssetPaths.Add(asset_path);
				_asset_pathMap.Add(key, asset_path);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
