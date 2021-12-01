namespace LibraryAPI.Services.Database
{
	using System;
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Users;
	using LibraryAPI.Services.Database.Interfaces;

	public class UserRoleMappingService : BaseService<UserRoleMapping>, IUserRoleMappingService
	{
		public UserRoleMappingService(LibraryAPIDbContext dbContext, IMapper mapper) 
			: base(dbContext, mapper)
		{
		}

		public async Task<UserRoleMapping> AddRoleToUserAsync(Guid roleId, Guid userId)
		{
			UserRoleMapping userRoleMapping = new UserRoleMapping()
			{
				RoleId = roleId,
				UserId = userId,
			};

			await this.DbSet.AddAsync(userRoleMapping);
			await this.DbContext.SaveChangesAsync();

			return userRoleMapping;
		}
	}
}
