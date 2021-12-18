namespace LibraryAPI.Controllers
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.DTOs.Book;
	using LibraryAPI.DTOs.User;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Mvc;

	public class UserController : BaseAPIController
	{
		private readonly IUserService userService;
		private readonly IBookService bookService;
		private readonly IBookUserMappingService bookUserMappingService;

		public UserController(
			IUserService userService,
			IBookService bookService,
			IBookUserMappingService bookUserMappingService)
		{
			this.userService = userService;
			this.bookService = bookService;
			this.bookUserMappingService = bookUserMappingService;
		}

		[HttpPost]
		[Route("get-book")]
		public async Task<IActionResult> Post(string userEmail, Guid bookId, DateTime deadLine)
		{
			var user = await this.userService.GetUserByEmailAsync<GetUserIdDTO>(userEmail);
			var book = await this.bookService.GetByIdAsync<GetBookDTO>(bookId);
			await this.bookUserMappingService.CreateRelationAsync<BookUserMapping>(user.Id, book.Id, deadLine);

			return this.Ok();
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(PostUserLoginDTO model)
		{
			string token = await this.userService.LoginAsync(model);

			if (token == null)
			{
				throw new ArgumentException();
			}

			return this.Ok(token);
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> Register(PostUserRegisterDTO model)
		{
			GetUserInformationDTO user = await this.userService.RegisterAsync<GetUserInformationDTO>(model);

			if (user == null)
			{
				throw new ArgumentException();
			}

			return this.Ok(user);
		}
	}
}
