namespace LibraryAPI.Services.Database
{
	using AutoMapper;

	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Services.Database.Interfaces;

	public class BookUserMappingService : BaseService<BookUserMapping>, IBookUserMappingService
	{
		public BookUserMappingService(LibraryAPIDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper)
		{
		}
	}
}
