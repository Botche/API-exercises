namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.BindingModels.Genre;

	public interface IGenreService
	{
		Task<T> GetAllAsync<T>();

		Task<T> GetByIdAsync<T>(Guid id);

		Task<T> AddAsync<T>(PostGenreBindingModel model);

		Task<bool> UpdateAsync(Guid id, PutGenreBindingModel model);

		Task<bool> DeleteAsync(Guid id);
	}
}
