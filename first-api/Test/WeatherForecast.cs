using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test
{
	public class WeatherForecast
	{
		public int Id { get; set; }

		[Required]
		public DateTime? Date { get; set; }

		[Range(-70, 70)]
		[Required]
		public int TemperatureC { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public int TemperatureF { get; set; }

		[MaxLength(128)]
		public string Summary { get; set; }
	}
}
