namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.DTOs.User;

	public interface IUserService
	{
		Task<T> RegisterAsync<T>(PostUserRegisterDTO model);

		Task<string> LoginAsync(PostUserLoginDTO model);

		Task<T> GetUserByIdAsync<T>(Guid id);

		Task<T> GetUserByEmailAsync<T>(string email);

		string GeneratePasswordSalt();

		string HashPassword(string password, string passwordSalt);

		Task<bool> IsThereAnyDataInTableAsync();
	}
}
