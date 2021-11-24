namespace LibraryAPI.Database.Models.Books
{
	using System;

	using LibraryAPI.Database.Models.Users;

	public class BookUserMapping : BaseModel
	{
		public BookUserMapping()
			: base()
		{
		}

		public Guid BookId { get; set; }
		public virtual Book Book { get; set; }

		public Guid UserId { get; set; }
		public virtual User User { get; set; }
	}
}
