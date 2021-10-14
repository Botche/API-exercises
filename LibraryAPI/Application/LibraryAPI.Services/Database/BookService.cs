namespace LibraryAPI.Services.Database
{
	using System.Threading.Tasks;

	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;

	public class BookService : BaseService<Book>
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
	}
}
