namespace LibraryAPI.DTOs.User
{
	using System;
	using System.Collections.Generic;

	public class GetUserForSessionDTO
	{
		public Guid Id { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public IEnumerable<GetRolesForSessionDTO> Roles { get; set; }
	}
}
