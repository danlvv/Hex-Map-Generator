using System;

namespace HexMapGeneration.DataModels
{
	public enum Direction
	{
		IGNORE,
		N,
		NE,
		SE,
		S,
		SW,
		NW,
		E,
		W
	}

	public static class DirectionConverter
	{
		public static Direction Convert(string value)
		{
			Direction direction;
			if (!Enum.TryParse(value, true, out direction))
			{
				throw new Exception("Failed to convert string to Direction");
			}

			return direction;
		}
	}
}