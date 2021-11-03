namespace LibraryAPI.BindingModels.Genre
{
	using System.Collections.Generic;

	public class GetAllGenreBindingModel
	{
		public IEnumerable<GetGenreBindingModel> Genres { get; set; }

		public int GenresCount { get; set; }
	}
}
