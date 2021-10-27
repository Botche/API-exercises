namespace LibraryAPI.BindingModels.Book
{
	using System.ComponentModel.DataAnnotations;

	public class PatchBookBindingModel
	{
		[StringLength(128)]
		public string Name { get; set; }

		[StringLength(128)]
		public string Author { get; set; }
	}
}
