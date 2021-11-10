namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.DTOs.Genre;

	public interface IGenreService
	{
		Task<T> GetAllAsync<T>();

		Task<T> GetByIdAsync<T>(Guid id);

		Task<T> AddAsync<T>(PostGenreDTO model);

		Task<bool> UpdateAsync(Guid id, PutGenreDTO model);

		Task<bool> DeleteAsync(Guid id);
	}
}
