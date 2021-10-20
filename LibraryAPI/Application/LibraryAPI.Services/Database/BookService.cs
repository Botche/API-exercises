namespace LibraryAPI.Services.Database
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	public class BookService : BaseService<Book>, IBookService
	{
		public BookService(LibraryAPIDbContext dbContext)
			: base(dbContext)
		{

		}

		public async Task<Book> AddAsync(Book book)
		{
			await this.DbSet.AddAsync(book);

			await this.DbContext.SaveChangesAsync();

			return book;
		}

		public Task<bool> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Book>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Book> GetByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateAsync(Book book)
		{
			throw new NotImplementedException();
		}
	}
}
