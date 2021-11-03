namespace LibraryAPI.BindingModels.Book
{
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.BindingModels.Constants;

	public class PutBookBindingModel
	{
		[Required]
		[StringLength(BookConstants.NAME_MAX_LENGTH)]
		public string Name { get; set; }

		[Required]
		[StringLength(BookConstants.AUTHOR_MAX_LENGTH)]
		public string Author { get; set; }
	}
}
