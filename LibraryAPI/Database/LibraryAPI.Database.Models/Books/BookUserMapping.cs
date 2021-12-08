namespace LibraryAPI.Database.Models.Books
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.Database.Models.Users;

	public class BookUserMapping : BaseModel
	{
		public BookUserMapping()
			: base()
		{
			this.IsReturned = false;
		}

		public Guid BookId { get; set; }
		public virtual Book Book { get; set; }

		public Guid UserId { get; set; }
		public virtual User User { get; set; }

		public DateTime? ReturnDate { get; set; }

		public bool IsReturned { get; set; }

		[Required]
		public DateTime DeadLine { get; set; }
	}
}
