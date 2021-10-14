namespace LibraryAPI.Database.Models.Books
{
	using System;
	using System.Collections.Generic;

	using LibraryAPI.Database.Models.Interfaces;

	public class Book : BaseModel, IDeletable
	{
		public Book()
			: base()
		{
			this.Genres = new HashSet<BookGenreMapping>();
		}

		public string Name { get; set; }

		public string Author { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

		public virtual ICollection<BookGenreMapping> Genres { get; set; }
	}
}