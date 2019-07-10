using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("acupoint", "Acupoint", "", "")]
	sealed public class Acupoint : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private string _name = "";
		[DbColumn(false, "name")]
		public string Name { get { return _name; } set { _name = value; } }

		private int _channel_id = 0;
		[DbColumn(false, "channel_id")]
		public int ChannelId { get { return _channel_id; } set { _channel_id = value; } }

		private string _icon_assert = "";
		[DbColumn(false, "icon_assert")]
		public string IconAssert { get { return _icon_assert; } set { _icon_assert = value; } }

		private int _atribute_group_id = 0;
		[DbColumn(false, "atribute_group_id")]
		public int AtributeGroupId { get { return _atribute_group_id; } set { _atribute_group_id = value; } }

		private int _crit_active_percent = 0;
		[DbColumn(false, "crit_active_percent")]
		public int CritActivePercent { get { return _crit_active_percent; } set { _crit_active_percent = value; } }

		private int _next_acupoint_id = 0;
		[DbColumn(false, "next_acupoint_id")]
		public int NextAcupointId { get { return _next_acupoint_id; } set { _next_acupoint_id = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }


		private List<AcupointRequireItem> _acupoint_require_items = new List<AcupointRequireItem>();
		[DbSplitColumn(typeof(AcupointRequireItem), "acupoint_require_item", true)]
		public List<AcupointRequireItem> AcupointRequireItems { get { return _acupoint_require_items; } }


		private List<int> _reward_attributes = new List<int>();
		[DbSplitColumn(typeof(int), "reward_attribute", false)]
		public List<int> RewardAttributes { get { return _reward_attributes; } }


		private List<int> _crit_reward_attributes = new List<int>();
		[DbSplitColumn(typeof(int), "crit_reward_attribute", false)]
		public List<int> CritRewardAttributes { get { return _crit_reward_attributes; } }

	}

	public class AcupointConfig : Configuration
	{
		private List<Acupoint> _acupoints = null;
		private Dictionary<int, Acupoint> _acupointMap = new Dictionary<int, Acupoint>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_acupoints = DbClassLoader.Instance.QueryAllData<Acupoint>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _acupoint in _acupoints)
			{
				if (_acupointMap.ContainsKey(_acupoint.Id) == false)
					_acupointMap.Add(_acupoint.Id, _acupoint);
				else
					_acupointMap[_acupoint.Id] = _acupoint;

				if (_refMap.ContainsKey(_acupoint.Id) == false)
					_refMap.Add(_acupoint.Id, DateTime.Now.Ticks);
				else
					_refMap[_acupoint.Id] = DateTime.Now.Ticks;
			}
		}

		public List<Acupoint> Acupoints
		{
			get
			{
				if (_acupoints == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _acupoints;
			}
		}

		public Acupoint Get(int id)
		{
			if(id <= 0)
				return null;
			Acupoint acupoint = null;
			if (_acupointMap.TryGetValue(id, out acupoint))
			{
				_refMap[acupoint.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return acupoint;
			}

			acupoint = DbClassLoader.Instance.QueryData<Acupoint>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (acupoint == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `id` value in table `acupoint` : " + id);
#endif
				return null;
			}

			_acupointMap.Add(id, acupoint);
			if (_refMap.ContainsKey(acupoint.Id) == false)
				_refMap.Add(acupoint.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return acupoint;
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
					_acupointMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _acupointMap.Count <= 0)
				_acupoints = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, Acupoint acupoint)
		{
			Acupoints.RemoveAll(n => n.Id == key);
			if (_acupointMap.ContainsKey(key))
			{
				_acupointMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (acupoint != null)
			{
				Acupoints.Add(acupoint);
				_acupointMap.Add(key, acupoint);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
