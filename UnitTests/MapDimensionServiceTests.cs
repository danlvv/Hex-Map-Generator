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

			var mockConfigObject = mockRepo.Create<IMapConfigObject>();
			mockConfigObject.Setup(o => o.Width)
				.Returns(496);
			mockConfigObject.Setup(o => o.Height)
				.Returns(430);
            
            mockConfigObject.Setup(o => o.ConfigSummary)
		        .Returns(new List<TileConfig>
                {
                    new TileConfig { Expand_Direction = Direction.NW },
                    new TileConfig { Expand_Direction = Direction.NE },
                    new TileConfig { Expand_Direction = Direction.SE },
                    new TileConfig { Expand_Direction = Direction.SW },
                });

			var smallTileWidth = offsets.First(p => p.Direction == Direction.NE).X -
			                     offsets.First(p => p.Direction == Direction.NW).X;

            // There are two wests and two easts, so they simply cancel out.
		    var maxPanningX = 496 + smallTileWidth;

            // There are two norths and two souths, so they simply cancel out.
			var maxPanningY = 430;

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