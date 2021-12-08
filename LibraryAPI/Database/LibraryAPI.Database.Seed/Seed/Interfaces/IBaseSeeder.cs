namespace LibraryAPI.Database.Seed.Seed.Interfaces
{
	using System.Threading.Tasks;

	using Microsoft.Extensions.DependencyInjection;

	public interface IBaseSeeder
	{
		Task SeedAsync(IServiceScope serviceProvider);
	}
}
