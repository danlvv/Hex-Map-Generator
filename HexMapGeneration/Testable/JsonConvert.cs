namespace HexMapGeneration.Testable
{
	public class JsonConvert
	{
		public virtual T DeserializeObject<T>(string json)
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
		}
	}
}