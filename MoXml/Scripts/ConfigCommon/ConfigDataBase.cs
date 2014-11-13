using System;
using System.Collections.Generic;

namespace Mophi.Xml
{
	public class ConfigDataBase
	{
		private ConfigDataBase()
		{
		}

		public static void AddLogger(ILogger logger)
		{
			LoggerManager.Instance.AddLogger(logger);
		}

		public static void RemoveLogger(ILogger logger)
		{
			LoggerManager.Instance.RemoveLogger(logger);
		}

		private static ConfigDataBase defaultCfg;
		public static ConfigDataBase DefaultCfg
		{
			get
			{
				if (defaultCfg == null)
					defaultCfg = new ConfigDataBase();

				return defaultCfg;
			}

			set { defaultCfg = value; }
		}

		private static bool isInitialized = false;
		public static bool IsInitialized
		{
			get { return isInitialized; }
		}

		private static IMathParserFactory _mathParserFactory;
		public static IMathParserFactory MathParserFactory
		{
			get { return _mathParserFactory; }
			set { _mathParserFactory = value; }
		}

		private Dictionary<Type, Configuration> configurations = new Dictionary<Type, Configuration>();
		public Dictionary<Type, Configuration> Configurations
		{
			get { return configurations; }
		}

		public static void Initialize(IMathParserFactory mathParserFactory)
		{
			if (isInitialized)
				return;

			isInitialized = true;

			_mathParserFactory = mathParserFactory;
		}

		private T GetConfiguration<T>() where T : Configuration, new()
		{
			Configuration config = null;
			configurations.TryGetValue(typeof(T), out config);
			return (T)config;
		}

		public void LoadConfig<T>(IFileLoader fileLoader, ConfigSetting cfgSetting) where T : Configuration, new()
		{
			var config = LoadConfig<T>(this, fileLoader, cfgSetting.FileFormat, cfgSetting.GetConfigName(typeof(T)));
			if (config != null)
				configurations[typeof(T)] = config;
		}

		public static T LoadConfig<T>(ConfigDataBase cfgDB, IFileLoader fileLoader, int fileFormat, string filePath) where T : Configuration, new()
		{
			T config = null;
			switch (fileFormat)
			{
				case _FileFormat.Xml:
					config = new T();

					try
					{
						config.LoadFromXml(fileLoader.LoadAsXML(filePath));
					}
					catch (System.Exception e)
					{
						Logger.Error("Error when load xml file format=" + fileFormat + " file=" + filePath + " message=" + e.Message);
					}

					break;
			}

			if (config != null)
				config.ConstructLogicData(cfgDB, fileFormat);

			return config;
		}

		public void UnloadConfig(params Type[] exceptType)
		{
			List<Configuration> exceptConfigs = new List<Configuration>();
			foreach (var t in exceptType)
			{
				Configuration config;
				if (configurations.TryGetValue(t, out config))
					exceptConfigs.Add(config);
			}

			configurations.Clear();

			foreach (var c in exceptConfigs)
				configurations.Add(c.GetType(), c);
		}
	}
}
