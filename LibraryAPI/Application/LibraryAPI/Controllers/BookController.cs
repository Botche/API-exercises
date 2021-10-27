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
		[Route("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			GetBookBindingModel book = await this.BookService.GetByIdAsync<GetBookBindingModel>(id);

			if (book == null)
			{
				return this.NotFound();
			}

			return this.Ok(book);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			GetAllBooksBindingModel books = await this.BookService.GetAllAsync<GetAllBooksBindingModel>();

			return this.Ok(books);
		}

		[HttpPost]
		public async Task<IActionResult> Post(PostBookBindingModel model)
		{
			Book createdBook = await this.BookService.AddAsync<Book>(model);
		
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
