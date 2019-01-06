using System;
using HexMapGeneration.DataAccess;
using HexMapGeneration.Services;
using HexMapGeneration.Testable;
using HexMapGeneration.Utilities;
using HexMapGeneration.ViewModels;
using File = System.IO.File;

namespace HexMapGeneration.Controllers
{
	public class MapController
	{
		private readonly IMapDimensionService _dimensionService;
		private readonly IMapGeneratorService _generatorService;

		public MapController(IMapDimensionService dimensionService, IMapGeneratorService generatorService)
		{
			_dimensionService = dimensionService;
			_generatorService = generatorService;

			ViewModel = new MapViewModel();
			View = new MapView(ViewModel);

			SetMaxPanValues += View.SetMaxPanValues;
		}

		private event EventHandler<double[]> SetMaxPanValues;

		private MapViewModel ViewModel { get; }

		private MapView View { get; }

		public void Start()
		{
			try
			{
				foreach (var tile in _generatorService.GenerateMapTiles())
				{
					ViewModel.MapTiles.Add(tile);
				}

				SetMaxPanValues?.Invoke(this, _dimensionService.GetMapDimensions());

				View.Show();
			}
			catch (ConfigAccessException e)
			{
				File.WriteAllLines(Constants.ErrorLog, new []
				{
					e.Message,
					e.StackTrace
				});
			}
		}
	}
}