namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.BindingModels.Book;

	public interface IBookService
	{
		Task<T> GetAllAsync<T>();

		Task<T> GetByIdAsync<T>(Guid id);

		Task<T> AddAsync<T>(PostBookBindingModel book);

		Task<bool> DeleteAsync(Guid id);

		Task<bool> UpdateAsync(Guid id, PutBookBindingModel book);

		Task<bool> PartialUpdateAsync(Guid id, PatchBookBindingModel model);
	}
}
