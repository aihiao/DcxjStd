using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("sale_group", "SaleGroup", "", "")]
	sealed public class SaleGroup : AutoCreateConfigElem
	{
		private int _group_id = 0;
		[DbColumn(true, "group_id")]
		public int GroupId { get { return _group_id; } set { _group_id = value; } }

		private int _store_type = 0;
		[DbColumn(false, "store_type")]
		public int StoreType { get { return _store_type; } set { _store_type = value; } }

		private int _chance = 0;
		[DbColumn(false, "chance")]
		public int Chance { get { return _chance; } set { _chance = value; } }

		private int _cert_need_count = 0;
		[DbColumn(false, "cert_need_count")]
		public int CertNeedCount { get { return _cert_need_count; } set { _cert_need_count = value; } }

		private List<SaleGroupList> _sale_group_list = new List<SaleGroupList>();
		[DbMergeColumn(typeof(SaleGroupList), "SaleGroupList")]
		public List<SaleGroupList> SaleGroupLists { get { return _sale_group_list; } }


		private string _description = "";
		[DbColumn(false, "description")]
		public string Description { get { return _description; } set { _description = value; } }

		private string _comment = "";
		[DbColumn(false, "comment")]
		public string Comment { get { return _comment; } set { _comment = value; } }

		private int _abandoned = 0;
		[DbColumn(false, "abandoned")]
		public int Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

		private List<int> _level_ranges = new List<int>();
		[DbSplitColumn(typeof(int), "level_range", false)]
		public List<int> LevelRanges { get { return _level_ranges; } }

	}

	public class SaleGroupConfig : Configuration
	{
		private List<SaleGroup> _sale_groups = null;
		private Dictionary<int, SaleGroup> _sale_groupMap = new Dictionary<int, SaleGroup>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_sale_groups = DbClassLoader.Instance.QueryAllData<SaleGroup>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _sale_group in _sale_groups)
			{
				if (_sale_groupMap.ContainsKey(_sale_group.GroupId) == false)
					_sale_groupMap.Add(_sale_group.GroupId, _sale_group);
				else
					_sale_groupMap[_sale_group.GroupId] = _sale_group;

				if (_refMap.ContainsKey(_sale_group.GroupId) == false)
					_refMap.Add(_sale_group.GroupId, DateTime.Now.Ticks);
				else
					_refMap[_sale_group.GroupId] = DateTime.Now.Ticks;
			}
		}

		public List<SaleGroup> SaleGroups
		{
			get
			{
				if (_sale_groups == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _sale_groups;
			}
		}

		public SaleGroup Get(int group_id)
		{
			if(group_id <= 0)
				return null;
			SaleGroup sale_group = null;
			if (_sale_groupMap.TryGetValue(group_id, out sale_group))
			{
				_refMap[sale_group.GroupId] = GetCurrentTimeTick();
				ReleaseData(false);
				return sale_group;
			}

			sale_group = DbClassLoader.Instance.QueryData<SaleGroup>(ConfigDataBase.Instance.DbAccessorFactory, group_id);
			if (sale_group == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `group_id` value in table `sale_group` : " + group_id);
#endif
				return null;
			}

			_sale_groupMap.Add(group_id, sale_group);
			if (_refMap.ContainsKey(sale_group.GroupId) == false)
				_refMap.Add(sale_group.GroupId, GetCurrentTimeTick());

			ReleaseData(false);
			return sale_group;
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
					_sale_groupMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _sale_groupMap.Count <= 0)
				_sale_groups = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, SaleGroup sale_group)
		{
			SaleGroups.RemoveAll(n => n.GroupId == key);
			if (_sale_groupMap.ContainsKey(key))
			{
				_sale_groupMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (sale_group != null)
			{
				SaleGroups.Add(sale_group);
				_sale_groupMap.Add(key, sale_group);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
