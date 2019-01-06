using System.Collections.Generic;
using HexMapGeneration.DataModels;

namespace HexMapGeneration.Services
{
	public interface IOffsetService
	{
		IList<Offset> GetCornerOffsets();

		IList<Offset> GetSideOffsets();
	}
}