using System;
using System.Collections.Generic;
using HexMapGeneration.Utilities;

namespace UnitTests
{
	public static class TestData
	{
		public static readonly string MapConfig = Environment.CurrentDirectory + @"\..\..\TestData\TestConfig.json";
		public static readonly string BadJson = Environment.CurrentDirectory + @"\..\..\TestData\BadConfig.json";

		public const int Rotation1 = 0;
		public const int Rotation2 = 60;
		public const int Rotation3 = 240;

		public const Direction Direction1 = Direction.SE;
		public const Direction Direction2 = Direction.SW;
		public const Direction Direction3 = Direction.IGNORE;

		public const string Image1 = "tile1";
		public const string Image2 = "tile2";
		public const string Image3 = "tile3";

		public const double Offset = 10;
		public const double Width = 450;
		public const double Height = 450;
		public const double TilesWidth = 2;
		public const double TilesHigh = 1;
	}
}