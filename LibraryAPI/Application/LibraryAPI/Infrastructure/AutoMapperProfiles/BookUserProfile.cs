namespace LibraryAPI.Infrastructure.AutoMapperProfiles
{
	using AutoMapper;

	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.DTOs.BookUserMapping;

	public class BookUserProfile : Profile
	{
		public BookUserProfile()
		{
			this.CreateMap<BookUserMapping, GetBookUserDTO>();
		}
	}
}
