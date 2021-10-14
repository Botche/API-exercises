namespace LibraryAPI.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database;

	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("api/[controller]")]
	public class BookController : ControllerBase
	{
		public BookController(BookService bookService)
		{
			this.BookService = bookService;
		}

		public BookService BookService { get; }

		[HttpPost]
		public async Task<IActionResult> Post(Book book)
		{
			Book createdBook = await this.BookService.AddAsync(book);
		
			return this.CreatedAtRoute(this.RouteData, book);
		}
	}
}
