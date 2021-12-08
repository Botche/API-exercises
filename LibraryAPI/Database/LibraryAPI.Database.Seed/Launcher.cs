namespace LibraryAPI.Database.Seed
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using LibraryAPI.Database.Seed.Seed;
	using LibraryAPI.Database.Seed.Seed.Interfaces;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.Extensions.DependencyInjection;

	public static class Launcher
	{
		public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
		{
			IEnumerable<IBaseSeeder> seeders = new List<IBaseSeeder>()
			{
				// new SeederClass(),
				new RoleSeeder(),
				new UserSeeder(),
				new UserRoleMappingSeeder(),
			};

			using (var serviceProvider = app.ApplicationServices.CreateScope())
			{
				foreach (var seeder in seeders)
				{
					await seeder.SeedAsync(serviceProvider);
				}
			}
		}
	}
}
