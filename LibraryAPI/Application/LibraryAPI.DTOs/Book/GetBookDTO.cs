namespace LibraryAPI.DTOs.Book
{
	using System;

	using LibraryAPI.DTOs.Genre;

	public class GetBookDTO
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string AuthorName { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public DateTime? DeletedOn { get; set; }

		public GetAllGenreDTO Genres { get; set; }
	}
}
