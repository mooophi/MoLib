using System.Collections.Generic;

namespace Mophi
{
	public class TypeNameContainer<Type>
	{
		protected static void SetTextSectionName(string name)
		{
			textSectionName = name;
		}

		protected static bool RegisterType(string typeName, int type)
		{
			return RegisterType(typeName, type, typeName);
		}

		protected static bool RegisterType(string typeName, int type, string displayName)
		{
			if (container.ContainsKey(typeName))
			{
				Logger.Error(string.Format("Invalid Type {0} in {1}", typeName, typeof(Type)));
				return false;
			}

			container.Add(typeName, new KeyValuePair<int, string>(type, displayName));

			return true;
		}

		public static bool IsValidType(string name)
		{
			return container.ContainsKey(name);
		}

		public static int GetTypeByName(string name)
		{
			if (container.ContainsKey(name) == false)
				return 0;

			return container[name].Key;
		}

		public static string GetNameByType(int type)
		{
			foreach (KeyValuePair<string, KeyValuePair<int, string>> pair in container)
			{
				if (pair.Value.Key == type)
					return pair.Key;
			}

			return "";
		}

		public static int Parse(string typeName, int defValue)
		{
			if (typeName == null || typeName == "")
				return defValue;

			if (IsValidType(typeName) == false)
			{
				Logger.Error(string.Format("Invalid Type {0} in {1}", typeName, typeof(Type)));
				return defValue;
			}

			return GetTypeByName(typeName);
		}

		public static List<int> ParseList(string typeNameList, int defValue)
		{
			if (typeNameList == null)
				return new List<int>();

			string[] typeNames = typeNameList.Split(',');

			List<int> types = new List<int>();
			foreach (var typeName in typeNames)
				types.Add(Parse(typeName, defValue));

			return types;
		}

		private static string textSectionName = "";
		private static Dictionary<string, KeyValuePair<int, string>> container = new Dictionary<string, KeyValuePair<int, string>>();
	}
}
