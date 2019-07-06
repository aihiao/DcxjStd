using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("league_boss", "LeagueBoss", "", "")]
    public sealed class LeagueBoss : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private int _league_level = 0;
		[DbColumn(false, "league_level")]
		public int LeagueLevel { get { return _league_level; } set { _league_level = value; } }

		private int _ligeance_count = 0;
		[DbColumn(false, "ligeance_count")]
		public int LigeanceCount { get { return _ligeance_count; } set { _ligeance_count = value; } }

		private int _dungeon_reward = 0;
		[DbColumn(false, "dungeon_reward")]
		public int DungeonReward { get { return _dungeon_reward; } set { _dungeon_reward = value; } }

		private int _activity_email_id = 0;
		[DbColumn(false, "activity_email_id")]
		public int ActivityEmailId { get { return _activity_email_id; } set { _activity_email_id = value; } }

		private int _activity_money = 0;
		[DbColumn(false, "activity_money")]
		public int ActivityMoney { get { return _activity_money; } set { _activity_money = value; } }

		private int _activity_contribution = 0;
		[DbColumn(false, "activity_contribution")]
		public int ActivityContribution { get { return _activity_contribution; } set { _activity_contribution = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }


		private List<DungeonWeight> _dungeon_weights = new List<DungeonWeight>();
		[DbSplitColumn(typeof(DungeonWeight), "dungeon_weight", true)]
		public List<DungeonWeight> DungeonWeights { get { return _dungeon_weights; } }


		private List<int> _ligeance_shows = new List<int>();
		[DbSplitColumn(typeof(int), "ligeance_show", false)]
		public List<int> LigeanceShows { get { return _ligeance_shows; } }


		private List<ActivityReward> _activity_rewards = new List<ActivityReward>();
		[DbSplitColumn(typeof(ActivityReward), "activity_reward", true)]
		public List<ActivityReward> ActivityRewards { get { return _activity_rewards; } }

	}

	public class LeagueBossConfig : Configuration
	{
		private List<LeagueBoss> _league_bosss = null;
		private Dictionary<int, LeagueBoss> _league_bossMap = new Dictionary<int, LeagueBoss>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_league_bosss = DbClassLoader.Instance.QueryAllData<LeagueBoss>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _league_boss in _league_bosss)
			{
				if (_league_bossMap.ContainsKey(_league_boss.Id) == false)
					_league_bossMap.Add(_league_boss.Id, _league_boss);
				else
					_league_bossMap[_league_boss.Id] = _league_boss;

				if (_refMap.ContainsKey(_league_boss.Id) == false)
					_refMap.Add(_league_boss.Id, DateTime.Now.Ticks);
				else
					_refMap[_league_boss.Id] = DateTime.Now.Ticks;
			}
		}

		public List<LeagueBoss> LeagueBosss
		{
			get
			{
				if (_league_bosss == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _league_bosss;
			}
		}

		public LeagueBoss Get(int id)
		{
			if(id <= 0)
				return null;
			LeagueBoss league_boss = null;
			if (_league_bossMap.TryGetValue(id, out league_boss))
			{
				_refMap[league_boss.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return league_boss;
			}

			league_boss = DbClassLoader.Instance.QueryData<LeagueBoss>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (league_boss == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `id` value in table `league_boss` : " + id);
#endif
				return null;
			}

			_league_bossMap.Add(id, league_boss);
			if (_refMap.ContainsKey(league_boss.Id) == false)
				_refMap.Add(league_boss.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return league_boss;
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
					_league_bossMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _league_bossMap.Count <= 0)
				_league_bosss = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, LeagueBoss league_boss)
		{
			LeagueBosss.RemoveAll(n => n.Id == key);
			if (_league_bossMap.ContainsKey(key))
			{
				_league_bossMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (league_boss != null)
			{
				LeagueBosss.Add(league_boss);
				_league_bossMap.Add(key, league_boss);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
