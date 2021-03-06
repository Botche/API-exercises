namespace LibraryAPI.Services.Database
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.Common.Constants;
	using LibraryAPI.Common.Exceptions;
	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.DTOs.Book;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.EntityFrameworkCore;

	public class BookService : BaseService<Book>, IBookService
	{
		private readonly IGenreService genreService;
		private readonly IBookGenreMappingService bookGenreMappingService;

		public BookService(LibraryAPIDbContext dbContext, 
			IMapper mapper,
			IActionContextAccessor actionContextAccessor,
			IGenreService genreService,
			IBookGenreMappingService bookGenreMappingService)
			: base(dbContext, mapper, actionContextAccessor)
		{
			this.genreService = genreService;
			this.bookGenreMappingService = bookGenreMappingService;
		}

		public async Task<T> GetAllAsync<T>(bool withDeleted = false)
		{
			IQueryable<Book> booksQuery = this.DbSet
				.OrderBy(b => b.Name)
				.ThenBy(b => b.Author)
			  .Include(b => b.Genres)
				.ThenInclude(b => b.Genre)
				.AsQueryable();

			if (withDeleted == false)
			{
				booksQuery = booksQuery
					.Where(b => b.IsDeleted == false)
					.Include(b => b.Genres.Where(bgm => bgm.Genre.IsDeleted == false));
			}

			List<Book> books = await booksQuery.ToListAsync();
			T mappedBooks = this.Mapper.Map<T>(books);
			return mappedBooks;
		}

		public async Task<T> GetByIdAsync<T>(Guid id, bool withDeleted = false)
		{
			IQueryable<Book> bookQuery = this.DbSet
				.Include(b => b.Genres)
				.ThenInclude(bgm => bgm.Genre)
				.AsQueryable();

			if (withDeleted == false)
			{
				bookQuery = bookQuery.Where(b => b.IsDeleted == false);
			}

			Book book = await bookQuery.SingleOrDefaultAsync(b => b.Id == id);
			if (book == null)
			{
				throw new EntityDoesNotExistException(ExceptionMessages.BOOK_DOES_NOT_EXIST_MESSAGE);
			}

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

		public async Task<bool> UpdateAsync(Guid id, PutBookDTO book, bool withDeleted = false)
		{
			Book bookToUpdate = await this.GetByIdAsync<Book>(id, withDeleted);

			if (bookToUpdate == null)
			{
				throw new EntityDoesNotExistException(ExceptionMessages.BOOK_DOES_NOT_EXIST_MESSAGE);
			}

			Book updatedBook = this.Mapper.Map(book, bookToUpdate);
			updatedBook.UpdatedOn = DateTime.UtcNow;

			this.DbContext.Update(updatedBook);
			await this.DbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> PartialUpdateAsync(Guid id, PatchBookDTO model, bool withDeleted = false)
		{
			Book bookToUpdate = await this.GetByIdAsync<Book>(id, withDeleted);

			if (bookToUpdate == null)
			{
				throw new EntityDoesNotExistException(ExceptionMessages.BOOK_DOES_NOT_EXIST_MESSAGE);
			}

			Type modelType = model.GetType();
			PropertyInfo[] properties = modelType.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				var propertyValue = propertyInfo.GetValue(model);
				if (propertyValue != null)
				{
					Type propertyType = propertyInfo.PropertyType;
					bool isPropertyTypeIEnumerable = propertyType.IsGenericType 
						&& propertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

					if (isPropertyTypeIEnumerable)
					{
						IEnumerable<Guid> genresId = propertyInfo.GetValue(model) as IEnumerable<Guid>;
						await this.SaveGenresToBook(genresId, bookToUpdate);

						continue;
					}

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
				throw new EntityDoesNotExistException(ExceptionMessages.BOOK_DOES_NOT_EXIST_MESSAGE);
			}

			bookToDelete.IsDeleted = true;
			bookToDelete.DeletedOn = DateTime.UtcNow;

			this.DbSet.Update(bookToDelete);
			int resultFromDb = await this.DbContext.SaveChangesAsync();

			bool result = resultFromDb != 0;
			return result;
		}

		private async Task SaveGenresToBook(IEnumerable<Guid> genresId, Book book)
		{
			foreach (Guid genreId in genresId)
			{
				Genre genre = await genreService.GetByIdAsync<Genre>(genreId);
				if (genre == null)
				{
					this.AddModelError("GenreId", string.Format(ExceptionMessages.GENRE_DOES_NOT_EXIST_MESSAGE, genreId));
					continue;
				}

				bool isGenreAlreadyAssigned = book.Genres
					.Any(bgm => bgm.BookId == book.Id 
							&& bgm.GenreId == genre.Id);
				if (isGenreAlreadyAssigned)
				{
					this.AddModelError("GenreId", string.Format(ExceptionMessages.GENRE_ALREADY_ADDED_MESSAGE, genreId));
					continue;
				}

				BookGenreMapping bookGenreMapping = new BookGenreMapping
				{
					BookId = book.Id,
					GenreId = genre.Id
				};

				await this.bookGenreMappingService.AddAsync<BookGenreMapping>(bookGenreMapping);
			}
		}
	}
}
