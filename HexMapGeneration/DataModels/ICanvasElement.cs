namespace HexMapGeneration.DataModels
{
	public interface ICanvasElement
	{
		double Width { get; }

		double Height { get; }

		double CanvasLeft { get; }

		double CanvasTop { get; }
	}
}