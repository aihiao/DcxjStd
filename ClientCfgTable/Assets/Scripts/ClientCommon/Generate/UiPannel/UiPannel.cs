using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("ui_pannel", "UiPannel", "", "")]
	sealed public class UiPannel : AutoCreateConfigElem
	{
#if UNITY_EDITOR
		private string _desc_designer_owner = "";
		[DbColumn(false, "desc_designer_owner")]
		public string DescDesignerOwner { get { return _desc_designer_owner; } set { _desc_designer_owner = value; } }

		private string _desc_program_owner = "";
		[DbColumn(false, "desc_program_owner")]
		public string DescProgramOwner { get { return _desc_program_owner; } set { _desc_program_owner = value; } }

		private string _desc_other_name = "";
		[DbColumn(false, "desc_other_name")]
		public string DescOtherName { get { return _desc_other_name; } set { _desc_other_name = value; } }

		private string _desc = "";
		[DbColumn(false, "desc")]
		public string Desc { get { return _desc; } set { _desc = value; } }

#endif
		private string _id = "";
		[DbColumn(true, "id")]
		public string Id { get { return _id; } set { _id = value; } }

		private bool _can_close_by_back_key = false;
		[DbColumn(false, "can_close_by_back_key")]
		public bool CanCloseByBackKey { get { return _can_close_by_back_key; } set { _can_close_by_back_key = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

	}

	public class UiPannelConfig : Configuration
	{
		private List<UiPannel> _ui_pannels = null;
		private Dictionary<string, UiPannel> _ui_pannelMap = new Dictionary<string, UiPannel>();
		private Dictionary<string, long> _refMap = new Dictionary<string, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_ui_pannels = DbClassLoader.Instance.QueryAllData<UiPannel>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _ui_pannel in _ui_pannels)
			{
				if (_ui_pannelMap.ContainsKey(_ui_pannel.Id) == false)
					_ui_pannelMap.Add(_ui_pannel.Id, _ui_pannel);
				else
					_ui_pannelMap[_ui_pannel.Id] = _ui_pannel;

				if (_refMap.ContainsKey(_ui_pannel.Id) == false)
					_refMap.Add(_ui_pannel.Id, DateTime.Now.Ticks);
				else
					_refMap[_ui_pannel.Id] = DateTime.Now.Ticks;
			}
		}

		public List<UiPannel> UiPannels
		{
			get
			{
				if (_ui_pannels == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _ui_pannels;
			}
		}

		public UiPannel Get(string id)
		{
			UiPannel ui_pannel = null;
			if (_ui_pannelMap.TryGetValue(id, out ui_pannel))
			{
				_refMap[ui_pannel.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return ui_pannel;
			}

			ui_pannel = DbClassLoader.Instance.QueryData<UiPannel>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (ui_pannel == null)
			{
#if UNITY_EDITOR
				LoggerManager.Instance.Warn("Invalid `id` value in table `ui_pannel` : " + id);
#endif
				return null;
			}

			_ui_pannelMap.Add(id, ui_pannel);
			if (_refMap.ContainsKey(ui_pannel.Id) == false)
				_refMap.Add(ui_pannel.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return ui_pannel;
		}

		public override void ReleaseData(bool isForce)
		{
			long nowtime = GetCurrentTimeTick();
			if (!isForce && nowtime - lastCheckReleaseTime < CheckReleaseTime)
				return;
			lastCheckReleaseTime = nowtime;


			var keys = new List<string>(_refMap.Keys);
			for (int index = 0; index < keys.Count; index++)
			{
				var key = keys[index];
				if (isForce || nowtime - _refMap[key] > MaxStayTime)
				{
					_ui_pannelMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _ui_pannelMap.Count <= 0)
				_ui_pannels = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(string key, UiPannel ui_pannel)
		{
			UiPannels.RemoveAll(n => n.Id == key);
			if (_ui_pannelMap.ContainsKey(key))
			{
				_ui_pannelMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (ui_pannel != null)
			{
				UiPannels.Add(ui_pannel);
				_ui_pannelMap.Add(key, ui_pannel);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
