using System.Collections.Generic;

namespace HexMapGeneration.DataModels
{
	public interface IMapConfigObject
	{
		IList<TileConfig> ConfigSummary { get; }

		int Width { get; }

		int Height { get; }

		int Offset { get; }
	}

	public interface ITileConfig
	{
		string ImagePath { get; }

		int Rotation { get; }

		Direction Expand_Direction { get; }
	}
}