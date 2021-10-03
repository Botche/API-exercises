namespace Test.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Microsoft.AspNetCore.Mvc;

	using Test.Database;

	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ApplicationDbContext dbContext;

		public WeatherForecastController(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			List<WeatherForecast> weatherForecasts = this.dbContext
				.WeatherForecasts
				.ToList();

			return weatherForecasts;
		}

		[HttpGet]
		[Route("{id?}")]
		public WeatherForecast Get(int id)
		{
			WeatherForecast weatherForecast = this.dbContext
				.WeatherForecasts
				.Where(wf => wf.Id == id)
				.SingleOrDefault();

			return weatherForecast;
		}

		[HttpPost]
		public bool Post(WeatherForecast model)
		{
			this.dbContext
				.WeatherForecasts
				.Add(model);

			this.dbContext.SaveChanges();

			return true;
		}

		[HttpDelete]
		[Route("{id?}")]
		public bool Delete(int id)
		{
			WeatherForecast weatherForecast = this.dbContext
				.WeatherForecasts
				.Find(id);

			this.dbContext
				.WeatherForecasts
				.Remove(weatherForecast);

			this.dbContext.SaveChanges();

			return true;
		}

		[HttpPut]
		public bool Put(WeatherForecast model)
		{
			WeatherForecast weatherForecast = this.dbContext
				.WeatherForecasts
				.Find(model.Id);

			weatherForecast.Date = model.Date;
			weatherForecast.TemperatureC = model.TemperatureC;
			weatherForecast.Summary = model.Summary;

			this.dbContext
				.WeatherForecasts
				.Update(weatherForecast);

			this.dbContext
				.SaveChanges();

			return true;
		}
	}
}
