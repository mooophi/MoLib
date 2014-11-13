using System;
using System.Collections.Generic;

namespace Mophi.Xml
{
	public class ConfigSetting
	{
		private Dictionary<Type, string> configList = new Dictionary<Type, string>();

		public ConfigSetting(int fileFormat)
		{
			this.fileFormat = fileFormat;
		}

		private int fileFormat = _FileFormat.Unknown;
		public int FileFormat { get { return fileFormat; } }

// 		public string SingleActivityConfig { set { SetConfigName(typeof(SingleActivityConfig), value); } }
// 		public string JiangHuActivityConfig { set { SetConfigName(typeof(JiangHuActivityConfig), value); } }
// 		public string AccumulatePurchaseActivityConfig { set { SetConfigName(typeof(AccumulatePurchaseActivityConfig), value); } }
// 		public string AccumulateCostActivityConfig { set { SetConfigName(typeof(AccumulateCostActivityConfig), value); } }
// 		public string ExchangeActivityConfig { set { SetConfigName(typeof(ExchangeActivityConfig), value); } }
// 		public string ActivityExchangeActivityConfig { set { SetConfigName(typeof(ActivityExchangeActivityConfig), value); } }

		public string GetConfigName(Type type)
		{
			if (configList.ContainsKey(type) == false)
				return "";

			return configList[type];
		}

		private void SetConfigName(Type type, string value)
		{
			if (type == null)
				return;

			if (value == null || value == "")
			{
				// Remove config name
				if (configList.ContainsKey(type))
				{
					configList.Remove(type);
				}

				return;
			}
			else
			{
				// Set config
				if (configList.ContainsKey(type))
				{
					configList[type] = value;
				}
				else
				{
					configList.Add(type, value);
				}
			}
		}

		private string StringValue(string value)
		{
			return value != null ? value : "";
		}
	}
}
