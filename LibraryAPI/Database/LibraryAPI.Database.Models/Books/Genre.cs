namespace LibraryAPI.Database.Models.Books
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.Common.Constants.ModelConstants;
	using LibraryAPI.Database.Models.Interfaces;

	public class Genre : BaseModel, IDeletable
	{
		public Genre()
			: base()
		{
			this.IsDeleted = false;
			this.DeletedOn = null;

			this.Books = new HashSet<BookGenreMapping>();
		}

		[Required]
		[StringLength(GenreConstants.NAME_MAX_LENGTH)]
		public string Name { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

		public virtual ICollection<BookGenreMapping> Books { get; set; }
	}
}
