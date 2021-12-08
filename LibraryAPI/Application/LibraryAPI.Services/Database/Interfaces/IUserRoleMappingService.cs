namespace LibraryAPI.Services.Database.Interfaces
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.Database.Models.Users;

	public interface IUserRoleMappingService
	{
		Task<UserRoleMapping> AddRoleToUserAsync(Guid roleId, Guid userId);

		Task<bool> IsThereAnyDataInTableAsync();
	}
}
