namespace LibraryAPI.Services.Database
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using LibraryAPI.BindingModels.Book;
	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.EntityFrameworkCore;

	public class BookService : BaseService<Book>, IBookService
	{
		public BookService(LibraryAPIDbContext dbContext)
			: base(dbContext)
		{

		}
		public async Task<IEnumerable<GetAllBooksBindingModel>> GetAllAsync()
		{
			List<GetAllBooksBindingModel> books = await this.DbSet
				.Select(b => new GetAllBooksBindingModel
				{
					Id = b.Id,
					Author = b.Author,
					Name = b.Name,
					CreatedOn = b.CreatedOn,
					UpdatedOn = b.UpdatedOn,
					DeletedOn = b.DeletedOn,
					IsDeleted = b.IsDeleted,
				})
				.OrderBy(b => b.Name)
				.ThenBy(b => b.Author)
				.ToListAsync();

			return books;
		}

		public async Task<Book> GetByIdAsync(Guid id)
		{
			Book book = await this.DbSet.FindAsync(id);

			return book;
		}

		public async Task<Book> AddAsync(PostBookBindingModel book)
		{
			// Add AutoMapper
			Book bookToAdd = new Book();
			bookToAdd.Name = book.Name;
			bookToAdd.Author = book.Author;

			await this.DbSet.AddAsync(bookToAdd);
			await this.DbContext.SaveChangesAsync();

			return bookToAdd;
		}

		public Task<bool> UpdateAsync(Book book)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			Book bookToDelete = await this.GetByIdAsync(id);

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
