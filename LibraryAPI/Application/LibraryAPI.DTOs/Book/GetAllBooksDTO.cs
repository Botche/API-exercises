namespace LibraryAPI.DTOs.Book
{
	using System.Collections.Generic;

	public class GetAllBooksDTO
	{
		public int BooksCount { get; set; }

		public ICollection<GetBookDTO> Books { get; set; }
	}
}
