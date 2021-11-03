namespace LibraryAPI.Controllers
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.BindingModels.Genre;
	using LibraryAPI.Constants;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Produces("application/json")]
	[Route("[controller]")]
	public class GenreController : ControllerBase
	{
		public GenreController(IGenreService genreService)
		{
			this.GenreService = genreService;
		}

		public IGenreService GenreService { get; }

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			GetAllGenreBindingModel genres = await this.GenreService.GetAllAsync<GetAllGenreBindingModel>();

			return this.Ok(genres);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			GetGenreBindingModel genre = await this.GenreService.GetByIdAsync<GetGenreBindingModel>(id);

			if (genre == null)
			{
				return this.NotFound();
			}

			return this.Ok(genre);
		}


		[HttpPost]
		public async Task<IActionResult> Post(PostGenreBindingModel model)
		{
			GetGenreBindingModel createdGenre = await this.GenreService.AddAsync<GetGenreBindingModel>(model);

			return this.CreatedAtRoute(this.RouteData, createdGenre);
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Put(Guid id, PutGenreBindingModel model)
		{
			bool resultFromUpdate = await this.GenreService.UpdateAsync(id, model);

			if (resultFromUpdate == false)
			{
				return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
			}

			return this.NoContent();
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			bool resultFromDelete = await this.GenreService.DeleteAsync(id);

			if (resultFromDelete == false)
			{
				return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
			}

			return this.NoContent();
		}
	}
}
