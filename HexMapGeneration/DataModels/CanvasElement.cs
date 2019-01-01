namespace HexMapGeneration.DataModels
{
	public class CanvasElement : ICanvasElement
	{
		protected CanvasElement(
			double width,
			double height,
			double xOffset,
			double yOffset)
		{
			Width = width;
			Height = height;

			CanvasLeft = xOffset;
			CanvasTop = yOffset;
		}

		public double Width { get; }

		public double Height { get; }

		public double CanvasLeft { get; }

		public double CanvasTop { get; }
	}
}