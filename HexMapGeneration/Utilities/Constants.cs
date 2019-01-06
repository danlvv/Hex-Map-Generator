using System;

namespace HexMapGeneration.Utilities
{
	public static class Constants
	{
		public static readonly string BaseDirectory = Environment.CurrentDirectory + @"\..\..\..\HexMapGeneration\";

		public static readonly string ImagesDirectory = BaseDirectory + @"Images\";

		// Change this constant if you wish to target a different config file
		public static readonly string MapConfig = BaseDirectory + @"ConfigFiles\MagicRealmMapConfigFile.json";

		public static readonly string AltMapConfig = BaseDirectory + @"ConfigFiles\SettlersMapConfigFile.json";

		public static readonly string ErrorLog = BaseDirectory + @"..\ErrorLog.txt";
	}
}