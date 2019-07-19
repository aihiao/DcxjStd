using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("dungeon", "Dungeon", "", "")]
	sealed public class Dungeon : AutoCreateConfigElem
	{
		private int _dungeon_id = 0;
		[DbColumn(true, "dungeon_id")]
		public int DungeonId { get { return _dungeon_id; } set { _dungeon_id = value; } }

		private string _dungeon_icon_id = "";
		[DbColumn(false, "dungeon_icon_id")]
		public string DungeonIconId { get { return _dungeon_icon_id; } set { _dungeon_icon_id = value; } }

		private string _dungeon_name = "";
		[DbColumn(false, "dungeon_name")]
		public string DungeonName { get { return _dungeon_name; } set { _dungeon_name = value; } }

		private string _dungeon_background_id = "";
		[DbColumn(false, "dungeon_background_id")]
		public string DungeonBackgroundId { get { return _dungeon_background_id; } set { _dungeon_background_id = value; } }

		private string _description = "";
		[DbColumn(false, "description")]
		public string Description { get { return _description; } set { _description = value; } }

		private int _dungeon_type = 0;
		[DbColumn(false, "dungeon_type")]
		public int DungeonType { get { return _dungeon_type; } set { _dungeon_type = value; } }

		private int _dungeon_open_level = 0;
		[DbColumn(false, "dungeon_open_level")]
		public int DungeonOpenLevel { get { return _dungeon_open_level; } set { _dungeon_open_level = value; } }

		private int _dungeon_recommend_fight_power = 0;
		[DbColumn(false, "dungeon_recommend_fight_power")]
		public int DungeonRecommendFightPower { get { return _dungeon_recommend_fight_power; } set { _dungeon_recommend_fight_power = value; } }

		private int _enter_times = 0;
		[DbColumn(false, "enter_times")]
		public int EnterTimes { get { return _enter_times; } set { _enter_times = value; } }

		private int _cost_stamina = 0;
		[DbColumn(false, "cost_stamina")]
		public int CostStamina { get { return _cost_stamina; } set { _cost_stamina = value; } }

		private int _first_pass_reward_id = 0;
		[DbColumn(false, "first_pass_reward_id")]
		public int FirstPassRewardId { get { return _first_pass_reward_id; } set { _first_pass_reward_id = value; } }

		private int _perfect_pass_reward_id = 0;
		[DbColumn(false, "perfect_pass_reward_id")]
		public int PerfectPassRewardId { get { return _perfect_pass_reward_id; } set { _perfect_pass_reward_id = value; } }

		private int _pass_reward_id = 0;
		[DbColumn(false, "pass_reward_id")]
		public int PassRewardId { get { return _pass_reward_id; } set { _pass_reward_id = value; } }

		private int _unlock_dungeon_id = 0;
		[DbColumn(false, "unlock_dungeon_id")]
		public int UnlockDungeonId { get { return _unlock_dungeon_id; } set { _unlock_dungeon_id = value; } }

		private int _scene_id = 0;
		[DbColumn(false, "scene_id")]
		public int SceneId { get { return _scene_id; } set { _scene_id = value; } }

		private string _comment = "";
		[DbColumn(false, "comment")]
		public string Comment { get { return _comment; } set { _comment = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }


		private List<int> _dungeon_unlock_idss = new List<int>();
		[DbSplitColumn(typeof(int), "dungeon_unlock_ids", false)]
		public List<int> DungeonUnlockIdss { get { return _dungeon_unlock_idss; } }


		private List<int> _perfect_pass_condition_ids = new List<int>();
		[DbSplitColumn(typeof(int), "perfect_pass_condition_id", false)]
		public List<int> PerfectPassConditionIds { get { return _perfect_pass_condition_ids; } }


		private List<int> _positions = new List<int>();
		[DbSplitColumn(typeof(int), "position", false)]
		public List<int> Positions { get { return _positions; } }


		private List<DungeonDropInfo> _dungeon_drop_infos = new List<DungeonDropInfo>();
		[DbSplitColumn(typeof(DungeonDropInfo), "dungeon_drop_info", true)]
		public List<DungeonDropInfo> DungeonDropInfos { get { return _dungeon_drop_infos; } }

	}

	public class DungeonConfig : Configuration
	{
		private List<Dungeon> _dungeons = null;
		private Dictionary<int, Dungeon> _dungeonMap = new Dictionary<int, Dungeon>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_dungeons = DbClassLoader.Instance.QueryAllData<Dungeon>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _dungeon in _dungeons)
			{
				if (_dungeonMap.ContainsKey(_dungeon.DungeonId) == false)
					_dungeonMap.Add(_dungeon.DungeonId, _dungeon);
				else
					_dungeonMap[_dungeon.DungeonId] = _dungeon;

				if (_refMap.ContainsKey(_dungeon.DungeonId) == false)
					_refMap.Add(_dungeon.DungeonId, DateTime.Now.Ticks);
				else
					_refMap[_dungeon.DungeonId] = DateTime.Now.Ticks;
			}
		}

		public List<Dungeon> Dungeons
		{
			get
			{
				if (_dungeons == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _dungeons;
			}
		}

		public Dungeon Get(int dungeon_id)
		{
			if(dungeon_id <= 0)
				return null;
			Dungeon dungeon = null;
			if (_dungeonMap.TryGetValue(dungeon_id, out dungeon))
			{
				_refMap[dungeon.DungeonId] = GetCurrentTimeTick();
				ReleaseData(false);
				return dungeon;
			}

			dungeon = DbClassLoader.Instance.QueryData<Dungeon>(ConfigDataBase.Instance.DbAccessorFactory, dungeon_id);
			if (dungeon == null)
			{
#if UNITY_EDITOR
				LoggerManager.Instance.Warn("Invalid `dungeon_id` value in table `dungeon` : {0}", dungeon_id);
#endif
				return null;
			}

			_dungeonMap.Add(dungeon_id, dungeon);
			if (_refMap.ContainsKey(dungeon.DungeonId) == false)
				_refMap.Add(dungeon.DungeonId, GetCurrentTimeTick());

			ReleaseData(false);
			return dungeon;
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
					_dungeonMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _dungeonMap.Count <= 0)
				_dungeons = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, Dungeon dungeon)
		{
			Dungeons.RemoveAll(n => n.DungeonId == key);
			if (_dungeonMap.ContainsKey(key))
			{
				_dungeonMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (dungeon != null)
			{
				Dungeons.Add(dungeon);
				_dungeonMap.Add(key, dungeon);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
