namespace LibraryAPI.Infrastructure.Filters
{
	using System;
	using System.Linq;

	using LibraryAPI.Common.Constants;
	using LibraryAPI.DTOs.User;

	using Microsoft.AspNetCore.Mvc.Filters;

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
	{
		public string[] Roles { get; set; }

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var user = (GetUserForSessionDTO)context.HttpContext.Items["User"];

			if (user == null)
			{
				throw new ArgumentException();
			}

			bool isInRole = true;
			for (int index = 0; index < Roles.Length; index++)
			{
				string role = Roles[index];

				isInRole = user.Roles.Any(x => x.RoleName == role);

				if (isInRole)
				{
					break;
				}
			}

			if (isInRole == false)
			{
				throw new ArgumentException(ExceptionMessages.USER_UNAUTHORIZED_MESSAGE);
			}
		}
	}
}
