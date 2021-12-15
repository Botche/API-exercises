namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	public interface IBookUserMappingService
	{
		Task<T> GetModelByBookIdAndUserIdAsync<T>(Guid bookId, Guid userId);

		Task<T> CreateRelationAsync<T>(Guid bookId, Guid userId, DateTime deadLine);

		Task<T> UpdateRelationAsync<T>(Guid bookId, Guid userId, DateTime deadLine);

		Task<T> DeleteRelationAsync<T>(Guid bookId, Guid userId);

		Task<T> SetReturnDateAsync<T>(Guid bookId, Guid userId, DateTime returnDate);
	}
}
