using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("text_config", "Text", "", "")]
	sealed public class Text : AutoCreateConfigElem
	{
		private string _text_key = "";
		[DbColumn(true, "text_key")]
		public string TextKey { get { return _text_key; } set { _text_key = value; } }

		private string _content = "";
		[DbColumn(false, "content")]
		public string Content { get { return _content; } set { _content = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

	}

	public class TextConfig : Configuration
	{
		private List<Text> _text_configs = null;
		private Dictionary<string, Text> _text_configMap = new Dictionary<string, Text>();
		private Dictionary<string, long> _refMap = new Dictionary<string, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_text_configs = DbClassLoader.Instance.QueryAllData<Text>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _text_config in _text_configs)
			{
				if (_text_configMap.ContainsKey(_text_config.TextKey) == false)
					_text_configMap.Add(_text_config.TextKey, _text_config);
				else
					_text_configMap[_text_config.TextKey] = _text_config;

				if (_refMap.ContainsKey(_text_config.TextKey) == false)
					_refMap.Add(_text_config.TextKey, DateTime.Now.Ticks);
				else
					_refMap[_text_config.TextKey] = DateTime.Now.Ticks;
			}
		}

		public List<Text> Texts
		{
			get
			{
				if (_text_configs == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _text_configs;
			}
		}

		public Text Get(string text_key)
		{
			Text text_config = null;
			if (_text_configMap.TryGetValue(text_key, out text_config))
			{
				_refMap[text_config.TextKey] = GetCurrentTimeTick();
				ReleaseData(false);
				return text_config;
			}

			text_config = DbClassLoader.Instance.QueryData<Text>(ConfigDataBase.Instance.DbAccessorFactory, text_key);
			if (text_config == null)
			{
#if UNITY_EDITOR
				LoggerManager.Instance.Warn("Invalid `text_key` value in table `text_config` : {0}", text_key.Replace('{', '_').Replace('}', '_'));
#endif
				return new Text() { Content = "" };
			}

			_text_configMap.Add(text_key, text_config);
			if (_refMap.ContainsKey(text_config.TextKey) == false)
				_refMap.Add(text_config.TextKey, GetCurrentTimeTick());

			ReleaseData(false);
			return text_config;
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
					_text_configMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _text_configMap.Count <= 0)
				_text_configs = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(string key, Text text_config)
		{
			Texts.RemoveAll(n => n.TextKey == key);
			if (_text_configMap.ContainsKey(key))
			{
				_text_configMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (text_config != null)
			{
				Texts.Add(text_config);
				_text_configMap.Add(key, text_config);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
