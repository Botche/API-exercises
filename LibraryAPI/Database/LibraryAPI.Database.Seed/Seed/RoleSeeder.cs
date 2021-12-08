namespace LibraryAPI.Database.Seed.Seed
{
	using System;
	using System.Threading.Tasks;

	using LibraryAPI.Common.Constants;
	using LibraryAPI.Database.Models.Users;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.Extensions.DependencyInjection;

	public class RoleSeeder : BaseSeeder
	{
		public override async Task SeedAsync(IServiceScope serviceProvider)
		{
			IRoleService roleService = serviceProvider.ServiceProvider.GetRequiredService(typeof(IRoleService)) as IRoleService;

			if (await roleService.IsThereAnyDataInTableAsync() == false)
			{
				await roleService.AddAsync<Role>(GlobalConstants.USER_ROLE_NAME);
				await roleService.AddAsync<Role>(GlobalConstants.ADMIN_ROLE_NAME);
			}
		}
	}
}
