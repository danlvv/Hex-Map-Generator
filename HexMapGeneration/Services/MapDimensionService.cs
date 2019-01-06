using System.Linq;
using HexMapGeneration.DataAccess;
using HexMapGeneration.DataModels;

namespace HexMapGeneration.Services
{
	public class MapDimensionService : IMapDimensionService
	{
		private readonly IConfigAccess _configAccess;
		private readonly IOffsetService _offsetService;

		public MapDimensionService(IConfigAccess configAccess, IOffsetService offsetService)
		{
			_configAccess = configAccess;
			_offsetService = offsetService;
		}

		public double[] GetMapDimensions()
		{
			var mapConfig = _configAccess.GetConfig();
			var cornerOffsets = _offsetService.GetCornerOffsets();

			var smallTileWidth = cornerOffsets.First(p => p.Direction == Direction.NE).X -
			                     cornerOffsets.First(p => p.Direction == Direction.NW).X;

			var width = mapConfig.TilesWide / 2 + 1;

			var maxPanningX = mapConfig.TilesWide % 2 == 0
				? width * smallTileWidth + width * mapConfig.Width
				: width * smallTileWidth - smallTileWidth + width * mapConfig.Width;

			var maxPanningY = mapConfig.TilesHigh * mapConfig.Height;

			return new [] { maxPanningX, maxPanningY};
		}
	}
}