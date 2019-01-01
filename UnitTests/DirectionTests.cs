using System;
using HexMapGeneration.Utilities;
using Xunit;

namespace UnitTests
{
	public class DirectionTests
	{
		[Fact]
		public void DirectionConvert_Failure()
		{
			var direction = "ES";

			Assert.Throws<Exception>(() => DirectionConverter.Convert(direction));
		}

		[Fact]
		public void ValidDirectionsConvert_Successfully()
		{
			var ignore = "IGNORE";
			var north = "N";
			var northEast = "NE";
			var east = "E";
			var southEast = "SE";
			var south = "S";
			var southWest = "SW";
			var west = "W";
			var northWest = "NW";

			Assert.Equal(Direction.IGNORE, DirectionConverter.Convert(ignore));
			Assert.Equal(Direction.N, DirectionConverter.Convert(north));
			Assert.Equal(Direction.NE, DirectionConverter.Convert(northEast));
			Assert.Equal(Direction.E, DirectionConverter.Convert(east));
			Assert.Equal(Direction.SE, DirectionConverter.Convert(southEast));
			Assert.Equal(Direction.S, DirectionConverter.Convert(south));
			Assert.Equal(Direction.SW, DirectionConverter.Convert(southWest));
			Assert.Equal(Direction.W, DirectionConverter.Convert(west));
			Assert.Equal(Direction.NW, DirectionConverter.Convert(northWest));
		}
	}
}