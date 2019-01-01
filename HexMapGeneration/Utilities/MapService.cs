using System.Collections.Generic;
using HexMapGeneration.DataModels;

namespace HexMapGeneration.Utilities
{
	public class MapService
	{
		private MapGenerator _generator;

		public MapService(MapGenerator generator)
		{
			_generator = generator;
		}

		public IEnumerable<MapTile> GetMapTiles()
		{
			return _generator.GenerateMapTiles();
		}

		public double[] GetMaxPanningDimensions()
		{
			return _generator.GenerateMapPanningDimensions();
		}
	}
}