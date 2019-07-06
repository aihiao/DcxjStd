using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("faction_tree_mission", "FactionTreeMission", "", "")]
	sealed public class FactionTreeMission : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private string _difficity_degree = "";
		[DbColumn(false, "difficity_degree")]
		public string DifficityDegree { get { return _difficity_degree; } set { _difficity_degree = value; } }

		private string _desc = "";
		[DbColumn(false, "desc")]
		public string Desc { get { return _desc; } set { _desc = value; } }

		private int _probability = 0;
		[DbColumn(false, "probability")]
		public int Probability { get { return _probability; } set { _probability = value; } }

		private int _event_type = 0;
		[DbColumn(false, "event_type")]
		public int EventType { get { return _event_type; } set { _event_type = value; } }

		private string _complete_desc = "";
		[DbColumn(false, "complete_desc")]
		public string CompleteDesc { get { return _complete_desc; } set { _complete_desc = value; } }

		private int _build_num = 0;
		[DbColumn(false, "build_num")]
		public int BuildNum { get { return _build_num; } set { _build_num = value; } }

		private int _faction_money = 0;
		[DbColumn(false, "faction_money")]
		public int FactionMoney { get { return _faction_money; } set { _faction_money = value; } }

		private int _reward_id = 0;
		[DbColumn(false, "reward_id")]
		public int RewardId { get { return _reward_id; } set { _reward_id = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }


		private List<int> _event_parameters = new List<int>();
		[DbSplitColumn(typeof(int), "event_parameter", false)]
		public List<int> EventParameters { get { return _event_parameters; } }


		private List<Rewards> _rewardss = new List<Rewards>();
		[DbSplitColumn(typeof(Rewards), "rewards", true)]
		public List<Rewards> Rewardss { get { return _rewardss; } }


		private List<GainData> _gain_datas = new List<GainData>();
		[DbSplitColumn(typeof(GainData), "gain_data", true)]
		public List<GainData> GainDatas { get { return _gain_datas; } }


		private List<int> _level_ranges = new List<int>();
		[DbSplitRange("level_range")]
		public List<int> LevelRanges { get { return _level_ranges; } }

	}

	public class FactionTreeMissionConfig : Configuration
	{
		private List<FactionTreeMission> _faction_tree_missions = null;
		private Dictionary<int, FactionTreeMission> _faction_tree_missionMap = new Dictionary<int, FactionTreeMission>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_faction_tree_missions = DbClassLoader.Instance.QueryAllData<FactionTreeMission>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _faction_tree_mission in _faction_tree_missions)
			{
				if (_faction_tree_missionMap.ContainsKey(_faction_tree_mission.Id) == false)
					_faction_tree_missionMap.Add(_faction_tree_mission.Id, _faction_tree_mission);
				else
					_faction_tree_missionMap[_faction_tree_mission.Id] = _faction_tree_mission;

				if (_refMap.ContainsKey(_faction_tree_mission.Id) == false)
					_refMap.Add(_faction_tree_mission.Id, DateTime.Now.Ticks);
				else
					_refMap[_faction_tree_mission.Id] = DateTime.Now.Ticks;
			}
		}

		public List<FactionTreeMission> FactionTreeMissions
		{
			get
			{
				if (_faction_tree_missions == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _faction_tree_missions;
			}
		}

		public FactionTreeMission Get(int id)
		{
			if(id <= 0)
				return null;
			FactionTreeMission faction_tree_mission = null;
			if (_faction_tree_missionMap.TryGetValue(id, out faction_tree_mission))
			{
				_refMap[faction_tree_mission.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return faction_tree_mission;
			}

			faction_tree_mission = DbClassLoader.Instance.QueryData<FactionTreeMission>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (faction_tree_mission == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `id` value in table `faction_tree_mission` : " + id);
#endif
				return null;
			}

			_faction_tree_missionMap.Add(id, faction_tree_mission);
			if (_refMap.ContainsKey(faction_tree_mission.Id) == false)
				_refMap.Add(faction_tree_mission.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return faction_tree_mission;
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
					_faction_tree_missionMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _faction_tree_missionMap.Count <= 0)
				_faction_tree_missions = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, FactionTreeMission faction_tree_mission)
		{
			FactionTreeMissions.RemoveAll(n => n.Id == key);
			if (_faction_tree_missionMap.ContainsKey(key))
			{
				_faction_tree_missionMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (faction_tree_mission != null)
			{
				FactionTreeMissions.Add(faction_tree_mission);
				_faction_tree_missionMap.Add(key, faction_tree_mission);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
