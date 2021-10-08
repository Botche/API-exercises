namespace Test.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using Test.BindingModels;
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
		public IActionResult Get()
		{
			List<WeatherForecast> weatherForecasts = this.dbContext
				.WeatherForecasts
				.ToList();

			return this.Ok(weatherForecasts);
		}

		[HttpGet]
		[Route("{id}")]
		public IActionResult Get(int id)
		{
			WeatherForecast weatherForecast = this.dbContext
				.WeatherForecasts
				.Where(wf => wf.Id == id)
				.SingleOrDefault();

			if (weatherForecast == null)
			{
				return this.NotFound();
			}

			return this.Ok(weatherForecast);
		}

		[HttpPost]
		public async Task<IActionResult> Post(PostWeatherForecast model)
		{
			if (this.ModelState.IsValid == false)
			{
				return BadRequest(this.ModelState);
			}

			WeatherForecast weatherForecast = new WeatherForecast();
			weatherForecast.Date = model.Date;
			weatherForecast.TemperatureC = model.TemperatureC;
			weatherForecast.Summary = model.Summary;

			await this.dbContext
				.WeatherForecasts
				.AddAsync(weatherForecast);

			await this.dbContext.SaveChangesAsync();

			return this.CreatedAtRoute(this.RouteData, weatherForecast);
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			WeatherForecast weatherForecast = this.dbContext
				.WeatherForecasts
				.Find(id);

			if (weatherForecast == null)
			{
				return this.NotFound();
			}

			this.dbContext
				.WeatherForecasts
				.Remove(weatherForecast);

			await this.dbContext.SaveChangesAsync();

			return this.NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> Put(PutWeatherForecast model)
		{
			if (this.ModelState.IsValid == false)
			{
				return BadRequest(this.ModelState);
			}

			WeatherForecast weatherForecast = this.dbContext
				.WeatherForecasts
				.Find(model.Id);

			if (weatherForecast == null)
			{
				return this.NotFound();
			}

			weatherForecast.Date = model.Date;
			weatherForecast.TemperatureC = model.TemperatureC;
			weatherForecast.Summary = model.Summary;

			this.dbContext
				.WeatherForecasts
				.Update(weatherForecast);

			await this.dbContext.SaveChangesAsync();

			return this.Ok(weatherForecast);
		}
	}
}
