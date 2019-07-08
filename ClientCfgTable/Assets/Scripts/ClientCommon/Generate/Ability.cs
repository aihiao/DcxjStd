using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("editor_ability", "Ability", "", "")]
	sealed public class Ability : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private string _skill_name = "";
		[DbColumn(false, "skill_name")]
		public string SkillName { get { return _skill_name; } set { _skill_name = value; } }

		private int _ability_type = 0;
		[DbColumn(false, "ability_type")]
		public int AbilityType { get { return _ability_type; } set { _ability_type = value; } }

		private bool _is_start_active = false;
		[DbColumn(false, "is_start_active")]
		public bool IsStartActive { get { return _is_start_active; } set { _is_start_active = value; } }

		private string _casting_action_animation_id = "";
		[DbColumn(false, "casting_action_animation_id")]
		public string CastingActionAnimationId { get { return _casting_action_animation_id; } set { _casting_action_animation_id = value; } }

		private int _casting_action_animation_type = 0;
		[DbColumn(false, "casting_action_animation_type")]
		public int CastingActionAnimationType { get { return _casting_action_animation_type; } set { _casting_action_animation_type = value; } }

		private string _completion_action_animation_id = "";
		[DbColumn(false, "completion_action_animation_id")]
		public string CompletionActionAnimationId { get { return _completion_action_animation_id; } set { _completion_action_animation_id = value; } }

		private int _completion_action_animation_type = 0;
		[DbColumn(false, "completion_action_animation_type")]
		public int CompletionActionAnimationType { get { return _completion_action_animation_type; } set { _completion_action_animation_type = value; } }

		private string _max_range = "";
		[DbColumn(false, "max_range")]
		public string MaxRange { get { return _max_range; } set { _max_range = value; } }

		private int _casting_time = 0;
		[DbColumn(false, "casting_time")]
		public int CastingTime { get { return _casting_time; } set { _casting_time = value; } }

		private string _cooldown_time = "";
		[DbColumn(false, "cooldown_time")]
		public string CooldownTime { get { return _cooldown_time; } set { _cooldown_time = value; } }

		private string _expiration_time = "";
		[DbColumn(false, "expiration_time")]
		public string ExpirationTime { get { return _expiration_time; } set { _expiration_time = value; } }

		private int _target_type = 0;
		[DbColumn(false, "target_type")]
		public int TargetType { get { return _target_type; } set { _target_type = value; } }

		private bool _cannot_be_interrupted = false;
		[DbColumn(false, "cannot_be_interrupted")]
		public bool CannotBeInterrupted { get { return _cannot_be_interrupted; } set { _cannot_be_interrupted = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

		private List<AbilityComponent> _ability_sub_ability_components = new List<AbilityComponent>();
		[DbColumnSubTable(typeof(AbilityComponent), "editor_ability_sub_ability_component")]
		public List<AbilityComponent> AbilityComponents { get { return _ability_sub_ability_components; } }

		private List<AbilityParameter> _ability_sub_ability_parameters = new List<AbilityParameter>();
		[DbColumnSubTable(typeof(AbilityParameter), "editor_ability_sub_ability_parameter")]
		public List<AbilityParameter> AbilityParameters { get { return _ability_sub_ability_parameters; } }

	}

	public class AbilityConfig : Configuration
	{
		private List<Ability> _abilitys = null;
		private Dictionary<int, Ability> _abilityMap = new Dictionary<int, Ability>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_abilitys = DbClassLoader.Instance.QueryAllData<Ability>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _ability in _abilitys)
			{
				if (_abilityMap.ContainsKey(_ability.Id) == false)
					_abilityMap.Add(_ability.Id, _ability);
				else
					_abilityMap[_ability.Id] = _ability;

				if (_refMap.ContainsKey(_ability.Id) == false)
					_refMap.Add(_ability.Id, DateTime.Now.Ticks);
				else
					_refMap[_ability.Id] = DateTime.Now.Ticks;
			}
		}

		public List<Ability> Abilitys
		{
			get
			{
				if (_abilitys == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _abilitys;
			}
		}

		public Ability Get(int id)
		{
			if(id <= 0)
				return null;
			Ability ability = null;
			if (_abilityMap.TryGetValue(id, out ability))
			{
				_refMap[ability.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return ability;
			}

			ability = DbClassLoader.Instance.QueryData<Ability>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (ability == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `id` value in table `ability` : " + id);
#endif
				return null;
			}

			_abilityMap.Add(id, ability);
			if (_refMap.ContainsKey(ability.Id) == false)
				_refMap.Add(ability.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return ability;
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
					_abilityMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _abilityMap.Count <= 0)
				_abilitys = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, Ability ability)
		{
			Abilitys.RemoveAll(n => n.Id == key);
			if (_abilityMap.ContainsKey(key))
			{
				_abilityMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (ability != null)
			{
				Abilitys.Add(ability);
				_abilityMap.Add(key, ability);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
