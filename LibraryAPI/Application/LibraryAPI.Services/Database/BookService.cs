namespace LibraryAPI.Services.Database
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.BindingModels.Book;
	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.EntityFrameworkCore;

	public class BookService : BaseService<Book>, IBookService
	{
		public BookService(LibraryAPIDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper)
		{

		}

		public async Task<T> GetAllAsync<T>()
		{
			List<Book> books = await this.DbSet
				.OrderBy(b => b.Name)
				.ThenBy(b => b.Author)
			  .Include(b => b.Genres)
				.ToListAsync();

			T mappedBooks = this.Mapper.Map<T>(books);
			return mappedBooks;
		}

		public async Task<T> GetByIdAsync<T>(Guid id)
		{
			Book book = await this.DbSet
				.Include(b => b.Genres)
				.SingleOrDefaultAsync(b => b.Id == id);

			T mappedBook = this.Mapper.Map<T>(book);

			return mappedBook;
		}

		public async Task<T> AddAsync<T>(PostBookBindingModel book)
		{
			Book bookToAdd = this.Mapper.Map<Book>(book);

			await this.DbSet.AddAsync(bookToAdd);
			await this.DbContext.SaveChangesAsync();

			T bookToReturn = this.Mapper.Map<T>(bookToAdd);
			return bookToReturn;
		}

		public Task<bool> UpdateAsync(Book book)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			Book bookToDelete = await this.GetByIdAsync<Book>(id);

			if (bookToDelete == null)
			{
				return false;
			}

			this.DbSet.Remove(bookToDelete);
			int resultFromDb = await this.DbContext.SaveChangesAsync();

			bool result = resultFromDb != 0;
			return result;
		}
	}
}
