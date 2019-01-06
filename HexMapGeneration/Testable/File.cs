namespace HexMapGeneration.Testable
{
	public class File
	{
		public virtual string ReadAsString(string file)
		{
			return System.IO.File.ReadAllText(file);
		}
	}
}