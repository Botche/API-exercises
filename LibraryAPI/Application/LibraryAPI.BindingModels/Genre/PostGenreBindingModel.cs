namespace LibraryAPI.BindingModels.Genre
{
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.BindingModels.Constants;

	public class PostGenreBindingModel
	{
		[Required]
		[StringLength(GenreConstants.NAME_MAX_LENGTH)]
		public string Name { get; set; }
	}
}
