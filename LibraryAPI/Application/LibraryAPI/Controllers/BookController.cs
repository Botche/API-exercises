namespace LibraryAPI.Controllers
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.DTOs.Book;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using System.Linq;
	using System.Collections.Generic;
	using Microsoft.AspNetCore.Mvc.ModelBinding;
	using LibraryAPI.Common.Exceptions;
	using LibraryAPI.Common.Constants;
	using LibraryAPI.Infrastructure.Filters;

	[JwtAuthorize(Roles = new[] { GlobalConstants.USER_ROLE_NAME, GlobalConstants.ADMIN_ROLE_NAME })]
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
		/// <param name="withDeleted"></param>
		/// <returns>Returns the book entity by the given id</returns>
		/// <response code="200">Returns the book entity by the given id</response>
		/// <response code="404">If the book is null</response>
		[HttpGet]
		[Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get(Guid id, bool withDeleted = false)
		{
			GetBookDTO book = await this.BookService.GetByIdAsync<GetBookDTO>(id, withDeleted);

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
		public async Task<IActionResult> Get(bool withDeleted = false)
		{
			GetAllBooksDTO books = await this.BookService.GetAllAsync<GetAllBooksDTO>(withDeleted);

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
		/// <param name="withDeleted"></param>
		/// <returns>The result from the update action</returns>
		/// <response code="200">If the book is updated successfully</response>
		/// <response code="400">If the body is not correct</response>
		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Put(Guid id, PutBookDTO model, bool withDeleted = false)
		{
			bool resultFromUpdate = await this.BookService.UpdateAsync(id, model, withDeleted);

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
		/// <param name="withDeleted"></param>
		/// <returns>The result from the update action</returns>
		/// <response code="200">If the book is updated successfully</response>
		/// <response code="400">If the body is not correct</response>
		[HttpPatch]
		[Route("{id}")]
		public async Task<IActionResult> Patch(Guid id, PatchBookDTO model, bool withDeleted = false)
		{
			bool resultFromPartialUpdate = await this.BookService.PartialUpdateAsync(id, model, withDeleted);

			if (this.ModelState.IsValid == false)
			{
				IEnumerable<ModelError> errors = this.ModelState.Values.SelectMany(v => v.Errors);
				throw new BulkEditModelException(errors);
			}

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
