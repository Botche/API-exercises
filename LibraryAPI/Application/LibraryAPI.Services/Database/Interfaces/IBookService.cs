namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.DTOs.Book;

	public interface IBookService
	{
		Task<T> GetAllAsync<T>();

		Task<T> GetByIdAsync<T>(Guid id);

		Task<T> AddAsync<T>(PostBookDTO book);

		Task<bool> UpdateAsync(Guid id, PutBookDTO book);

		Task<bool> PartialUpdateAsync(Guid id, PatchBookDTO model);

		Task<bool> DeleteAsync(Guid id);
	}
}
