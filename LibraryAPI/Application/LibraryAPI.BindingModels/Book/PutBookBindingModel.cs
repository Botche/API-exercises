namespace LibraryAPI.BindingModels.Book
{
	using System.ComponentModel.DataAnnotations;

	public class PutBookBindingModel
	{
		[Required]
		[StringLength(128)]
		public string Name { get; set; }

		[Required]
		[StringLength(128)]
		public string Author { get; set; }
	}
}
