using System.Windows;
using HexMapGeneration.Controllers;
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
			// Change which config file is referenced here or change the Map Config constant
			var mapGenerator = new MapGenerator(Constants.AltMapConfig);
			var mapService = new MapService(mapGenerator);

			var controller = new MapController(mapService);
			controller.Start();
		}
	}
}