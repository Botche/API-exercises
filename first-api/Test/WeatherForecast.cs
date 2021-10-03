using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test
{
	public class WeatherForecast
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public int TemperatureC { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public int TemperatureF { get; set; }

		public string Summary { get; set; }
	}
}
