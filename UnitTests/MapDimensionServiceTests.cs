using System.Collections.Generic;
using System.Linq;
using HexMapGeneration.DataAccess;
using HexMapGeneration.DataModels;
using HexMapGeneration.Services;
using Moq;
using Xunit;

namespace UnitTests
{
	public class MapDimensionServiceTests
	{
		[Fact]
		public void GetMapDimensions_Success()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var offsets = new List<Offset>
			{
				new Offset(124, 0, Direction.NW),
				new Offset(372, 0, Direction.NE)
			};

			var mockConfigObject = mockRepo.Create<MapConfigObject>();
			mockConfigObject.Setup(o => o.Width)
				.Returns(496);
			mockConfigObject.Setup(o => o.Height)
				.Returns(430);
			mockConfigObject.Setup(o => o.TilesWide)
				.Returns(2);
			mockConfigObject.Setup(o => o.TilesHigh)
				.Returns(2);

			var smallTileWidth = offsets.First(p => p.Direction == Direction.NE).X -
			                     offsets.First(p => p.Direction == Direction.NW).X;

			var width = 2 / 2 + 1;

			var maxPanningX = width * smallTileWidth + width * 496;
			var maxPanningY = 2 * 430;

			var configAccess = mockRepo.Create<IConfigAccess>();
			configAccess.Setup(c => c.GetConfig())
				.Returns(mockConfigObject.Object);

			var offsetService = mockRepo.Create<IOffsetService>();
			offsetService.Setup(o => o.GetCornerOffsets())
				.Returns(offsets);

			var mapDimensionService = new MapDimensionService(configAccess.Object, offsetService.Object);
			var dimensions = mapDimensionService.GetMapDimensions();

			Assert.Equal(dimensions[0], maxPanningX);
			Assert.Equal(dimensions[1], maxPanningY);

			mockRepo.VerifyAll();
		}

		[Fact]
		public void GetMapDimensions_Failure()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var configAccess = mockRepo.Create<IConfigAccess>();
			configAccess.Setup(c => c.GetConfig())
				.Throws<ConfigAccessException>();

			var offsetService = mockRepo.Create<IOffsetService>();

			var mapDimensionService = new MapDimensionService(configAccess.Object, offsetService.Object);

			Assert.Throws<ConfigAccessException>(() => mapDimensionService.GetMapDimensions());

			mockRepo.VerifyAll();
		}
	}
}