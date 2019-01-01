using System.Collections.ObjectModel;
using HexMapGeneration.DataModels;

namespace HexMapGeneration.ViewModels
{
	public class MapViewModel
	{
		public MapViewModel()
		{
			MapTiles = new ObservableCollection<Hexagon>();
		}

		public ObservableCollection<Hexagon> MapTiles { get; }
	}
}