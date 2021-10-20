namespace LibraryAPI.Controllers
{
	using System.Threading.Tasks;

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

		[HttpPost]
		public async Task<IActionResult> Post(Book book)
		{
			Book createdBook = await this.BookService.AddAsync(book);
		
			return this.CreatedAtRoute(this.RouteData, book);
		}
	}
}
