using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("dattribute", "Dattribute", "Attribute", "attribute")]
	sealed public class Dattribute : AutoCreateConfigElem
	{
		private int _dattribute_id = 0;
		[DbColumn(true, "dattribute_id")]
		public int DattributeId { get { return _dattribute_id; } set { _dattribute_id = value; } }

		private List<AttributeList> _attribute_list = new List<AttributeList>();
		[DbMergeColumn(typeof(AttributeList), "AttributeList")]
		public List<AttributeList> AttributeLists { get { return _attribute_list; } }


		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

#if UNITY_EDITOR
		private string _comment = "";
		[DbColumn(false, "comment")]
		public string Comment { get { return _comment; } set { _comment = value; } }

#endif
	}

	public class DattributeConfig : Configuration
	{
		private List<Dattribute> _dattributes = null;
		private Dictionary<int, Dattribute> _dattributeMap = new Dictionary<int, Dattribute>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_dattributes = DbClassLoader.Instance.QueryAllData<Dattribute>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _dattribute in _dattributes)
			{
				if (_dattributeMap.ContainsKey(_dattribute.DattributeId) == false)
					_dattributeMap.Add(_dattribute.DattributeId, _dattribute);
				else
					_dattributeMap[_dattribute.DattributeId] = _dattribute;

				if (_refMap.ContainsKey(_dattribute.DattributeId) == false)
					_refMap.Add(_dattribute.DattributeId, DateTime.Now.Ticks);
				else
					_refMap[_dattribute.DattributeId] = DateTime.Now.Ticks;
			}
		}

		public List<Dattribute> Dattributes
		{
			get
			{
				if (_dattributes == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _dattributes;
			}
		}

		public Dattribute Get(int dattribute_id)
		{
			if(dattribute_id <= 0)
				return null;
			Dattribute dattribute = null;
			if (_dattributeMap.TryGetValue(dattribute_id, out dattribute))
			{
				_refMap[dattribute.DattributeId] = GetCurrentTimeTick();
				ReleaseData(false);
				return dattribute;
			}

			dattribute = DbClassLoader.Instance.QueryData<Dattribute>(ConfigDataBase.Instance.DbAccessorFactory, dattribute_id);
			if (dattribute == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `dattribute_id` value in table `dattribute` : " + dattribute_id);
#endif
				return null;
			}

			_dattributeMap.Add(dattribute_id, dattribute);
			if (_refMap.ContainsKey(dattribute.DattributeId) == false)
				_refMap.Add(dattribute.DattributeId, GetCurrentTimeTick());

			ReleaseData(false);
			return dattribute;
		}

		public override void ReleaseData(bool isForce)
		{
			if (!isForce && ConfigDataBase.Instance.ReleaseData == false)
				return;

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
					_dattributeMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _dattributeMap.Count <= 0)
				_dattributes = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, Dattribute dattribute)
		{
			Dattributes.RemoveAll(n => n.DattributeId == key);
			if (_dattributeMap.ContainsKey(key))
			{
				_dattributeMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (dattribute != null)
			{
				Dattributes.Add(dattribute);
				_dattributeMap.Add(key, dattribute);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
