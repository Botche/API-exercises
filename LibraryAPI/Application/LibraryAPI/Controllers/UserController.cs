﻿namespace LibraryAPI.Controllers
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.DTOs.User;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Mvc;

	public class UserController : BaseAPIController
	{
		private readonly IUserService userService;

		public UserController(IUserService userService)
		{
			this.userService = userService;
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
