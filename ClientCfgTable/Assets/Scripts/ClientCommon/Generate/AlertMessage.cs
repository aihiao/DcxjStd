using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
	[DbTable("alert_message", "AlertMessage", "", "")]
	sealed public class AlertMessage : AutoCreateConfigElem
	{
		private string id = "";
		[DbColumn(false, "id")]
		public string Id { get { return id; } set { id = value; } }

		private int code = 0;
		[DbColumn(true, "code")]
		public int Code { get { return code; } set { code = value; } }

		private int type = 0;
		[DbColumn(false, "type")]
		public int Type { get { return type; } set { type = value; } }

		private string content = "";
		[DbColumn(false, "content")]
		public string Content { get { return content; } set { content = value; } }

		private string okLabel = "";
		[DbColumn(false, "ok_label")]
		public string OkLabel { get { return okLabel; } set { okLabel = value; } }

		private string cancelLabel = "";
		[DbColumn(false, "cancel_label")]
		public string CancelLabel { get { return cancelLabel; } set { cancelLabel = value; } }

		private string yesLabel = "";
		[DbColumn(false, "yes_label")]
		public string YesLabel { get { return yesLabel; } set { yesLabel = value; } }

		private string noLabel = "";
		[DbColumn(false, "no_label")]
		public string NoLabel { get { return noLabel; } set { noLabel = value; } }

		private string description = "";
		[DbColumn(false, "description")]
		public string Description { get { return description; } set { description = value; } }

		private bool abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return abandoned; } set { abandoned = value; } }

		private int version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return version; } set { version = value; } }

	}

	public class AlertMessageConfig : Configuration
	{
		private List<AlertMessage> altMsgList = null;

		private Dictionary<int, AlertMessage> altMsgDic = new Dictionary<int, AlertMessage>();

		private Dictionary<int, long> refDic = new Dictionary<int, long>();
		private long listRefTime = 0;
		private long lastCheckReleaseTime = 0;

		public override void LoadAllData()
		{
			altMsgList = DbClassLoader.Instance.QueryAllData<AlertMessage>(ConfigDataBase.Instance.DbAccessorFactory);

            foreach (var altMsg in altMsgList)
            {
                if (altMsgDic.ContainsKey(altMsg.Code) == false)
                {
                    altMsgDic.Add(altMsg.Code, altMsg);
                    refDic.Add(altMsg.Code, DateTime.Now.Ticks);
                }
                else
                {
                    altMsgDic[altMsg.Code] = altMsg;
                    refDic[altMsg.Code] = DateTime.Now.Ticks;
                }
			}

            HasLoadedAll = true;
		}

		public List<AlertMessage> AlertMessages
		{
			get
			{
                if (altMsgList == null)
                {
                    LoadAllData();
                }

				listRefTime = GetCurrentTimeTick();
				return altMsgList;
			}
		}

		public AlertMessage Get(int code)
		{
            if (code <= 0)
            {
                return null;
            }

			AlertMessage altMsg = null;
			if (altMsgDic.TryGetValue(code, out altMsg))
			{
				refDic[code] = GetCurrentTimeTick();
				ReleaseData(false);
				return altMsg;
			}

			altMsg = DbClassLoader.Instance.QueryData<AlertMessage>(ConfigDataBase.Instance.DbAccessorFactory, code);
			if (altMsg == null)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Invalid `code` value in table `alert_message` : " + code);
#endif
				return null;
			}

			altMsgDic.Add(code, altMsg);
            refDic.Add(code, GetCurrentTimeTick());
            
			ReleaseData(false);
			return altMsg;
		}

		public override void ReleaseData(bool isForce)
		{
			long nowtime = GetCurrentTimeTick();
            if (!isForce && nowtime - lastCheckReleaseTime < CheckReleaseTime)
            {
                return;
            }
			lastCheckReleaseTime = nowtime;

			var keys = new List<int>(refDic.Keys);
			for (int index = 0; index < keys.Count; index++)
			{
				var key = keys[index];
				if (isForce || nowtime - refDic[key] > MaxStayTime)
				{
					altMsgDic.Remove(key);
					refDic.Remove(key);
                }
			}

            if (isForce || nowtime - listRefTime > MaxStayTime || altMsgDic.Count <= 0)
            {
                altMsgList = null;
                HasLoadedAll = false;
                DbClassLoader.Instance.Release<AlertMessage>(ConfigDataBase.Instance.DbAccessorFactory);
            }
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, AlertMessage altMsg)
		{
            AlertMessages.RemoveAll(n => n.Code == key);
			if (altMsgDic.ContainsKey(key))
			{
				altMsgDic.Remove(key);
                refDic.Remove(key);
			}

			if (altMsg != null)
			{
				AlertMessages.Add(altMsg);
				altMsgDic.Add(key, altMsg);
				refDic.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
