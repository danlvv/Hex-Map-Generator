using System;

namespace HexMapGeneration.DataAccess
{
	public class ConfigAccessException : Exception
	{
		// Parameterless constructor for mocking
		public ConfigAccessException()
		{
		}

		public ConfigAccessException(Exception e)
			: base(e.Message, e)
		{
		}
	}
}