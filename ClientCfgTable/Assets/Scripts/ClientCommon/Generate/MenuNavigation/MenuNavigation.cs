using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("menu_navigation", "MenuNavigation", "", "")]
	sealed public class MenuNavigation : AutoCreateConfigElem
	{
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private string _ui_register_name = "";
		[DbColumn(false, "ui_register_name")]
		public string UiRegisterName { get { return _ui_register_name; } set { _ui_register_name = value; } }

		private string _menu_nav_param = "";
		[DbColumn(false, "menu_nav_param")]
		public string MenuNavParam { get { return _menu_nav_param; } set { _menu_nav_param = value; } }

		private int _close_all_ui = 0;
		[DbColumn(false, "close_all_ui")]
		public int CloseAllUi { get { return _close_all_ui; } set { _close_all_ui = value; } }

		private int _menu_unlock_type = 0;
		[DbColumn(false, "menu_unlock_type")]
		public int MenuUnlockType { get { return _menu_unlock_type; } set { _menu_unlock_type = value; } }

		private int _menu_unlock_para = 0;
		[DbColumn(false, "menu_unlock_para")]
		public int MenuUnlockPara { get { return _menu_unlock_para; } set { _menu_unlock_para = value; } }

		private string _unlock_ele_id = "";
		[DbColumn(false, "unlock_ele_id")]
		public string UnlockEleId { get { return _unlock_ele_id; } set { _unlock_ele_id = value; } }

		private string _unlock_ele_icon = "";
		[DbColumn(false, "unlock_ele_icon")]
		public string UnlockEleIcon { get { return _unlock_ele_icon; } set { _unlock_ele_icon = value; } }

		private int _parent_menu_id = 0;
		[DbColumn(false, "parent_menu_id")]
		public int ParentMenuId { get { return _parent_menu_id; } set { _parent_menu_id = value; } }

		private string _comment = "";
		[DbColumn(false, "comment")]
		public string Comment { get { return _comment; } set { _comment = value; } }

		private int _priority = 0;
		[DbColumn(false, "priority")]
		public int Priority { get { return _priority; } set { _priority = value; } }

		private int _gain_type = 0;
		[DbColumn(false, "gain_type")]
		public int GainType { get { return _gain_type; } set { _gain_type = value; } }

		private string _gain_name = "";
		[DbColumn(false, "gain_name")]
		public string GainName { get { return _gain_name; } set { _gain_name = value; } }

		private int _abandoned = 0;
		[DbColumn(false, "abandoned")]
		public int Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }


		private List<string> _menu_open_params = new List<string>();
		[DbSplitColumn(typeof(string), "menu_open_param", false)]
		public List<string> MenuOpenParams { get { return _menu_open_params; } }


		private List<int> _guide_ids = new List<int>();
		[DbSplitColumn(typeof(int), "guide_id", false)]
		public List<int> GuideIds { get { return _guide_ids; } }

	}

	public class MenuNavigationConfig : Configuration
	{
		private List<MenuNavigation> _menu_navigations = null;
		private Dictionary<int, MenuNavigation> _menu_navigationMap = new Dictionary<int, MenuNavigation>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_menu_navigations = DbClassLoader.Instance.QueryAllData<MenuNavigation>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _menu_navigation in _menu_navigations)
			{
				if (_menu_navigationMap.ContainsKey(_menu_navigation.Id) == false)
					_menu_navigationMap.Add(_menu_navigation.Id, _menu_navigation);
				else
					_menu_navigationMap[_menu_navigation.Id] = _menu_navigation;

				if (_refMap.ContainsKey(_menu_navigation.Id) == false)
					_refMap.Add(_menu_navigation.Id, DateTime.Now.Ticks);
				else
					_refMap[_menu_navigation.Id] = DateTime.Now.Ticks;
			}
		}

		public List<MenuNavigation> MenuNavigations
		{
			get
			{
				if (_menu_navigations == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _menu_navigations;
			}
		}

		public MenuNavigation Get(int id)
		{
			if(id <= 0)
				return null;
			MenuNavigation menu_navigation = null;
			if (_menu_navigationMap.TryGetValue(id, out menu_navigation))
			{
				_refMap[menu_navigation.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return menu_navigation;
			}

			menu_navigation = DbClassLoader.Instance.QueryData<MenuNavigation>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (menu_navigation == null)
			{
#if UNITY_EDITOR
                LoggerManager.Instance.Warn("Invalid `id` value in table `menu_navigation` : {0}", id);
#endif
				return null;
			}

			_menu_navigationMap.Add(id, menu_navigation);
			if (_refMap.ContainsKey(menu_navigation.Id) == false)
				_refMap.Add(menu_navigation.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return menu_navigation;
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
					_menu_navigationMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _menu_navigationMap.Count <= 0)
				_menu_navigations = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, MenuNavigation menu_navigation)
		{
			MenuNavigations.RemoveAll(n => n.Id == key);
			if (_menu_navigationMap.ContainsKey(key))
			{
				_menu_navigationMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (menu_navigation != null)
			{
				MenuNavigations.Add(menu_navigation);
				_menu_navigationMap.Add(key, menu_navigation);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
