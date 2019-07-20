using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("guide_child", "GuideChild", "", "")]
	sealed public class GuideChild : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private int _menu_nav_id = 0;
		[DbColumn(false, "menu_nav_id")]
		public int MenuNavId { get { return _menu_nav_id; } set { _menu_nav_id = value; } }

		private string _menu_name = "";
		[DbColumn(false, "menu_name")]
		public string MenuName { get { return _menu_name; } set { _menu_name = value; } }

		private int _is_key_guide = 0;
		[DbColumn(false, "is_key_guide")]
		public int IsKeyGuide { get { return _is_key_guide; } set { _is_key_guide = value; } }

		private int _target_type = 0;
		[DbColumn(false, "target_type")]
		public int TargetType { get { return _target_type; } set { _target_type = value; } }

		private string _target_param = "";
		[DbColumn(false, "target_param")]
		public string TargetParam { get { return _target_param; } set { _target_param = value; } }

		private int _is_npc_talk = 0;
		[DbColumn(false, "is_npc_talk")]
		public int IsNpcTalk { get { return _is_npc_talk; } set { _is_npc_talk = value; } }

		private string _npc_icon_name = "";
		[DbColumn(false, "npc_icon_name")]
		public string NpcIconName { get { return _npc_icon_name; } set { _npc_icon_name = value; } }

		private int _npc_postion_x = 0;
		[DbColumn(false, "npc_postion_x")]
		public int NpcPostionX { get { return _npc_postion_x; } set { _npc_postion_x = value; } }

		private int _npc_postion_y = 0;
		[DbColumn(false, "npc_postion_y")]
		public int NpcPostionY { get { return _npc_postion_y; } set { _npc_postion_y = value; } }

		private string _npc_message = "";
		[DbColumn(false, "npc_message")]
		public string NpcMessage { get { return _npc_message; } set { _npc_message = value; } }

		private int _arrow_direction = 0;
		[DbColumn(false, "arrow_direction")]
		public int ArrowDirection { get { return _arrow_direction; } set { _arrow_direction = value; } }

		private string _mission = "";
		[DbColumn(false, "mission")]
		public string Mission { get { return _mission; } set { _mission = value; } }

		private int _parent_id = 0;
		[DbColumn(false, "parent_id")]
		public int ParentId { get { return _parent_id; } set { _parent_id = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

	}

	public class GuideChildConfig : Configuration
	{
		private List<GuideChild> _guide_childs = null;
		private Dictionary<int, GuideChild> _guide_childMap = new Dictionary<int, GuideChild>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_guide_childs = DbClassLoader.Instance.QueryAllData<GuideChild>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _guide_child in _guide_childs)
			{
				if (_guide_childMap.ContainsKey(_guide_child.Id) == false)
					_guide_childMap.Add(_guide_child.Id, _guide_child);
				else
					_guide_childMap[_guide_child.Id] = _guide_child;

				if (_refMap.ContainsKey(_guide_child.Id) == false)
					_refMap.Add(_guide_child.Id, DateTime.Now.Ticks);
				else
					_refMap[_guide_child.Id] = DateTime.Now.Ticks;
			}
		}

		public List<GuideChild> GuideChilds
		{
			get
			{
				if (_guide_childs == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _guide_childs;
			}
		}

		public GuideChild Get(int id)
		{
			if(id <= 0)
				return null;
			GuideChild guide_child = null;
			if (_guide_childMap.TryGetValue(id, out guide_child))
			{
				_refMap[guide_child.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return guide_child;
			}

			guide_child = DbClassLoader.Instance.QueryData<GuideChild>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (guide_child == null)
			{
#if UNITY_EDITOR
				LoggerManager.Instance.Warn("Invalid `id` value in table `guide_child` : " + id);
#endif
				return null;
			}

			_guide_childMap.Add(id, guide_child);
			if (_refMap.ContainsKey(guide_child.Id) == false)
				_refMap.Add(guide_child.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return guide_child;
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
					_guide_childMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _guide_childMap.Count <= 0)
				_guide_childs = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, GuideChild guide_child)
		{
			GuideChilds.RemoveAll(n => n.Id == key);
			if (_guide_childMap.ContainsKey(key))
			{
				_guide_childMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (guide_child != null)
			{
				GuideChilds.Add(guide_child);
				_guide_childMap.Add(key, guide_child);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
