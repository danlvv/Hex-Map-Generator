using System.Collections.Generic;
using HexMapGeneration.DataModels;

namespace HexMapGeneration.Services
{
	public interface IMapGeneratorService
	{
		IEnumerable<MapTile> GenerateMapTiles();
	}
}