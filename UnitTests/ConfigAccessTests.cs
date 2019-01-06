using System.IO;
using HexMapGeneration.DataAccess;
using HexMapGeneration.DataModels;
using Moq;
using Newtonsoft.Json;
using Xunit;
using File = HexMapGeneration.Testable.File;
using JsonConvert = HexMapGeneration.Testable.JsonConvert;

namespace UnitTests
{
	public class ConfigAccessTests
	{
		[Fact]
		public void GetConfig_Success()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var jsonFile = "jsonFile";
			var returnedJson = "{ json }";
			var configObject = new MapConfigObject();

			var mockFile = mockRepo.Create<File>();
			mockFile.Setup(f => f.ReadAsString(jsonFile))
				.Returns(returnedJson);

			var mockJsonConvert = mockRepo.Create<JsonConvert>();
			mockJsonConvert.Setup(jc => jc.DeserializeObject<MapConfigObject>(returnedJson))
				.Returns(configObject);

			var configAccess = new ConfigAccess(jsonFile, mockJsonConvert.Object, mockFile.Object);
			var returnedConfig = configAccess.GetConfig();

			Assert.Equal(returnedConfig, configObject);

			mockRepo.VerifyAll();
		}

		[Fact]
		public void FileRead_Error()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var jsonFile = "jsonFile";

			var mockFile = mockRepo.Create<File>();
			mockFile.Setup(f => f.ReadAsString(jsonFile))
				.Throws<FileNotFoundException>();

			var mockJsonConvert = mockRepo.Create<JsonConvert>();

			var configAccess = new ConfigAccess(jsonFile, mockJsonConvert.Object, mockFile.Object);

			Assert.Throws<ConfigAccessException>(() => configAccess.GetConfig());

			mockRepo.VerifyAll();
		}

		[Fact]
		public void JsonConvert_Error()
		{
			var mockRepo = new MockRepository(MockBehavior.Strict);

			var jsonFile = "jsonFile";
			var returnedJson = "{ json }";

			var mockFile = mockRepo.Create<File>();
			mockFile.Setup(f => f.ReadAsString(jsonFile))
				.Returns(returnedJson);

			var mockJsonConvert = mockRepo.Create<JsonConvert>();
			mockJsonConvert.Setup(jc => jc.DeserializeObject<MapConfigObject>(returnedJson))
				.Throws<JsonReaderException>();

			var configAccess = new ConfigAccess(jsonFile, mockJsonConvert.Object, mockFile.Object);

			Assert.Throws<ConfigAccessException>(() => configAccess.GetConfig());

			mockRepo.VerifyAll();
		}
	}
}