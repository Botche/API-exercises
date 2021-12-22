namespace LibraryAPI.DTOs.BookUserMapping
{
	using System;

	public class GetBookUserDTO
	{
		public Guid Id { get; set; }

		public Guid BookId { get; set; }

		public string BookName { get; set; }

		public Guid UserId { get; set; }

		public string UserEmail { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public DateTime? ReturnDate { get; set; }

		public bool IsReturned { get; set; }

		public DateTime DeadLine { get; set; }

		public double PriceToPayForReturningAfterDeadLine { get; set; }
	}
}
