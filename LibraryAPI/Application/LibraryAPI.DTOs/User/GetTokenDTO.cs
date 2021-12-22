namespace LibraryAPI.DTOs.User
{
	public class GetTokenDTO
	{
		public GetTokenDTO(string token)
		{
			this.Token = token;
		}

		public string Token { get; set; }
	}
}
