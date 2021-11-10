namespace LibraryAPI.Infrastructure.AutoMapperProfiles
{
	using System.Collections.Generic;
	using System.Linq;

	using AutoMapper;

	using LibraryAPI.DTOs.Book;
	using LibraryAPI.Database.Models.Books;

	public class BookProfile : Profile
	{
		public BookProfile()
		{
			this.CreateMap<Book, GetBookDTO>()
				.ForMember(gbbm => gbbm.AuthorName, b => b.MapFrom(book => book.Author));
			this.CreateMap<IEnumerable<Book>, GetAllBooksDTO>()
				.ForMember(gabbm => gabbm.Books, b => b.MapFrom(books => books))
				.ForMember(gabbm => gabbm.BooksCount, b => b.MapFrom(books => books.Count()));

			this.CreateMap<PostBookDTO, Book>();
			this.CreateMap<PutBookDTO, Book>();
		}
	}
}
