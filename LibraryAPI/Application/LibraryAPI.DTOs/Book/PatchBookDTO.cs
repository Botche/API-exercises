namespace LibraryAPI.DTOs.Book
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.DTOs.Constants;

	public class PatchBookDTO
	{
		[StringLength(BookConstants.NAME_MAX_LENGTH)]
		public string Name { get; set; }

		[StringLength(BookConstants.AUTHOR_MAX_LENGTH)]
		public string Author { get; set; }

		public IEnumerable<Guid> GenresId { get; set; }
	}
}
