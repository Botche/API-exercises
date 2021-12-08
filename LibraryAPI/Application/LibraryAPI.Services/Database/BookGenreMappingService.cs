namespace LibraryAPI.Services.Database
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	public class BookGenreMappingService : BaseService<BookGenreMapping>, IBookGenreMappingService
	{
		public BookGenreMappingService(LibraryAPIDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper)
		{

		}

		public Task<T> GetAllAsync<T>()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByIdAsync<T>(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<T> AddAsync<T>(BookGenreMapping model)
		{
			BookGenreMapping genreToAdd = this.Mapper.Map<BookGenreMapping>(model);

			await this.DbSet.AddAsync(genreToAdd);
			await this.DbContext.SaveChangesAsync();

			T result = this.Mapper.Map<T>(genreToAdd);
			return result;
		}

		public Task<bool> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
