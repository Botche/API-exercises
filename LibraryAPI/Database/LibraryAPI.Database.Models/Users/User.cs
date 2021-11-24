namespace LibraryAPI.Database.Models.Users
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.Common.Constants.ModelConstants;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.Database.Models.Interfaces;

	public class User : BaseModel, IDeletable
	{
		public User()
			: base()
		{
			this.IsDeleted = false;
			this.DeletedOn = null;

			this.Roles = new HashSet<UserRoleMapping>();
			this.Books = new HashSet<BookUserMapping>();
		}

		[Required]
		[StringLength(UserConstants.FIRST_NAME_MAX_LENGTH)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(UserConstants.LAST_NAME_MAX_LENGTH)]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string PasswordHash { get; set; }

		[Required]
		public string Salt { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

		public virtual ICollection<UserRoleMapping> Roles { get; set; }

		public virtual ICollection<BookUserMapping> Books { get; set; }
	}
}
