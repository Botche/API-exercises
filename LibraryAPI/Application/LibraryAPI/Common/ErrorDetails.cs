namespace LibraryAPI.Common
{
	using System.Text.Json;

	public class ErrorDetails
	{
		public int StatusCode { get; set; }

		public dynamic Message { get; set; }

		// public IEnumerable<string> Messages { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}
