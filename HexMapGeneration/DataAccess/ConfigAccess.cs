using System;
using HexMapGeneration.DataModels;
using File = HexMapGeneration.Testable.File;
using JsonConvert = HexMapGeneration.Testable.JsonConvert;

namespace HexMapGeneration.DataAccess
{
	public class ConfigAccess : IConfigAccess
	{
		private readonly string _jsonFile;
		private readonly File _file;
		private readonly JsonConvert _jsonConvert;

		public ConfigAccess(string jsonFile, JsonConvert jsonConvert, File file)
		{
			_jsonFile = jsonFile;
			_jsonConvert = jsonConvert;
			_file = file;
		}

		public IMapConfigObject GetConfig()
		{
			try
			{
				var json = _file.ReadAsString(_jsonFile);
				return _jsonConvert.DeserializeObject<MapConfigObject>(json);
			}
			catch (Exception e)
			{
				throw new ConfigAccessException(e);
			}
		}
	}
}