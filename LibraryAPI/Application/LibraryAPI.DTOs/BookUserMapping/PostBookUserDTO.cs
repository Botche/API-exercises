namespace LibraryAPI.DTOs.BookUserMapping
{
	using System;

	public class PostBookUserDTO
	{
		public string UserEmail { get; set; }

		public Guid BookId { get; set; }

		public DateTime DeadLine { get; set; }
	}
}
