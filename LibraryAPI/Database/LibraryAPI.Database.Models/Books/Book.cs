namespace LibraryAPI.Database.Models.Books
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.Common.Constants.ModelConstants;
	using LibraryAPI.Database.Models.Interfaces;

	public class Book : BaseModel, IDeletable
	{
		public Book()
			: base()
		{
			this.IsDeleted = false;
			this.DeletedOn = null;

			this.Genres = new HashSet<BookGenreMapping>();
		}

		[Required]
		[StringLength(BookConstants.NAME_MAX_LENGTH)]
		public string Name { get; set; }

		[Required]
		[StringLength(BookConstants.AUTHOR_MAX_LENGTH)]
		public string Author { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

		public virtual ICollection<BookGenreMapping> Genres { get; set; }
	}
}