namespace LibraryAPI.Database.Models.Users
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.Common.Constants.ModelConstants;
	using LibraryAPI.Database.Models.Interfaces;

	public class User : BaseModel, IDeletable
	{
		public User()
			: base()
		{
			this.IsDeleted = false;
			this.DeletedOn = null;

			// TODO: create relation with books (many-to-many)
			this.Roles = new HashSet<UserRoleMapping>();
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
	}
}
