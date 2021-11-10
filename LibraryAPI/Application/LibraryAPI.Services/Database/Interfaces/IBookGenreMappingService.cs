namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.Database.Models.Books;

	public interface IBookGenreMappingService
	{
		Task<T> GetAllAsync<T>();

		Task<T> GetByIdAsync<T>(Guid id);

		Task<T> AddAsync<T>(BookGenreMapping model);

		Task<bool> DeleteAsync(Guid id);
	}
}
