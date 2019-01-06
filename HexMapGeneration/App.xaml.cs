using System.Windows;
using HexMapGeneration.Controllers;
using HexMapGeneration.DataAccess;
using HexMapGeneration.Services;
using HexMapGeneration.Testable;
using HexMapGeneration.Utilities;

namespace HexMapGeneration
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var file = new File();
			var jsonConvert = new JsonConvert();

			var configAccess = new ConfigAccess(Constants.AltMapConfig, jsonConvert, file);

			var offsetService = new OffsetService(configAccess);
			var mapDimensionService = new MapDimensionService(configAccess, offsetService);
			var mapGeneratorService = new MapGeneratorService(configAccess, offsetService);

			var controller = new MapController(mapDimensionService, mapGeneratorService);
			controller.Start();
		}
	}
}