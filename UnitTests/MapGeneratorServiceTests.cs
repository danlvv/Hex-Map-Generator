using System.Collections.Generic;
using System.Linq;
using HexMapGeneration.DataAccess;
using HexMapGeneration.DataModels;
using HexMapGeneration.Services;
using HexMapGeneration.Testable;
using Moq;
using Xunit;

namespace UnitTests
{
	public class MapGeneratorServiceTests
	{
		[StaFact]
		public void GenerateTiles_Success()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var tile1 = mockRepo.Create<TileConfig>();
			tile1.Setup(t => t.Rotation).Returns(0);
			tile1.Setup(t => t.ImagePath).Returns("badvalley1.gif");
			tile1.Setup(t => t.Expand_Direction).Returns(Direction.SE);

			var tile2 = mockRepo.Create<TileConfig>();
			tile2.Setup(t => t.Rotation).Returns(0);
			tile2.Setup(t => t.ImagePath).Returns("evilvalley1.gif");
			tile2.Setup(t => t.Expand_Direction).Returns(Direction.SW);

			var tile3 = mockRepo.Create<TileConfig>();
			tile3.Setup(t => t.Rotation).Returns(0);
			tile3.Setup(t => t.ImagePath).Returns("darkvalley1.gif");
			tile3.Setup(t => t.Expand_Direction).Returns(Direction.IGNORE);

			var configSummary = new List<TileConfig>
			{
				tile1.Object,
				tile2.Object,
				tile3.Object
			};

			var parameters = new SystemParameters();
			var initialOffsetX = parameters.GetWidth() / 4;
			var initialOffsetY = parameters.GetHeight() / 4;

			var mockConfigObject = mockRepo.Create<MapConfigObject>();
			mockConfigObject.Setup(o => o.Width)
				.Returns(496);
			mockConfigObject.Setup(o => o.Height)
				.Returns(430);
			mockConfigObject.Setup(o => o.ConfigSummary)
				.Returns(configSummary);

			var configAccess = mockRepo.Create<IConfigAccess>();
			configAccess.Setup(c => c.GetConfig())
				.Returns(mockConfigObject.Object);

			var offsetService = mockRepo.Create<IOffsetService>();
			offsetService.Setup(o => o.GetCornerOffsets())
				.Returns(TestData.cornerOffsets);
			offsetService.Setup(o => o.GetSideOffsets())
				.Returns(TestData.sideOffsets);

			var mapGeneratorService = new MapGeneratorService(configAccess.Object, offsetService.Object);
			var tiles = mapGeneratorService.GenerateMapTiles().ToList();

			Assert.Equal(initialOffsetX, tiles[0].CanvasLeft);
			Assert.Equal(initialOffsetY, tiles[0].CanvasTop);
			Assert.Equal(initialOffsetX + 382, tiles[1].CanvasLeft);
			Assert.Equal(initialOffsetY + 220, tiles[1].CanvasTop);
			Assert.Equal(initialOffsetX, tiles[2].CanvasLeft);
			Assert.Equal(initialOffsetY + 440, tiles[2].CanvasTop);

			mockRepo.VerifyAll();
		}

		[Fact]
		public void GenerateTiles_OffsetService_CornerThrows()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var configAccess = mockRepo.Create<IConfigAccess>();

			var offsetService = mockRepo.Create<IOffsetService>();
			offsetService.Setup(o => o.GetCornerOffsets())
				.Throws<ConfigAccessException>();

			var mapGeneratorService = new MapGeneratorService(configAccess.Object, offsetService.Object);
			Assert.Throws<ConfigAccessException>(() => mapGeneratorService.GenerateMapTiles().ToList());

			mockRepo.VerifyAll();
		}

		[Fact]
		public void GenerateTiles_OffsetService_SideThrows()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var configAccess = mockRepo.Create<IConfigAccess>();

			var offsetService = mockRepo.Create<IOffsetService>();
			offsetService.Setup(o => o.GetCornerOffsets())
				.Returns(TestData.cornerOffsets);
			offsetService.Setup(o => o.GetSideOffsets())
				.Throws<ConfigAccessException>();

			var mapGeneratorService = new MapGeneratorService(configAccess.Object, offsetService.Object);
			Assert.Throws<ConfigAccessException>(() => mapGeneratorService.GenerateMapTiles().ToList());

			mockRepo.VerifyAll();
		}

		[Fact]
		public void GenerateTiles_ConfigAccess_Throws()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var configAccess = mockRepo.Create<IConfigAccess>();
			configAccess.Setup(c => c.GetConfig())
				.Throws<ConfigAccessException>();

			var offsetService = mockRepo.Create<IOffsetService>();
			offsetService.Setup(o => o.GetCornerOffsets())
				.Returns(TestData.cornerOffsets);
			offsetService.Setup(o => o.GetSideOffsets())
				.Returns(TestData.sideOffsets);

			var mapGeneratorService = new MapGeneratorService(configAccess.Object, offsetService.Object);
			Assert.Throws<ConfigAccessException>(() => mapGeneratorService.GenerateMapTiles().ToList());

			mockRepo.VerifyAll();
		}
	}
}