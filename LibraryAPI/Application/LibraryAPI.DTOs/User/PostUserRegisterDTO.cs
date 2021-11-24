namespace LibraryAPI.DTOs.User
{
	public class PostUserRegisterDTO
	{
		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Password { get; set; }

		public string RepeatPassword { get; set; }
	}
}
