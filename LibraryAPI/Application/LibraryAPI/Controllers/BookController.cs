namespace LibraryAPI.Controllers
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.DTOs.Book;
	using LibraryAPI.Constants;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;

	public class BookController : BaseAPIController
	{
		public BookController(IBookService bookService)
		{
			this.BookService = bookService;
		}

		public IBookService BookService { get; }

		/// <summary>
		/// Get book by Id
		/// </summary>
		/// <param name="id">The book id</param>
		/// <returns>Returns the book entity by the given id</returns>
		/// <response code="200">Returns the book entity by the given id</response>
		/// <response code="404">If the book is null</response>
		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get(Guid id)
		{
			GetBookDTO book = await this.BookService.GetByIdAsync<GetBookDTO>(id);

			if (book == null)
			{
				return this.NotFound();
			}

			return this.Ok(book);
		}

		/// <summary>
		/// Get all books
		/// </summary>
		/// <returns>Returns all books sorted by name</returns>
		/// <response code="200">Returns all books sorted by name</response>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			GetAllBooksDTO books = await this.BookService.GetAllAsync<GetAllBooksDTO>();

			return this.Ok(books);
		}

		/// <summary>
		/// Create book
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /api/Book
		///     {
		///        "name": "BookName",
		///        "author": "Author"
		///     }
		///
		/// </remarks>
		/// <param name="model">Body model with data</param>
		/// <returns>The book that is created</returns>
		/// <response code="200">If the book is created successfully</response>
		/// <response code="400">If the body is not correct</response>
		[HttpPost]
		public async Task<IActionResult> Post(PostBookDTO model)
		{
			Book createdBook = await this.BookService.AddAsync<Book>(model);
		
			return this.CreatedAtRoute(this.RouteData, createdBook);
		}

		/// <summary>
		/// Update book
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     PUT /api/Book
		///     {
		///        "name": "BookName",
		///        "author": "Author"
		///     }
		///
		/// </remarks>
		/// <param name="id">The book id</param>
		/// <param name="model">Body model with data to update</param>
		/// <returns>The result from the update action</returns>
		/// <response code="200">If the book is updated successfully</response>
		/// <response code="400">If the body is not correct</response>
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Put(Guid id, PutBookDTO model)
		{
			bool resultFromUpdate = await this.BookService.UpdateAsync(id, model);

			if (resultFromUpdate == false)
			{
				return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
			}

			return this.Ok(resultFromUpdate);
		}

		/// <summary>
		/// Partial update book
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     PATCH /api/Book
		///     {
		///        "name": "BookName",
		///        "author": "Author",
		///        "genresId" [
		///          "GenreId"
		///        ]
		///     }
		///
		/// </remarks>
		/// <param name="id">The book id</param>
		/// <param name="model">Body model with data to partial update</param>
		/// <returns>The result from the update action</returns>
		/// <response code="200">If the book is updated successfully</response>
		/// <response code="400">If the body is not correct</response>
		[HttpPatch]
		[Route("{id}")]
		public async Task<IActionResult> Patch(Guid id, PatchBookDTO model)
		{
			bool resultFromPartialUpdate = await this.BookService.PartialUpdateAsync(id, model);

			if (resultFromPartialUpdate == false)
			{
				return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
			}

			return this.Ok();
		}

		/// <summary>
		/// Delete book by Id
		/// </summary>
		/// <param name="id">The book id</param>
		/// <returns>The result from the delete action</returns>
		/// <response code="200">If the book is deleted successfully</response>
		/// <response code="400">If the book is null</response>
		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			bool resultFromDelete = await this.BookService.DeleteAsync(id);

			if (resultFromDelete == false)
			{
				return this.BadRequest(ExceptionMessages.SOMETHING_WENT_WRONG_MESSAGE);
			}

			return this.Ok(resultFromDelete);
		}
	}
}
