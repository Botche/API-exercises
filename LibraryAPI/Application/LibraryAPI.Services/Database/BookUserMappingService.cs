namespace LibraryAPI.Services.Database
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.Common.Constants;
	using LibraryAPI.Common.Exceptions;
	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.EntityFrameworkCore;

	public class BookUserMappingService : BaseService<BookUserMapping>, IBookUserMappingService
	{
		public BookUserMappingService(LibraryAPIDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper)
		{
		}

		public async Task<T> GetModelByBookIdAndUserIdAsync<T>(Guid bookId, Guid userId)
		{
			var model = await this.DbSet
							.Where(bum => bum.BookId == bookId && bum.UserId == userId)
							.OrderByDescending(bum => bum.CreatedOn)
							.FirstOrDefaultAsync();

			if (model == null)
			{
				throw new EntityDoesNotExistException(ExceptionMessages.BOOK_USER_MAPPING_DOES_NOT_EXIST_MESSAGE);
			}

			T modelToReturn = this.Mapper.Map<T>(model);
			return modelToReturn;
		}

		public async Task<T> CreateRelationAsync<T>(Guid bookId, Guid userId, DateTime deadLine)
		{
			this.ValidateDateTime(deadLine);

			BookUserMapping model = new BookUserMapping()
			{
				BookId = bookId,
				UserId = userId,
				DeadLine = deadLine,
			};

			await this.DbSet.AddAsync(model);
			await this.DbContext.SaveChangesAsync();

			T modelToReturn = this.Mapper.Map<T>(model);
			return modelToReturn;
		}

		public async Task<T> UpdateRelationAsync<T>(Guid bookId, Guid userId, DateTime deadLine)
		{
			this.ValidateDateTime(deadLine);

			BookUserMapping model = await this.GetModelByBookIdAndUserIdAsync<BookUserMapping>(bookId, userId);

			model.DeadLine = deadLine;
			model.UpdatedOn = DateTime.UtcNow;

			this.DbSet.Update(model);
			await this.DbContext.SaveChangesAsync();

			T modelToReturn = this.Mapper.Map<T>(model);
			return modelToReturn;
		}

		public async Task<T> SetReturnDateAsync<T>(Guid bookId, Guid userId, DateTime returnDate)
		{
			BookUserMapping model = await this.GetModelByBookIdAndUserIdAsync<BookUserMapping>(bookId, userId);

			if (model.IsReturned == true)
			{
				throw new ArgumentException(ExceptionMessages.BOOK_ALREADY_RETURNED_MESSAGE);
			}

			if (DateTime.Compare(model.CreatedOn, returnDate) > 0)
			{
				throw new ArgumentException(ExceptionMessages.BOOK_INVALID_RETURN_DATE_MESSAGE);
			}

			model.IsReturned = true;
			model.ReturnDate = returnDate;
			model.UpdatedOn = DateTime.UtcNow;

			this.DbSet.Update(model);
			await this.DbContext.SaveChangesAsync();

			T modelToReturn = this.Mapper.Map<T>(model);
			return modelToReturn;
		}

		public async Task<T> DeleteRelationAsync<T>(Guid bookId, Guid userId)
		{
			BookUserMapping model = await this.GetModelByBookIdAndUserIdAsync<BookUserMapping>(bookId, userId);

			this.DbSet.Remove(model);
			await this.DbContext.SaveChangesAsync();

			T modelToReturn = this.Mapper.Map<T>(model);
			return modelToReturn;
		}

		private void ValidateDateTime(DateTime deadLine)
		{
			if (DateTime.Compare(DateTime.UtcNow, deadLine) > 0)
			{
				throw new ArgumentException(ExceptionMessages.BOOK_INVALID_DEADLINE_MESSAGE);
			}
		}
	}
}
