namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using LibraryAPI.Database.Models.Books;

	public interface IBookService
	{
		Task<IEnumerable<Book>> GetAllAsync();

		Task<Book> GetByIdAsync(Guid id);

		Task<Book> AddAsync(Book book);

		Task<bool> DeleteAsync(Guid id);

		Task<bool> UpdateAsync(Book book);
	}
}
