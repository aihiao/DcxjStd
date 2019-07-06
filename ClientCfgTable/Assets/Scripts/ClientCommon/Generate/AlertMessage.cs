using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("alert_message", "AlertMessage", "", "")]
	sealed public class AlertMessage : AutoCreateConfigElem
	{
		private string _id = "";
		[DbColumn(false, "id")]
		public string Id { get { return _id; } set { _id = value; } }

		private int _code = 0;
		[DbColumn(true, "code")]
		public int Code { get { return _code; } set { _code = value; } }

		private int _type = 0;
		[DbColumn(false, "type")]
		public int Type { get { return _type; } set { _type = value; } }

		private string _content = "";
		[DbColumn(false, "content")]
		public string Content { get { return _content; } set { _content = value; } }

		private string _ok_label = "";
		[DbColumn(false, "ok_label")]
		public string OkLabel { get { return _ok_label; } set { _ok_label = value; } }

		private string _cancel_label = "";
		[DbColumn(false, "cancel_label")]
		public string CancelLabel { get { return _cancel_label; } set { _cancel_label = value; } }

		private string _yes_label = "";
		[DbColumn(false, "yes_label")]
		public string YesLabel { get { return _yes_label; } set { _yes_label = value; } }

		private string _no_label = "";
		[DbColumn(false, "no_label")]
		public string NoLabel { get { return _no_label; } set { _no_label = value; } }

		private string _description = "";
		[DbColumn(false, "description")]
		public string Description { get { return _description; } set { _description = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

	}

	public class AlertMessageConfig : Configuration
	{
		private List<AlertMessage> _alert_messages = null;
		private Dictionary<int, AlertMessage> _alert_messageMap = new Dictionary<int, AlertMessage>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_alert_messages = DbClassLoader.Instance.QueryAllData<AlertMessage>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _alert_message in _alert_messages)
			{
				if (_alert_messageMap.ContainsKey(_alert_message.Code) == false)
					_alert_messageMap.Add(_alert_message.Code, _alert_message);
				else
					_alert_messageMap[_alert_message.Code] = _alert_message;

				if (_refMap.ContainsKey(_alert_message.Code) == false)
					_refMap.Add(_alert_message.Code, DateTime.Now.Ticks);
				else
					_refMap[_alert_message.Code] = DateTime.Now.Ticks;
			}
		}

		public List<AlertMessage> AlertMessages
		{
			get
			{
				if (_alert_messages == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _alert_messages;
			}
		}

		public AlertMessage Get(int code)
		{
			if(code <= 0)
				return null;
			AlertMessage alert_message = null;
			if (_alert_messageMap.TryGetValue(code, out alert_message))
			{
				_refMap[alert_message.Code] = GetCurrentTimeTick();
				ReleaseData(false);
				return alert_message;
			}

			alert_message = DbClassLoader.Instance.QueryData<AlertMessage>(ConfigDataBase.Instance.DbAccessorFactory, code);
			if (alert_message == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `code` value in table `alert_message` : " + code);
#endif
				return null;
			}

			_alert_messageMap.Add(code, alert_message);
			if (_refMap.ContainsKey(alert_message.Code) == false)
				_refMap.Add(alert_message.Code, GetCurrentTimeTick());

			ReleaseData(false);
			return alert_message;
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
					_alert_messageMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _alert_messageMap.Count <= 0)
				_alert_messages = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, AlertMessage alert_message)
		{
			AlertMessages.RemoveAll(n => n.Code == key);
			if (_alert_messageMap.ContainsKey(key))
			{
				_alert_messageMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (alert_message != null)
			{
				AlertMessages.Add(alert_message);
				_alert_messageMap.Add(key, alert_message);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
