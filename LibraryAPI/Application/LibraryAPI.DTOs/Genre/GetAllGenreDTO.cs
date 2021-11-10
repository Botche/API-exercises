namespace LibraryAPI.DTOs.Genre
{
	using System.Collections.Generic;

	public class GetAllGenreDTO
	{
		public IEnumerable<GetGenreDTO> Genres { get; set; }

		public int GenresCount { get; set; }
	}
}
