using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("gm_version", "GmVersion", "", "")]
	sealed public class GmVersion
	{
		private string _table_name = "";
		[DbColumn(true, "table_name")]
		public string TableName { get { return _table_name; } set { _table_name = value; } }

		private int _version_text = 0;
		[DbColumn(false, "version_text")]
		public int VersionText { get { return _version_text; } set { _version_text = value; } }

		private int _table_type = 0;
		[DbColumn(false, "table_type")]
		public int TableType { get { return _table_type; } set { _table_type = value; } }

		private bool _transfer = false;
		[DbColumn(false, "transfer")]
		public bool Transfer { get { return _transfer; } set { _transfer = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private string _desc = "";
		[DbColumn(false, "desc")]
		public string Desc { get { return _desc; } set { _desc = value; } }

	}

	public class GmVersionConfig : Configuration
	{
		private List<GmVersion> _gm_versions = null;
		private Dictionary<string, GmVersion> _gm_versionMap = new Dictionary<string, GmVersion>();
		private Dictionary<string, long> _refMap = new Dictionary<string, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_gm_versions = DbClassLoader.Instance.QueryAllData<GmVersion>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _gm_version in _gm_versions)
			{
				if (_gm_versionMap.ContainsKey(_gm_version.TableName) == false)
					_gm_versionMap.Add(_gm_version.TableName, _gm_version);
				else
					_gm_versionMap[_gm_version.TableName] = _gm_version;

				if (_refMap.ContainsKey(_gm_version.TableName) == false)
					_refMap.Add(_gm_version.TableName, DateTime.Now.Ticks);
				else
					_refMap[_gm_version.TableName] = DateTime.Now.Ticks;
			}
		}

		public List<GmVersion> GmVersions
		{
			get
			{
				if (_gm_versions == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _gm_versions;
			}
		}

		public GmVersion Get(string table_name)
		{
			GmVersion gm_version = null;
			if (_gm_versionMap.TryGetValue(table_name, out gm_version))
			{
				_refMap[gm_version.TableName] = GetCurrentTimeTick();
				ReleaseData(false);
				return gm_version;
			}

			gm_version = DbClassLoader.Instance.QueryData<GmVersion>(ConfigDataBase.Instance.DbAccessorFactory, table_name);
			if (gm_version == null)
			{
#if UNITY_EDITOR
				LoggerManager.Instance.Warn("Invalid `table_name` value in table `gm_version` : " + table_name);
#endif
				return null;
			}

			_gm_versionMap.Add(table_name, gm_version);
			if (_refMap.ContainsKey(gm_version.TableName) == false)
				_refMap.Add(gm_version.TableName, GetCurrentTimeTick());

			ReleaseData(false);
			return gm_version;
		}

		public override void ReleaseData(bool isForce)
		{
			long nowtime = GetCurrentTimeTick();
			if (!isForce && nowtime - lastCheckReleaseTime < CheckReleaseTime)
				return;
			lastCheckReleaseTime = nowtime;


			var keys = new List<string>(_refMap.Keys);
			for (int index = 0; index < keys.Count; index++)
			{
				var key = keys[index];
				if (isForce || nowtime - _refMap[key] > MaxStayTime)
				{
					_gm_versionMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _gm_versionMap.Count <= 0)
				_gm_versions = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(string key, GmVersion gm_version)
		{
			GmVersions.RemoveAll(n => n.TableName == key);
			if (_gm_versionMap.ContainsKey(key))
			{
				_gm_versionMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (gm_version != null)
			{
				GmVersions.Add(gm_version);
				_gm_versionMap.Add(key, gm_version);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
