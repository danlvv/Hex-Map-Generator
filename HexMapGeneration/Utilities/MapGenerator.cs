using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using HexMapGeneration.DataModels;
using Newtonsoft.Json;

namespace HexMapGeneration.Utilities
{
	public class MapGenerator
	{
		private readonly MapConfigObject _mapConfig;
		private IList<Offset> _sideOffsets;
		private IList<Offset> _cornerOffsets;

		public MapGenerator(string file)
		{
			var mapJson = File.ReadAllText(file);
			_mapConfig = JsonConvert.DeserializeObject<MapConfigObject>(mapJson);

			CalculateTilePoints();
		}

		public double[] GenerateMapPanningDimensions()
		{
			var smallTileWidth = _cornerOffsets.First(p => p.Direction == Direction.NE).X -
			                    _cornerOffsets.First(p => p.Direction == Direction.NW).X;

			var width = _mapConfig.TilesWide / 2 + 1;

			var maxPanningX = _mapConfig.TilesWide % 2 == 0
				? width * smallTileWidth + width * _mapConfig.Width
				: width * smallTileWidth - smallTileWidth + width * _mapConfig.Width;

			var maxPanningY = _mapConfig.TilesHigh * _mapConfig.Height;

			return new [] { maxPanningX, maxPanningY};
		}

		public IEnumerable<MapTile> GenerateMapTiles()
		{
			var width = _mapConfig.Width;
			var height = _mapConfig.Height;

			PointCollection points = new PointCollection();
			foreach (var point in _cornerOffsets)
			{
				points.Add(new Point(point.X, point.Y));
			}

			var workAreaSize = SystemParameters.WorkArea;

			var offset = new Offset(workAreaSize.Width / 4, workAreaSize.Height / 4);
			foreach (var tileConfig in _mapConfig.ConfigSummary)
			{
				var imagePath = Constants.ImagesDirectory + tileConfig.ImagePath;

				var tile = new MapTile(
					imagePath,
					offset.X,
					offset.Y,
					width,
					height,
					points,
					tileConfig.Rotation);

				if (tileConfig.Expand_Direction != Direction.IGNORE)
				{
					var offsetPoint = _sideOffsets.First(p => p.Direction == tileConfig.Expand_Direction);
					offset = new Offset(offset.X + offsetPoint.X, offset.Y + offsetPoint.Y);
				}

				yield return tile;
			}
		}

		private void CalculateTilePoints()
		{
			_sideOffsets = new List<Offset>();
			_cornerOffsets = new List<Offset>();

			var opposite = _mapConfig.Height / 2;
			var adjacent = opposite / Math.Sqrt(3);

			_cornerOffsets.Add(new Offset(adjacent, 0, Direction.NW));
			_cornerOffsets.Add(new Offset(adjacent * 3, 0, Direction.NE));
			_cornerOffsets.Add(new Offset(_mapConfig.Width, opposite, Direction.E));
			_cornerOffsets.Add(new Offset(adjacent * 3, _mapConfig.Height, Direction.SE));
			_cornerOffsets.Add(new Offset(adjacent, _mapConfig.Height, Direction.SW));
			_cornerOffsets.Add(new Offset(0, opposite, Direction.W));

			var angledFaceOffset = adjacent * 3 + _mapConfig.Offset;
			var heightOffset = _mapConfig.Height + _mapConfig.Offset;

			_sideOffsets.Add(new Offset(0, -heightOffset, Direction.N));
			_sideOffsets.Add(new Offset(angledFaceOffset, -heightOffset / 2, Direction.NE));
			_sideOffsets.Add(new Offset(angledFaceOffset, heightOffset / 2, Direction.SE));
			_sideOffsets.Add(new Offset(0, heightOffset, Direction.S));
			_sideOffsets.Add(new Offset(-angledFaceOffset, heightOffset / 2, Direction.SW));
			_sideOffsets.Add(new Offset(angledFaceOffset, -heightOffset / 2, Direction.NW));
		}
	}
}