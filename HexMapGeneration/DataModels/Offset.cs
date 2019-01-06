namespace HexMapGeneration.DataModels
{
	public class Offset
	{
		public Offset(double x, double y, Direction direction = Direction.IGNORE)
		{
			X = x;
			Y = y;

			Direction = direction;
		}

		public double X { get; }

		public double Y { get; }

		public Direction Direction { get; }
	}
}