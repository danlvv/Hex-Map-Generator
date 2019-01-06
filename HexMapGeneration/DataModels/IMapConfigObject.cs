using System.Collections.Generic;

namespace HexMapGeneration.DataModels
{
	public interface IMapConfigObject
	{
		IList<TileConfig> ConfigSummary { get; }

		int Width { get; }

		int Height { get; }

		int Offset { get; }

		int TilesHigh { get; }

		int TilesWide { get; }
	}

	public interface ITileConfig
	{
		string ImagePath { get; }

		int Rotation { get; }

		Direction Expand_Direction { get; }
	}
}