namespace LibraryAPI.BindingModels.Book
{
	using System.Collections.Generic;

	public class GetAllBooksBindingModel
	{
		public ICollection<GetBookBindingModel> Books { get; set; }

		public int BooksCount { get; set; }
	}
}
