using System;
using System.Collections.Generic;
using HexMapGeneration.DataAccess;
using HexMapGeneration.DataModels;

namespace HexMapGeneration.Services
{
	public class OffsetService : IOffsetService
	{
		private readonly IConfigAccess _configAccess;

		public OffsetService(IConfigAccess configAccess)
		{
			_configAccess = configAccess;
		}

		public IList<Offset> GetCornerOffsets()
		{
			List<Offset> cornerOffsets = new List<Offset>();

			var mapConfig = _configAccess.GetConfig();

			var opposite = mapConfig.Height / 2;
			var adjacent = opposite / Math.Sqrt(3);

			cornerOffsets.Add(new Offset(adjacent, 0, Direction.NW));
			cornerOffsets.Add(new Offset(adjacent * 3, 0, Direction.NE));
			cornerOffsets.Add(new Offset(mapConfig.Width, opposite, Direction.E));
			cornerOffsets.Add(new Offset(adjacent * 3, mapConfig.Height, Direction.SE));
			cornerOffsets.Add(new Offset(adjacent, mapConfig.Height, Direction.SW));
			cornerOffsets.Add(new Offset(0, opposite, Direction.W));

			return cornerOffsets;
		}

		public IList<Offset> GetSideOffsets()
		{
			List<Offset> sideOffsets = new List<Offset>();

			var mapConfig = _configAccess.GetConfig();

			var opposite = mapConfig.Height / 2;
			var adjacent = opposite / Math.Sqrt(3);

			var angledFaceOffset = adjacent * 3 + mapConfig.Offset;
			double heightOffset = mapConfig.Height + mapConfig.Offset;

			sideOffsets.Add(new Offset(0, -heightOffset, Direction.N));
			sideOffsets.Add(new Offset(angledFaceOffset, -heightOffset / 2, Direction.NE));
			sideOffsets.Add(new Offset(angledFaceOffset, heightOffset / 2, Direction.SE));
			sideOffsets.Add(new Offset(0, heightOffset, Direction.S));
			sideOffsets.Add(new Offset(-angledFaceOffset, heightOffset / 2, Direction.SW));
			sideOffsets.Add(new Offset(-angledFaceOffset, -heightOffset / 2, Direction.NW));

			return sideOffsets;
		}
	}
}