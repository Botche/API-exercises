namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using LibraryAPI.BindingModels.Book;
	using LibraryAPI.Database.Models.Books;

	public interface IBookService
	{
		Task<T> GetAllAsync<T>();

		Task<T> GetByIdAsync<T>(Guid id);

		Task<T> AddAsync<T>(PostBookBindingModel book);

		Task<bool> DeleteAsync(Guid id);

		Task<bool> UpdateAsync(Book book);
	}
}
