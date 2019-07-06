using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("monster", "Monster", "", "")]
	sealed public class Monster : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private string _name = "";
		[DbColumn(false, "name")]
		public string Name { get { return _name; } set { _name = value; } }

		private int _object_template_id = 0;
		[DbColumn(false, "object_template_id")]
		public int ObjectTemplateId { get { return _object_template_id; } set { _object_template_id = value; } }

		private int _faction_type = 0;
		[DbColumn(false, "faction_type")]
		public int FactionType { get { return _faction_type; } set { _faction_type = value; } }

		private string _head_icon = "";
		[DbColumn(false, "head_icon")]
		public string HeadIcon { get { return _head_icon; } set { _head_icon = value; } }

		private string _model_id = "";
		[DbColumn(false, "model_id")]
		public string ModelId { get { return _model_id; } set { _model_id = value; } }

		private int _body_part_id = 0;
		[DbColumn(false, "body_part_id")]
		public int BodyPartId { get { return _body_part_id; } set { _body_part_id = value; } }

		private int _head_part_id = 0;
		[DbColumn(false, "head_part_id")]
		public int HeadPartId { get { return _head_part_id; } set { _head_part_id = value; } }

		private int _left_wpn_id = 0;
		[DbColumn(false, "left_wpn_id")]
		public int LeftWpnId { get { return _left_wpn_id; } set { _left_wpn_id = value; } }

		private int _right_wpn_id = 0;
		[DbColumn(false, "right_wpn_id")]
		public int RightWpnId { get { return _right_wpn_id; } set { _right_wpn_id = value; } }

		private int _breathe_time = 0;
		[DbColumn(false, "breathe_time")]
		public int BreatheTime { get { return _breathe_time; } set { _breathe_time = value; } }

		private double _scale = 0;
		[DbColumn(false, "scale")]
		public double Scale { get { return _scale; } set { _scale = value; } }

		private int _level = 0;
		[DbColumn(false, "level")]
		public int Level { get { return _level; } set { _level = value; } }

		private int _elite_level = 0;
		[DbColumn(false, "elite_level")]
		public int EliteLevel { get { return _elite_level; } set { _elite_level = value; } }

		private int _pushable_level = 0;
		[DbColumn(false, "pushable_level")]
		public int PushableLevel { get { return _pushable_level; } set { _pushable_level = value; } }

		private int _reward_id = 0;
		[DbColumn(false, "reward_id")]
		public int RewardId { get { return _reward_id; } set { _reward_id = value; } }

		private int _ai_data_group_id = 0;
		[DbColumn(false, "ai_data_group_id")]
		public int AiDataGroupId { get { return _ai_data_group_id; } set { _ai_data_group_id = value; } }

		private int _view_range = 0;
		[DbColumn(false, "view_range")]
		public int ViewRange { get { return _view_range; } set { _view_range = value; } }

		private int _craft_type = 0;
		[DbColumn(false, "craft_type")]
		public int CraftType { get { return _craft_type; } set { _craft_type = value; } }

		private int _should_adapta_by_player_level = 0;
		[DbColumn(false, "should_adapta_by_player_level")]
		public int ShouldAdaptaByPlayerLevel { get { return _should_adapta_by_player_level; } set { _should_adapta_by_player_level = value; } }

		private double _patrol_range = 0;
		[DbColumn(false, "patrol_range")]
		public double PatrolRange { get { return _patrol_range; } set { _patrol_range = value; } }

		private int _prefer_craft_type = 0;
		[DbColumn(false, "prefer_craft_type")]
		public int PreferCraftType { get { return _prefer_craft_type; } set { _prefer_craft_type = value; } }

		private bool _disappear_after_dead = false;
		[DbColumn(false, "disappear_after_dead")]
		public bool DisappearAfterDead { get { return _disappear_after_dead; } set { _disappear_after_dead = value; } }

		private List<MonsterList> _monster_list = new List<MonsterList>();
		[DbMergeColumn(typeof(MonsterList), "MonsterList")]
		public List<MonsterList> MonsterLists { get { return _monster_list; } }


#if UNITY_EDITOR
		private string _comment = "";
		[DbColumn(false, "comment")]
		public string Comment { get { return _comment; } set { _comment = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

#endif

		private List<int> _abilitiess = new List<int>();
		[DbSplitColumn(typeof(int), "abilities", false)]
		public List<int> Abilitiess { get { return _abilitiess; } }


		private List<AttackTalk> _attack_talks = new List<AttackTalk>();
		[DbSplitColumn(typeof(AttackTalk), "attack_talk", true)]
		public List<AttackTalk> AttackTalks { get { return _attack_talks; } }

	}

	public class MonsterConfig : Configuration
	{
		private List<Monster> _monsters = null;
		private Dictionary<int, Monster> _monsterMap = new Dictionary<int, Monster>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_monsters = DbClassLoader.Instance.QueryAllData<Monster>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _monster in _monsters)
			{
				if (_monsterMap.ContainsKey(_monster.Id) == false)
					_monsterMap.Add(_monster.Id, _monster);
				else
					_monsterMap[_monster.Id] = _monster;

				if (_refMap.ContainsKey(_monster.Id) == false)
					_refMap.Add(_monster.Id, DateTime.Now.Ticks);
				else
					_refMap[_monster.Id] = DateTime.Now.Ticks;
			}
		}

		public List<Monster> Monsters
		{
			get
			{
				if (_monsters == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _monsters;
			}
		}

		public Monster Get(int id)
		{
			if(id <= 0)
				return null;
			Monster monster = null;
			if (_monsterMap.TryGetValue(id, out monster))
			{
				_refMap[monster.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return monster;
			}

			monster = DbClassLoader.Instance.QueryData<Monster>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (monster == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `id` value in table `monster` : " + id);
#endif
				return null;
			}

			_monsterMap.Add(id, monster);
			if (_refMap.ContainsKey(monster.Id) == false)
				_refMap.Add(monster.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return monster;
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
					_monsterMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _monsterMap.Count <= 0)
				_monsters = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, Monster monster)
		{
			Monsters.RemoveAll(n => n.Id == key);
			if (_monsterMap.ContainsKey(key))
			{
				_monsterMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (monster != null)
			{
				Monsters.Add(monster);
				_monsterMap.Add(key, monster);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
