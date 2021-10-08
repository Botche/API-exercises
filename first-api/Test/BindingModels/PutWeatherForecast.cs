namespace Test.BindingModels
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class PutWeatherForecast
	{
		[Required]
		public int? Id { get; set; }

		[Required]
		public DateTime? Date { get; set; }

		[Range(-70, 70)]
		[Required]
		public int TemperatureC { get; set; }

		[MaxLength(128)]
		public string Summary { get; set; }
	}
}
