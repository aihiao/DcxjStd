using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("activity_award", "ActivityAward", "", "")]
	sealed public class ActivityAward : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private int _activity_type = 0;
		[DbColumn(false, "activity_type")]
		public int ActivityType { get { return _activity_type; } set { _activity_type = value; } }

		private int _activity_param = 0;
		[DbColumn(false, "activity_param")]
		public int ActivityParam { get { return _activity_param; } set { _activity_param = value; } }

		private int _reward_id = 0;
		[DbColumn(false, "reward_id")]
		public int RewardId { get { return _reward_id; } set { _reward_id = value; } }

		private int _abandoned = 0;
		[DbColumn(false, "abandoned")]
		public int Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

	}

	public class ActivityAwardConfig : Configuration
	{
		private List<ActivityAward> _activity_awards = null;
		private Dictionary<int, ActivityAward> _activity_awardMap = new Dictionary<int, ActivityAward>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_activity_awards = DbClassLoader.Instance.QueryAllData<ActivityAward>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _activity_award in _activity_awards)
			{
				if (_activity_awardMap.ContainsKey(_activity_award.Id) == false)
					_activity_awardMap.Add(_activity_award.Id, _activity_award);
				else
					_activity_awardMap[_activity_award.Id] = _activity_award;

				if (_refMap.ContainsKey(_activity_award.Id) == false)
					_refMap.Add(_activity_award.Id, DateTime.Now.Ticks);
				else
					_refMap[_activity_award.Id] = DateTime.Now.Ticks;
			}
		}

		public List<ActivityAward> ActivityAwards
		{
			get
			{
				if (_activity_awards == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _activity_awards;
			}
		}

		public ActivityAward Get(int id)
		{
			if(id <= 0)
				return null;
			ActivityAward activity_award = null;
			if (_activity_awardMap.TryGetValue(id, out activity_award))
			{
				_refMap[activity_award.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return activity_award;
			}

			activity_award = DbClassLoader.Instance.QueryData<ActivityAward>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (activity_award == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `id` value in table `activity_award` : " + id);
#endif
				return null;
			}

			_activity_awardMap.Add(id, activity_award);
			if (_refMap.ContainsKey(activity_award.Id) == false)
				_refMap.Add(activity_award.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return activity_award;
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
					_activity_awardMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _activity_awardMap.Count <= 0)
				_activity_awards = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, ActivityAward activity_award)
		{
			ActivityAwards.RemoveAll(n => n.Id == key);
			if (_activity_awardMap.ContainsKey(key))
			{
				_activity_awardMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (activity_award != null)
			{
				ActivityAwards.Add(activity_award);
				_activity_awardMap.Add(key, activity_award);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
