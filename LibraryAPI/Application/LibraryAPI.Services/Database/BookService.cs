namespace LibraryAPI.Services.Database
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.DTOs.Book;
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

		public async Task<T> AddAsync<T>(PostBookDTO book)
		{
			Book bookToAdd = this.Mapper.Map<Book>(book);

			await this.DbSet.AddAsync(bookToAdd);
			await this.DbContext.SaveChangesAsync();

			T bookToReturn = this.Mapper.Map<T>(bookToAdd);
			return bookToReturn;
		}

		public async Task<bool> UpdateAsync(Guid id, PutBookDTO book)
		{
			Book bookToUpdate = await this.GetByIdAsync<Book>(id);

			if (bookToUpdate == null)
			{
				return false;
			}

			Book updatedBook = this.Mapper.Map(book, bookToUpdate);
			updatedBook.UpdatedOn = DateTime.UtcNow;

			this.DbContext.Update(updatedBook);
			await this.DbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> PartialUpdateAsync(Guid id, PatchBookDTO model)
		{
			Book bookToUpdate = await this.GetByIdAsync<Book>(id);

			if (bookToUpdate == null)
			{
				return false;
			}

			Type modelType = model.GetType();
			PropertyInfo[] properties = modelType.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				var propertyValue = propertyInfo.GetValue(model);
				if (propertyValue != null)
				{
					Type bookToUpdateType = bookToUpdate.GetType();
					PropertyInfo propertyToUpdate = bookToUpdateType.GetProperty(propertyInfo.Name);
					propertyToUpdate.SetValue(bookToUpdate, propertyValue);
				}
			}

			bookToUpdate.UpdatedOn = DateTime.UtcNow;

			this.DbContext.Update(bookToUpdate);
			await this.DbContext.SaveChangesAsync();

			return true;

			// Functional version
			//	model
			//		.GetType()
			//		.GetProperties()
			//		.ToList()
			//		.ForEach(propertyInfo =>
			//		{
			//			var propertyValue = propertyInfo.GetValue(model);
			//			if (propertyValue != null)
			//			{
			//				bookToUpdate
			//					.GetType()
			//					.GetProperty(propertyInfo.Name)
			//					.SetValue(bookToUpdate, propertyValue);
			//			}
			//		});
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
