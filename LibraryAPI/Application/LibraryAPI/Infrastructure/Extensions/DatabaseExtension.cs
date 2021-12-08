namespace LibraryAPI.Infrastructure.Extensions
{
	using System.Threading.Tasks;

	using LibraryAPI.Database;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;

	public static class DatabaseExtension
	{
		public async static Task MigrateDatabase(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetRequiredService<LibraryAPIDbContext>();
				using (dbContext)
				{
					await dbContext.Database.MigrateAsync();
				}
			}
		}
	}
}
