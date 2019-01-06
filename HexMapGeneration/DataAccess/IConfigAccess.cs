using HexMapGeneration.DataModels;

namespace HexMapGeneration.DataAccess
{
	public interface IConfigAccess
	{
		IMapConfigObject GetConfig();
	}
}