using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("version", "Version", "", "")]
	sealed public class Version
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

	}

	public class VersionConfig : Configuration
	{
		private List<Version> _versions = null;
		private Dictionary<string, Version> _versionMap = new Dictionary<string, Version>();
		private Dictionary<string, long> _refMap = new Dictionary<string, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_versions = DbClassLoader.Instance.QueryAllData<Version>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _version in _versions)
			{
				if (_versionMap.ContainsKey(_version.TableName) == false)
					_versionMap.Add(_version.TableName, _version);
				else
					_versionMap[_version.TableName] = _version;

				if (_refMap.ContainsKey(_version.TableName) == false)
					_refMap.Add(_version.TableName, DateTime.Now.Ticks);
				else
					_refMap[_version.TableName] = DateTime.Now.Ticks;
			}
		}

		public List<Version> Versions
		{
			get
			{
				if (_versions == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _versions;
			}
		}

		public Version Get(string table_name)
		{
			Version version = null;
			if (_versionMap.TryGetValue(table_name, out version))
			{
				_refMap[version.TableName] = GetCurrentTimeTick();
				ReleaseData(false);
				return version;
			}

			version = DbClassLoader.Instance.QueryData<Version>(ConfigDataBase.Instance.DbAccessorFactory, table_name);
			if (version == null)
			{
#if UNITY_EDITOR
				LoggerManager.Instance.Warn("Invalid `table_name` value in table `version` : " + table_name);
#endif
				return null;
			}

			_versionMap.Add(table_name, version);
			if (_refMap.ContainsKey(version.TableName) == false)
				_refMap.Add(version.TableName, GetCurrentTimeTick());

			ReleaseData(false);
			return version;
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
					_versionMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _versionMap.Count <= 0)
				_versions = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(string key, Version version)
		{
			Versions.RemoveAll(n => n.TableName == key);
			if (_versionMap.ContainsKey(key))
			{
				_versionMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (version != null)
			{
				Versions.Add(version);
				_versionMap.Add(key, version);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
