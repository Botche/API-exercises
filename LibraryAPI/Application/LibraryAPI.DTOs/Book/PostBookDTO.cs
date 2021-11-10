namespace LibraryAPI.DTOs.Book
{
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.DTOs.Constants;

	public class PostBookDTO
	{
		[Required]
		[StringLength(BookConstants.NAME_MAX_LENGTH)]
		public string Name { get; set; }

		[Required]
		[StringLength(BookConstants.AUTHOR_MAX_LENGTH)]
		public string Author { get; set; }
	}
}
