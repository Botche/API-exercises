namespace LibraryAPI.DTOs.User
{
	using System;

	public class GetUserInformationDTO
	{
		public Guid Id { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}
