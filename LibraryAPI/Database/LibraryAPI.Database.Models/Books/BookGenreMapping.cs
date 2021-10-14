namespace LibraryAPI.Database.Models.Books
{
	using System;

	public class BookGenreMapping : BaseModel
	{
		public BookGenreMapping()
			: base()
		{

		}

		public Guid BookId { get; set; }
		public virtual Book Book { get; set; }

		public Guid GenreId { get; set; }
		public virtual Genre Genre { get; set; }
	}
}
