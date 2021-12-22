namespace LibraryAPI.Controllers
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.DTOs.Book;
	using LibraryAPI.DTOs.BookUserMapping;
	using LibraryAPI.DTOs.User;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Mvc;

	[Route("api/get-book")]
	public class BookUserMappingController : BaseAPIController
	{
		private readonly IUserService userService;
		private readonly IBookService bookService;
		private readonly IBookUserMappingService bookUserMappingService;

		public BookUserMappingController(
			IUserService userService,
			IBookService bookService,
			IBookUserMappingService bookUserMappingService)
		{
			this.userService = userService;
			this.bookService = bookService;
			this.bookUserMappingService = bookUserMappingService;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string userEmail, Guid bookId)
		{
			var user = await this.userService.GetUserByEmailAsync<GetUserIdDTO>(userEmail);
			var book = await this.bookService.GetByIdAsync<GetBookDTO>(bookId);
			var bookUserMapping = await this.bookUserMappingService.GetModelByBookIdAndUserIdAsync<GetBookUserDTO>(book.Id, user.Id);

			return this.Ok(bookUserMapping);
		}

		[HttpPost]
		public async Task<IActionResult> Post(PostBookUserDTO model)
		{
			var user = await this.userService.GetUserByEmailAsync<GetUserIdDTO>(model.UserEmail);
			var book = await this.bookService.GetByIdAsync<GetBookDTO>(model.BookId);
			var bookUserMapping = await this.bookUserMappingService.CreateRelationAsync<GetBookUserDTO>(book.Id, user.Id, model.DeadLine);

			return this.Ok(bookUserMapping);
		}

		[HttpPatch]
		public async Task<IActionResult> Patch(PatchBookUserDTO model)
		{
			var user = await this.userService.GetUserByEmailAsync<GetUserIdDTO>(model.UserEmail);
			var book = await this.bookService.GetByIdAsync<GetBookDTO>(model.BookId);
			var bookUserMapping = await this.bookUserMappingService.UpdateDeadLineAsync<GetBookUserDTO>(book.Id, user.Id, model.DeadLine);

			return this.Ok(bookUserMapping);
		}

		[HttpPatch]
		[Route("return")]
		public async Task<IActionResult> ReturnBook(PatchReturnBookUserDTO model)
		{
			var user = await this.userService.GetUserByEmailAsync<GetUserIdDTO>(model.UserEmail);
			var book = await this.bookService.GetByIdAsync<GetBookDTO>(model.BookId);
			var bookUserMapping = await this.bookUserMappingService.SetReturnDateAsync<GetBookUserDTO>(book.Id, user.Id, model.ReturnDate);

			return this.Ok(bookUserMapping);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(string userEmail, Guid bookId)
		{
			var user = await this.userService.GetUserByEmailAsync<GetUserIdDTO>(userEmail);
			var book = await this.bookService.GetByIdAsync<GetBookDTO>(bookId);
			var bookUserMapping = await this.bookUserMappingService.DeleteRelationAsync<GetBookUserDTO>(book.Id, user.Id);

			return this.Ok(bookUserMapping);
		}
	}
}
