namespace Mophi.Xml
{
	public class Configuration
	{
		private ConfigDataBase cfgDB = null;
		public ConfigDataBase CfgDB
		{
			get { return cfgDB; }
		}

		public virtual void LoadFromXml(System.Security.SecurityElement element)
		{
		}

		public virtual void ConstructLogicData(ConfigDataBase cfgDB, int fileFormat)
		{
			this.cfgDB = cfgDB;
		}
	}
}
