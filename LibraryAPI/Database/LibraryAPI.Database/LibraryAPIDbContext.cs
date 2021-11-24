namespace LibraryAPI.Database
{
	using System.Reflection;

	using LibraryAPI.Common;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Database.Models.Users;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Options;

	public class LibraryAPIDbContext : DbContext
	{
		private readonly ApplicationSettings options;

		public LibraryAPIDbContext(IOptions<ApplicationSettings> options)
		{
			this.options = options.Value;
		}

		public DbSet<Book> Books { get; set; }

		public DbSet<Genre> Genres { get; set; }

		public DbSet<BookGenreMapping> BooksGenresMapping { get; set; }

		public DbSet<User> Users { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<UserRoleMapping> UsersRolesMapping { get; set; }

		public DbSet<BookUserMapping> BooksUsersMapping { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer(options.DbConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
