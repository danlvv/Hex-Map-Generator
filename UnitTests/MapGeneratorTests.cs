using System.IO;
using HexMapGeneration.Utilities;
using Newtonsoft.Json;
using Xunit;

namespace UnitTests
{
	public class MapGeneratorTests
	{
		[Fact]
		public void BadJson_ThrowsException()
		{
			Assert.Throws<JsonSerializationException>(() => new MapGenerator(TestData.BadJson));
		}

		[Fact]
		public void MapConfigFile_ReadsProperly()
		{
			var fileJson = File.ReadAllText(TestData.MapConfig);
			var configObject = JsonConvert.DeserializeObject<MapConfigObject>(fileJson);

			Assert.Equal(configObject.Height, TestData.Height);
			Assert.Equal(configObject.Width, TestData.Width);
			Assert.Equal(configObject.Offset, TestData.Offset);
			Assert.Equal(configObject.TilesHigh, TestData.TilesHigh);
			Assert.Equal(configObject.TilesWide, TestData.TilesWidth);

			Assert.Equal(configObject.ConfigSummary[0].Rotation, TestData.Rotation1);
			Assert.Equal(configObject.ConfigSummary[0].Expand_Direction, TestData.Direction1);
			Assert.Equal(configObject.ConfigSummary[0].ImagePath, TestData.Image1);

			Assert.Equal(configObject.ConfigSummary[1].Rotation, TestData.Rotation2);
			Assert.Equal(configObject.ConfigSummary[1].Expand_Direction, TestData.Direction2);
			Assert.Equal(configObject.ConfigSummary[1].ImagePath, TestData.Image2);

			Assert.Equal(configObject.ConfigSummary[2].Rotation, TestData.Rotation3);
			Assert.Equal(configObject.ConfigSummary[2].Expand_Direction, TestData.Direction3);
			Assert.Equal(configObject.ConfigSummary[2].ImagePath, TestData.Image3);
		}
	}
}