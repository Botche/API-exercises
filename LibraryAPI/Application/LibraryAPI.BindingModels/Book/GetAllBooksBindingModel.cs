namespace LibraryAPI.BindingModels.Book
{
	using System;

	public class GetAllBooksBindingModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Author { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime? UpdatedOn { get; set; }

		public DateTime? DeletedOn { get; set; }
	}
}
