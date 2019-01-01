using System.Collections.Generic;
using Newtonsoft.Json;

namespace HexMapGeneration.Utilities
{
	public class MapConfigObject
	{
		[JsonProperty("Tiles")]
		public IList<TileConfig> ConfigSummary { get; private set; }

		[JsonProperty("Width")]
		public int Width { get; private set; }

		[JsonProperty("Height")]
		public int Height { get; private set; }

		[JsonProperty("Tiles_wide")]
		public int TilesWide { get; private set; }

		[JsonProperty("Tiles_high")]
		public int TilesHigh { get; private set; }

		[JsonProperty("Offset")]
		public int Offset { get; private set; }
	}

	public class TileConfig
	{
		[JsonProperty("Image")]
		public string ImagePath { get; private set; }

		[JsonProperty("Rotation")]
		public int Rotation { get; private set; }

		[JsonProperty("Expand_direction")]
		public string DirectionString
		{
			set { Expand_Direction = DirectionConverter.Convert(value); }
		}

		public Direction Expand_Direction { get; private set; }
	}
}