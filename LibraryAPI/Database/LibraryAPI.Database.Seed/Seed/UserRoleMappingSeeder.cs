namespace LibraryAPI.Database.Seed.Seed
{
	using System.Threading.Tasks;

	using LibraryAPI.Common.Constants;
	using LibraryAPI.DTOs.Role;
	using LibraryAPI.DTOs.User;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.Extensions.DependencyInjection;

	public class UserRoleMappingSeeder : BaseSeeder
	{
		public override async Task SeedAsync(IServiceScope serviceProvider)
		{
			var userRoleMappingService = serviceProvider.ServiceProvider.GetRequiredService(typeof(IUserRoleMappingService)) as IUserRoleMappingService;
			var userService = serviceProvider.ServiceProvider.GetRequiredService(typeof(IUserService)) as IUserService;
			var roleService = serviceProvider.ServiceProvider.GetRequiredService(typeof(IRoleService)) as IRoleService;

			if (await userRoleMappingService.IsThereAnyDataInTableAsync() == false)
			{
				var user = await userService.GetUserByEmailAsync<GetUserIdDTO>(GlobalConstants.USER_EMAIL);
				var admin = await userService.GetUserByEmailAsync<GetUserIdDTO>(GlobalConstants.ADMIN_EMAIL);

				var userRole = await roleService.GetRoleByNameAsync<GetRoleIdDTO>(GlobalConstants.USER_ROLE_NAME);
				var adminRole = await roleService.GetRoleByNameAsync<GetRoleIdDTO>(GlobalConstants.ADMIN_ROLE_NAME);

				await userRoleMappingService.AddRoleToUserAsync(userRole.Id, user.Id);
				await userRoleMappingService.AddRoleToUserAsync(adminRole.Id, admin.Id);
			}
		}
	}
}
