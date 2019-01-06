using System.Collections.Generic;
using HexMapGeneration.DataModels;

namespace UnitTests
{
	public static class TestData
	{
		public static IList<Offset> cornerOffsets = new List<Offset>
		{
			new Offset(124, 0, Direction.NW),
			new Offset(372, 0, Direction.NE),
			new Offset(496, 215, Direction.E),
			new Offset(372, 430, Direction.SE),
			new Offset(124, 430, Direction.SW),
			new Offset(0, 215, Direction.W)
		};

		public static IList<Offset> sideOffsets = new List<Offset>
		{
			new Offset(0, 440, Direction.N),
			new Offset(382, -220, Direction.NE),
			new Offset(382, 220, Direction.SE),
			new Offset(0, -440, Direction.S),
			new Offset(-382, 220, Direction.SW),
			new Offset(-382, -220, Direction.NW)
		};
	}
}