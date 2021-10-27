namespace LibraryAPI.BindingModels.Book
{
	using System.Collections.Generic;

	public class GetAllBooksBindingModel
	{
		public int BooksCount { get; set; }

		public ICollection<GetBookBindingModel> Books { get; set; }
	}
}
