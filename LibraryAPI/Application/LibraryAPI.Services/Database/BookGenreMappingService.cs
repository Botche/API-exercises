namespace LibraryAPI.Services.Database
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.Common.Constants;
	using LibraryAPI.Common.Exceptions;
	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.EntityFrameworkCore;

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

		public async Task<T> GetByBookAndGenreIdAsync<T>(Guid bookId, Guid genreId)
		{
			var bookGenreRelation = await this.DbSet
				.Where(bgm => bgm.BookId == bookId
					&& bgm.GenreId == genreId)
				.Include(bgm => bgm.Book)
				.Include(bgm => bgm.Genre)
				.SingleOrDefaultAsync();

			if (bookGenreRelation == null)
			{
				throw new EntityDoesNotExistException(ExceptionMessages.GENRE_BOOK_MAPPING_DOES_NOT_EXIST_MESSAGE);
			}

			var bookGenreRelationToReturn = this.Mapper.Map<T>(bookGenreRelation);
			return bookGenreRelationToReturn;
		}

		public async Task<T> AddAsync<T>(BookGenreMapping model)
		{
			BookGenreMapping genreToAdd = this.Mapper.Map<BookGenreMapping>(model);

			await this.DbSet.AddAsync(genreToAdd);
			await this.DbContext.SaveChangesAsync();

			T result = this.Mapper.Map<T>(genreToAdd);
			return result;
		}

		public async Task<bool> DeleteAsync(Guid bookId, Guid genreId)
		{
			var bookGenreRelationToDelete = await this.GetByBookAndGenreIdAsync<BookGenreMapping>(bookId, genreId);

			this.DbSet.Remove(bookGenreRelationToDelete);
			await this.DbContext.SaveChangesAsync();

			return true;
		}
	}
}
