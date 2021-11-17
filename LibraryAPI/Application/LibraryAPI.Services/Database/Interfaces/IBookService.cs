namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.DTOs.Book;

	public interface IBookService
	{
		Task<T> GetAllAsync<T>(bool withDeleted = false);

		Task<T> GetByIdAsync<T>(Guid id, bool withDeleted = false);

		Task<T> AddAsync<T>(PostBookDTO book);

		Task<bool> UpdateAsync(Guid id, PutBookDTO book, bool withDeleted = false);

		Task<bool> PartialUpdateAsync(Guid id, PatchBookDTO model, bool withDeleted = false);

		Task<bool> DeleteAsync(Guid id);
	}
}
