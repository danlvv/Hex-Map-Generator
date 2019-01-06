namespace HexMapGeneration.Testable
{
	public class SystemParameters
	{
		public virtual double GetWidth()
		{
			return System.Windows.SystemParameters.WorkArea.Width;
		}

		public virtual double GetHeight()
		{
			return System.Windows.SystemParameters.WorkArea.Height;
		}
	}
}