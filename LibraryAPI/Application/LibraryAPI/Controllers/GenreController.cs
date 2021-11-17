namespace LibraryAPI.Controllers
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.DTOs.Genre;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Mvc;
	using LibraryAPI.Common.Constants;

	public class GenreController : BaseAPIController
	{
		public GenreController(IGenreService genreService)
		{
			this.GenreService = genreService;
		}

		public IGenreService GenreService { get; }

		[HttpGet]
		public async Task<IActionResult> Get(bool withDeleted = false)
		{
			GetAllGenreDTO genres = await this.GenreService.GetAllAsync<GetAllGenreDTO>(withDeleted);

			return this.Ok(genres);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> Get(Guid id, bool withDeleted = false)
		{
			GetGenreDTO genre = await this.GenreService.GetByIdAsync<GetGenreDTO>(id, withDeleted);

			if (genre == null)
			{
				return this.NotFound();
			}

			return this.Ok(genre);
		}


		[HttpPost]
		public async Task<IActionResult> Post(PostGenreDTO model)
		{
			GetGenreDTO createdGenre = await this.GenreService.AddAsync<GetGenreDTO>(model);

			return this.CreatedAtRoute(this.RouteData, createdGenre);
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Put(Guid id, PutGenreDTO model, bool withDeleted = false)
		{
			bool resultFromUpdate = await this.GenreService.UpdateAsync(id, model, withDeleted);

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
