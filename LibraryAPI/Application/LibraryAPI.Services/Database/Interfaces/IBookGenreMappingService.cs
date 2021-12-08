namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.Database.Models.Books;

	public interface IBookGenreMappingService
	{
		Task<T> GetAllAsync<T>();

		Task<T> GetByIdAsync<T>(Guid id);

		Task<T> GetByBookAndGenreIdAsync<T>(Guid bookId, Guid genreId);

		Task<T> AddAsync<T>(BookGenreMapping model);

		Task<bool> DeleteAsync(Guid bookId, Guid genreId);
	}
}
