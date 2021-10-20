namespace LibraryAPI.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using LibraryAPI.BindingModels.Book;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("api/[controller]")]
	public class BookController : ControllerBase
	{
		public BookController(IBookService bookService)
		{
			this.BookService = bookService;
		}

		public IBookService BookService { get; }

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			IEnumerable<GetAllBooksBindingModel> books = await this.BookService.GetAllAsync();

			return this.Ok(books);
		}

		[HttpPost]
		public async Task<IActionResult> Post(PostBookBindingModel model)
		{
			Book createdBook = await this.BookService.AddAsync(model);
		
			return this.CreatedAtRoute(this.RouteData, createdBook);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			bool resultFromDelete = await this.BookService.DeleteAsync(id);

			if (resultFromDelete == false)
			{
				return this.BadRequest("Something went wrong!");
			}

			return this.Ok(resultFromDelete);
		}
	}
}
