namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.DTOs.Genre;

	public interface IGenreService
	{
		Task<T> GetAllAsync<T>(bool withDeleted = false);

		Task<T> GetByIdAsync<T>(Guid id, bool withDeleted = false);

		Task<T> AddAsync<T>(PostGenreDTO model);

		Task<bool> UpdateAsync(Guid id, PutGenreDTO model, bool withDeleted = false);

		Task<bool> DeleteAsync(Guid id);
	}
}
