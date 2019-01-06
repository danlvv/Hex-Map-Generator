using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HexMapGeneration.DataModels
{
	public class Hexagon : CanvasElement
	{
		protected Hexagon(
			string imagePath,
			double xOffset,
			double yOffset,
			double width,
			double height,
			PointCollection points,
			int rotation)
			: base(width, height, xOffset, yOffset)
		{
			var image = new BitmapImage(new Uri(imagePath));

			var rotationTransform = new RotateTransform(rotation, width / 2, height / 2);
			var transformGroup = new TransformGroup();
			transformGroup.Children.Add(rotationTransform);

			Shape = new Polygon
			{
				Points = points,
				Fill = new ImageBrush(image)
			};

			Shape.Fill.Transform = transformGroup;
		}

		public Polygon Shape { get; }
	}
}