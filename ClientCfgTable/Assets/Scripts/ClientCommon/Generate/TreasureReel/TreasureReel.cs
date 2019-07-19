using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("treasure_reel", "TreasureReel", "", "")]
	sealed public class TreasureReel : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private string _name = "";
		[DbColumn(false, "name")]
		public string Name { get { return _name; } set { _name = value; } }

		private int _quality = 0;
		[DbColumn(false, "quality")]
		public int Quality { get { return _quality; } set { _quality = value; } }

		private string _icon_name = "";
		[DbColumn(false, "icon_name")]
		public string IconName { get { return _icon_name; } set { _icon_name = value; } }

		private string _description = "";
		[DbColumn(false, "description")]
		public string Description { get { return _description; } set { _description = value; } }

		private int _treasure_id = 0;
		[DbColumn(false, "treasure_id")]
		public int TreasureId { get { return _treasure_id; } set { _treasure_id = value; } }

		private int _decompose_reward_id = 0;
		[DbColumn(false, "decompose_reward_id")]
		public int DecomposeRewardId { get { return _decompose_reward_id; } set { _decompose_reward_id = value; } }

		private int _level_can_use = 0;
		[DbColumn(false, "level_can_use")]
		public int LevelCanUse { get { return _level_can_use; } set { _level_can_use = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }


		private List<GainData> _gain_datas = new List<GainData>();
		[DbSplitColumn(typeof(GainData), "gain_data", true)]
		public List<GainData> GainDatas { get { return _gain_datas; } }

	}

	public class TreasureReelConfig : Configuration
	{
		private List<TreasureReel> _treasure_reels = null;
		private Dictionary<int, TreasureReel> _treasure_reelMap = new Dictionary<int, TreasureReel>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_treasure_reels = DbClassLoader.Instance.QueryAllData<TreasureReel>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _treasure_reel in _treasure_reels)
			{
				if (_treasure_reelMap.ContainsKey(_treasure_reel.Id) == false)
					_treasure_reelMap.Add(_treasure_reel.Id, _treasure_reel);
				else
					_treasure_reelMap[_treasure_reel.Id] = _treasure_reel;

				if (_refMap.ContainsKey(_treasure_reel.Id) == false)
					_refMap.Add(_treasure_reel.Id, DateTime.Now.Ticks);
				else
					_refMap[_treasure_reel.Id] = DateTime.Now.Ticks;
			}
		}

		public List<TreasureReel> TreasureReels
		{
			get
			{
				if (_treasure_reels == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _treasure_reels;
			}
		}

		public TreasureReel Get(int id)
		{
			if(id <= 0)
				return null;
			TreasureReel treasure_reel = null;
			if (_treasure_reelMap.TryGetValue(id, out treasure_reel))
			{
				_refMap[treasure_reel.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return treasure_reel;
			}

			treasure_reel = DbClassLoader.Instance.QueryData<TreasureReel>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (treasure_reel == null)
			{
#if UNITY_EDITOR
			 	LoggerManager.Instance.Warn("Invalid `id` value in table `treasure_reel` : {0}", id);
#endif
				return null;
			}

			_treasure_reelMap.Add(id, treasure_reel);
			if (_refMap.ContainsKey(treasure_reel.Id) == false)
				_refMap.Add(treasure_reel.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return treasure_reel;
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
					_treasure_reelMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _treasure_reelMap.Count <= 0)
				_treasure_reels = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, TreasureReel treasure_reel)
		{
			TreasureReels.RemoveAll(n => n.Id == key);
			if (_treasure_reelMap.ContainsKey(key))
			{
				_treasure_reelMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (treasure_reel != null)
			{
				TreasureReels.Add(treasure_reel);
				_treasure_reelMap.Add(key, treasure_reel);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
