using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("monster_self_adaption", "MonsterSelfAdaption", "", "")]
	sealed public class MonsterSelfAdaption : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private int _monster_level = 0;
		[DbColumn(false, "monster_level")]
		public int MonsterLevel { get { return _monster_level; } set { _monster_level = value; } }

		private List<MonsterSelfAdaptionList> _monster_self_adaption_list = new List<MonsterSelfAdaptionList>();
		[DbMergeColumn(typeof(MonsterSelfAdaptionList), "MonsterSelfAdaptionList")]
		public List<MonsterSelfAdaptionList> MonsterSelfAdaptionLists { get { return _monster_self_adaption_list; } }


#if UNITY_EDITOR
		private string _description = "";
		[DbColumn(false, "description")]
		public string Description { get { return _description; } set { _description = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

#endif
	}

	public class MonsterSelfAdaptionConfig : Configuration
	{
		private List<MonsterSelfAdaption> _monster_self_adaptions = null;
		private Dictionary<int, MonsterSelfAdaption> _monster_self_adaptionMap = new Dictionary<int, MonsterSelfAdaption>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_monster_self_adaptions = DbClassLoader.Instance.QueryAllData<MonsterSelfAdaption>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _monster_self_adaption in _monster_self_adaptions)
			{
				if (_monster_self_adaptionMap.ContainsKey(_monster_self_adaption.Id) == false)
					_monster_self_adaptionMap.Add(_monster_self_adaption.Id, _monster_self_adaption);
				else
					_monster_self_adaptionMap[_monster_self_adaption.Id] = _monster_self_adaption;

				if (_refMap.ContainsKey(_monster_self_adaption.Id) == false)
					_refMap.Add(_monster_self_adaption.Id, DateTime.Now.Ticks);
				else
					_refMap[_monster_self_adaption.Id] = DateTime.Now.Ticks;
			}
		}

		public List<MonsterSelfAdaption> MonsterSelfAdaptions
		{
			get
			{
				if (_monster_self_adaptions == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _monster_self_adaptions;
			}
		}

		public MonsterSelfAdaption Get(int id)
		{
			if(id <= 0)
				return null;
			MonsterSelfAdaption monster_self_adaption = null;
			if (_monster_self_adaptionMap.TryGetValue(id, out monster_self_adaption))
			{
				_refMap[monster_self_adaption.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return monster_self_adaption;
			}

			monster_self_adaption = DbClassLoader.Instance.QueryData<MonsterSelfAdaption>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (monster_self_adaption == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `id` value in table `monster_self_adaption` : " + id);
#endif
				return null;
			}

			_monster_self_adaptionMap.Add(id, monster_self_adaption);
			if (_refMap.ContainsKey(monster_self_adaption.Id) == false)
				_refMap.Add(monster_self_adaption.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return monster_self_adaption;
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
					_monster_self_adaptionMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _monster_self_adaptionMap.Count <= 0)
				_monster_self_adaptions = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, MonsterSelfAdaption monster_self_adaption)
		{
			MonsterSelfAdaptions.RemoveAll(n => n.Id == key);
			if (_monster_self_adaptionMap.ContainsKey(key))
			{
				_monster_self_adaptionMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (monster_self_adaption != null)
			{
				MonsterSelfAdaptions.Add(monster_self_adaption);
				_monster_self_adaptionMap.Add(key, monster_self_adaption);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
