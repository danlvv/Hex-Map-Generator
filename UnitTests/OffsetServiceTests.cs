using System;
using System.Collections.Generic;
using HexMapGeneration.DataAccess;
using HexMapGeneration.DataModels;
using HexMapGeneration.Services;
using Moq;
using Xunit;

namespace UnitTests
{
	public class OffsetServiceTests
	{
		[Fact]
		public void GetCornerOffsets_Success()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var mockConfigObject = mockRepo.Create<MapConfigObject>();
			mockConfigObject.Setup(o => o.Width)
				.Returns(635);
			mockConfigObject.Setup(o => o.Height)
				.Returns(550);

			var configAccess = mockRepo.Create<IConfigAccess>();
			configAccess.Setup(c => c.GetConfig())
				.Returns(mockConfigObject.Object);

			List<Offset> cornerOffsets = new List<Offset>();

			var opposite = 550 / 2;
			var adjacent = opposite / Math.Sqrt(3);

			cornerOffsets.Add(new Offset(adjacent, 0, Direction.NW));
			cornerOffsets.Add(new Offset(adjacent * 3, 0, Direction.NE));
			cornerOffsets.Add(new Offset(635, opposite, Direction.E));
			cornerOffsets.Add(new Offset(adjacent * 3, 550, Direction.SE));
			cornerOffsets.Add(new Offset(adjacent, 550, Direction.SW));
			cornerOffsets.Add(new Offset(0, opposite, Direction.W));

			var offsetService = new OffsetService(configAccess.Object);
			var returnedOffsets = offsetService.GetCornerOffsets();

			AssertOffsetsEqual(cornerOffsets, returnedOffsets);

			mockRepo.VerifyAll();
		}

		[Fact]
		public void GetCornerOffsets_Failure()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var configAccess = mockRepo.Create<IConfigAccess>();
			configAccess.Setup(c => c.GetConfig())
				.Throws<ConfigAccessException>();

			var offsetService = new OffsetService(configAccess.Object);

			Assert.Throws<ConfigAccessException>(() => offsetService.GetCornerOffsets());

			mockRepo.VerifyAll();
		}

		[Fact]
		public void GetSideOffsets_Success()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var mockConfigObject = mockRepo.Create<MapConfigObject>();
			mockConfigObject.Setup(o => o.Height)
				.Returns(550);
			mockConfigObject.Setup(o => o.Offset)
				.Returns(10);

			var configAccess = mockRepo.Create<IConfigAccess>();
			configAccess.Setup(c => c.GetConfig())
				.Returns(mockConfigObject.Object);

			List<Offset> sideOffsets = new List<Offset>();

			var opposite = 550 / 2;
			var adjacent = opposite / Math.Sqrt(3);

			var angledFaceOffset = adjacent * 3 + 10;
			double heightOffset = 550 + 10;

			sideOffsets.Add(new Offset(0, -heightOffset, Direction.N));
			sideOffsets.Add(new Offset(angledFaceOffset, -heightOffset / 2, Direction.NE));
			sideOffsets.Add(new Offset(angledFaceOffset, heightOffset / 2, Direction.SE));
			sideOffsets.Add(new Offset(0, heightOffset, Direction.S));
			sideOffsets.Add(new Offset(-angledFaceOffset, heightOffset / 2, Direction.SW));
			sideOffsets.Add(new Offset(-angledFaceOffset, -heightOffset / 2, Direction.NW));

			var offsetService = new OffsetService(configAccess.Object);
			var returnedOffsets = offsetService.GetSideOffsets();

			AssertOffsetsEqual(sideOffsets, returnedOffsets);

			mockRepo.VerifyAll();
		}

		[Fact]
		public void GetSideOffsets_Failure()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var configAccess = mockRepo.Create<IConfigAccess>();
			configAccess.Setup(c => c.GetConfig())
				.Throws<ConfigAccessException>();

			var offsetService = new OffsetService(configAccess.Object);

			Assert.Throws<ConfigAccessException>(() => offsetService.GetSideOffsets());

			mockRepo.VerifyAll();
		}

		private void AssertOffsetsEqual(IList<Offset> expected, IList<Offset> actual)
		{
			for (var i = 0; i < expected.Count; i++)
			{
				Assert.Equal(expected[i].X, actual[i].X);
				Assert.Equal(expected[i].Y, actual[i].Y);
				Assert.Equal(expected[i].Direction, actual[i].Direction);
			}
		}
	}
}