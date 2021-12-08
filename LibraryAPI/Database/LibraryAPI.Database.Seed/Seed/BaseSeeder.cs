namespace LibraryAPI.Database.Seed.Seed
{
	using System.Threading.Tasks;

	using LibraryAPI.Database.Seed.Seed.Interfaces;

	using Microsoft.Extensions.DependencyInjection;

	public abstract class BaseSeeder : IBaseSeeder
	{
		public abstract Task SeedAsync(IServiceScope serviceProvider);
	}
}
