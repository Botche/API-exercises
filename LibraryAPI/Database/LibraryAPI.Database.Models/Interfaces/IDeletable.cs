namespace LibraryAPI.Database.Models.Interfaces
{
	using System;

	public interface IDeletable
	{
		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }
	}
}
