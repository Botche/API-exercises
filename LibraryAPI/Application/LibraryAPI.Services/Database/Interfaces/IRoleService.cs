namespace LibraryAPI.Services.Database.Interfaces
{
	using System.Threading.Tasks;

	public interface IRoleService
	{
		Task<T> GetRoleByNameAsync<T>(string name);

		Task<T> AddAsync<T>(string roleName);

		Task<bool> IsThereAnyDataInTableAsync();
	}
}
