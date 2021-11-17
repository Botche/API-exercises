﻿namespace LibraryAPI.Services.Database
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.Common.Constants;
	using LibraryAPI.Common.Exceptions;
	using LibraryAPI.DTOs.Genre;
	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.EntityFrameworkCore;

	public class GenreService : BaseService<Genre>, IGenreService
	{
		public GenreService(LibraryAPIDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper)
		{

		}

		public async Task<T> GetAllAsync<T>()
		{
			IEnumerable<Genre> genres = await this.DbSet
				.OrderBy(g => g.Name)
				.ToListAsync();

			T result = this.Mapper.Map<T>(genres);
			return result;
		}

		public async Task<T> GetByIdAsync<T>(Guid id)
		{
			Genre genre = await this.DbSet
				.SingleOrDefaultAsync(g => g.Id == id);

			if (genre == null)
			{
				throw new GenreDoesNotExist(string.Format(ExceptionMessages.GENRE_DOES_NOT_EXIST_MESSAGE, id));
			}

			T result = this.Mapper.Map<T>(genre);
			return result;
		}

		public async Task<T> AddAsync<T>(PostGenreDTO model)
		{
			Genre genreToAdd = this.Mapper.Map<Genre>(model);

			await this.DbSet.AddAsync(genreToAdd);
			await this.DbContext.SaveChangesAsync();

			T result = this.Mapper.Map<T>(genreToAdd);
			return result;
		}

		public async Task<bool> UpdateAsync(Guid id, PutGenreDTO model)
		{
			Genre genreToUpdate = await this.DbSet
				.FindAsync(id);

			if (genreToUpdate == null)
			{
				throw new GenreDoesNotExist(string.Format(ExceptionMessages.GENRE_DOES_NOT_EXIST_MESSAGE, id));
			}

			Genre updatedGenre = this.Mapper.Map(model, genreToUpdate);
			updatedGenre.UpdatedOn = DateTime.UtcNow;

			this.DbSet.Update(updatedGenre);
			await this.DbContext.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			Genre genreToDelete = await this.DbSet
				.FindAsync(id);

			if (genreToDelete == null)
			{
				throw new GenreDoesNotExist(string.Format(ExceptionMessages.GENRE_DOES_NOT_EXIST_MESSAGE, id));
			}

			this.DbContext.Remove(genreToDelete);
			await this.DbContext.SaveChangesAsync();

			return true;
		}
	}
}
