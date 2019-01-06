using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using HexMapGeneration.DataAccess;
using HexMapGeneration.DataModels;
using HexMapGeneration.Utilities;

namespace HexMapGeneration.Services
{
	public class MapGeneratorService : IMapGeneratorService
	{
		private readonly IConfigAccess _configAccess;
		private readonly IOffsetService _offsetService;

		public MapGeneratorService(IConfigAccess configAccess, IOffsetService offsetService)
		{
			_configAccess = configAccess;
			_offsetService = offsetService;
		}

		public IEnumerable<MapTile> GenerateMapTiles()
		{
			var cornerOffsets = _offsetService.GetCornerOffsets();
			var sideOffsets = _offsetService.GetSideOffsets();
			var mapConfig = _configAccess.GetConfig();

			PointCollection points = new PointCollection();
			foreach (var point in cornerOffsets)
			{
				points.Add(new Point(point.X, point.Y));
			}

			var workAreaSize = SystemParameters.WorkArea;

			var offset = new Offset(workAreaSize.Width / 4, workAreaSize.Height / 4);
			foreach (var tileConfig in mapConfig.ConfigSummary)
			{
				var imagePath = Constants.ImagesDirectory + tileConfig.ImagePath;

				var tile = new MapTile(
					imagePath,
					offset.X,
					offset.Y,
					mapConfig.Width,
					mapConfig.Height,
					points,
					tileConfig.Rotation);

				if (tileConfig.Expand_Direction != Direction.IGNORE)
				{
					var offsetPoint = sideOffsets.First(p => p.Direction == tileConfig.Expand_Direction);
					offset = new Offset(offset.X + offsetPoint.X, offset.Y + offsetPoint.Y);
				}

				yield return tile;
			}
		}
	}
}