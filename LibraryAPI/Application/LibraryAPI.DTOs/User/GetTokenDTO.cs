namespace LibraryAPI.DTOs.User
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class GetTokenDTO
	{
		public GetTokenDTO(string token)
		{
			this.Token = token;
		}

		public string Token { get; set; }
	}
}
