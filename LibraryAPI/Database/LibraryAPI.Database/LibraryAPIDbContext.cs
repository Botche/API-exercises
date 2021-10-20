﻿namespace LibraryAPI.Database
{
	using System.Reflection;

	using LibraryAPI.Database.Models.Books;

	using Microsoft.EntityFrameworkCore;

	public class LibraryAPIDbContext : DbContext
	{
		public DbSet<Book> Books { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<BookGenreMapping> BooksGenresMapping { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer("Server=.;Database=LibraryAPI;Integrated Security = true;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}