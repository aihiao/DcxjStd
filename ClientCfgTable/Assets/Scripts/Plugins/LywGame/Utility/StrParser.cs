using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;

namespace LywGames
{
	/// <summary>
	/// 字符串转换辅助类
	/// </summary>
	public static class StrParser
	{
		public static readonly char[] splitter = { ',' };
		public static readonly string[,] spcChars = 
		{ 
			{ "#quot#", "\"" }, 
			{ "#lt#", "<" }, 
			{ "#gt#", ">" },
			{ "#amp#", "&" },
			{ "#apos#", "'" },
			{"\\n","\n"}
		};

		private static CultureInfo provider = CultureInfo.InvariantCulture;

		public static bool ParseBool(string str, bool defVal = false)
		{
			if (string.IsNullOrEmpty(str))
				return defVal;

			bool v;
			if (Boolean.TryParse(str, out v))
				return v;
			else
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing Bool value : {0}", str));
				return defVal;
			}
		}

		// ParseDecInt 原来是先转成foat，容易丢失精度，导致转换错误（坑大，不好查），现在实现全改成ParseDecIntEx了

		public static int ParseDecInt(string str, int defVal, out bool tf)
		{
			return ParseDecIntEx(str, defVal, out tf);
		}

		public static int ParseDecInt(string str, int defVal = 0)
		{
			bool tf;
			return ParseDecIntEx(str, defVal, out tf);
		}

		public static int ParseDecIntEx(string str, int defVal = 0)
		{
			bool tf;
			return ParseDecIntEx(str, defVal, out tf);
		}

		public static int ParseDecIntEx(string str, int defVal, out bool tf)
		{
			tf = false;

			if (string.IsNullOrEmpty(str))
				return defVal;

			int v;
			if (tf = Int32.TryParse(str, NumberStyles.Integer, provider, out v))
				return v;
			else
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing DecInt value : {0}", str));
				return defVal;
			}
		}

		public static uint ParseDecUInt(string str, uint defVal)
		{
			if (string.IsNullOrEmpty(str))
				return defVal;

			uint v;
			if (UInt32.TryParse(str, NumberStyles.Integer, provider, out v))
				return v;
			else
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing DecUInt value : {0}", str));
				return defVal;
			}
		}

		public static int ParseHexInt(string str, int defVal = 0)
		{
			if (string.IsNullOrEmpty(str))
				return defVal;

			int v;
			if (Int32.TryParse(str, NumberStyles.HexNumber, provider, out v))
				return v;
			else
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing HexInt value : {0}", str));
				return defVal;
			}
		}

		public static long ParseDecLong(string str, long defVal)
		{
			if (string.IsNullOrEmpty(str))
				return defVal;

			long v;
			if (Int64.TryParse(str, NumberStyles.Integer, provider, out v))
				return v;
			else
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing DecLong value : {0}", str));
				return defVal;
			}
		}

		public static ulong ParseDecULong(string str, ulong defVal)
		{
			if (string.IsNullOrEmpty(str))
				return defVal;

			ulong v;
			if (UInt64.TryParse(str, NumberStyles.Integer, provider, out v))
				return v;
			else
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing DecULong value : {0}", str));
				return defVal;
			}
		}

		public static long ParseHexLong(string str, long defVal)
		{
			if (string.IsNullOrEmpty(str))
				return defVal;

			long v;
			if (Int64.TryParse(str, NumberStyles.HexNumber, provider, out v))
				return v;
			else
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing HexLong value : {0}", str));
				return defVal;
			}
		}

		public static float ParseFloat(string str, float defVal, out bool tf)
		{
			tf = false;

			if (string.IsNullOrEmpty(str))
				return defVal;

			float v = 0;
			if (tf = Single.TryParse(str, out v))
				return (float)v;
			else
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing Float value : {0}", str));
				return defVal;
			}
		}

		public static float ParseFloat(string str, float defVal = 0)
		{
			bool tf;
			return ParseFloat(str, defVal, out tf);
		}

		public static List<float> ParseFloatList(string str, float defVal)
		{
			List<float> values = new List<float>();

			if (str == null)
				return values;

			string[] vecs = str.Split(splitter);

			for (int i = 0; i < vecs.Length; i++)
				values.Add(ParseFloat(vecs[i], defVal));

			return values;
		}

		public static double ParseDouble(string str, double defVal)
		{
			if (string.IsNullOrEmpty(str))
				return defVal;

			double v = 0;
			if (Double.TryParse(str, out v))
				return v;
			else
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing Double value : {0}", str));
				return defVal;
			}
		}

		public static List<int> ParseDecIntList(string str, int defVal)
		{
			List<int> values = new List<int>();

			if (str == null)
				return values;

			string[] vecs = str.Split(splitter);

			for (int i = 0; i < vecs.Length; i++)
				values.Add(ParseDecInt(vecs[i], defVal));

			return values;
		}

		public static List<int> ParseHexIntList(string str, int defVal)
		{
			List<int> values = new List<int>();

			if (str == null)
				return values;

			string[] vecs = str.Split(splitter);

			for (int i = 0; i < vecs.Length; i++)
				values.Add(ParseHexInt(vecs[i], defVal));

			return values;
		}

		public static T ParseEnum<T>(string val, T defVal) where T : struct
		{
			Type type = typeof(T);

			try
			{
				if (!type.IsEnum)
				{
					LoggerManager.Instance.Error(string.Format("'{0}' is not enum type.", type));
					return defVal;
				}

				return (T)Enum.Parse(type, val, true);
			}
			catch
			{
				if (val != "")
					LoggerManager.Instance.Error(string.Format("'{0}' is not value of {1}.", val, type));
				return defVal;
			}
		}

		public static List<T> ParseEnumList<T>(string str, T defVal) where T : struct
		{
			List<T> values = new List<T>();

			if (str == null)
				return values;

			string[] vecs = str.Split(splitter);

			for (int i = 0; i < vecs.Length; i++)
				values.Add(ParseEnum<T>(vecs[i], defVal));

			return values;
		}

		public static int ParseEnum(string str, int defVal, string[] enumVals)
		{
			if (enumVals == null || str == null)
				return defVal;

			for (int i = 0; i < enumVals.Length; i++)
				if (str == enumVals[i])
					return i;

			return defVal;
		}

		public static List<int> ParseEnumList(string str, int defVal, string[] enumVals)
		{
			List<int> values = new List<int>();

			if (str == null)
				return values;

			string[] vecs = str.Split(splitter);

			for (int i = 0; i < vecs.Length; i++)
				values.Add(ParseEnum(vecs[i], defVal, enumVals));

			return values;
		}

		public static string ParseEnumStr(int val, string[] enumVals)
		{
			if (enumVals == null || val < 0 || val >= enumVals.Length)
				return "";

			return enumVals[val];
		}

		public static string ParseEnumFullStr(int val, string[] enumVals, string enumName)
		{
			return enumName + "." + ParseEnumStr(val, enumVals);
		}

		public static string ParseStr(string str, string defValue = "")
		{
			return str == null ? defValue : str;
		}

		public static string ParseStr(string str, string defValue, bool prcSpcChar)
		{
			if (str == null)
				return defValue;

			if (!prcSpcChar)
				return str;

			StringBuilder bd = new StringBuilder(str);

			for (int i = 0; i < spcChars.GetLength(0); i++)
				bd.Replace(spcChars[i, 0], spcChars[i, 1]);

			return bd.ToString();
		}

		//#if UNITY_PRO_LICENSE
		public static UnityEngine.Vector2 ParseVector2(string str)
		{
			var vector2 = default(UnityEngine.Vector2);

			string[] vecs = str.Split(splitter);
			if (vecs.Length < 2)
				return vector2;

			vector2.x = StrParser.ParseFloat(vecs[0], 0);
			vector2.y = StrParser.ParseFloat(vecs[1], 0);
			return vector2;
		}

		public static UnityEngine.Vector3 ParseVector3(string str)
		{
			var vector3 = default(UnityEngine.Vector3);

			str = str.TrimStart('(');
			str = str.TrimEnd(')');

			string[] vecs = str.Split(splitter);
			if (vecs.Length < 3)
				return vector3;

			vector3.x = StrParser.ParseFloat(vecs[0], 0);
			vector3.y = StrParser.ParseFloat(vecs[1], 0);
			vector3.z = StrParser.ParseFloat(vecs[2], 0);
			return vector3;
		}
		//#endif

		/// <summary>
		/// default is black
		/// </summary>
		public static UnityEngine.Color ParseColor(string str)
		{
			UnityEngine.Color res = new UnityEngine.Color(1f, 1f, 1f, 0.3f);
			if (str == null)
				return res;

			//RGBA(r,g,b,a)
			str = str.Substring(5, str.Length - 6);

			string[] vecs = str.Split(splitter);
			if (vecs.Length < 4)
				return res;

			res.r = StrParser.ParseFloat(vecs[0], 0);
			res.g = StrParser.ParseFloat(vecs[1], 0);
			res.b = StrParser.ParseFloat(vecs[2], 0);
			res.a = StrParser.ParseFloat(vecs[3], 0);

			return res;
		}

		public static string Null2Empty(string str)
		{
			return str == null ? "" : str;
		}

		public static long ParseDateTime(string str)
		{
			long def = DateTime.MinValue.Ticks / TimeSpan.TicksPerMillisecond;
			if (str == null)
				return def;

			DateTime dateTime;
			if (DateTime.TryParse(str, provider, DateTimeStyles.AdjustToUniversal, out dateTime) == false)
				return def;

			return dateTime.Ticks / TimeSpan.TicksPerMillisecond;
		}

		public static long ParseDateTimeOfDay(string str, long defVal)
		{
			if (string.IsNullOrEmpty(str))
				return defVal;

			DateTime dateTime;
			if (DateTime.TryParse(str, provider, DateTimeStyles.AdjustToUniversal, out dateTime) == false)
			{
				LoggerManager.Instance.Error(string.Format("Invalid parameter when parsing DateTime value : {0}", str));
				return defVal;
			}

			return (long)dateTime.AddYears(1).ToUniversalTime().TimeOfDay.TotalMilliseconds;
		}

		public static DateTime ToUTCDateTime(long time)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			DateTime utcDate = origin.AddMilliseconds(time);
			return utcDate;
		}

		public static DateTime ToLocalDataTime(long time)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			DateTime utcDate = origin.AddMilliseconds(time);
			return utcDate.ToLocalTime();
		}

		public static long DateTimeToInt64(DateTime dateTime)
		{
			DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, new GregorianCalendar(), DateTimeKind.Utc);
			return (dateTime.Ticks - epoch.Ticks) / TimeSpan.TicksPerMillisecond;
		}

		// Make the TextAsset Encoded with UTF-8 without BOM Flag
		public static string GetTextWithoutBOM(byte[] bytes)
		{
			MemoryStream memoryStream = new MemoryStream(bytes);
			StreamReader streamReader = new StreamReader(memoryStream, true);

			string result = streamReader.ReadToEnd();

			streamReader.Close();
			memoryStream.Close();

			return result;
		}
	}
} // namespace ClientServerCommon