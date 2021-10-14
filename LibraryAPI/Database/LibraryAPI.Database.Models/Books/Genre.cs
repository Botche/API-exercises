namespace LibraryAPI.Database.Models.Books
{
	using System;
	using System.Collections.Generic;

	using LibraryAPI.Database.Models.Interfaces;

	public class Genre : BaseModel, IDeletable
	{
		public Genre()
			: base()
		{
			this.Books = new HashSet<BookGenreMapping>();
		}

		public string Name { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

		public virtual ICollection<BookGenreMapping> Books { get; set; }
	}
}
