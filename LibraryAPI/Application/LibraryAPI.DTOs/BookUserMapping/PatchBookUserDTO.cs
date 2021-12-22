namespace LibraryAPI.DTOs.BookUserMapping
{
	using System;

	public class PatchBookUserDTO
	{
		public string UserEmail { get; set; }

		public Guid BookId { get; set; }

		public DateTime ReturnDate { get; set; }
	}
}
