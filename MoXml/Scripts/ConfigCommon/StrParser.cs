using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace Mophi.Xml
{
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

		public static bool ParseBool(string str, bool defVal)
		{
			bool v;

			if (Boolean.TryParse(str, out v))
				return v;
			else
				return defVal;
		}

		public static int ParseDecInt(string str, int defVal, out bool tf)
		{
			// Parse as float as int
			return (int)Math.Round(ParseFloat(str, defVal, out tf));
		}

		public static int ParseDecInt(string str, int defVal)
		{
			// Parse as float as int
			bool tf;
			return ParseDecInt(str, defVal, out tf);
		}

		public static uint ParseDecUInt(string str, uint defVal)
		{
			uint v;

			if (UInt32.TryParse(str, NumberStyles.Integer, provider, out v))
				return v;
			else
				return defVal;
		}

		public static int ParseHexInt(string str, int defVal)
		{
			int v;

			if (Int32.TryParse(str, NumberStyles.HexNumber, provider, out v))
				return v;
			else
				return defVal;
		}

		public static long ParseDecLong(string str, long defVal)
		{
			long v;

			if (Int64.TryParse(str, NumberStyles.Integer, provider, out v))
				return v;
			else
				return defVal;
		}

		public static ulong ParseDecULong(string str, ulong defVal)
		{
			ulong v;

			if (UInt64.TryParse(str, NumberStyles.Integer, provider, out v))
				return v;
			else
				return defVal;
		}

		public static long ParseHexLong(string str, long defVal)
		{
			long v;

			if (Int64.TryParse(str, NumberStyles.HexNumber, provider, out v))
				return v;
			else
				return defVal;
		}

		public static float ParseFloat(string str, float defVal, out bool tf)
		{
			float v = 0;

			if (tf = Single.TryParse(str, out v))
				return v;
			else
				return defVal;
		}

		public static float ParseFloat(string str, float defVal)
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
			double v = 0;

			if (Double.TryParse(str, out v))
				return v;
			else
				return defVal;
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
					Logger.Error(string.Format("'{0}' is not enum type.", type));
					return defVal;
				}

				return (T)Enum.Parse(type, val, true);
			}
			catch
			{
				if (val != "")
					Logger.Error(string.Format("'{0}' is not value of {1}.", val, type));
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

		public static string ParseStr(string str, string defValue)
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

		public static long ParseDateTimeOfDay(string str, long def)
		{
			if (str == null)
				return def;

			DateTime dateTime;
			if (DateTime.TryParse(str, provider, DateTimeStyles.AdjustToUniversal, out dateTime) == false)
				return def;

			return (long)dateTime.AddYears(1).ToUniversalTime().TimeOfDay.TotalMilliseconds;
		}

		public static System.DateTime ToUTCDateTime(long time)
		{
			System.DateTime origin = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
			System.DateTime utcDate = origin.AddMilliseconds(time);
			return utcDate;
		}

		public static System.DateTime ToLocalDataTime(long time)
		{
			System.DateTime origin = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
			System.DateTime utcDate = origin.AddMilliseconds(time);
			return utcDate.ToLocalTime();
		}

		public static long DateTimeToInt64(System.DateTime dateTime)
		{
			System.DateTime epoch = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, new System.Globalization.GregorianCalendar(), System.DateTimeKind.Utc);
			return (dateTime.Ticks - epoch.Ticks) / System.TimeSpan.TicksPerMillisecond;
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

		private static CultureInfo provider = CultureInfo.InvariantCulture;
	}
}
