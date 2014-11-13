using System;
using System.IO;
using System.Security;

namespace Mophi.Xml
{
	public interface IFileLoader
	{
		SecurityElement LoadAsXML(string filePath);
		Stream LoadAsSteam(string filePath);
	}

	public class FileLoader : IFileLoader
	{
		public SecurityElement LoadAsXML(string filePath)
		{
			// Open the file to read from.
			StreamReader sr = File.OpenText(filePath);
			if (sr == null)
				throw new Exception("Can not open file : " + filePath);

			var xmlParser = new SecurityParser();
			xmlParser.LoadXml(sr.ReadToEnd());

			sr.Close();

			return xmlParser.ToXml();
		}

		public Stream LoadAsSteam(string filePath)
		{
			return File.Open(filePath, FileMode.Open);
		}
	}
}
