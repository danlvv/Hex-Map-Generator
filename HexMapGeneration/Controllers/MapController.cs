using System;
using HexMapGeneration.Utilities;
using HexMapGeneration.ViewModels;

namespace HexMapGeneration.Controllers
{
	public class MapController
	{
		private readonly MapService _mapService;

		public MapController(MapService mapService)
		{
			_mapService = mapService;

			ViewModel = new MapViewModel();
			View = new MapView(ViewModel);

			SetMaxPanValues += View.SetMaxPanValues;
		}

		private event EventHandler<double[]> SetMaxPanValues;

		private MapViewModel ViewModel { get; }

		private MapView View { get; }

		public void Start()
		{
			foreach (var tile in _mapService.GetMapTiles())
			{
				ViewModel.MapTiles.Add(tile);
			}

			SetMaxPanValues?.Invoke(this, _mapService.GetMaxPanningDimensions());

			View.Show();
		}
	}
}