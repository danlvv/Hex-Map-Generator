using System.Windows.Media;

namespace HexMapGeneration.DataModels
{
	public class MapTile : Hexagon
	{
		public MapTile(
			string imagePath,
			double xOffset,
			double yOffset,
			double width,
			double height,
			PointCollection points,
			int rotation = 0)
			: base(imagePath, xOffset, yOffset, width, height, points, rotation)
		{
		}
	}
}