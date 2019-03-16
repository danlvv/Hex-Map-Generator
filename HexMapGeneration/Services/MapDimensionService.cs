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

			 double maxPanningX = mapConfig.Width + smallTileWidth;
		    foreach (var tile in mapConfig.ConfigSummary)
		    {
		        maxPanningX += WidthAdjustment(tile.Expand_Direction, smallTileWidth);
		    }

		    double maxPanningY = mapConfig.Height;
		    foreach (var tile in mapConfig.ConfigSummary)
		    {
		        maxPanningY += HeightAdjustment(tile.Expand_Direction, mapConfig.Height);
		    }

			return new [] { maxPanningX, maxPanningY};
		}

	    private double WidthAdjustment(Direction direction, double width)
	    {
	        if (direction == Direction.NE ||
	            direction == Direction.SE)
	        {
	            return width;
	        }

	        if (direction == Direction.NW ||
	            direction == Direction.SW)
	        {
	            return -width;
	        }

	        return 0.0;
	    }

	    private double HeightAdjustment(Direction direction, double height)
	    {
	        if (direction == Direction.NE ||
	            direction == Direction.NW)
	        {
	            return -height / 2.0;
	        }

	        if (direction == Direction.SE ||
	            direction == Direction.SW)
	        {
	            return height / 2.0;
	        }

	        if (direction == Direction.N)
	        {
	            return -height;
	        }

	        if (direction == Direction.S)
	        {
                return height;
	        }

	        return 0.0;
	    }
	}
}